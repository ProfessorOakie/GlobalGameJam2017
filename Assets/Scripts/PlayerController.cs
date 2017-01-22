using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController  : MonoBehaviour {

    private Rigidbody rig;

    [SerializeField]
    private float HeartbeatThreshold = 1.0f;
    public float HeartbeatVolume = 1.0f;
    private float HeartbeatVolumeInitial;
    public float HeartbeatVolumeMax = 3.0f;
    [SerializeField]
    private AudioClip[] heartbeatSounds;
    private AudioSource source;

    private void Start()
    {
        rig = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        HeartbeatVolumeInitial = HeartbeatVolume;
        StartCoroutine(Heartbeat());
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

    private void Update()
    {
        if(rig.velocity.magnitude < HeartbeatThreshold)
        {
            IntensifyHeartbeat(Time.deltaTime);
        }
    }

    //Not moving
    private void IntensifyHeartbeat(float timestep)
    {
        HeartbeatVolume += timestep * 0.3f;//how fast volume increases
        if (HeartbeatVolume > HeartbeatVolumeMax)
            HeartbeatVolume = HeartbeatVolumeMax;
    }
    private void ResetHeartbeat()
    {
        HeartbeatVolume = HeartbeatVolumeInitial;
    }

    IEnumerator Heartbeat()
    {
        AudioClip clip = heartbeatSounds[Random.Range(0, heartbeatSounds.Length)];
        source.PlayOneShot(clip, HeartbeatVolume);
        //how long you wait between beats, scaled with volume
        yield return new WaitForSeconds(clip.length + (HeartbeatVolumeMax - HeartbeatVolume)/HeartbeatVolumeMax * 2.0f);
        StartCoroutine(Heartbeat());
    }
    

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

}
