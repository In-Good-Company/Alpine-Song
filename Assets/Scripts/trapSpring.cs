using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trapSpring : MonoBehaviour
{
    public int AI_CowTarget;

    void Start()
    {
        AI_CowTarget = 4;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == ("Cow"))
        {
            print("trapped!");
            AI_CowTarget = 4;
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == ("Cow"))
        {
            gameObject.SetActive(false);
        }
    }
    void Update()
    {
        
    }
}
