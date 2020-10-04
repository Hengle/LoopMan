using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundManager : MonoBehaviour
{
    private AudioSource mySource;
    // Start is called before the first frame update
    void Start()
    {
        mySource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound(AudioClip clip)
    {
        mySource.PlayOneShot(clip);
    }


}
