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
    // Start is called before the first frame update
    void Start()
    {
        
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

    public void registerEnemy(Transform enemyTrans, int enemyTrack)
    {
        switch(enemyTrack)
        {
            case 1:
                enemy1Trans = enemyTrans;
                break;
            case 2:
                enemy2Trans = enemyTrans;
                break;
            case 3:
                enemy3Trans = enemyTrans;
                break;
            case 4:
                enemy4Trans = enemyTrans;
                break;
        }
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
                findCorrespondingTimesFromRecording();
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
            
            if (enemy1Trans != null)
            {
                enemy1Pos.Add(enemy1Trans.position);
            }
            else
            {
                enemy1Pos.Add(new Vector3(1000, 1000, 1000));
            }
            if (enemy2Trans != null)
            {
                enemy2Pos.Add(enemy2Trans.position);
            }
            else
            {
                enemy2Pos.Add(new Vector3(1000, 1000, 1000));
            }

            if (enemy3Trans != null)
            {
                enemy3Pos.Add(enemy3Trans.position);
            }
            else
            {
                enemy3Pos.Add(new Vector3(1000, 1000, 1000));
            }
            if (enemy4Trans != null)
            {
                enemy4Pos.Add(enemy4Trans.position);
            }
            else
            {
                enemy4Pos.Add(new Vector3(1000, 1000, 1000));
            }
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
