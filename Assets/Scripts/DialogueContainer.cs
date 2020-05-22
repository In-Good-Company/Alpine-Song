using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class DialogueContainer : ScriptableObject
{
    public List<NodeLinkData> nodeLinks = new List<NodeLinkData>();
    public List<DialogueNodeData> dialogueNodeData = new List<DialogueNodeData>();
}
