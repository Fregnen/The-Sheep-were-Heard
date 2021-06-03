// Tutorial: https://www.youtube.com/watch?v=i_XinoVBqt8&ab_channel=BoardToBitsGames
//===========================================================
// 
//  Purpose: Making the agent move
//
//===========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;

    // A public accessor method for the collider
    public Collider AgentCollider { get { return agentCollider; } }

    // Start is called before the first frame update
    void Start()
    {
        // Find the instance of a collider attatched to this object
        agentCollider = GetComponent<Collider>();
    }

    public void Move(Vector2 velocity){
        
        Vector3 forwardZ = new Vector3(velocity.x, 0f, velocity.y);
        transform.forward = forwardZ;
        transform.position += forwardZ * Time.deltaTime; // because transform is working with struct Vector3
    
    }
    
}

