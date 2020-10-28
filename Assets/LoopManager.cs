using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopManager : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioClip[] sounds;
   

    // we could have levels as an array of sounds


    // Start is called before the first frame update
    private void OnEnable()
    {
        MyEventSystem.track1Event += playTrack1;
        MyEventSystem.track2Event += playTrack2;
        MyEventSystem.track3Event += playTrack3;
        MyEventSystem.track4Event += playTrack4;
        MyEventSystem.melodyEvent += playMelody;
    }

    private void OnDisable()
    {
        MyEventSystem.track1Event -= playTrack1;
        MyEventSystem.track2Event -= playTrack2;
        MyEventSystem.track3Event -= playTrack3;
        MyEventSystem.track4Event -= playTrack4;
        MyEventSystem.melodyEvent -= playMelody;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void playTrack1(float i)
    {
        audioSources[0].PlayOneShot(sounds[0]);
        audioSources[0].pitch = 1/i;
    }

    public void playTrack2(float i)
    {
        audioSources[1].PlayOneShot(sounds[1]);
        audioSources[1].pitch = 1 / i;
    }

    public void playTrack3(float i)
    {
        audioSources[2].PlayOneShot(sounds[2]);
        audioSources[2].pitch = 1 / i;
    }

    public void playTrack4(float i)
    {
        audioSources[3].PlayOneShot(sounds[3]);
        audioSources[3].pitch = 1 / i;
    }

    public void playMelody(float i)
    {
        audioSources[4].Play();
    }

}
