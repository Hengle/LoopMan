using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    public float startingTick = 0;
    public PlayheadBehavior playHead;
    public List<Vector3> positions;
    public List<Sprite> Sprites;
    public int playbackTick = 0;
    bool startPlayback = false;
    SpriteRenderer mySpriteRen;
    bool staticCloneCreated = false;
    public bool player = false;
    

    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<ParticleSystem>())
        {
            Destroy(GetComponent<ParticleSystem>());
        }
    }

    public void initializeGhost(List<Vector3> pos, List<Sprite> sprites, PlayheadBehavior playhead, float beginTick)
    {
        positions = new List<Vector3>();
        positions = pos;
        mySpriteRen = GetComponent<SpriteRenderer>();
        mySpriteRen.color = new Color(1, 1, 1, 0.2f);

        //mySpriteRen.enabled = false;
        playHead = playhead;
        startingTick = beginTick;

        if (sprites != null)
        {
            Sprites = new List<Sprite>();
            Sprites = sprites;
            replaceAllSpritesWithRoll();
            
           
        }
    }

    void replaceAllSpritesWithRoll()
    {
        Sprite rollSprite = Sprites[Sprites.Count / 2 + 2];
        for (int i = 0; i < Sprites.Count; i++)
        {
            if (Sprites[i].name.ToString().Contains("ROLL"))
            {
                rollSprite = Sprites[i];
                Debug.Log(rollSprite);
                break;
            }

        }  
            mySpriteRen.sprite = rollSprite;
            if (!staticCloneCreated)
            {
                GameObject staticClone = Instantiate(gameObject);
                Destroy(staticClone.GetComponent<GhostBehavior>());
                staticClone.GetComponent<SpriteRenderer>().sprite = rollSprite;
                staticCloneCreated = true;
            }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playHead.ticker == startingTick)
        {
            startPlayback = true;
            playbackTick = 0;
        }

        if(startPlayback)
        {
            playbackTick++;
            if (playbackTick < positions.Count -1)
            {
                mySpriteRen.enabled = true;
                transform.position = positions[playbackTick];
               
            } else
            {
                mySpriteRen.enabled = false;
            } 

        }
    }
}
