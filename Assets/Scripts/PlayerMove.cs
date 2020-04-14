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
    public float distanceCheck;

    void Start()
    {
        PlayerNav = GetComponent<NavMeshAgent>();
        destinationReached = false;
    }

   private void walkDistanceCheck()
    {
        
   distanceCheck = Vector3.Distance(transform.position, navMarker.transform.position);
        if(distanceCheck < 1)
        {
            destinationReached = true;
        }
        AkSoundEngine.PostEvent("Footsteps", gameObject);

    }
    
    void Update()
    {
        

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

                AkSoundEngine.PostEvent("Location_Movement", gameObject);
            }
        
        }

    if (destinationReached == false)
    {
        walkDistanceCheck();
    }
    
    }
}
