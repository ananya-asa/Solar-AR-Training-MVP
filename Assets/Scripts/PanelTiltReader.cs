// using UnityEngine;
// using TMPro;

// public class PanelTiltReader : MonoBehaviour
// {
//     public enum NormalAxis { Up, Forward, Right }

//     [Header("Find Panel Automatically (optional)")]
//     [SerializeField] private bool autoFindByTag = true;
//     [SerializeField] private string panelTag = "SolarPanel";

//     [Header("Panel Reference")]
//     [SerializeField] private Transform panelTransform;
//     [SerializeField] private NormalAxis panelNormalAxis = NormalAxis.Up;

//     [Header("UI")]
//     [SerializeField] private TMP_Text angleText;

//     public float CurrentTiltDeg { get; private set; }

//     void Update()
//     {
//         // Auto-find if not assigned
//         if (!panelTransform && autoFindByTag)
//         {
//             var go = GameObject.FindGameObjectWithTag(panelTag);
//             if (go) panelTransform = go.transform;
//         }

//         if (!panelTransform || !angleText) return;

//         Vector3 normal = GetAxis(panelTransform, panelNormalAxis);
//         CurrentTiltDeg = Vector3.Angle(normal, Vector3.up); // 0 = flat, 90 = vertical

//         angleText.text = $"Tilt: {CurrentTiltDeg:0.0}°";
//     }

//     Vector3 GetAxis(Transform t, NormalAxis axis)
//     {
//         return axis switch
//         {
//             NormalAxis.Up => t.up,
//             NormalAxis.Forward => t.forward,
//             NormalAxis.Right => t.right,
//             _ => t.up
//         };
//     }

//     // If you spawn the panel, call this.
//     public void SetPanel(Transform panel) => panelTransform = panel;
// }




using UnityEngine;
using TMPro;

public class PanelTiltReader : MonoBehaviour
{
    public enum NormalAxis { Up, Forward, Right }

    [Header("Find Panel Automatically (optional)")]
    [SerializeField] private bool autoFindByTag = true;
    [SerializeField] private string panelTag = "SolarPanel";

    [Header("Panel Reference")]
    [SerializeField] private Transform panelTransform;
    [SerializeField] private NormalAxis panelNormalAxis = NormalAxis.Up;

    [Header("UI")]
    [SerializeField] private TMP_Text angleText;

    public float CurrentTiltDeg { get; private set; }
    private bool panelFound = false;

    void Update()
    {
        // Auto-find if not assigned and not found yet
        if (!panelTransform && autoFindByTag && !panelFound)
        {
            var go = GameObject.FindGameObjectWithTag(panelTag);
            if (go != null)
            {
                panelTransform = go.transform;
                panelFound = true;
                Debug.Log($"Panel found: {go.name}");
            }
        }

        // Only calculate tilt if panel exists
        if (panelTransform != null)
        {
            // Get the panel's normal vector in world space
            Vector3 normal = GetAxis(panelTransform, panelNormalAxis);
            
            // Calculate angle from vertical (up)
            float angleFromVertical = Vector3.Angle(normal, Vector3.up);
            
            // Convert to tilt: 0° = flat, 90° = vertical
            CurrentTiltDeg = angleFromVertical;

            // Update UI if available
            if (angleText != null)
            {
                angleText.text = $"Tilt: {CurrentTiltDeg:0.0}°";
            }
        }
        else
        {
            // Panel not placed yet
            CurrentTiltDeg = 0f;
            if (angleText != null)
            {
                angleText.text = "Waiting for panel...";
            }
        }
    }

    Vector3 GetAxis(Transform t, NormalAxis axis)
    {
        return axis switch
        {
            NormalAxis.Up => t.up,
            NormalAxis.Forward => t.forward,
            NormalAxis.Right => t.right,
            _ => t.up
        };
    }

    // Call this from SinglePanelManager after spawning panel
    public void SetPanel(Transform panel)
    {
        panelTransform = panel;
        panelFound = true;
        Debug.Log($" Panel manually set: {panel.name}");
    }
}