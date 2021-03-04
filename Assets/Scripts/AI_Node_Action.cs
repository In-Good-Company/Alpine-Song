using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Node_Action : MonoBehaviour
{
    public bool nodeReached;
    public bool objReached;
    public GameObject AI_Instance;
    public GameObject objOfInterest;
    public bool objDone;

    public int AI_Case_Ref;
    [Header("Character Waiting on Node")]
    [Tooltip("The actor will stay in place for a designated amount of time.")]
    public bool isStanding;
    [Tooltip("Time in seconds the actor will stay in place before moving on to the next node.")]
    public float StandTime;

    public bool GoToLocation;

    void Start()
    {
        AI_Case_Ref = 1;
        StandTime = 2.0f;
        objDone = false;
        if(objOfInterest == null)
        {
            objOfInterest = null;
        }
    }
    public void findObj()
    {
        if (GoToLocation == true && nodeReached == true && objDone == false)
        {
            AI_Case_Ref = 3;
           
        }
        else if(objDone == true)
        {
            AI_Case_Ref = 1;
        }
    }
    public void execStand()
    {
        if(isStanding == true && nodeReached == true)
        {
            AI_Case_Ref = 2;
        }
        
    }

    void Update()
    {
        if (Mathf.Abs(AI_Instance.transform.position.x - this.transform.position.x) <= 0.2 && Mathf.Abs(AI_Instance.transform.position.z - this.transform.position.z) <= 0.2)
        {
            nodeReached = true;
           
            
        }
        else
        {
            //AI_Case_Ref = 1;
            nodeReached = false;
        }
        if (objOfInterest != null && Mathf.Abs(AI_Instance.transform.position.x - objOfInterest.transform.position.x) <= 0.2 && Mathf.Abs(AI_Instance.transform.position.z - objOfInterest.transform.position.z) <= 0.2)
        {
            objReached = true;

        }
        else
        {
            objReached = false;
        }
        findObj();
        execStand();
        
    }
}
