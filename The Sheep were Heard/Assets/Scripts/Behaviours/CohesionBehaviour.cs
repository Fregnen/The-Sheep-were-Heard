using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        
        // if no neigbours, return no adjustment
        if(context.Count == 0) return Vector2.zero;

        // add all points together and average 
        Vector2 cohesionMove = Vector2.zero;
        foreach(Transform item in context)
        {
            cohesionMove += new Vector2(item.position.x, item.position.z);
        }
        cohesionMove /= context.Count;

        // create offset from agent position
        cohesionMove -= new Vector2(agent.transform.position.x, agent.transform.position.z);
        return cohesionMove;

    }


}
