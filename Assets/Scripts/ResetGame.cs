using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGame : MonoBehaviour
{
    [SerializeField] PlayerCamera playerCamera;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] ExitTrigger exitTrigger;
    private EnemyAI enemyAI;
    // Start is called before the first frame update
    void Start()
    {
        enemyAI = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the reset key is pressed on the keyboard (Home key) or gamepad (Fire1 button, e.g., A on Xbox controller)
        if (Input.GetKeyDown(KeyCode.Home) || Input.GetButtonDown("Fire1"))
        {
            playerCamera.ResetCamera();
            playerMovement.ResetPlayerPosition();
            enemyAI.ResetEnemy();
            exitTrigger.ResetExitTrigger();
        }
    }
}
