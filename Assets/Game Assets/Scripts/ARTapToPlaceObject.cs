using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
//using UnityEngine.Experimental.XR

using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    public GameObject placementIndicator;
    //private ARSessionOrigin arOrigin;
    private Pose PlacementPose; //data structure that describes the position and the rotation of a 3d point
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    void Start()
    {
        //arOrigin = FindObjectOfType<ARSessionOrigin>();
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        //Input.touchCount(Unity) if the user has any fingers on the screen
        //el phase el heya awel ma ydoos (began) we feh (ended) we btb2a b3d ma ysheel el finger 
        if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            PlaceObject();
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Debug.Log("Dost");
        //    PlaceObject();
        //}
    }

    private void PlaceObject()
    {
        Instantiate(objectToPlace, PlacementPose.position, PlacementPose.rotation);
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid) //lw feh points valid
        {
            placementIndicator.SetActive(true); //e3ml activate lel indicator
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    private void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));//tl3 el ray mn nos el screen (camera)
        var hits = new List<ARRaycastHit>();//list el 7agat el hy3ml collide m3aha
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes); //type el planes

        placementPoseIsValid = hits.Count > 0; //lw 3ml detect le planes  
        if (placementPoseIsValid)
        {
            PlacementPose = hits[0].pose; //el position bta3 el point elly el raycast 5bt feha
            var cameraForward = Camera.current.transform.forward; //el rotation blnsba lel camera 
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized; //mesh fahmha 5ales
            PlacementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }
}

