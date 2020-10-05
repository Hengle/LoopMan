using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayheadBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private IEnumerator looper;
    public int BPM = 90;
    public int numberOfBars = 1;
    private float beatWidth;
    private float timeForBar;
    public Vector3 endPosition;
    public GameObject sampleBeat;
    private bool barRunning = false;
    bool loopStarted = false;
    Vector3 initialPosition;
    Rigidbody2D myRb;
    public CameraShake Cam2DShake;
    new List<NoteBehavior> activeNotes1 = new List<NoteBehavior>();
    new List<NoteBehavior> activeNotes2 = new List<NoteBehavior>();
    new List<NoteBehavior> activeNotes3 = new List<NoteBehavior>();
    new List<NoteBehavior> activeNotes4 = new List<NoteBehavior>();
    public int notesInTrack1 = 4;
    public int notesInTrack2 = 4;
    public int notesInTrack3 = 4;
    public int notesInTrack4 = 4;
    public bool track1Complete = false;
    public bool track2Complete = false;
    public bool track3Complete = false;
    public bool track4Complete = false;
    public float ticker = 0.0f;
    public float time = 0f;
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
        timeForBar = numberOfBars *  4.0f * (60.0f / BPM);
        Debug.Log("1 beat time " + 60.0f / (4.0f*BPM));
        endPosition = transform.position + new Vector3(4 * 4 * numberOfBars* beatWidth, 0, 0) ;
        //IEnumerator moveHeader = MoveToPosition(transform, endPosition, timeForBar);
        // StartCoroutine(moveHeader);
        IEnumerator mainLoop = Looper();
        StartCoroutine(mainLoop);
        track1Complete = false;
        track2Complete = false;
        track3Complete = false;
        track4Complete = false;
    }

    // Update is called once per frame
    void Update()
    {
        ticker++;
        /*
        if (!loopStarted)
        {
            looper = Looper();
            StartCoroutine(looper);
            loopStarted = true;
        }
        */

        //reseting loop when right mouse is released!
        TrackCompleteCheck();
        if(Input.GetMouseButtonUp(1))
        {

            if(activeNotes1.Count > 0)
            {

                if (activeNotes1.Count == notesInTrack1)
                {
                    activeNotes1 = new List<NoteBehavior>();
                }
                else
                {
                    foreach (NoteBehavior n in activeNotes1)
                    {
                        n.setInactive();
                       
                    }
                    Cam2DShake.shakeDuration = 0.1f;
                    //destroy recording on error
                    if (player.currentRecording.gameObject != null)
                    {
                        Destroy(player.currentRecording.gameObject);
                    }
                    activeNotes1 = new List<NoteBehavior>();
                }
            }

            if (activeNotes2.Count > 0)
            {
                if (activeNotes2.Count == notesInTrack2)
                {
                    activeNotes2 = new List<NoteBehavior>();
                }
                else
                {
                    foreach (NoteBehavior n in activeNotes2)
                    {
                        n.setInactive();
                        
                    }
                    Cam2DShake.shakeDuration = 0.1f;
                    if (player.currentRecording != null)
                    {
                        Destroy(player.currentRecording.gameObject);
                    }
                    activeNotes2 = new List<NoteBehavior>();
                }
            }

            if (activeNotes3.Count > 0)
            {
                if (activeNotes3.Count == notesInTrack3)
                {
                    activeNotes3 = new List<NoteBehavior>();
                }
                else
                {
                    foreach (NoteBehavior n in activeNotes3)
                    {
                        n.setInactive();
                       
                    }
                    Cam2DShake.shakeDuration = 0.1f;
                    if (player.currentRecording != null)
                    {
                        Destroy(player.currentRecording.gameObject);
                    }
                    activeNotes3 = new List<NoteBehavior>();
                }
            }

            if (activeNotes4.Count > 0 )
            {

                if (activeNotes4.Count == notesInTrack4)
                {
                    activeNotes4 = new List<NoteBehavior>();
                }
                else
                {
                    foreach (NoteBehavior n in activeNotes4)
                    {
                        n.setInactive();
                        
                    }
                    Cam2DShake.shakeDuration = 0.1f;
                    if (player.currentRecording.gameObject != null)
                    {
                        Destroy(player.currentRecording.gameObject);
                    }
                    activeNotes4 = new List<NoteBehavior>();
                }
            }

        }
    }
    
    void TrackCompleteCheck()
    {
        if(activeNotes1.Count == notesInTrack1)
        {
            track1Complete = true;
        }

        if (activeNotes2.Count == notesInTrack2)
        {
            track2Complete = true;
        }

        if (activeNotes3.Count == notesInTrack3)
        {
            track3Complete = true;
        }

        if (activeNotes4.Count == notesInTrack4)
        {
            track4Complete = true;
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
                yield return new WaitForSecondsRealtime(timeForBar);
                transform.position = initialPosition;
                barRunning = false;
                ticker = 0;

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

    //Check for timed presses!
    void checkTrack1(int a)
    {
        if (!track1Complete)
        {
            bool matchFound = false;
            Debug.Log("Checking track 1");
            for (int i = 0; i < overlappingNotes.Count; i++)
            {
                if (overlappingNotes[i].trackNumber == 1)
                {
                    overlappingNotes[i].setActive();
                    activeNotes1.Add(overlappingNotes[i]);
                    matchFound = true;
                    MyEventSystem.successfulNote(1);
                }

            }
        

        if(!matchFound)
        {
            foreach (NoteBehavior n in activeNotes1)
            {
                n.setInactive();
                Cam2DShake.shakeDuration = 0.1f;
                activeNotes1 = new List<NoteBehavior>();
                    /*
                    if (player.currentRecording.gameObject != null)
                    {
                        Destroy(player.currentRecording.gameObject);
                    }
                    */

                }
        }
        }
    }

    void checkTrack2(int a)
    {
        if (!track2Complete)
        {
            bool matchFound = false;
            for (int i = 0; i < overlappingNotes.Count; i++)
            {
                if (overlappingNotes[i].trackNumber == 2)
                {
                    overlappingNotes[i].setActive();
                    activeNotes2.Add(overlappingNotes[i]);
                    matchFound = true;
                    MyEventSystem.successfulNote(1);

                }

            }

            if (!matchFound)
            {
                foreach (NoteBehavior n in activeNotes2)
                {
                    n.setInactive();
                    Cam2DShake.shakeDuration = 0.1f;
                    activeNotes2 = new List<NoteBehavior>();
                    /*
                    if (player.currentRecording.gameObject != null)
                    {
                        Destroy(player.currentRecording.gameObject);
                    }
                    */
                }
            }
        }
    }

    void checkTrack3(int a)
    {
        if (!track3Complete)
        {
            bool matchFound = false;
            for (int i = 0; i < overlappingNotes.Count; i++)
            {
                if (overlappingNotes[i].trackNumber == 3)
                {
                    overlappingNotes[i].setActive();
                    activeNotes3.Add(overlappingNotes[i]);
                    matchFound = true;
                    MyEventSystem.successfulNote(1);
                }

            }

            if (!matchFound)
            {
                foreach (NoteBehavior n in activeNotes3)
                {
                    n.setInactive();
                    Cam2DShake.shakeDuration = 0.1f;
                    activeNotes2 = new List<NoteBehavior>();
                    /*
                    if (player.currentRecording.gameObject != null)
                    {
                        Destroy(player.currentRecording.gameObject);
                    }
                    */
                }
            }
        }
    }

    void checkTrack4(int a)
    {
        if (!track4Complete)
        {
            bool matchFound = false;
            for (int i = 0; i < overlappingNotes.Count; i++)
            {
                if (overlappingNotes[i].trackNumber == 4)
                {
                    overlappingNotes[i].setActive();
                    activeNotes4.Add(overlappingNotes[i]);
                    matchFound = true;
                    MyEventSystem.successfulNote(1);
                }

            }

            if (!matchFound)
            {
                foreach (NoteBehavior n in activeNotes4)
                {
                    n.setInactive();
                    Cam2DShake.shakeDuration = 0.1f;
                    activeNotes4 = new List<NoteBehavior>();
                    /*
                   if (player.currentRecording.gameObject != null)
                   {
                       Destroy(player.currentRecording.gameObject);
                   }
                   */
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
