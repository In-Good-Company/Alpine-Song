using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Actor_CharMove : MonoBehaviour
{
    public GameObject[] Points;
    public NavMeshAgent AI_Actor;
    public GameObject travelNodeScript;

    private bool arrayReverse;
    public bool followNodes;
    public bool resetCor;
    public bool desReached;
    private int pointNum;
    private int arrayEnd;
    public int AI_Case;

    [Header("Travel Loop")]
    [Tooltip("The character will loop through the startLoop and endLoop travel points starting from the first time the endLoop travel point is reached.")]
    public bool isTravelLoop;
    [Tooltip("The start point for the isTravelLoop.")]
    public int startLoop;
    [Tooltip("The end point for the isTravelLoop.")]
    public int endLoop;
    [Space(10)]
    [Tooltip("The character will loop back to the first travel point.")]
    public bool isLooping;
    [Tooltip("The character will follow the travel points in reverse until it reaches the beginning.")]
    public bool isBackTracking;
    [Tooltip("The character will stop and stay at the end travel point")]
    public bool isStaying;

    private Vector3 charVectorPos;
    private Vector3 nodeVectorPos;
    
    private void Awake()
    {
        AI_Actor = this.GetComponent<NavMeshAgent>();
        arrayReverse = false;
        followNodes = true;
        AI_Case = 1;  
    }
    public IEnumerator Stand(float waitTime)
    {

        print("Fire");
        followNodes = false;
        
        
        while (resetCor)
        {
            yield return new WaitForSeconds(waitTime);
            
        }
        
        AI_Case = 1;
        followNodes = true;
    }
    private void destSwitch()
    {

        if (desReached == true && followNodes == true)
        {


            if (arrayReverse == false)
            {
                pointNum += 1;
            }

            if (arrayReverse == true)
            {
                pointNum -= 1;
            }
        }

        travelFind();

    }

    public void nodeActions()
    {
        AI_Case = travelNodeScript.GetComponentInChildren<AI_Node_Action>().AI_Case_Ref;
        IEnumerator coroutine = Stand(5.0f);
        switch (AI_Case)
        {
            case 1:

                

                break;
            case 2:

                resetCor = true;
                StartCoroutine(coroutine);
                resetCor = false;
                break;

        }
    }
    private void travelFind()
    {
        arrayEnd = Points.Length;
        if (isBackTracking)
        {
            if (arrayEnd == pointNum)
            {
                arrayReverse = true;
                pointNum -= 1;
            }

            else if (pointNum < 0)
            {
                arrayReverse = false;
                pointNum += 1;
            }
        }

        if (isLooping)
        {
            if (arrayEnd == pointNum)
            {
                pointNum = 0;
                
            }
        }

        if (isStaying)
        {
            if(arrayEnd == pointNum)
            {
                pointNum -= 1;
            }
        }

        if (isTravelLoop)
        {
            if (pointNum == endLoop)
            {
                pointNum = startLoop;
            }
            
        }
       
    }
    void Update()
    {
        //// this finds the position of the travel node and the AI_character
        charVectorPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        nodeVectorPos = new Vector3(Points[pointNum].transform.position.x, 0, Points[pointNum].transform.position.z);
        ///////////////////////////////////////////////
        desReached = Points[pointNum].GetComponentInChildren<AI_Node_Action>().nodeReached;
        travelNodeScript = Points[pointNum];
        AI_Actor.SetDestination(travelNodeScript.transform.position);
        nodeActions();
       

    }
    private void LateUpdate()
    {
        if (followNodes == true)
        {
            destSwitch();

        }
    }
}
