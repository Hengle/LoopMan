using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreestyleManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform track1;
    public Transform track2;
    public Transform track3;
    public Transform track4;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ClearTracksOnInput();
    }

    void ClearTracksOnInput()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            foreach( Transform child in track1)
            {
                Destroy(child.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            foreach (Transform child in track2)
            {
                Destroy(child.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            foreach (Transform child in track3)
            {
                Destroy(child.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            foreach (Transform child in track4)
            {
                Destroy(child.gameObject);
            }
        }
    }
}
