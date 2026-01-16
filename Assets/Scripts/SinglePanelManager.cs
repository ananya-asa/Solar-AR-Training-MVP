// using UnityEngine;
// using UnityEngine.XR.Interaction.Toolkit.AR;

// public class SinglePanelManager : MonoBehaviour
// {
//     private GameObject spawnedPanel;

//     // This logic handles professional placement: Move instead of Duplicate
//     public void HandlePlacement(ARObjectPlacementEventArgs args)
//     {
//         if (spawnedPanel == null)
//         {
//             // First tap: Store the panel that was just spawned
//             spawnedPanel = args.placementObject;
//             Debug.Log("✅ First panel placed");
//         }
//         else
//         {
//             // Second tap: Move the existing panel to the new location
//             spawnedPanel.transform.position = args.placementObject.transform.position;
//             spawnedPanel.transform.rotation = args.placementObject.transform.rotation;
            
//             Debug.Log("📍 Panel moved to new position");
            
//             // Immediately destroy the 'duplicate' that was just created
//             Destroy(args.placementObject);
//         }
//     }
// }




using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.AR;

public class SinglePanelManager : MonoBehaviour
{
    private GameObject spawnedPanel;
    
    [Header("Notify Tilt Reader (Optional)")]
    [SerializeField] private PanelTiltReader tiltReader;

    // This logic handles professional placement: Move instead of Duplicate
    public void HandlePlacement(ARObjectPlacementEventArgs args)
    {
        if (spawnedPanel == null)
        {
            // First tap: Store the panel that was just spawned
            spawnedPanel = args.placementObject;
            Debug.Log("✅ First panel placed");
            
            // Notify the tilt reader that panel is now available
            if (tiltReader != null)
            {
                tiltReader.SetPanel(spawnedPanel.transform);
            }
        }
        else
        {
            // Second tap: Move the existing panel to the new location
            spawnedPanel.transform.position = args.placementObject.transform.position;
            spawnedPanel.transform.rotation = args.placementObject.transform.rotation;
            
            Debug.Log("📍 Panel moved to new position");
            
            // Immediately destroy the 'duplicate' that was just created
            Destroy(args.placementObject);
        }
    }
}