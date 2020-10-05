using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    public SpriteRenderer track1;
    public SpriteRenderer track2;
    public SpriteRenderer track3;
    public SpriteRenderer track4;

    private Color initialColor;


    private void OnEnable()
    {
        MyEventSystem.track1Hit += note1Hit;
        MyEventSystem.track2Hit += note2Hit;
        MyEventSystem.track3Hit += note3Hit;
        MyEventSystem.track4Hit += note4Hit;
    }

    private void OnDisable()
    {
        
            MyEventSystem.track1Hit -= note1Hit;
            MyEventSystem.track2Hit -= note2Hit;
            MyEventSystem.track3Hit -= note3Hit;
            MyEventSystem.track4Hit -= note4Hit;
        
    }

    void note1Hit(int i)
    {
        IEnumerator blink1 = blinkTrack(1, track1);
        StartCoroutine(blink1);
    }

    void note2Hit(int i)
    {
        IEnumerator blink2 = blinkTrack(2, track2);
        StartCoroutine(blink2);
    }

    void note3Hit(int i)
    {
        IEnumerator blink3 = blinkTrack(3, track3);
        StartCoroutine(blink3);
    }

    void note4Hit(int i)
    {
        IEnumerator blink4 = blinkTrack(4, track4);
        StartCoroutine(blink4);
    }

    private IEnumerator blinkTrack(int track, SpriteRenderer sprite)
    {
        sprite.color = Color.black;
        yield return new WaitForSeconds(0.1f);
        sprite.color = initialColor;
    }


    void Start()
    {
        initialColor = track1.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
