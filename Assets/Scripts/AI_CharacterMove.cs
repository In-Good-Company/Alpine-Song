using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_CharacterMove : MonoBehaviour
{
    public GameObject[] Points;
    public NavMeshAgent AI_test;

    private bool arrayReverse;
    private int pointNum;
    private int arrayEnd;

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

    
    private void Start()
    {
        AI_test = this.GetComponent<NavMeshAgent>();
        arrayReverse = false;
    }

    private void destSwitch()
    {


        if (this.transform.position.x == Points[pointNum].transform.position.x && this.transform.position.z == Points[pointNum].transform.position.z && arrayReverse == false)
        {
            pointNum += 1;
        }

        if (this.transform.position.x == Points[pointNum].transform.position.x && this.transform.position.z == Points[pointNum].transform.position.z && arrayReverse)
        {
            pointNum -= 1;
        }
    }

    private void arrayFind()
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
    void FixedUpdate()
    {
        print("End" + arrayEnd);
        print("point" + Points.Length);
        arrayFind();
        destSwitch();
        AI_test.SetDestination(Points[pointNum].transform.position);
        
    }
}
