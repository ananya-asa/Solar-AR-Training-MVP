using System;
using System.IO;
using System.Collections;
using UnityEngine;
using TMPro;

public class CertificateGenerator : MonoBehaviour
{
    [Header("Certificate UI Elements")]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI subtitleText;
    [SerializeField] private TextMeshProUGUI dateTimeText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI statusText;

    private int finalScore;
    private int finalTotal;

    public void SetScoreData(int score, int total)
    {
        finalScore = score;
        finalTotal = total;
    }

    public void GenerateCertificate()
    {
        if (titleText != null)
        {
            titleText.text = "SOLAR AR TRAINING";
        }

        if (subtitleText != null)
        {
            subtitleText.text = "Certificate of Completion";
        }

        if (dateTimeText != null)
        {
            dateTimeText.text = DateTime.Now.ToString("dd MMMM yyyy, hh:mm tt");
        }

        if (scoreText != null)
        {
            scoreText.text = $"Score: {finalScore} / {finalTotal}";
        }

        if (statusText != null)
        {
            float percentage = (float)finalScore / finalTotal * 100f;
            if (percentage >= 80f)
                statusText.text = "STATUS: PASSED WITH DISTINCTION";
            else if (percentage >= 50f)
                statusText.text = "STATUS: PASSED";
            else
                statusText.text = "STATUS: NEEDS IMPROVEMENT";
        }
    }

    public void SaveCertificateImage()
    {
        StartCoroutine(CaptureAndSave());
    }

    private IEnumerator CaptureAndSave()
    {
        yield return new WaitForEndOfFrame();

        // Capture screenshot
        Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        texture.Apply();

        // Generate filename
        string fileName = $"SolarAR_Certificate_{DateTime.Now:yyyyMMdd_HHmmss}.png";

        #if UNITY_ANDROID
        // Save to Gallery on Android
        SaveToGallery(texture, fileName);
        #else
        // Fallback: Save to persistent data path
        SaveToFile(texture, fileName);
        #endif

        Destroy(texture);
    }

    private void SaveToFile(Texture2D texture, string fileName)
    {
        byte[] pngData = texture.EncodeToPNG();
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllBytes(filePath, pngData);
            Debug.Log($"✅ Certificate saved to: {filePath}");

            if (statusText != null)
            {
                statusText.text = $"✅ Saved!\n{filePath}";
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ Save failed: {e.Message}");
            if (statusText != null)
            {
                statusText.text = "❌ Save failed. Check permissions.";
            }
        }
    }

    #if UNITY_ANDROID
    private void SaveToGallery(Texture2D texture, string fileName)
    {
        try
        {
            byte[] pngData = texture.EncodeToPNG();

            // Use Android MediaStore to save to gallery
            using (AndroidJavaClass environment = new AndroidJavaClass("android.os.Environment"))
            using (AndroidJavaObject picturesDir = environment.CallStatic<AndroidJavaObject>("getExternalStoragePublicDirectory", 
                environment.GetStatic<string>("DIRECTORY_PICTURES")))
            {
                string dirPath = picturesDir.Call<string>("getAbsolutePath");
                string filePath = Path.Combine(dirPath, fileName);

                File.WriteAllBytes(filePath, pngData);

                // Notify Android to scan the file
                using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                using (AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                using (AndroidJavaClass mediaScanner = new AndroidJavaClass("android.media.MediaScannerConnection"))
                {
                    mediaScanner.CallStatic("scanFile", currentActivity, 
                        new string[] { filePath }, null, null);
                }

                Debug.Log($"✅ Certificate saved to Gallery: {filePath}");

                if (statusText != null)
                {
                    statusText.text = "✅ Saved to Gallery!";
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"❌ Gallery save failed: {e.Message}");
            
            // Fallback to regular save
            SaveToFile(texture, fileName);
        }
    }
    #endif
}