using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerMove : MonoBehaviour
{
    public LayerMask ClickableWorld;
    public LayerMask Interactables;
    public GameObject navMarker;
    public GameObject navMarkerPrefab;
    public Camera cam;
    public GameObject cameraParent;

    private NavMeshAgent PlayerNav;
    public bool markerPlaced;
    public bool destinationReached;
    public float distanceCheck;

    public bool pressHeld;
    public float pressTimer;
    public float pressHeldThreshhold;
    public float lookSensitivity = 3.0f;

    public bool interactablePressed;

    void Start()
    {
        PlayerNav = GetComponent<NavMeshAgent>();
        destinationReached = false;
        cam = GetComponent<Camera>();
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
                float rot = Input.GetAxis("Mouse X");
                cameraParent.transform.Rotate(0, rot * lookSensitivity, 0);
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

                if (Physics.Raycast(clickRay, out hitInfo, 100, Interactables))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Interactable>() != null)
                    {
                        interactablePressed = true;
                        Interactable interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();
                        interactable.Interact();
                    }
                }

                if (Physics.Raycast(clickRay, out hitInfo, 100, ClickableWorld)&& interactablePressed == false)
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
            interactablePressed = false;
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

    private IEnumerator cameraRefocusTimer()
    {


        yield return null;
    }
}
