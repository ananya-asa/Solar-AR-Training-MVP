// using UnityEngine;
// using UnityEngine.UI;
// using TMPro;

// public class TiltCheckController : MonoBehaviour
// {
//     [Header("References")]
//     [SerializeField] private PanelTiltReader tiltReader;

//     [Header("Tilt Rule")]
//     [SerializeField] private float targetTilt = 15f;
//     [SerializeField] private float tolerance = 2f;

//     [Header("UI")]
//     [SerializeField] private TMP_Text feedbackText;
//     [SerializeField] private Button nextButton;

//     void Awake()
//     {
//         if (nextButton) nextButton.interactable = false;
//         if (feedbackText) feedbackText.text = "";
//     }

//     public void CheckAngle()
//     {
//         if (!tiltReader)
//         {
//             if (feedbackText) feedbackText.text = "TiltReader not set.";
//             return;
//         }

//         float a = tiltReader.CurrentTiltDeg;
//         bool pass = Mathf.Abs(a - targetTilt) <= tolerance;

//         if (pass)
//         {
//             if (feedbackText) feedbackText.text = $"OK ✓ Tilt {a:0.0}° (Target {targetTilt:0}°)";
//             if (nextButton) nextButton.interactable = true;
//         }
//         else
//         {
//             if (feedbackText) feedbackText.text = $"Adjust tilt to {targetTilt:0}° ± {tolerance:0}°. Current: {a:0.0}°";
//             if (nextButton) nextButton.interactable = false;
//         }
//     }
// }


using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TiltCheckController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PanelTiltReader tiltReader;

    [Header("Tilt Rule")]
    [SerializeField] private float targetTilt = 15f;
    [SerializeField] private float tolerance = 2f;

    [Header("UI")]
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private Button nextButton;

    void Awake()
    {
        if (nextButton) nextButton.interactable = false;
        if (feedbackText) feedbackText.text = "";
    }

    public void CheckAngle()
    {
        if (!tiltReader)
        {
            if (feedbackText) feedbackText.text = " TiltReader not set.";
            return;
        }

        // Check if panel has been placed
        if (tiltReader.CurrentTiltDeg == 0f)
        {
            if (feedbackText) feedbackText.text = " Please place the solar panel first by tapping on the ground.";
            if (nextButton) nextButton.interactable = false;
            return;
        }

        float a = tiltReader.CurrentTiltDeg;
        bool pass = Mathf.Abs(a - targetTilt) <= tolerance;

        if (pass)
        {
            if (feedbackText) feedbackText.text = $" OPTIMAL!\nTilt: {a:0.0}° (Target: {targetTilt:0}°)";
            if (nextButton) nextButton.interactable = true;
        }
        else
        {
            if (feedbackText) feedbackText.text = $" Adjust tilt to {targetTilt:0}° ± {tolerance:0}°\nCurrent: {a:0.0}°";
            if (nextButton) nextButton.interactable = false;
        }
    }
}