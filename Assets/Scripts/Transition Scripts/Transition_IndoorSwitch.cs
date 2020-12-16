using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Transition_IndoorSwitch : Camera_IndoorSwitch
{
    public bool sceneChange;
    public string Scene;
  
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == Player)
        {
            if (sceneChange == true)
            {
                SceneManager.LoadScene(Scene);
            }
            navSwitch.enabled = false;
            Player.transform.position = playerTransitionPos.transform.position;
            mainCam.transform.position = camPos.transform.position;

            navSwitch.enabled = true;

           
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == Player)
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
