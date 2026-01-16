using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LibraryManager : MonoBehaviour
{
    public GameObject[] components; 
    public Text descriptionText;    
    
    [Header("Navigation Buttons")]
    public GameObject prevButton;    // NEW: Drag your '<PREV' button here
    public GameObject nextButton;    
    public GameObject finishButton;  

    private int currentIndex = 0;

    private string[] descriptions = {
        " SOLAR PANEL\n\nPURPOSE: CONVERTS SUNLIGHT INTO DC ELECTRICITY.\n\nCOMMON MISTAKES: WRONG TILT/ANGLE CAUSES 20-30% LOSS.",
        " INVERTER\n\nPURPOSE: CONVERTS DC POWER TO AC POWER FOR HOME USE.\n\nCOMMON MISTAKES: POOR VENTILATION CAUSES OVERHEATING.",
        " BATTERY\n\nPURPOSE: STORES EXCESS ENERGY FOR NIGHT USE.\n\nCOMMON MISTAKES: DEEP DISCHARGING REDUCES LIFESPAN.",
        " SOLAR MOUNT\n\nPURPOSE: PROVIDES THE STRUCTURAL FOUNDATION.\n\nCOMMON MISTAKES: LOOSE FASTENERS ARE A WIND HAZARD.",
        " CABLES\n\nPURPOSE: SAFELY TRANSPORTS ELECTRICITY.\n\nCOMMON MISTAKES: UNDERSIZED WIRING CAUSES FIRE HAZARDS."
    };

    void Start() 
    { 
        if(finishButton != null) finishButton.SetActive(false); 
        UpdateUI(); 
    }

    public void Next() 
    {
        // Only move forward if we aren't at the end
        if (currentIndex < components.Length - 1)
        {
            components[currentIndex].SetActive(false); 
            currentIndex++; 
            UpdateUI();
        }
    }

    public void Previous() 
    {
        // Only move backward if we aren't at the start
        if (currentIndex > 0)
        {
            components[currentIndex].SetActive(false); 
            currentIndex--;
            UpdateUI();
        }
    }

    void UpdateUI() 
    {
        // 1. Show the correct 3D model
        components[currentIndex].SetActive(true); 
        
        // 2. Update the text description
        descriptionText.text = descriptions[currentIndex];

        // 3. BUTTON VISIBILITY LOGIC

        // Handle Previous Button: Hide it if we are on the first item (Index 0)
        if (prevButton != null)
        {
            prevButton.SetActive(currentIndex > 0);
        }

        // Handle Next/Finish Buttons: Swap them on the last item
        if (currentIndex == components.Length - 1) 
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

    public void LoadInstallationScene()
    {
        SceneManager.LoadScene("Installation");
    }
}