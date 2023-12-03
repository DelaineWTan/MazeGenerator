using UnityEngine;
using UnityEngine.SceneManagement;

public class PongDoor : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        GameObject hit = collision.gameObject;
        if (hit.tag == "Player") {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            GameObject.FindFirstObjectByType<GameManager>().SaveData();
            GameObject.Find("Player").gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameObject.FindFirstObjectByType<MazeGenerator>().gameObject.SetActive(false);
            SceneManager.LoadScene("Pong");
        }
    }
}
