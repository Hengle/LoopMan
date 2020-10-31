using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public int trackNumber;
    public bool active = false;
    private SpriteRenderer mySprite;
    private float noteWidth = 1;
    public  Color inactiveColor;
    public Color activeColor;
    public Animator attachedEnemyAnimator;
    public Transform attachedEnemyTransform;
    private GameObject myGhost;
    public GameObject attackGhostPrefab;
    private Animator attachedGhostAnimator;
    private Transform attachedGhostTransform;
    private Transform playerTransform;
    private int playerFacingDirection;
    private PlayerBehavior playerControls;
    public Sprite inactiveSprite;
    public Sprite activeSprite;
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerControls = playerTransform.gameObject.GetComponent<PlayerBehavior>();
        mySprite = GetComponent<SpriteRenderer>();
        
        inactiveColor = mySprite.color;
        //activeColor = mySprite.color;
        noteWidth = transform.localScale.x;
        
    }

    public void initializeNewNote()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerControls = playerTransform.gameObject.GetComponent<PlayerBehavior>();
        mySprite = GetComponent<SpriteRenderer>();

        inactiveColor = mySprite.color;
        //activeColor = mySprite.color;
        noteWidth = transform.localScale.x;
        setActive();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActive()
    {
        
        active = true;
        mySprite.sprite = activeSprite;
        recordGhost(playerTransform.position);
    }

    void recordGhost(Vector3 position)
    {
        if(myGhost==null)
        {
            myGhost = Instantiate(attackGhostPrefab, position, Quaternion.identity);
            attachedGhostAnimator = myGhost.GetComponent<Animator>();
            attachedGhostTransform = myGhost.transform;
            playerFacingDirection = playerControls.directionInteger;
           
        } else
        {
            myGhost.transform.position = position;
            attachedGhostTransform = myGhost.transform;
            playerFacingDirection = playerControls.directionInteger;

        }
    }

    public void setInactive()
    {
        active = false;
        
        mySprite.sprite = inactiveSprite;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Linked note width to pitch of the sound
        if (active)
        {
            if (collision.gameObject.CompareTag("playHead"))
            {
                //play animations etc. only if its NOT a melody note.
                if (trackNumber != 0)
                {
                    attachedEnemyAnimator.Play("Base Layer.enemy1Hit");
                    //check ghost position with respect to enemy and set appropriate animation
                    if (playerFacingDirection == 1)
                    {
                        attachedGhostAnimator.Play("Base Layer.ghostHitAnimation");
                    }
                    else if (playerFacingDirection == 2)
                    {
                        attachedGhostAnimator.Play("Base Layer.ghostHitRight");
                    }
                    else if (playerFacingDirection == 3)
                    {
                        attachedGhostAnimator.Play("Base Layer.ghostHitDown");
                    }
                    else if (playerFacingDirection == 4)
                    {
                        attachedGhostAnimator.Play("Base Layer.ghostHitLeft");
                    }
                }
               // attachedGhostAnimator.Play("Base Layer.ghostHitAnimation");
                if (trackNumber == 1)
                {
                    MyEventSystem.track1Event(noteWidth);
                }
                else if (trackNumber == 2)
                {
                    MyEventSystem.track2Event(noteWidth);
                }
                else if (trackNumber == 3)
                {
                    MyEventSystem.track3Event(noteWidth);
                }
                else if (trackNumber == 4)
                {
                    MyEventSystem.track4Event(noteWidth);
                } 
                else if(trackNumber ==0)
                {
                    MyEventSystem.melodyEvent(noteWidth);
                    active = false;
                }
            }
        } else
        {
            if (collision.gameObject.CompareTag("playHead"))
            {
                MyEventSystem.trackGlow(trackNumber);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("playHead"))
        {

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("playHead"))
        {
          
        }
    }
}
