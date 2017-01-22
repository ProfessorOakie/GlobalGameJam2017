using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class GeneratorSound : MonoBehaviour
    {
        public AudioClip generatorOn;
        public AudioClip generatorOff;
        
        AudioSource audio;
        // call these methods when you want to play the sound 
        // using MonsterSoundSystem;
        // MonsterSound.IdleSound();
        public void GeneratorOn()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(generatorOn, 1.5f);
        } 
        public void GeneratorOff()
        {
            audio = GetComponent<AudioSource>();
            audio.PlayOneShot(generatorOff, 1.5f);
        }
        
    }

