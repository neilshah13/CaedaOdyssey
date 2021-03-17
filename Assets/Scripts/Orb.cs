using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Orb : MonoBehaviour
{
    // references
    public GameObject controller;


    // positionse
    private int xBoard = -1;
    private int yBoard = -1;
    private int health = 3;

    // keep track of object attribute
    public Sprite red_orb, green_orb, blue_orb;
    public Sprite destroyed_orb;
    public bool status; // true if alive, false if dead
    public bool turnComplete;
    public bool isPlayer;

    public void Activate()
    {
        // when the orb is create; activate() is called
        controller = GameObject.FindGameObjectWithTag("GameController");

        // take instantiated lococation and transform it
        SetCoords();
        status = true;
        turnComplete = false;

        switch (this.name)
        {
            case "red": this.GetComponent<SpriteRenderer>().sprite = red_orb; break;
            case "blue": this.GetComponent<SpriteRenderer>().sprite = blue_orb; break;
            case "green": this.GetComponent<SpriteRenderer>().sprite = green_orb; break;
        };
    }

    public void Update()
    {
        
        // check if health drops to 0
        // switch to destroyed orb sprite 
        if (health <= 0) 
        {
            status = false;
            this.GetComponent<SpriteRenderer>().sprite = destroyed_orb;
        }
    }

    public void Attack(Orb obj)
    {

        // if effective matchup against enemy: deal 2 damage
        // if not-effective matchup against enemy: deal 0 damage
        // if same matchup against enemy: deal 1 damage
        if (!status || obj == null) {
            return; // can't do any damage
        }
        float currY = this.transform.position.y;
        float enemyY = obj.transform.position.y;

        Orb enemy = obj.GetComponent<Orb>();
        int damage = 0;


        // calculate damage
        switch (this.name)
        {
            case "red":
                switch (enemy.name) {
                    case "green": damage = 3; break;
                    case "blue": damage = 1;  break;
                    case "red": damage = 2;  break;
                }; break;
            case "blue":
                switch (enemy.name) {
                    case "green": damage = 1;  break;
                    case "blue": damage = 2;  break;
                    case "red": damage = 3;  break;
                }; break;
            case "green":
                switch (enemy.name) {
                    case "green": damage = 2;  break;
                    case "blue": damage = 3;  break;
                    case "red": damage = 1;  break;
                }; break;
        }
        enemy.DealDamage(damage); // deduct damage from health
        if (enemy.GetHealth() <= 0)
        {
            enemy.GetComponent<SpriteRenderer>().sprite = destroyed_orb;
            enemy.status = false;
        }
        turnComplete = true;  
    }

    void OnMouseDown()
    {
        if (isPlayer)
        {
            // if red -> green
            // if green -> blue
            // if blue -> red

            switch (this.name) 
            {
                case "red": 
                    this.GetComponent<SpriteRenderer>().sprite = green_orb;
                    name = "green";
                break;
                case "green": 
                    this.GetComponent<SpriteRenderer>().sprite = blue_orb;
                    name = "blue";
                break;
                case "blue": 
                    this.GetComponent<SpriteRenderer>().sprite = red_orb;
                    name = "red";
                break;
			}
		}
	}

    public int GetHealth()
    {
        return health;
    }

    public void DealDamage(int dmg)
    {
        if (health > 0) {
            health -= dmg;
        }
    }

    public void setPlayerOrbs(bool flag)
    {
        isPlayer = flag;
	}

    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;

        x *= 1.33f;
        y *= 1.33f;

        x+=-1.5f;
        y+=-3.8f;
        this.transform.position = new Vector3(x,y,-1.0f);
    }

    
    public int GetXBoard()
    {
        return xBoard;
    }

    public int GetYBoard()
    {
        return yBoard;
    }

    public void SetXBoard(int x)
    {
        xBoard = x;
    }

    public void SetYBoard(int y)
    {
        yBoard = y;
    }


}
