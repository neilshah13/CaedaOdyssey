using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDeployChallenge : MonoBehaviour
{
    public GameObject MainScript;
    // public int me;
    void Start() 
    {
        print("yikes");
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}

    public void TaskOnClick()
    {
        print("HELLLLLLLO");
        MainScript.GetComponent<ChallengeLevel>().Deploy();
	}
}