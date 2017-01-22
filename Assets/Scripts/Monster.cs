using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//This is a singleton!
public class Monster : MonoBehaviour {

    // the only instance of Monster
    public static Monster instance = null;

    private Vector3 target;
    NavMeshAgent agent;

    private float targetPriority = 0;
    private float maxPriority = 5;

    [Tooltip("How close the monster has to be to have 'reached' it's destination")]
    [SerializeField]
    private float monsterReach = 0.4f;

    [SerializeField]
    private float dashSpeed = 3.0f;
    private float walkSpeed;
    private bool isDashing = false;

	void Start () {

        // Singleton logic
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        agent = GetComponent<NavMeshAgent>();
        walkSpeed = agent.speed;
        //StartCoroutine(NewPath());
	}

    private void Update()
    {
        if (!isDashing)
        {
            targetPriority *= 0.99f;
            Debug.Log("Priority: " + targetPriority);
        }
        else 
        {    //Monster is dashing
            if (agent.remainingDistance < monsterReach)
            {
                StopDash();
            }
        }

        // For testing
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartDash(target);
        }
    }

    //IEnumerator NewPath()
    //{
    //    agent.destination = target;
    //    yield return new WaitForSeconds(0.5f);
    //    StartCoroutine(NewPath());
    //}
    
    private void StartDash(Vector3 position)
    {
        agent.destination = position;
        agent.speed = dashSpeed;
        isDashing = true;
    }
    private void StopDash()
    {
        agent.speed = walkSpeed;
        isDashing = false;
    }


    public void SetTarget(Vector3 position, float priority = -1)
    {
        if(priority > targetPriority || priority == -1)
        {
            target = position;
            agent.destination = position;
            if (priority == -1) priority = maxPriority;
            else if (priority > maxPriority) priority = maxPriority;
            targetPriority = priority;
        }
        else
        {
            targetPriority *= 0.96f;
        }
    }
}
