using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ExitTrigger exitTrigger;
    private EnemyAI enemyAI;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the reset key is pressed on the keyboard (Home key) or gamepad "A" button
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Reset();
        }
    }

    public void Reset()
    {
        playerCamera.ResetCamera();
        playerMovement.ResetPlayerPosition();
        enemyAI.ResetEnemy();
        exitTrigger.ResetExitTrigger();
    }

    public void SetNewEnemy(EnemyAI newEnemyAI) {
        enemyAI = newEnemyAI;
    }
}
