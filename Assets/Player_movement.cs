using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_movement : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    public Joystick joystick;
    public Rigidbody2D rb;
    public GameObject interactive_btn;

    public float moveSpeed = 5f;
    Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        //input
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        movement.x = joystick.Horizontal;
        if (movement.x > 0.025) {
        	anim.SetBool("isWalking", true);
        } else {
        	anim.SetBool("isWalking", false);
        }

        if (this.gameObject.GetComponent<Transform>().position.x > 13) {
            interactive_btn.SetActive(true);
        }
        //movement.y = joystick.Vertical;
    }

    private void FixedUpdate()
    {
        // Movement
        rb.MovePosition(rb.position + movement * moveSpeed *  Time.fixedDeltaTime);
    }
}
