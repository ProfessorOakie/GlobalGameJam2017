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
    
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Example());
        // prevent the capsule from droping through the roof
        MonsterCapsule.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;


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
        // wait for 5 seconds before monster footsteps 
        // drop monster and play footstep
        MonsterCapsule.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
        monsterSound.CeilingBreak();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        yield return new WaitForSeconds(1);
        monsterSound.MonsterFootStep();
        // wait for 30 seconds 
       
        
    }
}
