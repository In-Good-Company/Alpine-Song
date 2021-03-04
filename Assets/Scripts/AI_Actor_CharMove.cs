using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Actor_CharMove : MonoBehaviour
{
    public GameObject[] Points;
    public NavMeshAgent AI_Actor;
    public GameObject travelNodeScript;
    private GameObject travelSwitchDest;

    
    public bool followNodes;
    public bool resetCor;
    public bool desReached;
    public bool objReached;

    private bool findOnce;
    private bool addOnce;
    private bool arrayReverse;

    private int pointNum;
    private int arrayEnd;
    public int AI_Case;

    private float standTimer;

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
   //
    private Vector3 charVectorPos;
    private Vector3 nodeVectorPos;


    private void Awake()
    {
        AI_Actor = this.GetComponent<NavMeshAgent>();
        arrayReverse = false;
        followNodes = true;
        AI_Case = 1;
        travelSwitchDest = travelNodeScript;
        findOnce = true;
        addOnce = true;
    }

    public IEnumerator Stand(float waitTime)
    {

        print("Fire");
        followNodes = false;
        

        while (resetCor)
        {
            yield return new WaitForSeconds(waitTime);
            addOnce = true;
            resetCor = false;

        }

        AI_Case = 1;
        followNodes = true;
        

    }

    public IEnumerator Find(float waitTime, bool FireOnce)
    {
        if(objReached == true && FireOnce == true)
        {
            while (resetCor)
            {
                
                yield return new WaitForSeconds(waitTime);
                travelNodeScript.GetComponentInChildren<AI_Node_Action>().objDone = true;
                AI_Case = 1;
                print("fire corou");
                findOnce = false;
                resetCor = false;
                
                

            }
           
            
        }
        
    }

    
    private void destSwitch()
    {
        travelFind();
        if (desReached == true && addOnce == true && charVectorPos == nodeVectorPos/* && followNodes == true*/)
        {


            if (arrayReverse == false/* && travelNodeScript.GetComponentInChildren<AI_Node_Action>().objDone == true*/)
            {
                pointNum += 1;
                travelNodeScript.GetComponentInChildren<AI_Node_Action>().objDone = false;
            }

            if (arrayReverse == true)
            {
                pointNum -= 1;
            }
            addOnce = false;
        }

        

    }

    public void nodeActions()
    {
        
        standTimer = travelNodeScript.GetComponentInChildren<AI_Node_Action>().StandTime;
        IEnumerator findCoroutine = Find(standTimer, findOnce);
        IEnumerator standCoroutine = Stand(standTimer);

        switch (AI_Case)
        {
            case 1:
                print("case 1");
                travelSwitchDest = travelNodeScript;
                resetCor = true;
                findOnce = true;
                break;
            case 2:
                
                
                StartCoroutine(standCoroutine);
                
                break;

            case 3:
                print("case 3");
                travelSwitchDest = travelNodeScript.GetComponentInChildren<AI_Node_Action>().objOfInterest;
                StartCoroutine(findCoroutine);
                

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
      ////// this finds the position of the travel node and the AI_character
        charVectorPos = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        nodeVectorPos = new Vector3(Points[pointNum].transform.position.x, 0, Points[pointNum].transform.position.z);
      /////////////////////////////////////////////////
        AI_Case = travelNodeScript.GetComponentInChildren<AI_Node_Action>().AI_Case_Ref;
        desReached = travelNodeScript.GetComponentInChildren<AI_Node_Action>().nodeReached;
        objReached = travelNodeScript.GetComponentInChildren<AI_Node_Action>().objReached;
        travelNodeScript = Points[pointNum];
        
        nodeActions();
        AI_Actor.SetDestination(travelSwitchDest.transform.position);
        

    }
    private void LateUpdate()
    {
        if (followNodes == true && AI_Case == 1)
        {
            destSwitch();
            
        }
    }
}
