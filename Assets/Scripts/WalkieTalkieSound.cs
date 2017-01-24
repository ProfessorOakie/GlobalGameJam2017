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

        private void Start()
        {
            audio = GetComponent<AudioSource>();
        }

        public void CheckIn()
        {
            StartCoroutine(PlayVoiceLineWithSonar(checkIn, 0.7f + volumeAddition));
        } 
        public float MonsterAbility()
        {
            StartCoroutine(PlayVoiceLineWithSonar(monsterAbility, 0.7f + volumeAddition));
            return monsterAbility.length;
        }

        public void NoPickUpChekin()
        {
            StartCoroutine(PlayVoiceLineWithSonar(noPickUpCheckin, 0.9f + volumeAddition));
        }

        public void OpeningDialogue1()
        {
            StartCoroutine(PlayVoiceLineWithSonar(openingDialogue1, 0.6f + volumeAddition));
        }

        public void OpeningDialogue2()
        {
            StartCoroutine(PlayVoiceLineWithSonar(openingDialogue2, 0.6f + volumeAddition));
        }

        public void PickupCheckin()
        {
            StartCoroutine(PlayVoiceLineWithSonar(pickupCheckin, 0.6f + volumeAddition));
        }

        public void PowerOut()
        {
            StartCoroutine(PlayVoiceLineWithSonar(powerOut, 0.6f + volumeAddition));
        }

        public void OnPickup()
        {
            StartCoroutine(PlayVoiceLineWithSonar(onPickup, 0.6f + volumeAddition));
        }

        IEnumerator PlayVoiceLineWithSonar(AudioClip clip, float vol)
        {
            audio.PlayOneShot(clip, vol);
            for(int i = 0; i < clip.length; i++)
            {
                SonarParent.instance.StartScan(transform.position, vol);
                Monster.instance.SetTarget(transform.position, vol);
                yield return new WaitForSeconds(1);
            }
        }
    }
}
