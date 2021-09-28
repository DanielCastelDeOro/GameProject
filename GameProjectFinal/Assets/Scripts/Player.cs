using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransfrom = null; // <-- A way to insert fields ONLY into the inspector
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
                                                     // Is not good practice to apply physics to the Update method
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyWasPressed = true;
        }

        horizontalInput = Input.GetAxis("Horizontal"); //neg left pos right to move left and right
    }

    // Fixedupdate runs every physics update (100hrz)
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput, rigidbodyComponent.velocity.y, 0); // The value of the left and right movement 

        if (Physics.OverlapSphere(groundCheckTransfrom.position, 0.1f, playerMask).Length == 0) //checks to see when in the air and colides with itself to prevent double jump
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
