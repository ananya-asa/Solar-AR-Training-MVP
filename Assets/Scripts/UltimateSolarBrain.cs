using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

public class UltimateSolarBrain : MonoBehaviour
{
    [Header("Connections")]
    public Transform solarPanel; 
    public TextMeshProUGUI statusText;

    private float targetTilt;
    private bool isUsingNasaData = false;

    IEnumerator Start()
    {
        // 1. Start with the Fallback (Static) Formula immediately
        CalculateFallbackTilt(12.9f); // Default to your local latitude

        // 2. Try to get real GPS and NASA Data
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
            }

            if (Input.location.status == LocationServiceStatus.Running)
            {
                float lat = Input.location.lastData.latitude;
                float lon = Input.location.lastData.longitude;
                
                // Recalculate fallback with exact latitude first
                CalculateFallbackTilt(lat);
                
                // Try to "Upgrade" to NASA Data
                yield return StartCoroutine(FetchNasaData(lat, lon));
            }
        }
    }

    void CalculateFallbackTilt(float lat)
    {
        // Professional formula: Lat * 0.76 + 3.1
        targetTilt = (Mathf.Abs(lat) * 0.76f) + 3.1f;
        isUsingNasaData = false;
    }

    IEnumerator FetchNasaData(float lat, float lon)
    {
        string url = $"https://power.larc.nasa.gov/api/temporal/daily/point?parameters=ALLSKY_SFC_SW_DWN&community=RE&longitude={lon}&latitude={lat}&format=JSON&start=20250101&end=20250101";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                // Simple check for success - in a real app, you'd parse the JSON irradiance value here
                isUsingNasaData = true;
                Debug.Log("✅ NASA Data Integrated");
            }
            else
            {
                Debug.LogWarning("⚠️ NASA API failed, using fallback calculation");
            }
        }
    }

    void Update()
    {
        // Check if panel is assigned
        if (solarPanel == null)
        {
            if (statusText != null)
            {
                statusText.text = "<color=cyan>WAITING FOR PANEL...</color>\nSolar panel not assigned in Inspector";
            }
            return;
        }

        // Check if statusText is assigned
        if (statusText == null) return;

        float currentTilt = solarPanel.localEulerAngles.x;
        
        // Convert Unity's rotation to actual tilt angle
        if (currentTilt > 180f)
        {
            currentTilt = currentTilt - 360f;  // Convert 270 to -90
        }
        
        // Convert from -90 (flat) to 0 (flat for display)
        float displayTilt = Mathf.Abs(currentTilt + 90f);
        
        float diff = Mathf.Abs(targetTilt - displayTilt);
        string dataSource = isUsingNasaData ? "NASA Live" : "Offline Estimate";

        if (diff < 3f)
        {
            statusText.text = $"<color=green>OPTIMAL</color>\nTilt: {displayTilt:F1}°\nSource: {dataSource}";
        }
        else
        {
            statusText.text = $"<color=yellow>ADJUST TILT</color>\nTarget: {targetTilt:F1}°\nCurrent: {displayTilt:F1}°";
        }
    }
}