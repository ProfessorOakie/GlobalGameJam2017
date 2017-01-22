using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController  : MonoBehaviour {

    [SerializeField]
    private float HeartbeatThreshold = 1.0f;
    public float HeartbeatVolume = 1.0f;
    private float HeartbeatVolumeInitial;
    public float HeartbeatVolumeMax = 3.0f;
    [SerializeField]
    private AudioClip[] heartbeatSounds;
    private AudioSource source;

    private Vector3 lastPosition;

    [SerializeField]
    private AudioClip dieSound;
    public GameObject deathCurtain;

    private void Start()
    {
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
        source.PlayOneShot(dieSound);

        deathCurtain.SetActive(true);

        //Destroy(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            Die();
        Debug.Log(Vector3.Distance(transform.position, lastPosition));
        if (Vector3.Distance(transform.position, lastPosition) < HeartbeatThreshold)
        {
            IntensifyHeartbeat(Time.deltaTime);
        }
        else
        {
            ResetHeartbeat();
        }
        lastPosition = transform.position;
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
        yield return new WaitForEndOfFrame();
        AudioClip clip = heartbeatSounds[Random.Range(0, heartbeatSounds.Length)];
        source.PlayOneShot(clip, HeartbeatVolume);
        Monster.instance.SetTarget(transform.position, HeartbeatVolume/HeartbeatVolumeMax);
        SonarParent.instance.StartScan(transform.position, HeartbeatVolume/HeartbeatVolumeMax * 2.0f);
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
