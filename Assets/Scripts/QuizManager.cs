// using UnityEngine;
// using UnityEngine.UI; // For Legacy Text and Buttons
// using TMPro;           // For Button Text (TMP)
// using UnityEngine.SceneManagement;

// public class QuizManager : MonoBehaviour
// {
//     [Header("UI References (Legacy Text)")]
//     public Text questionText;      
//     public Text feedbackText;      

//     [Header("UI References (TextMeshPro)")]
//     public Button[] answerButtons; 
//     public TextMeshProUGUI[] buttonTexts; 

//     [Header("Result Screen Buttons")]
//     public GameObject backButton;  // Drag your 'Go Back' button here
//     public GameObject homeButton;  // Drag your 'Home' button here

//     private int currentQuestion = 0;
//     private int score = 0;

//     private string[] questions = {
//         "Which component converts sunlight into DC electricity?",
//         "What causes 20-30% efficiency loss in panels?",
//         "What is the main purpose of the Solar Inverter?",
//         "What safety gear is required for installation?"
//     };

//     private string[][] options = {
//         new string[] {"Solar Panel", "Battery", "Inverter", "Cables"},
//         new string[] {"Using Gloves", "Wrong Tilt Angle", "Cleaning Panels", "Checking Meter"},
//         new string[] {"Store Power", "Support Panels", "Convert DC to AC", "Measure Sunlight"},
//         new string[] {"Normal Shoes", "Sunglasses", "Insulated Gloves", "No Gear Needed"}
//     };

//     private int[] correctAnswers = { 0, 1, 2, 2 };

//     void Start()
//     {
//         // Ensure result buttons are hidden at the start
//         if (backButton != null) backButton.SetActive(false);
//         if (homeButton != null) homeButton.SetActive(false);
        
//         if (feedbackText != null) feedbackText.text = "";
//         ShowQuestion();
//     }

//     void ShowQuestion()
//     {
//         questionText.text = questions[currentQuestion];
//         for (int i = 0; i < answerButtons.Length; i++)
//         {
//             buttonTexts[i].text = options[currentQuestion][i];
//             int index = i; 
//             answerButtons[i].onClick.RemoveAllListeners();
//             answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
//         }
//     }

//     void CheckAnswer(int index)
//     {
//         if (index == correctAnswers[currentQuestion])
//         {
//             score++;
//             if (feedbackText != null) 
//             {
//                 feedbackText.text = "CORRECT!";
//                 feedbackText.color = Color.green;
//             }
//         }
//         else
//         {
//             if (feedbackText != null) 
//             {
//                 feedbackText.text = "WRONG!";
//                 feedbackText.color = Color.red;
//             }
//         }
//         Invoke("NextQuestion", 1.2f);
//     }

//     void NextQuestion()
//     {
//         currentQuestion++;
//         if (currentQuestion < questions.Length)
//         {
//             if (feedbackText != null) feedbackText.text = "";
//             ShowQuestion();
//         }
//         else
//         {
//             // --- QUIZ FINISHED STATE ---
//             if (feedbackText != null) 
//             {
//                 feedbackText.text = "FINISHED!";
//                 feedbackText.color = new Color32(233, 30, 99, 255); // Magenta
//             }

//             questionText.text = "QUIZ COMPLETE!\n\nYour Final Score: " + score + " / " + questions.Length;
            
//             // Hide the 4 option buttons
//             foreach (var btn in answerButtons) 
//             {
//                 if(btn != null) btn.gameObject.SetActive(false);
//             }

//             // Show the final Result buttons
//             if(backButton != null) backButton.SetActive(true);
//             if(homeButton != null) homeButton.SetActive(true);
//         }
//     }

//     // BUTTON FUNCTIONS
//     public void RestartQuiz()
//     {
//         // Reloads the current scene to try the quiz again
//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }

//     public void GoHome()
//     {
//         // Goes back to your first scene (Intro)
//         SceneManager.LoadScene("Intro"); 
//     }
// }





