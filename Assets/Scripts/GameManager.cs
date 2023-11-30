using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject enemyRespawnSFX;
    public GameObject enemyDeathSFX;

    public GameObject scoreTracker;

    private int score;

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
        ResetGame resetGame = GameObject.FindFirstObjectByType<ResetGame>();
        GameObject BGM = GameObject.FindFirstObjectByType<DayNightToggle>().GetBGMTrack();

        PlaySfx.PlayThenDestroy(enemyRespawnSFX, GameObject.FindGameObjectWithTag("Player").transform);
        EnemyAI enemyAI = Instantiate(enemyPrefab, transform).GetComponent<EnemyAI>();
        resetGame.SetNewEnemy(enemyAI);
        enemyAI.SetEnemyBGM(BGM);
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>().ResetEnemy();
    }

    public void IncrementScore() {
        score++;
        scoreTracker.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }

    public void ResetScore() {
        score = 0;
        scoreTracker.GetComponent<TextMeshProUGUI>().text = "Score: " + 0;
    }


}