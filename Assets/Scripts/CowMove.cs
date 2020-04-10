using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CowMove : MonoBehaviour
{
    public NavMeshAgent cowNav;
    public GameObject target;
    public GameObject playerTarget;
    public Button callBtn;
    public bool isInRange;
    public bool cowFollow;
    public bool isGrass;
    public bool targetNull;
    private bool cowWander;


    public Vector3 wanderTarget;
    private GameObject wanderNav;
    private GameObject wanderNavPrefab;
    public int AI_Case;


    private void Start()
    {
        Button call = callBtn.GetComponent<Button>();
        call.onClick.AddListener(cowCalled);
        isInRange = false;
        cowFollow = false;
        AI_Case = 1;
       
    }

    private void cowCalled()
    {
        
        if(isInRange == true && AI_Case != 3)
        {
            cowFollow = true;
            target = playerTarget;
            AI_Case = 2;
        }
    }



    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.name == "cowCallCol" )
        {
            
            isInRange = true;
            playerTarget = col.gameObject;
            
            
        }
        if (col.gameObject.tag == "Grass")
        {

            target = col.gameObject;
            AI_Case = col.GetComponent<grassEating>().AI_CowTarget;
            

        }
        if (col.gameObject.tag == "Trap")
        {

            target = col.gameObject;
            AI_Case = col.GetComponent<trapSpring>().AI_CowTarget;

        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "cowCallCol")
        {

            isInRange = false;


        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == ("Pen"))
        {

            AI_Case = 5;
            
        }
    }

 
    void Update()
    {

        
        switch (AI_Case)
        {
            case 1:

                wanderTarget = GetComponent<AI_Cow_wanderTimer>().wanderPoint;
                cowNav.SetDestination(wanderTarget);
                print("wander");
                break;

            case 2:

                cowNav.SetDestination(target.transform.position);
                print("Follow Player");
                break;

            case 3:

                cowNav.SetDestination(target.transform.TransformPoint(0, 0, 0));
                AI_Case = target.GetComponent<grassEating>().AI_CowTarget;
                print("Grass!");
                break;

            case 4:
                AI_Case = target.GetComponent<trapSpring>().AI_CowTarget;
                cowNav.SetDestination(target.transform.TransformPoint(0, 0, 0));
                
                break;

            case 5:
                cowNav.SetDestination(transform.position);
                break;
        }

        wanderTarget = GetComponent<AI_Cow_wanderTimer>().wanderPoint;
      
    }
}
