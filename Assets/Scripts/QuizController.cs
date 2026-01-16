using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI questionNumberText;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Image questionImage;
    [SerializeField] private Button[] answerButtons;
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private Button submitButton;
    [SerializeField] private TextMeshProUGUI submitButtonText;

    // Runtime variables
    private AssessmentFlow assessmentFlow;
    private List<QuestionData> activeQuestions = new List<QuestionData>();
    private int currentIndex = 0;
    private int score = 0;
    private int selectedAnswer = -1;
    private bool hasAnswered = false;

    // Question data structure
    private class QuestionData
    {
        public string question;
        public string[] options;
        public int correctIndex;
        public string explanation;
    }

    // All available questions (logic/reasoning type for solar AR training)
    private List<QuestionData> questionPool = new List<QuestionData>()
    {
        new QuestionData()
        {
            question = "If a solar panel's efficiency is 20%, what happens to the remaining 80% of sunlight energy?",
            options = new string[] {
                "Stored in batteries for later",
                "Converted to heat and reflected away",
                "Sent back to the power grid",
                "Absorbed by the mounting frame"
            },
            correctIndex = 1,
            explanation = "Most unconverted energy becomes heat or is reflected."
        },
        new QuestionData()
        {
            question = "What would happen if you connect a solar panel directly to a battery without a charge controller?",
            options = new string[] {
                "The system works normally",
                "Battery charges faster and more efficiently",
                "Battery may overcharge, overheat, and get damaged",
                "Solar panel output doubles automatically"
            },
            correctIndex = 2,
            explanation = "Without regulation, batteries can overcharge and become hazardous."
        },
        new QuestionData()
        {
            question = "Why do solar panels generate less power on extremely hot days despite more sunlight?",
            options = new string[] {
                "The glass becomes opaque in heat",
                "High temperature increases electrical resistance and reduces voltage",
                "Sunlight contains less energy in summer",
                "The inverter automatically limits output"
            },
            correctIndex = 1,
            explanation = "Heat increases resistance in solar cells, reducing efficiency."
        },
        new QuestionData()
        {
            question = "If you connect two 12V solar panels in series, what is the resulting voltage?",
            options = new string[] {
                "6V (voltage divides)",
                "12V (stays the same)",
                "24V (voltage adds up)",
                "36V (voltage multiplies)"
            },
            correctIndex = 2,
            explanation = "Series connection adds voltages: 12V + 12V = 24V."
        },
        new QuestionData()
        {
            question = "A bird's shadow falls on one cell of a solar panel. What is the likely effect?",
            options = new string[] {
                "Only that one cell stops working",
                "The entire panel output drops significantly",
                "No effect, other cells compensate",
                "The panel automatically adjusts its angle"
            },
            correctIndex = 1,
            explanation = "Cells are connected in series; one shaded cell can bottleneck the entire string."
        },
        new QuestionData()
        {
            question = "Why is a south-facing orientation recommended for solar panels in India?",
            options = new string[] {
                "South wind keeps panels cool",
                "The sun travels across the southern sky in the Northern Hemisphere",
                "Southern direction has less dust",
                "Government regulations require it"
            },
            correctIndex = 1,
            explanation = "In the Northern Hemisphere, the sun's path is predominantly in the southern sky."
        },
        new QuestionData()
        {
            question = "What is the main advantage of an MPPT charge controller over a PWM controller?",
            options = new string[] {
                "MPPT is cheaper to manufacture",
                "MPPT tracks optimal voltage/current for maximum power extraction",
                "MPPT eliminates the need for batteries",
                "MPPT allows panels to generate power at night"
            },
            correctIndex = 1,
            explanation = "MPPT continuously adjusts to extract maximum available power."
        },
        new QuestionData()
        {
            question = "If a 300W panel receives only 50% sunlight due to clouds, approximately how much power will it generate?",
            options = new string[] {
                "300W (panels compensate automatically)",
                "Around 150W or less",
                "600W (clouds diffuse light, increasing absorption)",
                "0W (panels need direct sunlight)"
            },
            correctIndex = 1,
            explanation = "Output is roughly proportional to available irradiance."
        },
        new QuestionData()
        {
            question = "Why must the voltage rating of solar panels match the inverter's input specifications?",
            options = new string[] {
                "For aesthetic uniformity in the installation",
                "Mismatched voltage can damage equipment or cause shutdown",
                "It only matters for the warranty paperwork",
                "Voltage matching is optional for small systems"
            },
            correctIndex = 1,
            explanation = "Inverters have specific input voltage ranges; exceeding them causes damage."
        },
        new QuestionData()
        {
            question = "Why is proper earthing/grounding critical in a solar installation?",
            options = new string[] {
                "It increases panel efficiency by 10%",
                "It protects against electric shock and lightning damage",
                "It is only required for off-grid systems",
                "It helps panels track the sun better"
            },
            correctIndex = 1,
            explanation = "Grounding provides a safe path for fault currents and lightning."
        },
        new QuestionData()
        {
            question = "What is the primary function of bypass diodes in a solar panel?",
            options = new string[] {
                "To increase voltage output",
                "To prevent reverse current flow at night",
                "To allow current to bypass shaded or damaged cells",
                "To convert DC to AC power"
            },
            correctIndex = 2,
            explanation = "Bypass diodes prevent shaded cells from becoming bottlenecks."
        },
        new QuestionData()
        {
            question = "If a solar system is producing power but the battery isn't charging, what is the most likely cause?",
            options = new string[] {
                "The sun is too bright",
                "Faulty or disconnected charge controller",
                "The inverter is working correctly",
                "Panels are facing the wrong direction"
            },
            correctIndex = 1,
            explanation = "The charge controller regulates power flow to the battery."
        },
        new QuestionData()
        {
            question = "Why should there be an air gap between solar panels and the roof surface?",
            options = new string[] {
                "To allow rainwater to wash the panels",
                "To enable air circulation for cooling and better efficiency",
                "To make installation easier",
                "To prevent birds from nesting"
            },
            correctIndex = 1,
            explanation = "Airflow keeps panels cooler, improving performance."
        },
        new QuestionData()
        {
            question = "What happens to solar panel output as the panel ages over 20-25 years?",
            options = new string[] {
                "Output increases due to material settling",
                "Output decreases gradually (typically 0.5-1% per year)",
                "Output stays exactly the same",
                "Output fluctuates randomly"
            },
            correctIndex = 1,
            explanation = "Degradation is normal; quality panels retain 80%+ output after 25 years."
        },
        new QuestionData()
        {
            question = "Why is it dangerous to work on a solar system during daylight without proper disconnection?",
            options = new string[] {
                "Panels are fragile and break easily",
                "Panels generate voltage whenever light hits them",
                "The inverter produces harmful radiation",
                "Batteries may explode from heat"
            },
            correctIndex = 1,
            explanation = "Solar panels cannot be turned off; they produce power whenever illuminated."
        }
    };

    void Awake()
    {
        // Wire up answer button clicks
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i;
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }

        if (submitButton != null)
        {
            submitButton.onClick.AddListener(OnSubmitClicked);
        }
    }

    // Called by AssessmentFlow to start the quiz
    public void StartQuiz(int numberOfQuestions, AssessmentFlow flow)
    {
        assessmentFlow = flow;
        currentIndex = 0;
        score = 0;
        selectedAnswer = -1;
        hasAnswered = false;

        // Shuffle and pick questions
        PrepareQuestions(numberOfQuestions);

        // Show first question
        DisplayCurrentQuestion();
    }

    private void PrepareQuestions(int count)
    {
        activeQuestions.Clear();

        // Create shuffled copy of pool
        List<QuestionData> shuffled = new List<QuestionData>(questionPool);
        for (int i = shuffled.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var temp = shuffled[i];
            shuffled[i] = shuffled[j];
            shuffled[j] = temp;
        }

        // Take required number
        int take = Mathf.Min(count, shuffled.Count);
        for (int i = 0; i < take; i++)
        {
            activeQuestions.Add(shuffled[i]);
        }
    }

    private void DisplayCurrentQuestion()
    {
        if (currentIndex >= activeQuestions.Count)
        {
            EndQuiz();
            return;
        }

        hasAnswered = false;
        selectedAnswer = -1;

        var q = activeQuestions[currentIndex];

        // Update question number (Question 2, 3, etc. since Q1 is AR)
        if (questionNumberText != null)
        {
            questionNumberText.text = $"Question {currentIndex + 2} of 10";
        }

        // Update question text
        if (questionText != null)
        {
            questionText.text = q.question;
        }

        // Hide image for text-based questions (or show if you have images)
        if (questionImage != null)
        {
            questionImage.gameObject.SetActive(false);
        }

        // Clear feedback
        if (feedbackText != null)
        {
            feedbackText.text = "";
        }

        // Setup answer buttons
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < q.options.Length)
            {
                answerButtons[i].gameObject.SetActive(true);
                answerButtons[i].interactable = true;

                // Reset button color
                var colors = answerButtons[i].colors;
                colors.normalColor = Color.white;
                answerButtons[i].colors = colors;
                answerButtons[i].image.color = Color.white;

                // Set answer text
                var txt = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                if (txt != null)
                {
                    txt.text = q.options[i];
                }
            }
            else
            {
                answerButtons[i].gameObject.SetActive(false);
            }
        }

        // Reset submit button
        if (submitButtonText != null)
        {
            submitButtonText.text = "Submit";
        }

        if (submitButton != null)
        {
            submitButton.interactable = true;
        }
    }

    private void OnAnswerSelected(int index)
    {
        if (hasAnswered) return;

        selectedAnswer = index;

        // Highlight selected answer
        for (int i = 0; i < answerButtons.Length; i++)
        {
            if (i < activeQuestions[currentIndex].options.Length)
            {
                answerButtons[i].image.color = (i == selectedAnswer) 
                    ? new Color(0.7f, 0.85f, 1f) 
                    : Color.white;
            }
        }
    }

    private void OnSubmitClicked()
    {
        // If already answered, go to next question
        if (hasAnswered)
        {
            currentIndex++;
            DisplayCurrentQuestion();
            return;
        }

        // Check if answer selected
        if (selectedAnswer < 0)
        {
            if (feedbackText != null)
            {
                feedbackText.text = "<color=#FFD700>Please select an answer first.</color>";
            }
            return;
        }

        // Lock in answer
        hasAnswered = true;

        // Disable all answer buttons
        foreach (var btn in answerButtons)
        {
            btn.interactable = false;
        }

        var q = activeQuestions[currentIndex];

        // Check answer and show feedback
        if (selectedAnswer == q.correctIndex)
        {
            score++;
            answerButtons[selectedAnswer].image.color = new Color(0.5f, 1f, 0.5f); // Green
            if (feedbackText != null)
            {
                feedbackText.text = $"<color=#00FF00>Correct!</color>\n{q.explanation}";
            }
        }
        else
        {
            answerButtons[selectedAnswer].image.color = new Color(1f, 0.5f, 0.5f); // Red
            if (q.correctIndex >= 0 && q.correctIndex < answerButtons.Length)
            {
                answerButtons[q.correctIndex].image.color = new Color(0.5f, 1f, 0.5f); // Show correct
            }
            if (feedbackText != null)
            {
                feedbackText.text = $"<color=#FF4444>Incorrect.</color>\n{q.explanation}";
            }
        }

        // Update submit button text
        if (submitButtonText != null)
        {
            submitButtonText.text = (currentIndex + 1 < activeQuestions.Count) ? "Next" : "See Results";
        }
    }

    private void EndQuiz()
    {
        if (assessmentFlow != null)
        {
            assessmentFlow.OnQuizCompleted(score, activeQuestions.Count);
        }
    }
}