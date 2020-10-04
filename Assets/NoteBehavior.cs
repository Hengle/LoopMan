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
    private  Color inactiveColor;
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        inactiveColor = mySprite.color;
        //activeColor = mySprite.color;
        noteWidth = transform.localScale.x;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setActive()
    {
        active = true;
        mySprite.color = new Color(inactiveColor.r, inactiveColor.g, inactiveColor.b, 1);
    }

    public void setInactive()
    {
        active = false;
        mySprite.color = inactiveColor;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Linked note width to pitch of the sound
        if (active)
        {
            if (collision.gameObject.CompareTag("playHead"))
            {

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
