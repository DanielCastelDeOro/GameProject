using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Transform is a way to make sure the lower portion or feet of a player is touching a colider block
    [SerializeField] private Transform groundCheckTransform = null; // <-- A way to insert fields ONLY into the inspector
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyWasPressed;
    private float horizontalInput;
    private Rigidbody rigidbodyComponent;
    private int superJumpsRemaining;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //will check every frame for a space enter
        /*Debug.Log("Space key was pressed down");*/ // <-- is a way to log an error code to see if the code works as needed. Vectory is force up!
        //Works against the rigidbody component to make player move up. Is not good practice to apply physics to the Update method
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal"); //neg left pos right to move left and right.
                                                       //Will feed in 0 all the time if A, D, Left, and right are left alone, once key is pressed values increase. 
    }

    // Fixedupdate runs every physics update (100hrz)
    private void FixedUpdate()
    {
        // The value of the left and right movement. A Vector3 is x,y,z along with a magnitude
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0);

        //checks to see when in the air and colides with itself to prevent double jump
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0) 
        {
            return;
        }
        if (jumpKeyWasPressed)
        {
            float jumpPower = 5;
            if (superJumpsRemaining > 0)
            {
                jumpPower *= 2;
                superJumpsRemaining--;
            }
            rigidbodyComponent.AddForce(Vector3.up * jumpPower, ForceMode.VelocityChange);
            jumpKeyWasPressed = false;
        }

    }
    // Will colide with the coin. Other.gameobject is the coin, withouth other. the player would distroy itself..
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            superJumpsRemaining++;
        }
    }


}
