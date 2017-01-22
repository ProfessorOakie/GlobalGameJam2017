using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class HeartBeatSound : MonoBehaviour
    {
        public AudioClip heartBeatSlow;
        public AudioClip heartBeatMed;
        public AudioClip heartBeatFast;

        AudioSource audio;
        // call these methods when you want to play the sound 
        // eg. HeartSoundSystem....IdleSound();
        public void HeartBeatSlow()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(heartBeatSlow);
        }

        public void HeartBeatMed()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(heartBeatMed);
        }

        public void HeartBeatFast()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(heartBeatFast);
        }
    }
