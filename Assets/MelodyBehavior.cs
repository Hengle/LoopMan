using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MelodyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayheadBehavior playHead;
    AudioSource mySource;
    bool readyToPlay = false;
    bool coroutStarted = false;
    public GameObject boomBox;
    public GameObject melodyNoteGO;
    int count = 0;
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        melodyNoteGO.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        checkForTracksComplete();
    }

    void checkForTracksComplete()
    {
        if(playHead.track1Complete && playHead.track2Complete && playHead.track3Complete && playHead.track4Complete)
        {
            if(playHead.ticker == 1)
            {
                if(!coroutStarted)
                {
                    IEnumerator songDelay = startSongIn2Bars();
                    StartCoroutine(songDelay);
                    coroutStarted = true;
                }
            }
        }

        if(readyToPlay)
        {
            //OLD CODE handling playing.
            /*
            if (playHead.ticker == 1)
            {
                if (!mySource.isPlaying) { 
                mySource.Play();
                }
            }
            */
            //
        }

        if(readyToPlay && mySource.isPlaying)
        {
            if(playHead.ticker ==1)
            {
                count++;
            }
        }

        if(count==4)
        {
            boomBox.SetActive(true);
        }
    }

    private IEnumerator startSongIn2Bars()
    {
        float timeFor2Bars = playHead.numberOfBars * 4 * 60 / playHead.BPM;
        yield return new WaitForSeconds(timeFor2Bars-0.5f);
        melodyNoteGO.SetActive(true);
        //just create the gameObject which will trigger the loop.
        readyToPlay = true;
    }
}
