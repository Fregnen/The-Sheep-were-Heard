//------------------------- Made by Linnea Pedersen -------------------------
// Help from: https://www.youtube.com/watch?v=4HpC--2iowE
// Purpose: control the character with arrowkeys (2D) 
// 
// --------------------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DogBehaviour : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField]
    [Range(10f,50f)]
    [Tooltip("The movement speed of the dog")]
    private float speed = 10f;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private bool fastSpeed = false;
    

    private void Start() {
        //  Get character ctonroller
        controller = gameObject.GetComponent<CharacterController>();
    }


    void Update() {
        
        
        if(Input.GetKeyUp("space"))
        {
            if(!fastSpeed)
            {
                speed = 20f;    
                fastSpeed = true;
            } 
            else if (fastSpeed) 
            {
                speed = 10f;
                fastSpeed = false;
            }
   
        }
        
        // Get the direction based on arrow key input        
        Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical")).normalized;

        
        // If the movement is big enough, move the character
        if(movementVector.magnitude >= 0.1f)
        {
            /* 
                Atan2: angle between (x,0) and vector (x,y). Because z is forward in Unity, x should be first (x/y, instead of y/x)
                Then convert from radians to Euler
            */
            float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg; 
                        
            // Smooth the shift of rotation
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            // Rotation (look to forward)
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            // Translation
            controller.Move(movementVector * speed * Time.deltaTime);


        }
            
    }
 
}
