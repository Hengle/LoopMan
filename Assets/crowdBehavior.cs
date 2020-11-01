using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crowdBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        MyEventSystem.levelComplete += rave;
    }

    private void OnDisable()
    {
        MyEventSystem.levelComplete -= rave;   
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void rave(int i)
    {
        GetComponent<Animator>().Play("Base Layer.crowdAnimation");
    }
}
