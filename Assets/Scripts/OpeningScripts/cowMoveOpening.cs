using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CowMoveOpening : MonoBehaviour
{
    public NavMeshAgent cowNav;
    public GameObject target;
    public bool targetNull;
    private bool cowWander;


    public Vector3 wanderTarget;
    private GameObject wanderNav;
    private GameObject wanderNavPrefab;
    public int AI_Case;


    private void Start()
    {

        AI_Case = 1;

    }



    void Update()
    {

        wanderTarget = GetComponent<AI_Cow_wanderTimer>().wanderPoint;
        cowNav.SetDestination(wanderTarget);

    }
}