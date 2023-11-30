using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private readonly int IdleAnimationName = Animator.StringToHash("HumanoidIdle");
    private readonly int RunAnimationName = Animator.StringToHash("HumanoidRun");
    public MazeGenerator maze;

    // private System.Random rng;

    [SerializeField] NavMeshSurface navMeshSurface;
    [SerializeField] NavMeshAgent agent;

    private int hitCount = 0;
        
    private System.Random rng;

    [SerializeField] Animator animator;


    void Start() {
        maze = FindFirstObjectByType<MazeGenerator>();
        navMeshSurface = maze.surface;
        rng = new System.Random();
    }

    void Update()
    {
        // if (agent.remainingDistance == 0)
            agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
            // setRandomDestination();

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
        int x = rng.Next(0, maze.width);
        int z = rng.Next(0, maze.height);
        transform.position = new Vector3(x, 0, z);
        setRandomDestination();
    }

    public int IncrementHitCount() {
        return ++hitCount;
    }

    public GameObject SetEnemyBGM(GameObject bgm) {
        GameObject playingBGM = PlaySfx.PlayWithLoop(bgm, transform);
        playingBGM.transform.parent = this.transform;
        return playingBGM;
    }

}
