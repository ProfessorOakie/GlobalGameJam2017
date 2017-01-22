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
    [SerializeField]
    private float briskWalkSpeed = 1.5f;
    [SerializeField]
    private float walkSpeed = 0.5f;
    private bool isDashing = false;

    private int agitationStage = 1;
    private float agitationValue = 0;
    private float flipToStage2 = 10.0f;
    private float flipToStage3 = 15.0f;

    public float testSpeed = -1;

	void Start () {

        // Singleton logic
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        agent = GetComponent<NavMeshAgent>();
        agent.speed = walkSpeed;
        //StartCoroutine(NewPath());
	}

    private void Update()
    {
        if (!isDashing)
        {
            targetPriority *= 0.99f;

            agitationValue -= Time.deltaTime * 0.7f;
            CheckAgitationPhase();
            Debug.Log(agitationValue);
            
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

        if(testSpeed != -1)
            agent.speed = testSpeed;
    }

    //IEnumerator NewPath()
    //{
    //    agent.destination = target;
    //    yield return new WaitForSeconds(0.5f);
    //    StartCoroutine(NewPath());
    //}

    private void CheckAgitationPhase()
    {
        if (agitationValue < 0) agitationValue = 0;
        else if (agitationValue < flipToStage2) agitationStage = 1;
        else if (agitationValue < flipToStage3) agitationStage = 2;
        else agitationStage = 3;
    }


    private void StartBriskWalk(Vector3 position)
    {
        agent.destination = position;
        agent.speed = briskWalkSpeed;
        isDashing = true;
    }
    private void StopBriskWalk()
    {
        agent.speed = walkSpeed;
        isDashing = false;
    }

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
            agitationValue += priority;
            CheckAgitationPhase();
            if (agitationStage == 2) StartBriskWalk(position);
            else if (agitationStage == 3) StartDash(position);
        }
        else
        {
            targetPriority *= 0.96f;
        }
    }
}
