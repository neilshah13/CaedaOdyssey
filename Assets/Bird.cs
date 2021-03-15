using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour //This monobehaviour is unity custom script which allows it to be attached to like the scenes and things?? i think
{

    Vector3 _initialPosition;
    private bool _birdWasLaunched = false;
    private float _timeSittingAround;
   

    [SerializeField] private float _launchPower = 500; //We can eactually make this a vector2 form and change the speed on x and y axis separately
    // SerializeField allows us to change this value in te unity gui!

    private void Awake() { //Awake is always called when the application starts up
        _initialPosition = transform.position;  //transform.position is the current position at any point of time
        Debug.Log("Game starts!!");

    }

    private void Update() {

        GetComponent<LineRenderer>().SetPosition(1, _initialPosition); //Setting start of line
        GetComponent<LineRenderer>().SetPosition(0, transform.position); //Setting end of line

        if (transform.position.y > 25 || transform.position.y < -35 || transform.position.x > 35 || transform.position.x < -35 || _timeSittingAround > 2) {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName); //Scene manage is the order at which scenes go... Can specify name also
            // This brings back loadscene to current activescene 
        }
        if (_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1 ) {
            _timeSittingAround += Time.deltaTime; //amount of time since last frame
        }
    }

    private void OnMouseDown() {
        GetComponent<SpriteRenderer>().color = Color.red;
        GetComponent<LineRenderer>().enabled = true;
    }

    private void OnMouseUp() {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 directionToInitialPosition = _initialPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionToInitialPosition * _launchPower); //Setting the force when we release the mouse
        GetComponent<Rigidbody2D>().gravityScale = 1; //only set gravity when we release the mouse
        _birdWasLaunched = true;

        GetComponent<LineRenderer>().enabled = false;

    }

    private void OnMouseDrag() {
        //Note that the mouse position and the bird position are diff. Mouse position follows the aspect ratio
        //bird position (world space position) has 0,0 in the middle, but moust position has 0,0 in the bottom left
        Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Transforms positions from screen space into world space (mouse to world)
        transform.position = new Vector3(newPosition.x, newPosition.y);
    }

}


//Rigidbody to allow objects to fall && box collider to allow it to hit other colliders
