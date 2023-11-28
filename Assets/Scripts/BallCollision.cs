using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject bounceSFX;
    public GameManager gameManager;

    void Start() {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void OnCollisionEnter(Collision collision)
    {
        PlaySfx.PlayThenDestroy(bounceSFX, transform);
        GameObject hit = collision.gameObject;
        if (hit.tag == "EnemyMesh") {
            int hitCount = hit.transform.parent.GetComponent<EnemyAI>().IncrementHitCount();
            if (hitCount >= 3)
                gameManager.KillAndRespawnEnemy();
            Destroy(gameObject);
        }
    }

}
