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
        Invoke("RespawnEnemy", 5.0f);
    }

    public void RespawnEnemy() {
        EnemyAI enemyAI = Instantiate(enemyPrefab, transform).GetComponent<EnemyAI>();
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>().ResetEnemy();
        // playsfx
    }
}
