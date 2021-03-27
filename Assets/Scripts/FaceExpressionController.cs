using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceExpressionController : MonoBehaviour
{
    public Material material;
    public Texture Base;
    public Texture Angry;
    public Texture Confused;
    public Texture Excited;
    public Texture Happy;
    public Texture Sad;
    public Texture Surprised;
    public Texture Sus;

    //Change Texture according to vaule sent by DialogueManager
    public void ChangeExpression(string newExpression)
    {
        Debug.Log("changed expression to" + newExpression);
        switch (newExpression)
        {
            case "default":
                material.mainTexture = Base;
                break;
            case "angry":
                material.mainTexture = Angry;
                break;
            case "confused":
                material.mainTexture = Confused;
                break;
            case "excited":
                material.mainTexture = Excited;
                break;
            case "happy":
                material.mainTexture = Happy;
                break;
            case "sad":
                material.mainTexture = Sad;
                break;
            case "surprised":
                material.mainTexture = Surprised;
                break;
            case "sus":
                material.mainTexture = Sus;
                break;
        }
            
    }
}
