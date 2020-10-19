using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    public float closeCamPos_X;
    public float closeCamPos_Y;
    public float closeCamPos_Z;

    public float closeCamRot_X;
    private Vector3 closeCamPositioner;
    private Vector3 closeCamRot;

    public float farCamPos_X;
    public float farCamPos_Y;
    public float farCamPos_Z;

    public float lerpSpeed;

    public float farCamRot_X;
    private Vector3 farCamPositioner;
    private Vector3 farCamRot;

    public int camCase;

    public GameObject camera_Main;

    public Vector3 farLerpPos;
    public Vector3 farLerpRot;
    public Vector3 closeLerpPos;
    public Vector3 closeLerpRot;

    public Transform MyPos;
    void Start()
    {
        closeCamPos_X = 0.0f;
        closeCamPos_Y = 16.0f;
        closeCamPos_Z = -15.0f;
        closeCamRot_X = 40.0f;

        farCamPos_X = 0.0f;
        farCamPos_Y = 60.0f;
        farCamPos_Z = -21.0f;
        farCamRot_X = 60.0f;

        lerpSpeed = 0.01f;

       
        farCamRot = new Vector3(farCamRot_X, 0, 0);

        closeCamPositioner = new Vector3(0.0f, 16.0f, -15.0f);
        farCamPositioner = new Vector3(0.0f, 60.0f, -21.0f);

        camCase = 2;
        
        farLerpRot = Vector3.Lerp(closeCamRot, farCamRot, lerpSpeed * Time.deltaTime);
        
        closeLerpRot = Vector3.Lerp(farCamRot, closeCamRot, lerpSpeed * Time.deltaTime);
    }

    private void Awake()
    {
        MyPos = this.transform;


    }
    public void camSwitch(int camCase)
    {
        switch (camCase)
        {
            case 1:

                farLerpPos = Vector3.Lerp(MyPos.position, farCamPositioner, lerpSpeed * Time.deltaTime);
                //camera_Main.transform.localEulerAngles = Quaternion.Slerp(this.transform.rotation, farCamRot, lerpSpeed * Time.deltaTime); ;
                camera_Main.transform.position = farLerpPos;
                MyPos = this.transform;




                break;
            case 2:

                closeLerpPos = Vector3.Lerp(MyPos.position, closeCamPositioner, lerpSpeed * Time.deltaTime);
                camera_Main.transform.position = closeLerpPos;
                // camera_Main.transform.localEulerAngles = Vector3.Lerp(farCamPositioner, closeCamPositioner, lerpSpeed / Time.deltaTime);
                MyPos = this.transform;



                break;

        }

    }

   

    private void Update()
    {
        
        camSwitch(camCase);

       
    }
};
