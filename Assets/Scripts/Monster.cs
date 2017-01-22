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

    private Transform playerHeadset;

    public GameObject SonarPulseSpot;
    public float SonarPulseCooldown = 10.0f;

    private MonsterSound monsterSound;

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
        monsterSound = GetComponent<MonsterSound>();
        StartCoroutine(SonarPulse());
        
        playerHeadset = FindObjectOfType<NewtonVR.NVRHead>().gameObject.transform; 
	}

    private void Update()
    {
        if (!isDashing)
        {
            targetPriority *= 0.99f;

            agitationValue -= Time.deltaTime * 0.7f;
            CheckAgitationPhase();
            //Debug.Log(agitationValue);
            
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

    IEnumerator SonarPulse()
    {
        //TODO Play sonar pulse animation

        //wait for time till pulse
        float num = monsterSound.ChargeSound();
        yield return new WaitForSeconds(num + 0.5f);

        //visual sonar pulse
        SonarParent.instance.StartScan(SonarPulseSpot.transform.position, 10);

        monsterSound.PulseSound();
        
        //Check if can see player
        RaycastHit hit;
        Vector3 rayDirection = playerHeadset.position - SonarPulseSpot.transform.position;
        if (Physics.Raycast(SonarPulseSpot.transform.position, rayDirection, out hit, 100f))
        {
            Debug.Log(hit.collider.gameObject.ToString());
             if (hit.collider.gameObject.GetComponent<NewtonVR.NVRHead>()) {
                 // enemy can see the player!
                 SetTarget(playerHeadset.position);
             }
        }

        //cooldown till next pulse
        yield return new WaitForSeconds(SonarPulseCooldown);
        StartCoroutine(SonarPulse());
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
