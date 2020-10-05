using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordingBehavior : MonoBehaviour
{
    List<Vector3> playerPositions;
    List<Vector3> enemy1Pos;
    List<Vector3> enemy2Pos;
    List<Vector3> enemy3Pos;
    List<Vector3> enemy4Pos;
    List<float> playHeadTicks;
    List<float> successTicks;
    List<Sprite> playerStates;
    Transform playerTrans;
    Transform enemy1Trans;
    Transform enemy2Trans;
    Transform enemy3Trans;
    Transform enemy4Trans;
    GameObject playerGhost;
    GameObject enemy1Ghost;
    GameObject enemy2Ghost;
    GameObject enemy3Ghost;
    GameObject enemy4Ghost;
    Transform pgTrans;
    Transform g1Trans;
    Transform g2Trans;
    Transform g3Trans;
    Transform g4Trans;
    SpriteRenderer pgRens;
    PlayheadBehavior playHead;
    bool playing = false;
    bool ghostsCreated = false;
    public float startingTick = 0.0f;
    public float time = 0.0f;
    public bool recording = false;
    bool started = false;
    private SpriteRenderer playerRends;
    int playbackTick = 0;
    int recordTick = 0;
    Transform t1;
    Transform t2;
    Transform t3;
    Transform t4;

    // Start is called before the first frame update
    void Start()
    {
        t1 = GameObject.FindGameObjectWithTag("enemy1").GetComponent<Transform>();
        t2 = GameObject.FindGameObjectWithTag("enemy2").GetComponent<Transform>();
        t3 = GameObject.FindGameObjectWithTag("enemy3").GetComponent<Transform>();
        t4 = GameObject.FindGameObjectWithTag("enemy4").GetComponent<Transform>();
    }

    void registerEnemies()
    {
        if(playHead.track1Complete)
        {
            enemy1Trans = t1;
        } else
        {
            enemy1Trans = null;
        }

        if (playHead.track2Complete)
        {
            enemy2Trans = t2;
        }
        else
        {
            enemy2Trans = null;
        }

        if (playHead.track3Complete)
        {
            enemy3Trans = t3;
        }
        else
        {
            enemy3Trans = null;
        }


        if (playHead.track4Complete)
        {
            enemy4Trans = t4;
        }
        else
        {
            enemy4Trans = null;
        }
    }
    //record tick at which recording was started
    public void createRecording(float t, Transform player, PlayheadBehavior theHead)
    {
        startingTick = t;
        playerTrans = player;
        playerRends = player.GetComponent<SpriteRenderer>();
        playerPositions = new List<Vector3>();
        playerStates = new List<Sprite>();
        enemy1Pos = new List<Vector3>();
        enemy2Pos = new List<Vector3>();
        enemy3Pos = new List<Vector3>();
        enemy4Pos = new List<Vector3>();
        playHeadTicks = new List<float>();
        playHead = theHead;
        successTicks = new List<float>();
        
    }

  
    private void OnEnable()
    {
        MyEventSystem.successfulNote += noteSuccess;
    }

    private void OnDestroy()
    {
        MyEventSystem.successfulNote -= noteSuccess;
    }
    private void OnDisable()
    {
        MyEventSystem.successfulNote -= noteSuccess;
    }


    void noteSuccess(int i)
    {
        successTicks.Add(playHeadTicks[playHeadTicks.Count-1]);
    }

    void findCorrespondingTimesFromRecording()
    {
        foreach(float tick in successTicks)
        {

            
            int correspondingIndex = playHeadTicks.IndexOf(tick);
            // enemyGhosts
            if (enemy1Trans != null)
            {
                GameObject enemy1Ghost = Instantiate(enemy1Trans.gameObject);
                Destroy(enemy1Ghost.GetComponent<BoxCollider2D>());
                Destroy(enemy1Ghost.GetComponent<enemyBehavior>());
                enemy1Ghost.AddComponent<GhostBehavior>();

                GhostBehavior enemy1GhostSettings = enemy1Ghost.GetComponent<GhostBehavior>();
                int lowLimit;
                if (correspondingIndex < 5)
                {
                    lowLimit = 0;
                }
                else
                {
                    lowLimit = correspondingIndex - 5;
                }
                enemy1GhostSettings.initializeGhost(enemy1Pos.GetRange(lowLimit, 20), null, playHead, tick);
            }

            if (enemy2Trans != null)
            {
                GameObject enemy2Ghost = Instantiate(enemy2Trans.gameObject);
                Destroy(enemy2Ghost.GetComponent<BoxCollider2D>());
                Destroy(enemy2Ghost.GetComponent<enemyBehavior>());
                enemy2Ghost.AddComponent<GhostBehavior>();

                GhostBehavior enemy2GhostSettings = enemy2Ghost.GetComponent<GhostBehavior>();
                int lowLimit;
                if (correspondingIndex < 5)
                {
                    lowLimit = 0;
                }
                else
                {
                    lowLimit = correspondingIndex - 5;
                }
                enemy2GhostSettings.initializeGhost(enemy2Pos.GetRange(lowLimit, 20), null, playHead, tick);
            }
            if (enemy3Trans != null)
            {
                GameObject enemy3Ghost = Instantiate(enemy3Trans.gameObject);
                Destroy(enemy3Ghost.GetComponent<BoxCollider2D>());
                Destroy(enemy3Ghost.GetComponent<enemyBehavior>());
                enemy3Ghost.AddComponent<GhostBehavior>();

                GhostBehavior enemy3GhostSettings = enemy3Ghost.GetComponent<GhostBehavior>();
                int lowLimit;
                if (correspondingIndex < 5)
                {
                    lowLimit = 0;
                }
                else
                {
                    lowLimit = correspondingIndex - 5;
                }
                enemy3GhostSettings.initializeGhost(enemy3Pos.GetRange(lowLimit, 20), null, playHead, tick);
            }
            if (enemy4Trans != null)
            {
                GameObject enemy4Ghost = Instantiate(enemy4Trans.gameObject);
                Destroy(enemy4Ghost.GetComponent<BoxCollider2D>());
                Destroy(enemy4Ghost.GetComponent<enemyBehavior>());
                enemy4Ghost.AddComponent<GhostBehavior>();

                GhostBehavior enemy4GhostSettings = enemy4Ghost.GetComponent<GhostBehavior>();
                int lowLimit;
                if (correspondingIndex < 5)
                {
                    lowLimit = 0;
                }
                else
                {
                    lowLimit = correspondingIndex - 5;
                }
                enemy4GhostSettings.initializeGhost(enemy4Pos.GetRange(lowLimit, 20), null, playHead, tick);
            }

            GameObject ghostForTick = Instantiate(playerTrans.gameObject);
            Destroy(ghostForTick.GetComponent<PlayerBehavior>());
            Destroy(ghostForTick.GetComponent<CircleCollider2D>());
            ghostForTick.AddComponent<GhostBehavior>();
            GhostBehavior ghostSettings = ghostForTick.GetComponent<GhostBehavior>();
            int lowerLimit;
            if(correspondingIndex < 5)
            {
                lowerLimit = 0;
            } else
            {
                lowerLimit = correspondingIndex - 5;
            }
            ghostSettings.initializeGhost(playerPositions.GetRange(lowerLimit, 20), playerStates.GetRange(lowerLimit, 20), playHead, tick );
        }

        ghostsCreated = true;
    }
    // Update is called once per frame
    void Update()
    {
        registerEnemies();


       

       
    }

    private void FixedUpdate()
    {
        if (startingTick > 0 && !started)
        {
            started = true;
            recording = true;

        }

        if (started)
        {
            beginRecording();
            checkForStartingTick();

        }

        playBackRecording();
    }
    public void stopRecording()
    {
        recording = false;

        
        if(!playHead.track1Complete && !playHead.track2Complete && !playHead.track3Complete & !playHead.track4Complete) {
            Destroy(gameObject);
        } else
        {
            if (!ghostsCreated)
            {
                if (playHead.streak)
                {
                    findCorrespondingTimesFromRecording();
                }
            }
        }
    }
    void beginRecording()
    {
        if (recording)
        {
            recordTick++;
            playerPositions.Add(playerTrans.position);
            playerStates.Add(playerRends.sprite);
            playHeadTicks.Add(playHead.ticker);
            
          
                enemy1Pos.Add(t1.position);
          
            enemy2Pos.Add(t2.position);
            
           
           
                enemy3Pos.Add(t3.position);
          
            
             enemy4Pos.Add(t4.position);
           
        }

    }

    public void createGhosts()
    {
        /*
        playerGhost = Instantiate(playerTrans.gameObject);
        playerGhost.GetComponent<SpriteRenderer>().color = Color.white;
        Destroy(playerGhost.GetComponent<PlayerBehavior>());
        if (enemy1Trans != null)
        {
            enemy1Ghost = Instantiate(enemy1Trans.gameObject,transform);
            g1Trans = enemy1Ghost.GetComponent<Transform>();
        }
        if (enemy2Trans != null)
        {
            enemy2Ghost = Instantiate(enemy2Trans.gameObject, transform);
            g2Trans = enemy2Ghost.GetComponent<Transform>();
        }
        if (enemy3Trans != null)
        {
            enemy3Ghost = Instantiate(enemy3Trans.gameObject, transform);
            g3Trans = enemy3Ghost.GetComponent<Transform>();
        }

        if (enemy4Trans != null)
        {
            enemy4Ghost = Instantiate(enemy4Trans.gameObject, transform);
            g4Trans = enemy3Ghost.GetComponent<Transform>();
        }
        pgTrans = playerGhost.GetComponent<Transform>();
        pgRens = playerGhost.GetComponent<SpriteRenderer>();

        
        
        
        ghostsCreated = true;
        playbackTick = 0;
        */
    }

    void checkForStartingTick()
    {
        /*
        if (ghostsCreated)
        {
            
            if (playHead.ticker == startingTick)
            {
                playing = true;

            }
        }
        */
    }

    public void playBackRecording()
    {
        /*
        if (playing)
        {
            
            if (playbackTick < playerPositions.Count)
            {
                pgTrans.position = playerPositions[playbackTick];
                pgRens.sprite = playerStates[playbackTick];
                playbackTick++;
            } else
            {
                playbackTick = 0;
            }
        }
        */
    }
}
