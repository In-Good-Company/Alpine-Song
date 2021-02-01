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
    public GameObject interactionPoint;
    public Interactable interactTarget;

    private NavMeshAgent PlayerNav;
    public bool markerPlaced;
    public bool destinationReached;
    public float distanceCheck;

    public bool pressHeld;
    public float pressTimer;
    public float pressHeldThreshhold = 0.1f;
    public float lookSensitivity = 3.0f;

    public bool interactablePressed;
    public bool waitingToActivate;

    void Start()
    {
        if (pressHeldThreshhold == 0)
        {
            pressHeldThreshhold = 0.1f;
        }
        PlayerNav = GetComponent<NavMeshAgent>();
        destinationReached = false;
        //cam = GetComponent<Camera>();
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
    
   // private void lookAround()
   // {
   //     //if (Input.touchCount == 2)
   //     if(Input.GetMouseButton(0))
   //     {
   //         
   //         pressTimer += Time.deltaTime;
   //         if (pressTimer >= pressHeldThreshhold)
   //         {
   //             pressHeld = true;
   //         }
   //
   //     }
   // }

    void Update()
    {
        if (waitingToActivate && interactTarget != null)
        {
            if (destinationReached == true)
            {
                interactTarget.Interact();
                interactTarget = null;
                waitingToActivate = false;
            }
        }

        
        if (Input.GetMouseButtonUp(0))
        {
           
            if (pressHeld == false)
            {
                Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;

                //check if click is over a UI element or not to prevent clicking through
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

              // if (EventSystem.current.IsPointerOverGameObject())
              //     return;

                if (Physics.Raycast(clickRay, out hitInfo, 100, Interactables))
                {
                    if (hitInfo.collider.gameObject.GetComponent<Interactable>() != null)
                    {

                        

                        Debug.Log("interactable found");
                        waitingToActivate = true;
                        interactablePressed = true;
                        interactTarget = hitInfo.collider.gameObject.GetComponent<Interactable>();
                        Interactable interactable = hitInfo.collider.gameObject.GetComponent<Interactable>();
                        Vector3 navPoint = interactable.playerInteractPos.transform.position;
                        interactionPoint = interactable.playerInteractPos;
                        //interactionPoint.GetComponent<SphereCollider>().enabled = true;
                        PlayerNav.SetDestination(interactable.playerInteractPos.transform.position);
                        if (markerPlaced)
                        {
                            Destroy(navMarker);
                        }
                        navMarker = Instantiate(navMarkerPrefab) as GameObject;
                        navMarker.transform.position = navPoint;
                        markerPlaced = true;
                        destinationReached = false;

                        //AkSoundEngine.PostEvent("Location_Movement", gameObject);

                        
                  
                    }
                }

                if (Physics.Raycast(clickRay, out hitInfo, 100, ClickableWorld)&& interactablePressed == false)
                {
                    Debug.Log("world position found");
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

                    //AkSoundEngine.PostEvent("Location_Movement", gameObject);
                }
            }
            interactablePressed = false;
            pressHeld = false;
            pressTimer = 0;
        }

        //lookAround();
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
