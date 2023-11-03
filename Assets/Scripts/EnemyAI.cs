using System.Collections;
using System.Collections.Generic;
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
        setRandomDestination();
    }

    void Update()
    {
        if (agent.remainingDistance == 0)
            setRandomDestination();
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetButtonDown("Fire1"))
        {
            Respawn();
            Debug.Log("EnemyAI HOME pressed");
        }
    }

    private void setRandomDestination() {
        int x = rng.Next(maze.width);
        int z = rng.Next(maze.height);
        Vector3 randomDestination = new Vector3(x, 0, z);
        agent.SetDestination(randomDestination);
    }

    public void Respawn() {
        gameObject.transform.position = spawnLocation;
        setRandomDestination();
    }

}
