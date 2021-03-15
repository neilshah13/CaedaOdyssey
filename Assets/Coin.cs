using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject _celebratoryPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Ball ball = collision.collider.GetComponent<Ball>();

        if (ball != null)
        {
            Instantiate(_celebratoryPrefab, transform.position, Quaternion.identity); //spon where the enemy is
            Destroy(gameObject);

            return;
        }
    }
}
