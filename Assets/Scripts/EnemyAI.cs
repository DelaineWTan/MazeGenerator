using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private readonly int IdleAnimationName = Animator.StringToHash("HumanoidIdle");
    private readonly int RunAnimationName = Animator.StringToHash("HumanoidRun");
    private Vector3 spawnLocation;
    public MazeGenerator maze;

    private System.Random rng;

    [SerializeField] NavMeshSurface navMeshSurface;
    private NavMeshAgent agent;

    private Animator animator;

    void Start()
    {
        maze = GameObject.FindWithTag("Maze").GetComponent<MazeGenerator>();
        navMeshSurface = GameObject.FindWithTag("NavMeshSurface").GetComponent<NavMeshSurface>();
        spawnLocation = gameObject.transform.position;
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        rng = new System.Random();
    }

    void Update()
    {
        if (agent.remainingDistance == 0)
            setRandomDestination();

        if (agent.velocity == Vector3.zero)
            animator.Play(IdleAnimationName);
        else
            animator.Play(RunAnimationName);
    }

    private void setRandomDestination() 
    {
        if (navMeshSurface.navMeshData != null)
        {
            int x = rng.Next(maze.width);
            int z = rng.Next(maze.height);
            Vector3 randomDestination = new Vector3(x, 0, z);
            agent.SetDestination(randomDestination);
        }
    }

    public void ResetEnemy()
    {
        gameObject.transform.position = spawnLocation;
        setRandomDestination();
    } 
}
