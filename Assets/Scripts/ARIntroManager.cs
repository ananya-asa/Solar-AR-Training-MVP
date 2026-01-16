using UnityEngine;
using UnityEngine.SceneManagement;

public class ARIntroManager : MonoBehaviour
{
    public void LoadARScene()
    {
        // Ensure this matches your AR scene name exactly
        SceneManager.LoadScene("ARPlacementFinal"); 
    }

    public void GoBackToInstallation()
    {
        SceneManager.LoadScene("Installation");
    }
}