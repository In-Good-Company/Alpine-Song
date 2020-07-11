using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class house_exit : MonoBehaviour
{
   
   private void OnTriggerEnter(Collider col)
    {
        print("contact");
        if (col.gameObject.tag == "Player")
        {
            
            SceneManager.LoadScene("SampleScene");
        }
        
    }
}
