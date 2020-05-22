using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GraphSaveUtility
{
    private DialogueGraphView _targetGraphView;
    private DialogueContainer _containerCache;

    private List<Edge> Edges => _targetGraphView.edges.ToList();
    private List<DialogueNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogueNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogueGraphView targetGraphView)
    {
        return new GraphSaveUtility
        {
            _targetGraphView = targetGraphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!Edges.Any()) return;

        var dialogueContainer = ScriptableObject.CreateInstance<DialogueContainer>();

        var connectedPorts = Edges.Where(Edge => Edge.input.node != null).ToArray();
        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogueNode;
            var inputNode = connectedPorts[i].input.node as DialogueNode;

            dialogueContainer.nodeLinks.Add(new NodeLinkData
            {
                baseNodeGUID = outputNode.GUID,
                portName = connectedPorts[i].output.name,
                targetNodeGUID = inputNode.GUID
            });
            
        }

        foreach (var dialogueNode in Nodes.Where(node => !node.entryPoint))
        {
            dialogueContainer.dialogueNodeData.Add(new DialogueNodeData
            {
                nodeGUID = dialogueNode.GUID,
                DialogueText = dialogueNode.dialogueText,
                Pos = dialogueNode.GetPosition().position
            });
        }

        if (!AssetDatabase.IsValidFolder($"Assets/Resources"))
            AssetDatabase.CreateFolder($"Assets", "Resources");

        AssetDatabase.CreateAsset(dialogueContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }

    public void LoadGraph(string fileName)
    {
        _containerCache = Resources.Load<DialogueContainer>(fileName);
        if(_containerCache==null)
        {
            EditorUtility.DisplayDialog("File not found", "Target dialogue graph file does not exist", "ok");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }

    private void ClearGraph()
    {
        Nodes.Find(dialogueNode => dialogueNode.entryPoint).GUID = _containerCache.nodeLinks[0].baseNodeGUID;

        foreach ( var node in Nodes)
        {
            if (node.entryPoint) return;
            Edges.Where(edge => edge.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));

            _targetGraphView.RemoveElement(node);
        }
    }
    private void CreateNodes()
    {
        foreach ( var nodeData in _containerCache.dialogueNodeData)
        {
            var tempNode = _targetGraphView.CreateDialogueNode(nodeData.DialogueText);
            tempNode.GUID = nodeData.nodeGUID;
            _targetGraphView.AddElement(tempNode);

            //var nodePort =
        }
    }
    private void ConnectNodes()
    {

    }
}
