using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonsterSoundSystem
{
    public class MonsterSound : MonoBehaviour
    {
        public AudioClip[] monsterIdle;
        public AudioClip[] monsterCharge;
        public AudioClip[] monsterPulse;
        public AudioClip[] monsterWalk;
        public AudioClip shatter;
        public AudioClip ceilingBreak;
        AudioSource audio;
        // call these methods when you want to play the sound 
        // using MonsterSoundSystem;
        // MonsterSound.IdleSound();
        public void Shatter()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(shatter, 0.7f);
        } 
        public void IdleSound()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterIdle.Length);
            audio.PlayOneShot(monsterIdle[random], 0.7f);
        }

        public void ChargeSound()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterCharge.Length);
            audio.PlayOneShot(monsterCharge[random], 0.9f);
        }

        public void PulseSound()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterPulse.Length);
            audio.PlayOneShot(monsterPulse[random], 0.6f);
        }

        public void MonsterFootStep()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterWalk.Length);
            audio.PlayOneShot(monsterWalk[random], 0.6f);
        }

        public void CeilingBreak()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(ceilingBreak, 0.9f);
        }
    }
}
