using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OpenOverview() {
        SceneManager.LoadScene("ComponentOverview"); 
    }

    public void OpenInstallation() {
        SceneManager.LoadScene("Installation"); 
    }

    public void OpenARExperience() {
        // This goes to your intermediate instruction scene first
        SceneManager.LoadScene("intermediate"); 
    }

    public void OpenQuiz() {
        SceneManager.LoadScene("QuizScene"); 
    }
}