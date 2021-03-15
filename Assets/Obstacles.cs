using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private GameObject _destroyPrefab;
    private bool _canGetHit = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Ball ball = collision.collider.GetComponent<Ball>();

        if (ball != null)
        {
            if (_canGetHit == true)
            {
                Ball.numberOfFailures++;
                _canGetHit = false;
            } 

            Instantiate(_destroyPrefab, transform.position, Quaternion.identity); //spon where the enemy is
            Debug.Log("Obstacle Hit!!!");
            StartCoroutine(Wait(1.0F));

            return;
        }
    }

    IEnumerator Wait(float waitTime)
    {
        //do stuff
        yield return new WaitForSeconds(waitTime);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName); //Scene manage is the order at which scenes go... Can specify name also

    }

}

