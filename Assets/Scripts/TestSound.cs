using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterSoundSystem;
using HeartSoundSystem;
using WalkieSoundSystem;

public class TestSound : MonoBehaviour {
    public MonsterSound monsterSound;
    public HeartBeatSound heartBeatSound;
    public WalkieTalkieSound walkieTalkieSound;
	// Use this for initialization
	void Start () {
        StartCoroutine(Example());
        
       
	}
    IEnumerator Example()
    {
        yield return new WaitForSeconds(3);
        walkieTalkieSound.CheckIn();
        yield return new WaitForSeconds(5);
        walkieTalkieSound.NoPickUpChekin();
        yield return new WaitForSeconds(3);
        monsterSound.PulseSound();
        yield return new WaitForSeconds(1);
        heartBeatSound.HeartBeatSlow();
        yield return new WaitForSeconds(4);
        monsterSound.Shatter();
        heartBeatSound.HeartBeatFast();
        yield return new WaitForSeconds(5);
        heartBeatSound.HeartBeatSlow();
        monsterSound.IdleSound();
        yield return new WaitForSeconds(2);
        monsterSound.ChargeSound();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
