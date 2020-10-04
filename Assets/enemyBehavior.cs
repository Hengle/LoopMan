using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip mySound;
    public int trackNumber = 0;
    EnemySoundManager enemyManager;
    bool recording = false;
    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("enemyManager").GetComponent<EnemySoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        inputManager();
    }

    void inputManager()
    {
        if (Input.GetMouseButton(1))
        {
            recording = true;
        }
        else
        {
            recording = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if(collision.CompareTag("playerAttackBox"))
        {
            
            enemyManager.playSound(mySound);
        }
    }
    private void OnMouseDown()
    {
       
        enemyManager.playSound(mySound);
        if (recording)
        {
            if (trackNumber == 1)
            {
                MyEventSystem.track1Hit(1);
            }
            if (trackNumber == 2)
            {
                MyEventSystem.track2Hit(1);
            }
            if (trackNumber == 3)
            {
                MyEventSystem.track3Hit(1);
            }
            if (trackNumber == 4)
            {
                MyEventSystem.track4Hit(1);
            }
        }
    }
}
