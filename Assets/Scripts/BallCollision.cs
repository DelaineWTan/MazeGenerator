using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject bounceSFX;

    void OnCollisionEnter(Collision collision)
    {
        PlaySfx.PlayThenDestroy(bounceSFX, transform);
        GameObject hit = collision.gameObject;
        if (hit.tag == "EnemyMesh")
            Debug.Log("Hit the enemy");
    }

}
