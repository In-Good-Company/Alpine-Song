using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Dialogue", menuName = "ScriptableObjects/Dialogue")]
public class Dialogue : ScriptableObject {

    public string name;

    [TextArea(3,10)]
    public string[] sentences;

    public Response[] responses;

}

[System.Serializable]
public struct Response
{
    [TextArea(3, 10)]
    public string choiceText;
    public Dialogue nextDialogue;
}
