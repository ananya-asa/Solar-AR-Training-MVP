using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstallationManager : MonoBehaviour
{
    [Header("UI References")]
    public Image displaySpace;
    public Text descriptionText;

    [Header("Navigation Buttons")]
    public GameObject prevButton;    // NEW: Drag your '<PREV' button here
    public GameObject nextButton;    // Drag your '>NEXT' button here
    public GameObject finishButton;  // Drag your 'START QUIZ' button here

    [Header("Content")]
    public Sprite[] stepImages;

    private int currentIndex = 0;

    private string[] instructions = {
        "STEP 1: SAFETY FIRST\n\nWear insulated gloves and a safety harness. Ensure the workspace is dry.",
        "STEP 2: MOUNTING SYSTEM\n\nSecure the aluminum rails to the roof. Ensure perfect level alignment.",
        "STEP 3: PANEL PLACEMENT\n\nSlide the solar panels onto the rails and tighten the clamps.",
        "STEP 4: ELECTRICAL WIRING\n\nConnect the DC strings from the panels to the inverter.",
        "STEP 5: SYSTEM TESTING\n\nVerify all connections and check the meter for power generation."
    };

    void Start()
    {
        if(finishButton != null) finishButton.SetActive(false); // Hide finish button at start
        UpdateUI();
    }

    public void Next()
    {
        // Prevent going past the last item
        if (currentIndex < instructions.Length - 1)
        {
            currentIndex++;
            UpdateUI();
        }
    }

    public void Previous()
    {
        // Prevent going before the first item
        if (currentIndex > 0)
        {
            currentIndex--;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (stepImages.Length > 0 && currentIndex < stepImages.Length) 
            displaySpace.sprite = stepImages[currentIndex];
            
        descriptionText.text = instructions[currentIndex];

        // 1. Handle Previous Button: Hide at Step 1 (Index 0)
        if (prevButton != null)
        {
            prevButton.SetActive(currentIndex > 0);
        }

        // 2. Handle Next/Finish Buttons: Swap at Step 5
        if (currentIndex == instructions.Length - 1)
        {
            nextButton.SetActive(false);
            finishButton.SetActive(true);
        }
        else
        {
            nextButton.SetActive(true);
            finishButton.SetActive(false);
        }
    }

    public void LoadIntermediateArScene()
    {
        SceneManager.LoadScene("intermediate"); 
    }
}