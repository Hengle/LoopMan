using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] enemies;
    public float[] cameraValues;
    public Camera actionCam;
    float smoothedHeight = 0f;
    float targetHeight = 0f;
    int index = 0;
    bool advanced1 = false;
    bool advanced2 = false;
    bool advanced3 = false;
    
    public PlayheadBehavior playHead;
    
    void Start()
    {
        targetHeight = cameraValues[0];
    }

    // Update is called once per frame
    void Update()
    {
        checkForTrackCompletes();
        moveCameraToTarget();
    }

    void checkForTrackCompletes()
    {
        if(playHead.track1Complete)
        {
            if(!advanced1)
            {
                if (playHead.ticker == 1)
                {
                    updateTaget();
                    advanced1 = true;
                }
            }
        }

        if (playHead.track2Complete)
        {
            if (!advanced2)
            {
                if (playHead.ticker == 1)
                {
                    updateTaget();
                    advanced2 = true;
                }
            }
        }

        if (playHead.track3Complete)
        {
            if (!advanced3)
            {
                if (playHead.ticker == 1)
                {
                    updateTaget();
                    advanced3 = true;
                }
            }
        }

      
    }

    void moveCameraToTarget()
    {
          smoothedHeight = Mathf.Lerp(actionCam.rect.height, targetHeight, Time.deltaTime);
          actionCam.rect = new Rect(0, 0, 1, smoothedHeight);
        
    }

    void updateTaget()
    {
        index++;
        targetHeight = cameraValues[index];
        enemies[index].SetActive(true);
        
    }

}
