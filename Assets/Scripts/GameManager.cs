using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillAndRespawnEnemy() {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Destroy(enemy);
        Debug.Log("We've destroyed the enemy");
        Invoke("RespawnEnemy", 5.0f);
    }

    public void RespawnEnemy() {
        EnemyAI enemyAI = Instantiate(enemyPrefab, transform).GetComponent<EnemyAI>();
        Debug.Log("Did we reach this point?");
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>().ResetEnemy();
        // playsfx
        Debug.Log("We've respawned the enemy");
    }
}
