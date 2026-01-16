using UnityEngine;

public class ManualTilt : MonoBehaviour
{
    [Header("Tilt Settings")]
    public float tiltSpeed = 15f;
    public float minTilt = -90f;   // Start at -90 (flat)
    public float maxTilt = 0f;     // End at 0 (standing up)
    
    private float currentXRotation;
    private bool isInitialized = false;

    void Start()
    {
        // Sync with the panel's ACTUAL starting rotation
        currentXRotation = transform.localEulerAngles.x;
        
        // Handle Unity's 270° = -90° conversion
        if (currentXRotation > 180f) {
            currentXRotation = currentXRotation - 360f;
        }
        
        isInitialized = true;
        Debug.Log($"ManualTilt initialized at: {currentXRotation}°");
    }

    void Update()
    {
        if (!isInitialized) return;
        
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                float deltaY = touch.deltaPosition.y;
                
                // Increase rotation (making it less negative = tilting up)
                currentXRotation += deltaY * tiltSpeed * Time.deltaTime;
                
                // Clamp between -90 (flat) and 0 (vertical)
                currentXRotation = Mathf.Clamp(currentXRotation, minTilt, maxTilt);
                
                // Apply the rotation
                transform.localRotation = Quaternion.Euler(
                    currentXRotation, 
                    transform.localRotation.eulerAngles.y, 
                    transform.localRotation.eulerAngles.z
                );
                
                Debug.Log($"Panel tilted to: {Mathf.Abs(currentXRotation + 90f)}° from horizontal");
            }
        }
    }
}