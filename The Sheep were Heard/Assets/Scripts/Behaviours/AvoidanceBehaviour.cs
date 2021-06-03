using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        
        // if no neigbours, return no adjustment
        if(context.Count == 0) return Vector2.zero;

        // add all points together and average 
        Vector2 avoidanceMove = Vector3.zero;
        int nAvoid = 0;

        // Get position
        Vector2 agentPosition = new Vector2(agent.transform.position.x, agent.transform.position.z);

        foreach(Transform item in context)
        {
            Vector2 itemPosition = new Vector2(item.position.x, item.position.z);
            if(Vector2.SqrMagnitude(itemPosition - agentPosition) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += agentPosition - itemPosition;
            }
            
        }
        
        if(nAvoid > 0) avoidanceMove /= nAvoid;

        return avoidanceMove;

    }


}
