using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Node_Action : MonoBehaviour
{
    public bool nodeReached;
    public GameObject AI_Instance;
    public GameObject objOfInterest;
    public int AI_Case_Ref;
    public bool isStanding;
    void Start()
    {
        AI_Case_Ref = 1;
    }
    public void findObj()
    {

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
            AI_Case_Ref = 1;
            nodeReached = false;
        }
        execStand();
    }
}
