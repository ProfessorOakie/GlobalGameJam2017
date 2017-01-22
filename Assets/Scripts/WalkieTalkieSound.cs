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

        private float volumeAddition = 0.5f;
        // call these methods when you want to play the sound 
        // using MonsterSoundSystem;
        // MonsterSound.IdleSound();
        public void CheckIn()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(checkIn, 0.7f + volumeAddition);
        } 
        public void MonsterAbility()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(monsterAbility, 0.7f + volumeAddition);
        }

        public void NoPickUpChekin()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(noPickUpCheckin, 0.9f + volumeAddition);
        }

        public void OpeningDialogue1()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(openingDialogue1, 0.6f + volumeAddition);
        }

        public void OpeningDialogue2()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(openingDialogue2, 0.6f + volumeAddition);
        }

        public void PickupCheckin()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(pickupCheckin, 0.6f + volumeAddition);
        }

        public void PowerOut()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(powerOut, 0.6f + volumeAddition);
        }
    }
}
