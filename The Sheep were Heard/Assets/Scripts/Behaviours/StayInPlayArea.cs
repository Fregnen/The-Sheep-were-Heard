using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay in Area")]
public class StayInPlayArea : FlockBehaviour
{
    
    public Vector3 centre;
    public float radius = 10f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        
        Vector2 centreOffset = new Vector2(centre.x - agent.transform.position.x, centre.z - agent.transform.position.z);
        float t = centreOffset.magnitude / radius; // some kind of ratio to keep the flock inside

        if(t < 0.9) 
        {
            return Vector2.zero;
        }
        if(t > 10)
        {
            t += t;
            Debug.Log("Well, obviously StayInPlayArea needs an update");
        }
        
        // Debug.Log("t: -- " + t + " and centreOffset: " + centreOffset + " and result: --- " + (centreOffset * t * t));
        return centreOffset * t * t;

    }
}
