using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        
        // if no neigbours, maintain current alignment
        if(context.Count == 0) return agent.transform.forward;

        /// add all points together and average 
        Vector3 alignmentMove = Vector3.zero;
        foreach(Transform item in context)
        {
            alignmentMove += item.transform.transform.forward;
        }
        alignmentMove /= context.Count;

        return new Vector2(alignmentMove.x,alignmentMove.z);

    }
}
