using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Camera_IndoorSwitch : MonoBehaviour
{
    public GameObject camPos;
    public GameObject playerTransitionPos;
    public Camera mainCam;
    public GameObject Player;
    public NavMeshAgent navSwitch;


    private void Awake()
    {
        mainCam = Camera.main;
        Player = GameObject.FindGameObjectWithTag("Player");
        navSwitch = Player.GetComponentInChildren<NavMeshAgent>();
    }


}
