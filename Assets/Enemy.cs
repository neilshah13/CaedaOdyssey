
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private GameObject _cloudParticlePrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Bird bird = collision.collider.GetComponent<Bird>();

        if (bird != null) {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity); //spon where the enemy is
            Destroy(gameObject);
            return; 
            }

        Enemy enemy = collision.collider.GetComponent<Enemy>(); //if enemy hit into another enemy, just stop
        if (enemy != null)  
        {
            return;
        }

        if (collision.contacts[0].normal.y < -0.5) //first thing which collides with itself hit it from top at certain angle
        {
            Instantiate(_cloudParticlePrefab, transform.position, Quaternion.identity); //spon where the enemy is
            Destroy(gameObject);
        }
    }
}
