using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelController : MonoBehaviour
{

    public int _nextLevelIndex;

    private Coin _coin;

    private void OnEnable()
    {
        _coin = FindObjectOfType<Coin>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Ball.numberOfFailures >= 3)
        {
            Debug.Log("3 Times Failed!!!");
            StartCoroutine(Wait1(0.75F));

        }


        if (_coin != null)
        {
            return;
        }

        Debug.Log("You finished this level!");
        //_nextLevelIndex++;
        string nextLevelName = "Level" + _nextLevelIndex;
        Debug.Log(nextLevelName);

        StartCoroutine(Wait(3.0F, nextLevelName));

    }

    IEnumerator Wait(float waitTime, string nextLevelName)
    {
        //do stuff
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextLevelName);

    }

    IEnumerator Wait1(float waitTime)
    {
        //do stuff
        yield return new WaitForSeconds(waitTime);

        Ball.numberOfFailures = 0;
        SceneManager.LoadScene("FailureLevel");

    }


}
