using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nextDialogueContainer : MonoBehaviour
{
    public Dialogue nextDialogue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dialogue GetNextDialogue(Dialogue _nextDialogue)
    {
        nextDialogue = _nextDialogue;
        return _nextDialogue;
    }
}
