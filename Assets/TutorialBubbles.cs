using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBubbles : MonoBehaviour
{
    // Start is called before the first frame update
    int w = 0;
    int a = 0;
    int s = 0;
    int d = 0;
    int wasdbool = 0;
    int strike = 0;
    int record = 0;
    int onbeat = 0;
    int camshake = 1;
    public GameObject wasd;
    public GameObject strikeGO;
    public GameObject recordGO;
    public GameObject onbeatGO;
    public GameObject camShakeGO;
    public PlayheadBehavior playHead;

    private void OnEnable()
    {
        MyEventSystem.track1Hit += CheckForStrike;
        MyEventSystem.recFail += showCamShakeWarning;
    }

    private void OnDisable()
    {
        MyEventSystem.track1Hit -= CheckForStrike;
        MyEventSystem.recFail -= showCamShakeWarning;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkForTrackComplete();
        CheckForWASD();
        if(w==1 && a ==1 && d == 1 && s ==1 && wasdbool ==0)
        {
            wasd.SetActive(false);
            strikeGO.SetActive(true);
            wasdbool = 1;
            
        }


        if (record == 1)
        {
            recordGO.SetActive(false);
        }

        if(onbeat ==1 )
        {
            onbeatGO.SetActive(false);
        }

        if(camshake ==1)
        {
            camShakeGO.SetActive(false);
        }

        if (camshake == 0)
        {
            camShakeGO.SetActive(true);
        }
        
        
        
    }

    void CheckForWASD()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            w = 1;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            a = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            d = 1;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            s = 1;
        }
    }

    void checkForTrackComplete()
    {
        if(playHead.track1Complete)
        {
            camshake = 1;
            onbeat = 1;
        }
    }

    void CheckForStrike(int i)
    {
        record = 1;
    }

    void showCamShakeWarning(int i)
    {
        camshake = 0;
    }


    
}
