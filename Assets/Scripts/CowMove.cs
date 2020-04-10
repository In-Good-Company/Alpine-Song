using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class CowMove : MonoBehaviour
{
    public NavMeshAgent cowNav;
    public GameObject target;
    public Button callBtn;
    public bool isInRange;
    public bool cowFollow;


    private void Start()
    {
        Button call = callBtn.GetComponent<Button>();
        call.onClick.AddListener(cowCalled);
        isInRange = false;
        cowFollow = false;
    }

    private void cowCalled()
    {
        print("called");
        if(isInRange == true)
        {
            print("following");
            cowFollow = true;
        }
    }



    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("I collided");
        if (col.gameObject.name == "cowCallCol")
        {
            print("found you");
            isInRange = true;
            target = col.gameObject;
            Debug.Log("Target Aquired");
        }

    }

    private void OnTriggerStay(Collider col)
    {
        if (col.gameObject.name == ("Pen"))
        {
            print("Im in!");
            target = null;
            
        }
    }


    void Update()
    {
        if (target != null && cowFollow == true)
        {
            cowNav.SetDestination(target.transform.position);
        }
    }
}
