using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitchTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject camera;

    public bool triggerExit;
    public int camCase;

    private void Awake()
    {
        camera = GameObject.Find("Main Camera");
        camCase = camera.GetComponent<CameraShift>().camCase;
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            camCase = 2;
        }

    }

    private void OnTriggerExit(Collider col)
    {
        print("exit");
        if (col.gameObject.name == "Player" && triggerExit == true)
        {
            camCase = 1;
        }
    }
}
