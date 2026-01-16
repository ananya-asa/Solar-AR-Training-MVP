using UnityEngine;
using UnityEngine.SceneManagement;

public class QuizzSceneLoader : MonoBehaviour
{
    // Load normal quiz
    public void LoadQuizScene()
    {
        SceneManager.LoadScene("QuizScene");
    }

    // Load AR-based assessment
    public void LoadARAssessmentScene()
    {
        SceneManager.LoadScene("ARAssessment");
    }

    // Load assessment intro / intermediator screen
    public void LoadAssessmentIntermediator()
    {
        SceneManager.LoadScene("AssessmentIntermediator");
    }
}
