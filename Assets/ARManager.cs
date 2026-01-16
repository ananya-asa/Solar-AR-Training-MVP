using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARManager : MonoBehaviour 
{
    public GameObject solarPanelPrefab;
    public ARRaycastManager raycastManager;
    
    public void PlacePanel() 
    {
        List<ARRaycastHit> hits = new List<ARRaycastHit>();
        Vector2 screenCenter = Camera.main.ViewportToScreenPoint(new Vector2(0.5f, 0.5f));
        
        if (raycastManager.Raycast(screenCenter, hits, TrackableType.Planes))
        {
            Pose pose = hits[0].pose;
            Instantiate(solarPanelPrefab, pose.position, pose.rotation);
        }
    }
}
