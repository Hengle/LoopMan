using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator looper;
    public int BPM = 90;
    private float beatWidth;
    private float timeForBar;
    public Vector3 endPosition;
    public GameObject sampleBeat;
    private bool barRunning = false;
    bool loopStarted = false;
    Vector3 initialPosition;
    Rigidbody2D myRb;
    public CameraShake Cam2DShake;
    new List<NoteBehavior> activeNotes = new List<NoteBehavior>();
    //update this to increase instrument count
    new List<NoteBehavior> overlappingNotes = new List<NoteBehavior> ();
    public PlayerBehavior player;
    

    private void OnEnable()
    {
        MyEventSystem.track1Hit += checkTrack1;
        MyEventSystem.track2Hit += checkTrack2;
        MyEventSystem.track3Hit += checkTrack3;
        MyEventSystem.track4Hit += checkTrack4;
    }

    private void OnDisable()
    {
        MyEventSystem.track1Hit -= checkTrack1;
        MyEventSystem.track2Hit -= checkTrack2;
        MyEventSystem.track3Hit -= checkTrack3;
        MyEventSystem.track4Hit -= checkTrack4;
    }
    void Start()
    {
        Initialize();
        
    }

    public void Initialize()
    {
        myRb = GetComponent<Rigidbody2D>();
        initialPosition = transform.position;
        beatWidth = sampleBeat.GetComponent<Transform>().localScale.x;
        
        Debug.Log("beatwidth " + beatWidth) ;
        timeForBar = 4.0f * (60.0f / BPM);
        Debug.Log("1 beat time " + 60.0f / (4.0f*BPM));
        endPosition = transform.position + new Vector3(4 * 4 * beatWidth, 0, 0) ;
        //IEnumerator moveHeader = MoveToPosition(transform, endPosition, timeForBar);
        // StartCoroutine(moveHeader);
        IEnumerator mainLoop = Looper();
        StartCoroutine(mainLoop);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (!loopStarted)
        {
            looper = Looper();
            StartCoroutine(looper);
            loopStarted = true;
        }
        */

        if(Input.GetMouseButtonUp(1))
        {
            activeNotes = new List<NoteBehavior>();
        }
    }

    public IEnumerator Looper()
    {
        while(true)
        {
            if(!barRunning) { 
                IEnumerator moveHeader = MoveToPosition(transform, endPosition, timeForBar);
                StartCoroutine(moveHeader);
                barRunning = true;
                yield return new WaitForSeconds(timeForBar);
                transform.position = initialPosition;
                barRunning = false;

            }
            
        }
    }
    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            transform.position = Vector3.Lerp(currentPos, position, t);
           // myRb.MovePosition(Vector3.Lerp(currentPos, position, t));
            
            yield return null;
        }
        
        
        barRunning = false;
    }


    void checkTrack1(int a)
    {
        Debug.Log("Checking track 1");
        for(int i=0; i<overlappingNotes.Count;i++)
        {
            if(overlappingNotes[i].trackNumber ==1 )
            {
                overlappingNotes[i].setActive();
                activeNotes.Add(overlappingNotes[i]);
            } else
            {
                foreach (NoteBehavior n in activeNotes)
                {
                    n.setInactive();
                    Cam2DShake.shakeDuration = 0.1f;
                }
            }
        }
    }

    void checkTrack2(int a)
    {
        for (int i = 0; i < overlappingNotes.Count; i++)
        {
            if (overlappingNotes[i].trackNumber == 2)
            {
                overlappingNotes[i].setActive();
                activeNotes.Add(overlappingNotes[i]);
            }
            else
            {
                foreach (NoteBehavior n in activeNotes)
                {
                    n.setInactive();
                    Cam2DShake.shakeDuration = 0.1f;
                }
            }
        }
    }

    void checkTrack3(int a)
    {
        for (int i = 0; i < overlappingNotes.Count; i++)
        {
            if (overlappingNotes[i].trackNumber == 3)
            {
                overlappingNotes[i].setActive();
                activeNotes.Add(overlappingNotes[i]);
            }
            else
            {
                foreach (NoteBehavior n in activeNotes)
                {
                    n.setInactive();
                    Cam2DShake.shakeDuration = 0.1f;
                }
            }
        }
    }

    void checkTrack4(int a)
    {
        for (int i = 0; i < overlappingNotes.Count; i++)
        {
            if (overlappingNotes[i].trackNumber == 4)
            {
                overlappingNotes[i].setActive();
                activeNotes.Add(overlappingNotes[i]);
            }
            else
            {
                foreach (NoteBehavior n in activeNotes)
                {
                    n.setInactive();
                    Cam2DShake.shakeDuration = 0.1f;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.GetComponent<NoteBehavior>())
        {
            
            NoteBehavior atttachedNote = collision.GetComponent<NoteBehavior>();
            if (!overlappingNotes.Contains(atttachedNote))
            {
                overlappingNotes.Add(atttachedNote);
                
            }
        }
        
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        NoteBehavior atttachedNote = collision.GetComponent<NoteBehavior>();
        if (collision.GetComponent<NoteBehavior>())
        {
            if (!overlappingNotes.Contains(atttachedNote))
            {
                overlappingNotes.Add(atttachedNote);
             
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<NoteBehavior>())
        {
            NoteBehavior attachedNote = collision.GetComponent<NoteBehavior>();
            IEnumerator removeNote = removeinSomeTime(attachedNote);
            StartCoroutine(removeNote);
            //if (overlappingNotes.Contains(atttachedNote))
            //{
            //   overlappingNotes.Remove(atttachedNote);
               
            //}
        }
    }

    IEnumerator removeinSomeTime(NoteBehavior attachedNote)
    {
        yield return new WaitForSeconds(0.1f);
        if (overlappingNotes.Contains(attachedNote))
        {
            overlappingNotes.Remove(attachedNote);

        }
        
        
    }
}
