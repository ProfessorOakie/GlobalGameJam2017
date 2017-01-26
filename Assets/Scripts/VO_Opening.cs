using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental;
using WalkieSoundSystem;
// Awu: Script so that once walkietalkie is grabbed stop looping through intro
public class VO_Opening : MonoBehaviour
{
    public WalkieTalkieSound walkieTalkieSound;
    public bool grabbing = true;
    public MonsterSound monsterSound;
    public GameObject MonsterCapsule;
    public GeneratorSound generatorSound;
    public HeartBeatSound heartBeatSound;
    // Use this for initialization

    public GameObject[] powerLightsOff;
    public GameObject[] powerLightsOn;

    void Start()
    {
        StartCoroutine(Example());

        foreach(GameObject g in powerLightsOff)
            g.SetActive(true);

        foreach (GameObject g in powerLightsOn)
            g.SetActive(false);

    }
    public void isGrabbing()
    {
        grabbing = false; 
    }
    void Update()
    {
        
    }
    
    IEnumerator Example()
    {

        while (grabbing) {
            yield return new WaitForSeconds(5);
            walkieTalkieSound.OpeningDialogue1();
            yield return new WaitForSeconds(15);
            while (grabbing)
            {
                walkieTalkieSound.OpeningDialogue2();
                yield return new WaitForSeconds(10);
            }
        }

        walkieTalkieSound.OnPickup();
        yield return new WaitForSeconds(27);

        generatorSound.GeneratorOff();
        foreach (GameObject g in powerLightsOff)
            g.SetActive(false);

        foreach (GameObject g in powerLightsOn)
            g.SetActive(true);

        //start heartbeat
        PlayerController.instance.StartHeartbeat();

        yield return new WaitForSeconds(3);
        walkieTalkieSound.PowerOut();
        yield return new WaitForSeconds(10);
        

        
        // play footsteps        
        for(int i = 0; i < 15; i++)
        {
            monsterSound.MonsterFootStep();
            yield return new WaitForSeconds(1);
        }

        
        // drop monster
        monsterSound.CeilingBreak();
        Monster.instance.enabled = true;
        Monster.instance.Activate();
        
        
        // wait for 30 seconds 
        yield return new WaitForSeconds(30);
        
        // initiate monster wander loop
        // play checkin part 1
        walkieTalkieSound.CheckIn();
        // set grabbing = true 
        
        grabbing = true;
            print("grabbing = true");
        
        // if picks up within 5 seconds 
        yield return new WaitForSeconds(10);
        // while did not pickup
        while (grabbing)
        {
            walkieTalkieSound.NoPickUpChekin();
            yield return new WaitForSeconds(30);
            // return monster back into the wanderloop 
            // play 45 seconds timer 
            //yield return new WaitForSeconds(20);
            // start generator sound 
            /*generatorSound.GeneratorOn();

            foreach (GameObject g in powerLightsOff)
                g.SetActive(true);

            foreach (GameObject g in powerLightsOn)
                g.SetActive(false);

            yield return new WaitForSeconds(5);
            walkieTalkieSound.MonsterAbility();*/
        }
        // while picked up within 5 seconds 
        while (!grabbing)
        {
            walkieTalkieSound.PickupCheckin();
            yield return new WaitForSeconds(30);
            // get monster back into the wanderloop 
            // start 15 seconds timer 
            //yield return new WaitForSeconds(10);
            // return monster back into the wanderloop 
            // start generator sound 
            /*generatorSound.GeneratorOn();

            foreach (GameObject g in powerLightsOff)
                g.SetActive(true);

            foreach (GameObject g in powerLightsOn)
                g.SetActive(false);

            yield return new WaitForSeconds(5);
            walkieTalkieSound.MonsterAbility();*/
        }

    }
}
