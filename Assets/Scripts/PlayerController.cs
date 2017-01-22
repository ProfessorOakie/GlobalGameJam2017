using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController  : MonoBehaviour {

    private void OnCollisionEnter(Collision collision)
    {
        CheckCollidingWithMonster(collision.collider);
    }

    private void OnCollisionStay(Collision collision)
    {
        CheckCollidingWithMonster(collision.collider);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckCollidingWithMonster(other);
    }

    private void OnTriggerStay(Collider other)
    {
        CheckCollidingWithMonster(other);
    }

    void CheckCollidingWithMonster(Collider collider)
    {
        if (collider.gameObject.GetComponent<Monster>())
        {
            // collided with monster
            // TODO: play dying animations
            Die();
        }

    }

    private void Die()
    {
        Debug.Log("Player Has Died");
        Destroy(gameObject);
    }

}
