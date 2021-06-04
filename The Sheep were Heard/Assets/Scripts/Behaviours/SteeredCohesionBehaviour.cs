using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Steered Cohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    
    Vector3 currentVelocityXYZ;
    public float agentSmoothTime = 0.5f; //0.5 sec

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        
        // if no neigbours, return no adjustment
        if(context.Count == 0) return Vector2.zero;

        // add all points together and average 
        Vector2 cohesionMove = Vector2.zero;

        // add filter if filter == null is false, otherwise just use context as-is
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach(Transform item in filteredContext)
        {
            cohesionMove += new Vector2(item.position.x, item.position.z);
        }
        cohesionMove /= context.Count;

        // create offset from agent position
        cohesionMove -= new Vector2(agent.transform.position.x, agent.transform.position.z);

        Vector2 currentVelocityXZ = new Vector2(currentVelocityXYZ.x, currentVelocityXYZ.z);
        cohesionMove = Vector2.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocityXZ, agentSmoothTime); // ref means that the value will change when in play mode
        return cohesionMove;

    }


}
