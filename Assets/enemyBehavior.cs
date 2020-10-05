using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip mySound;
    public int trackNumber = 0;
    EnemySoundManager enemyManager;
    bool recording = false;
    private PlayerBehavior player;
    public GameObject glow;
    public ParticleSystem ps;
    public Color psColor;
    public LoopManager loopManager;
    private bool enabled = true;
    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("enemyManager").GetComponent<EnemySoundManager>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehavior>();
        ps = GetComponent<ParticleSystem>();
        loopManager = GameObject.FindGameObjectWithTag("looper").GetComponent<LoopManager>();
        mySound = loopManager.sounds[trackNumber - 1];
    }

    private void OnEnable()
    {
        MyEventSystem.trackGlow += playGlow;
    }

    private void OnDisable()
    {
        MyEventSystem.trackGlow -= playGlow;
    }

    // Update is called once per frame
    void Update()
    {
        inputManager();
    }

    void inputManager()
    {
        recording = player.recording;
    }

   
    public void playGlow(int i)
    {
        if (i == trackNumber)
        {
            IEnumerator glow = removeinSomeTime();
            StartCoroutine(glow);
        }
    }
    public void playSound()
    {
        ps.startColor = psColor;
        enemyManager.playSound(mySound);
        ps.Play();
        if (enabled)
        {
            if (recording)
            {
                if (trackNumber == 1)
                {
                    MyEventSystem.track1Hit(1);
                }
                if (trackNumber == 2)
                {
                    MyEventSystem.track2Hit(1);
                }
                if (trackNumber == 3)
                {
                    MyEventSystem.track3Hit(1);
                }
                if (trackNumber == 4)
                {
                    MyEventSystem.track4Hit(1);
                }
            }
        }
    }
    private void OnMouseDown()
    {
       /*
        enemyManager.playSound(mySound);
        if (recording)
        {
            if (trackNumber == 1)
            {
                MyEventSystem.track1Hit(1);
            }
            if (trackNumber == 2)
            {
                MyEventSystem.track2Hit(1);
            }
            if (trackNumber == 3)
            {
                MyEventSystem.track3Hit(1);
            }
            if (trackNumber == 4)
            {
                MyEventSystem.track4Hit(1);
            }
        }
       */
    }

    IEnumerator removeinSomeTime()
    {
        glow.SetActive(true);
        yield return new WaitForSeconds(0.1667f);
        glow.SetActive(false);
       


    }
}
