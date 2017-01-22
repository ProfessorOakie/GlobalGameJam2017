using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WalkieSoundSystem
{
    public class WalkieTalkieSound : MonoBehaviour
    {
        public AudioClip checkIn;
        public AudioClip monsterAbility;
        public AudioClip noPickUpCheckin;
        public AudioClip onPickup;
        public AudioClip openingDialogue1;
        public AudioClip openingDialogue2;
        public AudioClip pickupCheckin;
        public AudioClip powerOut;
        AudioSource audio;
        // call these methods when you want to play the sound 
        // using MonsterSoundSystem;
        // MonsterSound.IdleSound();
        public void Checkin()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(checkIn, 0.7f);
        } 
        public void MonsterAbility()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(monsterAbility, 0.7f);
        }

        public void NoPickUpChekin()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(noPickUpCheckin, 0.9f);
        }

        public void OpeningDialogue1()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(openingDialogue1, 0.6f);
        }

        public void OpeningDialogue2()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(openingDialogue2, 0.6f);
        }

        public void PickupCheckin()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(pickupCheckin, 0.6f);
        }

        public void PowerOut()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(powerOut, 0.6f);
        }
    }
}