using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [Header("UI References")]
    public Text questionText;
    public Text feedbackText;

    public Button[] answerButtons;
    public TextMeshProUGUI[] buttonTexts;

    [Header("Result Buttons")]
    public GameObject backButton;
    public GameObject homeButton;

    private int currentQuestion = 0;
    private int score = 0;

    // ---------------- QUESTIONS ----------------
    private string[] questions = {
        "Which component converts sunlight into DC electricity?",
        "Why does a wrong tilt angle reduce solar panel efficiency?",
        "What is the main purpose of a solar inverter?",
        "Why should solar panels face south in India?",
        "What happens if a solar panel is partially shaded?",
        "What tilt angle is roughly optimal for Mangalore/Udupi region?",
        "Why is ventilation important behind solar panels?",
        "What causes up to 20–30% energy loss in installations?",
        "Which wiring practice is the safest?",
        "Why are insulated gloves required during installation?",
        "What happens if panels are installed completely flat?",
        "Why should panels be cleaned regularly?",
        "What does DC stand for in solar systems?",
        "Why is proper earthing important in solar setups?",
        "What is the biggest risk of poor solar installation?"
    };

    // ---------------- OPTIONS ----------------
    private string[][] options = {
        new string[] {"Solar Panel", "Battery", "Inverter", "Cables"},
        new string[] {"Less Sun Exposure", "More Wind", "Higher Voltage", "Better Cooling"},
        new string[] {"Store Energy", "Convert DC to AC", "Clean Power", "Measure Sunlight"},
        new string[] {"Looks Better", "Gets Maximum Annual Sun", "Avoids Rain", "Reduces Heat"},
        new string[] {"No Effect", "Voltage Increases", "Energy Output Drops", "Panel Stops Working"},
        new string[] {"5°", "13°", "30°", "45°"},
        new string[] {"For Appearance", "To Avoid Birds", "Cooling Improves Efficiency", "To Reduce Weight"},
        new string[] {"Wrong Tilt & Direction", "Good Wiring", "Cleaning Panels", "Quality Inverter"},
        new string[] {"Loose Connections", "Exposed Copper", "Proper Insulation", "Twisted Wires"},
        new string[] {"Comfort", "Electrical Safety", "Grip", "Style"},
        new string[] {"More Power", "Energy Loss", "Water Storage", "No Effect"},
        new string[] {"Looks Clean", "Increase Voltage", "Avoid Dirt & Shading Loss", "Reduce Wind"},
        new string[] {"Direct Current", "Dynamic Current", "Dual Charge", "Distributed Current"},
        new string[] {"Increase Power", "Avoid Shock & Fire", "Reduce Cost", "Improve Efficiency"},
        new string[] {"Lower Efficiency", "Fire Hazard", "Equipment Damage", "All of the Above"}
    };

    // ---------------- CORRECT ANSWERS ----------------
    private int[] correctAnswers = {
        0, 0, 1, 1, 2, 1, 2, 0, 2, 1, 1, 2, 0, 1, 3
    };

    // ---------------- EXPLANATIONS ----------------
    private string[] correctExplanations = {
        "Solar panels directly convert sunlight into DC electricity using photovoltaic cells.",
        "Incorrect tilt reduces the amount of sunlight hitting the panel.",
        "Inverters convert DC electricity into AC usable in homes.",
        "South-facing panels receive the most sunlight annually in India.",
        "Even partial shading significantly reduces total energy output.",
        "A tilt close to local latitude (~13°) gives best yearly performance.",
        "Proper airflow prevents overheating and improves efficiency.",
        "Wrong tilt and direction can reduce output by up to 30%.",
        "Proper insulation prevents electrical shock and fire hazards.",
        "Insulated gloves protect installers from electric shock.",
        "Flat panels fail to capture optimal sunlight throughout the day.",
        "Dust and dirt block sunlight and reduce power generation.",
        "DC means Direct Current, produced directly by solar panels.",
        "Proper earthing prevents shock, fire, and system damage.",
        "Poor installation causes efficiency loss, safety risks, and failures."
    };

    private string[] wrongExplanations = {
        "Batteries and inverters do not generate electricity.",
        "Voltage and wind do not improve solar efficiency.",
        "Without an inverter, solar power cannot be used in homes.",
        "Orientation affects energy, not appearance.",
        "Shading reduces current flow across the panel.",
        "Extreme angles reduce yearly energy yield.",
        "Overheated panels lose efficiency and lifespan.",
        "Good wiring alone cannot fix wrong orientation.",
        "Unsafe wiring increases shock and fire risk.",
        "Gloves are for safety, not comfort.",
        "Flat panels lose optimal sun exposure.",
        "Dirty panels reduce energy production.",
        "DC is the base electrical output of panels.",
        "Earthing is essential for safety, not power.",
        "All listed risks are serious installation failures."
    };

    // ---------------- UNITY METHODS ----------------
    void Start()
    {
        feedbackText.text = "";

        if (backButton != null) backButton.SetActive(false);
        if (homeButton != null) homeButton.SetActive(false);

        ShowQuestion();
    }

    void ShowQuestion()
    {
        questionText.text = questions[currentQuestion];

        for (int i = 0; i < answerButtons.Length; i++)
        {
            buttonTexts[i].text = options[currentQuestion][i];
            int index = i;

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
            answerButtons[i].interactable = true;
        }
    }

    void CheckAnswer(int index)
    {
        foreach (var btn in answerButtons)
            btn.interactable = false;

        if (index == correctAnswers[currentQuestion])
        {
            score++;
            feedbackText.text =
                "✅ Amazing! You’re correct.\n\n" + correctExplanations[currentQuestion];
            feedbackText.color = Color.green;
        }
        else
        {
            feedbackText.text =
                "❌ Oops! That’s incorrect.\n\n" + wrongExplanations[currentQuestion];
            feedbackText.color = Color.red;
        }

        Invoke(nameof(NextQuestion), 2.8f);
    }

    void NextQuestion()
    {
        currentQuestion++;

        if (currentQuestion < questions.Length)
        {
            feedbackText.text = "";
            ShowQuestion();
        }
        else
        {
            FinishQuiz();
        }
    }

    void FinishQuiz()
    {
        questionText.text =
            "QUIZ COMPLETE!\n\nScore: " + score + " / " + questions.Length;

        feedbackText.text =
            score >= 10
            ? "🎉 Great job! You have strong solar installation knowledge."
            : "📘 Good attempt! Review the concepts and try again.";

        feedbackText.color = Color.cyan;

        foreach (var btn in answerButtons)
            btn.gameObject.SetActive(false);

        if (backButton != null) backButton.SetActive(true);
        if (homeButton != null) homeButton.SetActive(true);
    }

    // ---------------- BUTTON FUNCTIONS ----------------
    public void RestartQuiz()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        SceneManager.LoadScene("Intro");
    }
}
