using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BPMReader : MonoBehaviour
{
    public string BPMValue;
    public int BPMint;
    public PlayheadBehavior playHead;
    public InputField attachedField;
    // Start is called before the first frame update
    void Start()
    {
        attachedField = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void storeBPM()
    {
        BPMValue = attachedField.text;
        BPMint = int.Parse(BPMValue);
        if (BPMint > 1)
        {
            playHead.updateBPM(BPMint);
        }
    }
}
