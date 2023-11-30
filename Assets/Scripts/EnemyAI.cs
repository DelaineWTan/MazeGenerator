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
        agent = GetComponent<NavMeshAgent>();
        maze = transform.parent.gameObject.GetComponent<MazeGenerator>();
        navMeshSurface = maze.surface;
        rng = new System.Random();
    }

    void Update()
    {
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);

        if (agent.velocity == Vector3.zero)
            animator.Play(IdleAnimationName);
        else
            animator.Play(RunAnimationName);
    }

    public void ResetEnemyPosition()
    {
        int skipWidthStart = (maze.width * 2) / 5;
        int skipWidthEnd = (maze.width * 3) / 5;

        int skipHeightStart = (maze.height * 2) / 5;
        int skipHeightEnd = (maze.height * 3) / 5;

        int x;
        int z;

        // Keep generating new positions until it's not in the middle
        do
        {
            x = rng.Next(0, maze.width);
            z = rng.Next(0, maze.height);
        } while (x > skipWidthStart && x < skipWidthEnd && skipHeightStart < z && z < skipHeightEnd);

        transform.position = new Vector3(x, 0, z);
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
