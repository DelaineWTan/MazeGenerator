using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    private Camera camera;

    [SerializeField]
    private GameObject m_ballPrefab;

    [SerializeField]
    private float m_cdTime = 0.5f;

    [SerializeField]
    private float m_shootForce = 5.0f;

    private float timer = 0f;

    private bool onCd = false;

    private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 centreScreen = camera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, camera.nearClipPlane));
        // transform.position = new Vector3(transform.position.x, centreScreen.y, centreScreen.z);

        if (onCd) {
            timer += Time.deltaTime;
            if (timer >= m_cdTime) {
                onCd = false;
                timer = 0f;
                Destroy(ball);
            }
        }

        if (!onCd && Input.GetKeyDown(KeyCode.Mouse0))
            ShootBall();
    }

    private void ShootBall() {
        //cooldown code below (so you can't machine-gun fire balls)
        ball = Instantiate(m_ballPrefab, transform.position + transform.forward * 0.3f, Quaternion.identity);
        ball.GetComponent<Rigidbody>().AddForce(transform.forward * m_shootForce, ForceMode.Impulse);
        onCd = true;
    }
}
