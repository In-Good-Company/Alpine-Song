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

    public bool pressHeld;
    public float pressTimer;
    public float pressHeldThreshhold;
    public float lookSensitivity = 3.0f;

    void Start()
    {
        PlayerNav = GetComponent<NavMeshAgent>();
        destinationReached = false;
    }

   private void walkDistanceCheck()
    {
        // walking sound here
   distanceCheck = Vector3.Distance(transform.position, navMarker.transform.position);
        if(distanceCheck < 1)
        {
            destinationReached = true;
        }
        //This is currently commented out as the sound is being played in the TerrainMaterialManager as well.
        //AkSoundEngine.PostEvent("Footsteps", gameObject);

    }
    
    void Update()
    {
        

        if (Input.GetMouseButton(0))
        {
            pressTimer += Time.deltaTime;
            if (pressTimer >= pressHeldThreshhold)
            {
                pressHeld = true;
                if (markerPlaced == false || destinationReached == true)
                {
                    float rot = Input.GetAxis("Mouse X");
                    transform.Rotate(0, rot * lookSensitivity, 0);
                }
<<<<<<< Updated upstream
=======
                navMarker = Instantiate(navMarkerPrefab) as GameObject;
                navMarker.transform.position = navPoint;
                markerPlaced = true;
                destinationReached = false;
                //sound for clicking
               // AkSoundEngine.PostEvent("Location_Movement", gameObject);
>>>>>>> Stashed changes
            }
        
        }
        if (Input.GetMouseButtonUp(0))
        {
            if (pressHeld == false)
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
            pressHeld = false;
            pressTimer = 0;
        }

        //Currently commented out just in case
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;
        //
        //    if (EventSystem.current.IsPointerOverGameObject())
        //        return;
        //
        //    if (Physics.Raycast(clickRay, out hitInfo, 100, ClickableWorld))
        //    {
        //        Vector3 navPoint = hitInfo.point;
        //        PlayerNav.SetDestination(hitInfo.point);
        //        if (markerPlaced)
        //        {
        //            Destroy(navMarker);
        //        }
        //        navMarker = Instantiate(navMarkerPrefab) as GameObject;
        //        navMarker.transform.position = navPoint;
        //        markerPlaced = true;
        //        destinationReached = false;
        //
        //        AkSoundEngine.PostEvent("Location_Movement", gameObject);
        //    }
        //
        //}

        if (destinationReached == false && navMarker != null)
    {
        walkDistanceCheck();
            // if that fail, put here
    }
    
    }
}
