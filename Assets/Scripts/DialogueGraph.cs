using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphview;
    private string _fileName = "New Sentence";

    [MenuItem("Graph/Dialogue Graph")]
    public static void OpenDialogueWindow()
    {
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("Dialogue Graph");
    }

    public void OnEnable()
    {
        ConstructGraph();
        GenerateToolbar();
    }

    private void ConstructGraph()
    {
        _graphview = new DialogueGraphView
        {
            name = "Dialogue Graph"
        };

        _graphview.StretchToParentSize();
        rootVisualElement.Add(_graphview);
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var filenameTextField = new TextField("File Name");
        filenameTextField.SetValueWithoutNotify(_fileName);
        filenameTextField.MarkDirtyRepaint();
        filenameTextField.RegisterCallback((EventCallback<ChangeEvent<string>>)(evt => _fileName = evt.newValue));
        toolbar.Add(filenameTextField);

        toolbar.Add(new Button(clickEvent:() => RequestDataOperation(true)) { text = "Save Data" });
        toolbar.Add(new Button(clickEvent:() => RequestDataOperation(false)) { text = "Load Data" });

        var nodeCreateButton = new Button( clickEvent:() => { _graphview.CreateNode("Dialogue Node"); });
        nodeCreateButton.text = "Create Node";
        toolbar.Add(nodeCreateButton);

        rootVisualElement.Add(toolbar);
    }

    private void RequestDataOperation(bool save)
    {
        if (string.IsNullOrEmpty(_fileName))
        {
            EditorUtility.DisplayDialog("Invalid file name", "please enter a valid file name.", "ok");
            return;
        }

       var saveUtility = GraphSaveUtility.GetInstance(_graphview);
       if (save)
           saveUtility.SaveGraph(_fileName);
       else
       {
           saveUtility.LoadGraph(_fileName);
       }
    }

    private void LoadData()
    {
        //throw new NotImplementedException();
    }

    private void SaveData()
    {
        //throw new NotImplementedException();
    }

    public void OnDisable()
    {
        rootVisualElement.Remove(_graphview);
    }
}
