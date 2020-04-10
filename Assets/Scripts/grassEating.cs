using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grassEating : MonoBehaviour
{
    public float grassEaten;
    public int AI_CowTarget;
    private bool cowEating;
    
    void Start()
    {
        grassEaten = 20;
        cowEating = false;
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == ("Cow"))
        {
            cowEating = true;
            AI_CowTarget = 3;
        }
    }

    private void Update()
    {

        if(grassEaten <= 0)
        {
            cowEating = false;
            AI_CowTarget = 1;

            
            gameObject.SetActive(false);
            //print("Cow Done");
        }

        else if (cowEating == true && grassEaten >= 0)
        {
            grassEaten -= Time.deltaTime;
        }
    }
}
