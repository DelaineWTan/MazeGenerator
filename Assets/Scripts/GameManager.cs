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

    // PlayerPrefs keys
    private string playerPositionKey = "PlayerPosition";
    private string enemyPositionKey = "EnemyPosition";
    private string scoreKey = "Score";

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Save data when the "S" key is pressed
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveData();
        }

        // Save data when the "S" key is pressed
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadSavedData();
        }
    }

    public void KillAndRespawnEnemy() {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");

        PlaySfx.PlayThenDestroy(enemyDeathSFX, enemy.transform);
        Destroy(enemy);
        Invoke("RespawnEnemy", 5.0f);
    }

    public void RespawnEnemy() {
        ResetGame resetGame = FindFirstObjectByType<ResetGame>();
        GameObject BGM = FindFirstObjectByType<ToggleEffects>().GetBGMTrack();

        PlaySfx.PlayThenDestroy(enemyRespawnSFX, GameObject.FindGameObjectWithTag("Player").transform);
        EnemyAI enemyAI = Instantiate(enemyPrefab, GameObject.FindFirstObjectByType<MazeGenerator>().transform).GetComponent<EnemyAI>();
        resetGame.SetNewEnemy(enemyAI);
        enemyAI.SetEnemyBGM(BGM);
        GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>().ResetEnemyPosition();
    }

    public void IncrementScore() {
        score++;
        scoreTracker.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }

    public void ResetScore() {
        score = 0;
        scoreTracker.GetComponent<TextMeshProUGUI>().text = "Score: " + 0;
    }

    public void SaveData()
    {
        // Save player position
        PlayerPrefs.SetFloat(playerPositionKey + "X", Mathf.Round(GameObject.FindGameObjectWithTag("Player").transform.position.x));
        PlayerPrefs.SetFloat(playerPositionKey + "Y", GameObject.FindGameObjectWithTag("Player").transform.position.y);
        PlayerPrefs.SetFloat(playerPositionKey + "Z", Mathf.Round(GameObject.FindGameObjectWithTag("Player").transform.position.z));

        // Save enemy position
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            PlayerPrefs.SetFloat(enemyPositionKey + "X", enemy.transform.position.x);
            PlayerPrefs.SetFloat(enemyPositionKey + "Y", enemy.transform.position.y);
            PlayerPrefs.SetFloat(enemyPositionKey + "Z", enemy.transform.position.z);
        }

        // Save score
        PlayerPrefs.SetInt(scoreKey, score);

        PlayerPrefs.Save();
    }

    public void LoadSavedData()
    {
        // Load player position
        float playerX = PlayerPrefs.GetFloat(playerPositionKey + "X");
        float playerY = PlayerPrefs.GetFloat(playerPositionKey + "Y");
        float playerZ = PlayerPrefs.GetFloat(playerPositionKey + "Z");

        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector3(playerX, playerY, playerZ);

        // Load enemy position
        float enemyX = PlayerPrefs.GetFloat(enemyPositionKey + "X");
        float enemyY = PlayerPrefs.GetFloat(enemyPositionKey + "Y");
        float enemyZ = PlayerPrefs.GetFloat(enemyPositionKey + "Z");

        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            enemy.transform.position = new Vector3(enemyX, enemyY, enemyZ);
        }

        // Load score
        score = PlayerPrefs.GetInt(scoreKey);
        scoreTracker.GetComponent<TextMeshProUGUI>().text = "Score: " + score;
    }
}