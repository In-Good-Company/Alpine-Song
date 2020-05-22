using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{

    private readonly Vector2 defualtNodeSize = new Vector2(150, 200);
    public DialogueGraphView()
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogueGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();


        AddElement(GenerateEntryPOintNode());
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();

        ports.ForEach((port) => 
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
            var portView = port;
        });
        return compatiblePorts;
    }

    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal,portDirection,capacity, typeof(float));
    }

    private DialogueNode GenerateEntryPOintNode()
    {
        var node = new DialogueNode
        {
            title = "start",
            GUID = Guid.NewGuid().ToString(),
            dialogueText = "entryPoint",
            entryPoint = true
        };

        var generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode(string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }

    public DialogueNode CreateDialogueNode(string nodeName)
    {
        var dialogueNode = new DialogueNode
        {
            title = nodeName,
            dialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };
        var _inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        _inputPort.portName = "input";
        dialogueNode.inputContainer.Add(_inputPort);

        var button = new Button(clickEvent:() => { AddChoicePort(dialogueNode);});
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        dialogueNode.SetPosition(new Rect(Vector2.zero, defualtNodeSize));
        return dialogueNode;
    }

    private void AddChoicePort(DialogueNode dialogueNode)
    {
        var generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var _outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;
        var ouputPortName = $"Choice {_outputPortCount}";

        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }

}
