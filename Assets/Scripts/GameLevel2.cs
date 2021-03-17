using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameLevel2 : MonoBehaviour
{
    public GameObject orb_turret; // declares that a orb_turret exist in the game
    private GameObject[,] positions = new GameObject[3, 8];
    private GameObject[,] enemy_orbs = new GameObject[2, 3];
    private GameObject[,] player_orbs = new GameObject[2, 3];
    string[] all_class = new string[]{
            "red", "green", "blue"
	};
    private int row = 2;
    private int col = 3;

 
    // Start is called before the first frame update1
    void Start()
    {
        enemy_orbs =  new GameObject[row, col];
        for (int r = 0; r < row; r++) 
        {
            for (int c = 0; c < col; c++) 
            {

                string rand_class = all_class[Random.Range(0, col)];
                enemy_orbs[r, c] = Create(rand_class, c, 6-r, false);   
                SetPosition(enemy_orbs[r, c]);
			}
        }

        Select();
        
    }

    public GameObject Create(string name, int xPos, int yPos, bool isPlayer) 
    {
        GameObject obj = Instantiate(orb_turret, new Vector3(0,0,-1), Quaternion.identity);
        Orb single_orb =  obj.GetComponent<Orb>();
        single_orb.name = name;
        
        single_orb.SetXBoard(xPos);
        single_orb.SetYBoard(yPos);
        single_orb.Activate();
        single_orb.setPlayerOrbs(isPlayer);
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Orb set_orb = obj.GetComponent<Orb>();
        positions[set_orb.GetXBoard(), set_orb.GetYBoard()] = obj;
    }


    public int CalculateHealth(GameObject[,] side)
    {
        int result = 0;

        for (int r = 0; r < row; r++) 
        {
            for (int c = 0; c < col; c++) 
            {
                Orb turret = side[r, c].GetComponent<Orb>();
                result += turret.GetHealth();
			}
        }
        return result;
    }
    
    void Select() 
    {
        // player decides oritentation and arrnangment
        player_orbs = new GameObject[row, col];
        for (int r = 0; r < row; r++) 
        {
            for (int c = 0; c < col; c++) 
            {
                string rand_class = all_class[Random.Range(0, col)];
                player_orbs[r, c] = Create(rand_class, c, r, true);
                SetPosition(player_orbs[r, c]);
			}
        }
    }

    Orb findEnemy(Orb attacking_orb, int xPos, int yPos) 
    {
        for (int i = row-1; i > 0; i--) 
        {
            Orb selected_enemy = enemy_orbs[i, yPos].GetComponent<Orb>();
            if (selected_enemy.status) {
                return selected_enemy;     
			}
		}
        return null;
	}

    Orb findPlayer(Orb attacking_orb, int xPos, int yPos)
    {
        for (int i = 0; i < row; i++)
        {
            
            Orb selected_player = player_orbs[i, yPos].GetComponent<Orb>();
            if (selected_player.status) {
                 return selected_player;
			}
		}
        return null;
	}


    public void Deploy()
    {
        // after player has finished choosing their arrangement
        // deploy autoplays the process
        
        // player goes first
        
        for (int r = 0; r < row; r++) 
        {
            for (int c = 0; c < col; c++) 
            {
                
               Orb curr_orb = player_orbs[r, c].GetComponent<Orb>();
               Orb enemy = findEnemy(curr_orb, r, c);
               curr_orb.Attack(enemy);
			}
        }
        
        // let enemy retaliate
        for (int r = 2; r > 0; r--) 
        {
            for (int c = 0; c < col; c++) 
            {
                Orb curr_orb = enemy_orbs[r-1, c].GetComponent<Orb>();
                Orb enemy = findPlayer(curr_orb, r-1, c);
                if (enemy.status) {
                    
                    curr_orb.Attack(enemy);
				}

			}
        }

        
        int player_health = CalculateHealth(player_orbs);
        int enemy_health = CalculateHealth(enemy_orbs);
        if (player_health >= enemy_health)
        {
            // if tie or health greater than enemy
            // yay u win
            GameObject.FindGameObjectWithTag("WinText").GetComponent<Text>().enabled = true;
            StartCoroutine(Wait(5.0f, "ChallengeLevel"));
        } else {
            // nooo u lose, try again?
            GameObject.FindGameObjectWithTag("LoseText").GetComponent<Text>().enabled = true;
            StartCoroutine(Wait(5.0f, "GameLevel2"));
        }

    }
    
    IEnumerator Wait(float waitTime, string nextLevelName)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextLevelName);
	}

    
}
