// Tutorial: https://www.youtube.com/watch?v=i_XinoVBqt8&ab_channel=BoardToBitsGames
//===========================================================
// 
//  Purpose: 1) Populating the flock. 2) Handle the iteration and executing the behaviour
//
//===========================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    #region Variables
    #region Agent
    // THE AGENT
    public FlockAgent agentPrefab; // the prefab for each agent
    private List<FlockAgent> agents = new List<FlockAgent>(); // list with all the spawned agents
    

    // AGENT BEHAVIOUR
    [SerializeField]
    [Tooltip("How much space should be in-between the agents?")]
    private float AgentDensity = 2f; // The density of the herd
    public FlockBehaviour behaviour; //

    
    // Drivefactor: making it go slower/faster
    [Range(1f,100f)]
    public float driveFactor = 10f;
    [Range(1f,100f)]
    public float maxSpeed = 5f; // maximum speed for the agent
    [Range(1f,10f)]
    public float neighbourRadius = 1.5f; // radius for our neighbours (or obstacles)
    [Range(0f,1f)]
    public float avoidanceRadiusMultiplier = 0.5f; // 

    // Linn note: I set the next three variables to private, for the sake of practicing. Were just floats before. 
    // These are used to easilier handle and compare the magnitude of the vectors (math in Unity...)
    [HideInInspector]
    public float squareMaxspeed; 
    [HideInInspector]
    public float squareNeighbourRadius;
    [HideInInspector]
    public float squareAvoidanceRadius;
    // Getter method for squareAvoidanceRadius
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
        
    // [SerializeField]
    // [Tooltip("How many flocks should be spawned?")]
    // private int numberOfFlocks; // How many flocks
    #endregion
    #region Herd
    // The Grass field
    private static GameObject field;
    private float grassFieldwidth, grassFieldDepth;
    [SerializeField] 
    [Tooltip("How much space should the flock take up on the grass field?")]
    [Range(0,100)]
    private float flockFill = 20;
    private Vector2 flockSize;
    
    // Poisson Disc Sampling
    private PoissonDiscSpawner2D poissonDiscSampling; // function to calculate spawning points
    private List<Vector2> points; // Spawning points will be saved in a list
    #endregion
    #endregion

    #region Start functions
    public void ResetSettings()
    {
        driveFactor = 10f;
        maxSpeed = 5f;    
        neighbourRadius = 1.5f; 
        avoidanceRadiusMultiplier = 0.5f;
        flockFill = 20;
    }

    #region Awake
    private void Awake() 
    {
        
        // Set the grass field
        field = GameObject.FindGameObjectWithTag("PlayArea"); 
        
        if (field == null) {
            Debug.Log("No game object called 'Grass' found");
        }

        // Set the width and depth of the grass field area. Multiplication for setting a border
        grassFieldwidth = field.transform.localScale.x*0.95f;
        grassFieldDepth = field.transform.localScale.z*0.95f;

        // Set percentage of how much the field should be filled with one flock
        flockFill = flockFill/100;
        flockSize = new Vector2(grassFieldwidth*flockFill, grassFieldDepth*flockFill);
        // flockFill = 1;
        
    }
    #endregion

    #region Start
    void Start()
    {

        // Set parameters for the PoissonDisc. Multiplication by flockFill, to not(?) fill up whole grass field
        poissonDiscSampling = new PoissonDiscSpawner2D(flockSize.x, flockSize.y, AgentDensity);

        int agentNumber = 0;
        foreach (Vector2 sample in poissonDiscSampling.Samples()) {

            // GameObject newAgent = Instantiate(agentPrefab, 
            FlockAgent newAgent = Instantiate(agentPrefab, 
                new Vector3(sample.x, 0f, sample.y), 
                Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                transform // setting the parent (the flock's transform)
            );

            newAgent.name = "Agent " + agentNumber++;
            agents.Add(newAgent);

        };
    
        // Set the flock a random location
        int xPosition = Random.Range((int)(-(grassFieldwidth/2)+flockSize.x),(int)((grassFieldwidth/2)-flockSize.x));
        int zPosition = Random.Range((int)(-(grassFieldDepth/2)+flockSize.y),(int)((grassFieldDepth/2)-flockSize.y));
        transform.position = new Vector3(xPosition, transform.position.y+1, zPosition);
        
        // Calculate the different math-stuff for behaviour
        squareMaxspeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        
    }
    #endregion
    #endregion
    
    private void Update() {
        
        foreach(FlockAgent agent in agents)
        {

            List<Transform> context = GetNearbyObjects(agent);

            // Calculate movement based on neighbours
            Vector2 move = behaviour.CalculateMove(agent, context, this); //this = this Flock

            move *= driveFactor;    

            // If exceeding maxspeed, then keep it at maxSpeed
            if(move.sqrMagnitude > squareMaxspeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);

        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        
        List<Transform> context = new List<Transform>();

        // Getting the colliders of the agents
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);

        foreach(Collider c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;

    }
        


}
