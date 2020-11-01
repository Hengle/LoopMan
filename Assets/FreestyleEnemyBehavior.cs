using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FreestyleEnemyBehavior : MonoBehaviour
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
    public GameObject attachedNote;
    private Transform playHeadTransform;
    public Transform noteLinePosition;
    public Transform parentTrackTransform;
    private Tilemap activeTilemap;
    void Start()
    {
        activeTilemap = parentTrackTransform.GetComponent<Tilemap>();
        playHeadTransform = GameObject.FindGameObjectWithTag("playHead").transform;
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
        /*
        if (i == trackNumber)
        {
            IEnumerator glow = removeinSomeTime();
            StartCoroutine(glow);
        }
        */
    }
    public void playSound(int directionInteger)
    {
        //ps.startColor = psColor;
        enemyManager.playSound(mySound);
        //ps.Play();
        GetComponent<Animator>().Play("Base Layer.enemy1Hit");
        if (enabled)
        {
            if (recording)
            {
                 
                Vector3 notePosition = new Vector3(playHeadTransform.position.x, noteLinePosition.position.y, playHeadTransform.position.z);
                
                Vector3Int tilePosition = activeTilemap.WorldToCell(notePosition);
                Vector3 adjustedPosition = activeTilemap.CellToWorld(tilePosition);
                
                GameObject newNote = Instantiate(attachedNote, notePosition, Quaternion.identity, parentTrackTransform);
                NoteBehavior newNoteBehavior = newNote.GetComponent<NoteBehavior>();
                newNoteBehavior.attachedEnemyAnimator = GetComponent<Animator>();
                newNoteBehavior.attachedEnemyTransform = transform;
                newNoteBehavior.trackNumber = trackNumber;
                newNoteBehavior.initializeNewNote();

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
