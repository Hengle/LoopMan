using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBubble : MonoBehaviour
{
    public GameObject recordGO;
    public GameObject onbeatGO;
    // Start is called before the first frame update
    private void OnDisable()
    {
        recordGO.SetActive(true);
        onbeatGO.SetActive(true);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
