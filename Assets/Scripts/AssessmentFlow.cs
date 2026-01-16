using UnityEngine;
using TMPro;

public class AssessmentFlow : MonoBehaviour
{
    [Header("AR Root (disable to hide AR completely)")]
    [SerializeField] private GameObject arRoot;

    [Header("UI Panels")]
    [SerializeField] private GameObject arQuestionPanel;
    [SerializeField] private GameObject quizPanel;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject certificatePanel;

    [Header("Controllers")]
    [SerializeField] private QuizController quizController;
    [SerializeField] private CertificateGenerator certificateGenerator;

    [Header("Result UI")]
    [SerializeField] private TextMeshProUGUI resultScoreText;
    [SerializeField] private TextMeshProUGUI resultMessageText;

    // Track scores
    private int arScore = 0;
    private int quizScore = 0;
    private int quizTotal = 0;

    void Start()
    {
        ShowARQuestion();
    }

    // Show AR Question (Question 1)
    public void ShowARQuestion()
    {
        if (arRoot != null) arRoot.SetActive(true);
        SetActivePanel(arQuestionPanel);
    }

    // Called when AR task is completed (pass true if user passed the tilt check)
    public void OnARTaskCompleted(bool passed)
    {
        arScore = passed ? 1 : 0;
        GoToQuiz();
    }

    // Transition to Quiz
    public void GoToQuiz()
    {
        // Hide AR completely
        if (arRoot != null) arRoot.SetActive(false);

        SetActivePanel(quizPanel);

        // Start quiz with 9 questions (Question 2 to Question 10)
        if (quizController != null)
        {
            quizController.StartQuiz(9, this);
        }
    }

    // Called by QuizController when quiz ends
    public void OnQuizCompleted(int score, int total)
    {
        quizScore = score;
        quizTotal = total;

        int finalScore = arScore + quizScore;
        int finalTotal = 1 + quizTotal; // AR question + quiz questions

        SetActivePanel(resultPanel);

        // Update result UI
        if (resultScoreText != null)
        {
            resultScoreText.text = $"{finalScore} / {finalTotal}";
        }

        if (resultMessageText != null)
        {
            float percentage = (float)finalScore / finalTotal * 100f;
            if (percentage >= 80f)
                resultMessageText.text = "Excellent! You have mastered solar panel installation.";
            else if (percentage >= 60f)
                resultMessageText.text = "Good job! Review the topics you missed.";
            else
                resultMessageText.text = "Keep practicing! Solar installation requires careful attention.";
        }

        // Prepare certificate data
        if (certificateGenerator != null)
        {
            certificateGenerator.SetScoreData(finalScore, finalTotal);
        }
    }

    // Called by Download Certificate button
    public void ShowCertificate()
    {
        SetActivePanel(certificatePanel);

        if (certificateGenerator != null)
        {
            certificateGenerator.GenerateCertificate();
        }
    }

    // Called by Download button on certificate panel
    public void DownloadCertificate()
    {
        if (certificateGenerator != null)
        {
            certificateGenerator.SaveCertificateImage();
        }
    }

    // Helper to show only one panel
    private void SetActivePanel(GameObject activePanel)
    {
        if (arQuestionPanel != null) arQuestionPanel.SetActive(arQuestionPanel == activePanel);
        if (quizPanel != null) quizPanel.SetActive(quizPanel == activePanel);
        if (resultPanel != null) resultPanel.SetActive(resultPanel == activePanel);
        if (certificatePanel != null) certificatePanel.SetActive(certificatePanel == activePanel);
    }
}