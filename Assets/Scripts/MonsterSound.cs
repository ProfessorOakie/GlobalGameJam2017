using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class MonsterSound : MonoBehaviour
    {
        public AudioClip[] monsterIdle;
        public AudioClip[] monsterCharge;
        public AudioClip[] monsterPulse;
        public AudioClip[] monsterWalk;
        public AudioClip monsterStinger;
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
        public float Stinger()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(monsterStinger, 0.7f);
            return monsterStinger.length;
        }
        public float IdleSound()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterIdle.Length);
            audio.PlayOneShot(monsterIdle[random], 0.7f);
            return monsterIdle[random].length;
        }

        public float ChargeSound()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterCharge.Length);
            audio.PlayOneShot(monsterCharge[random], 0.9f);
            return monsterCharge[random].length;
        }

        public float PulseSound()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterPulse.Length);
            audio.PlayOneShot(monsterPulse[random], 0.6f);
            return monsterPulse[random].length;
    }

        public float MonsterFootStep()
        {
            audio = GetComponent<AudioSource>();
            int random = Random.Range(0, monsterWalk.Length);
            audio.PlayOneShot(monsterWalk[random], 0.6f);
            return monsterWalk[random].length;
    }

        public float CeilingBreak()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(ceilingBreak, 0.9f);
            return ceilingBreak.length;
        }
    }
