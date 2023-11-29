using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyRespawnSFX;
    public GameObject enemyDeathSFX;

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

        PlaySfx.PlayThenDestroy(enemyDeathSFX, enemy.transform);
        Destroy(enemy);
        Invoke("RespawnEnemy", 5.0f);
    }

    public void RespawnEnemy() {
        PlaySfx.PlayThenDestroy(enemyRespawnSFX, GameObject.FindGameObjectWithTag("Player").transform);
        EnemyAI enemyAI = Instantiate(enemyPrefab, transform).GetComponent<EnemyAI>();
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>().ResetEnemy();
    }


}