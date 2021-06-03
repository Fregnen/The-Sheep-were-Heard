// Tutorial: https://www.youtube.com/watch?v=i_XinoVBqt8&ab_channel=BoardToBitsGames
//===========================================================
// 
//  Purpose: The policy/behaviour of the flock
//
//===========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* It's an abstract class, since it will not be instantiated. 
It is a form of template for the agents. */
/* ScriptableObject: A class you can derive from if you want to 
create objects that don't need to be attached to game objects.*/ 
public abstract class FlockBehaviour : ScriptableObject {
    public abstract Vector2 CalculateMove (FlockAgent agent, List<Transform> context, Flock flock);
    // agent = current, agent that we aree working with, to calculate its move
    // context = what neighbours are around agent
    // flock = we may need some info about the flock itself as well as the agent
    // because it's abstract, I don't need a body for this method 
}
