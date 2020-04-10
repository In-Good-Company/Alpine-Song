using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public LayerMask ClickableWorld;
    public GameObject navMarker;
    public GameObject navMarkerPrefab;

    private NavMeshAgent PlayerNav;
    public bool markerPlaced;
    public bool destinationReached;

    void Start()
    {
        PlayerNav = GetComponent<NavMeshAgent>();
        destinationReached = true;
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "navPoint")
        {
            destinationReached = true;
        }
    }
    
    void Update()
    {
        if (destinationReached == false)
        {
            //Add sound data here
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (Physics.Raycast(clickRay, out hitInfo, 100, ClickableWorld))
            {
                Vector3 navPoint = hitInfo.point;
                PlayerNav.SetDestination(hitInfo.point);
                if (markerPlaced)
                {
                    Destroy(navMarker);
                }
                navMarker = Instantiate(navMarkerPrefab) as GameObject;
                navMarker.transform.position = navPoint;
                markerPlaced = true;
                destinationReached = false;

                
            }
        }
    }
}
