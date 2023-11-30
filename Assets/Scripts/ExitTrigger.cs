using UnityEngine;
using TMPro;

public class ExitTrigger : MonoBehaviour
{
    public TMP_Text systemText;
    public bool hasPlayerExited = false;
    public Transform playerTransform;

    private void Start()
    {
        // Find the "SystemText" TMP Text object in the hierarchy
        Transform textTransform = GameObject.Find("SystemText").transform;
        if (textTransform != null)
            systemText = textTransform.GetComponent<TextMeshProUGUI>();

        if (systemText != null)
            systemText.text = "";
        // else
        // // Find the Player object in the hierarchy
        // playerTransform = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (!hasPlayerExited)
        {
            // Define a trigger area box that's wider than the trigger object
            Vector3 triggerScale = transform.localScale;
            Vector3 triggerSize = new Vector3(triggerScale.x, 1.0f, triggerScale.z);

            // Calculate the distance between the player and the trigger area center
            Vector3 playerPosition = playerTransform.position; // Assuming playerTransform is a reference to the player's Transform
            Vector3 triggerCenter = transform.position;

            float distance = Vector3.Distance(playerPosition, triggerCenter);
            // Check if the player is within the trigger area
            float triggerDistance = triggerSize.magnitude / 2.0f;

            if (distance <= triggerDistance)
            {
                // The player has exited the maze.

                // Display the "You Win" message
                if (systemText != null)
                    systemText.text = "You exited the maze, you win!";

                hasPlayerExited = true;
            }
        }
    }
    public void ResetExitTrigger()
    {
        hasPlayerExited = false;
        if (systemText != null)
            systemText.text = "";
    }
}
