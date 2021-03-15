using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{

    public void goLevel1()
    {
        SceneManager.LoadScene("Level1"); //Scene manage is the order at which scenes go... Can specify name also
    }
}
