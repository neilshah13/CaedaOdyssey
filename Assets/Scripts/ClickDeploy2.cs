using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickDeploy2 : MonoBehaviour
{
    public GameObject MainScript;
    // public int me;
    void Start() 
    {
        Button btn = this.gameObject.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
	}

    public void TaskOnClick()
    {
        MainScript.GetComponent<GameLevel2>().Deploy();
	}
}