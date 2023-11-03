using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private Vector3 spawnLocation;
    public MazeGenerator maze;

    private System.Random rng;

    private NavMeshAgent agent;


    void Start()
    {
        spawnLocation = gameObject.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        rng = new System.Random();
        ResetEnemy();
    }

    void Update()
    {
        if (agent.remainingDistance == 0)
            setRandomDestination();
    }

    private void setRandomDestination() 
    {
        int x = rng.Next(maze.width);
        int z = rng.Next(maze.height);
        Vector3 randomDestination = new Vector3(x, 0, z);
        agent.SetDestination(randomDestination);
    }

    public void ResetEnemy()
    {
        gameObject.transform.position = spawnLocation;
        setRandomDestination();
    } 
}
