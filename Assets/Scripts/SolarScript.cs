using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using Unity.XR.CoreUtils;
using UnityEngine.SceneManagement; // Added for scene switching

public class SolarScript : MonoBehaviour
{
    // Solar panel prefab
    public GameObject SpawnableFurniture;

    // AR references
    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private List<ARRaycastHit> raycastHits = new List<ARRaycastHit>();
    private GameObject spawnedSolar;

    // ===== BUTTON METHOD (Linked to your UI Button OnClick) =====
    public void PlaceSolar()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);

        bool collision = raycastManager.Raycast(
            screenCenter,
            raycastHits,
            TrackableType.PlaneWithinPolygon
        );

        if (collision)
        {
            if (spawnedSolar == null)
            {
                spawnedSolar = Instantiate(
                    SpawnableFurniture,
                    raycastHits[0].pose.position,
                    raycastHits[0].pose.rotation
                );
            }
            else
            {
                spawnedSolar.transform.position = raycastHits[0].pose.position;
                spawnedSolar.transform.rotation = raycastHits[0].pose.rotation;
            }
        }
    }

    // ===== NAVIGATION METHODS (Scene Switching) =====

    public void GoBackToInstallation()
    {
        // Make sure "Installation" matches your scene name exactly
        SceneManager.LoadScene("Installation"); 
    }

    public void StartQuiz()
    {
        // Make sure "QuizScene" matches your scene name exactly
        SceneManager.LoadScene("QuizScene"); 
    }
}