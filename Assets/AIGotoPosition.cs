using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGotoPosition : MonoBehaviour
{

    public Transform position;
    public UnityEngine.AI.NavMeshAgent ai;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ai.SetDestination(position.position);
    }
}
