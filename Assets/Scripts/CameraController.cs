using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;
    public float CameraMoveSpeed = 120.0f;
    public float lookSensitivity = 3.0f;
    public float lastMoveTime = 0.0f;
    public float camRecenterTime = 3.0f;
    public PlayerMove playerMove;

    private void Start()
    {
        lastMoveTime = 3.0f;
        if (playerMove == null)
        {
            playerMove = GameObject.FindObjectOfType<PlayerMove>();
        }
        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (playerMove.pressTimer >= playerMove.pressHeldThreshhold)
            {
                lastMoveTime = 0;
                float rot = Input.GetAxis("Mouse X");
                this.gameObject.transform.Rotate(0, rot * lookSensitivity, 0);
            }
        }
        CameraPosUpdate();
        if (lastMoveTime >= camRecenterTime)
        {
            this.gameObject.transform.rotation = Quaternion.Lerp(this.gameObject.transform.rotation, player.transform.rotation, 0.05f);
        }
        lastMoveTime += Time.deltaTime;
    }
    
    void CameraPosUpdate()
    {
        Transform target = player.transform;
    
        float step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    


    }

    void CameraAvoid()
    {

    }
}
