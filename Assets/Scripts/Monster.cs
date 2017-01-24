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

    public int agitationStage = 1;
    public float agitationValue = 0;
    public float agitationLowering = 0.2f;
    public float flipToStage2 = 10.0f;
    public float flipToStage3 = 15.0f;

    public float testSpeed = -1;

    private Transform playerHeadset;

    public GameObject SonarPulseSpot;
    public float SonarPulseCooldown = 10.0f;

    private MonsterSound monsterSound;

    private Animator monsterAnimator;

    private int lastStage = 1;
    private bool stingerPlaying = false;
    private bool hardMode = false;

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
        monsterAnimator = GetComponentInChildren<Animator>();
        StartCoroutine(InitialSonarPulse(30));
        
        playerHeadset = FindObjectOfType<NewtonVR.NVRHead>().gameObject.transform; 
	}

    private void Update()
    {

        if (agitationStage == 3 && lastStage == 2 && !stingerPlaying)
        {
            StartCoroutine(Stinger());
        }
        if (!isDashing)
        {
            targetPriority *= 0.99f;

            if (!hardMode) agitationValue -= agitationLowering;
            else agitationValue += agitationLowering;

            CheckAgitationPhase();
            
            //Debug.Log(agitationValue);

            //if (agent.remainingDistance < monsterReach)
            //    monsterAnimator.SetTrigger("Idle");

        }
        else 
        {    //Monster is 
            if (agent.enabled && agent.remainingDistance < monsterReach)
            {
                if (agitationStage == 2)
                    StopBriskWalk();
                else if (agitationStage == 3)
                    StopDash();
                else if (agitationStage == 1)
                    StopWalk();
                //else
                //    monsterAnimator.SetTrigger("Idle");
            }
        }

        // For testing
        if (Input.GetKeyDown(KeyCode.Y))
        {
            StartDash(target);
        }

        if(testSpeed != -1)
            agent.speed = testSpeed;
        lastStage = agitationStage;
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

    IEnumerator InitialSonarPulse(float timedelay)
    {
        yield return new WaitForSeconds(timedelay);
        StartCoroutine(SonarPulse());
    }


    IEnumerator Stinger()
    {
        if(agitationStage == 3)
        {
            stingerPlaying = true;
            monsterSound.Stinger();
            yield return new WaitForSeconds(30);
            stingerPlaying = false;
            StartCoroutine(Stinger());
        }
    }

    IEnumerator SonarPulse()
    {
        //TODO Play sonar pulse animation

        //wait for time till pulse
        float num = monsterSound.ChargeSound();
        yield return new WaitForSeconds(num + 0.5f);

        monsterSound.PulseSound();

        //visual sonar pulse
        for(int i = 0; i < 10; i++)
        {
            SonarParent.instance.StartScan(SonarPulseSpot.transform.position, 10);
            yield return new WaitForSeconds(0.1f);
        }

        
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


    private void StartWalk(Vector3 position)
    {
        monsterAnimator.SetTrigger("Walk");
        agent.destination = position;
        agent.speed = walkSpeed;
        isDashing = true;
    }
    private void StopWalk()
    {
        monsterAnimator.SetTrigger("Idle");
        agent.speed = walkSpeed;
        isDashing = false;
    }
    private void StartBriskWalk(Vector3 position)
    {
        monsterAnimator.SetTrigger("BriskWalk");
        agent.destination = position;
        agent.speed = briskWalkSpeed;
        isDashing = true;
    }
    private void StopBriskWalk()
    {
        monsterAnimator.SetTrigger("Idle");
        agent.speed = walkSpeed;
        isDashing = false;
    }

    private void StartDash(Vector3 position)
    {
        monsterAnimator.SetTrigger("Dash");
        agent.destination = position;
        agent.speed = dashSpeed;
        isDashing = true;
    }
    private void StopDash()
    {
        monsterAnimator.SetTrigger("Idle");
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
            else StartWalk(position);
        }
        else
        {
            targetPriority *= 0.96f;
        }
    }

    public void StartHardMode()
    {
        hardMode = true;
        if (!stingerPlaying) StartCoroutine(Stinger());
    }
}
