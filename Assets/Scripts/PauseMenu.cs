using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    private bool IsPaused = true;
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private TextMeshProUGUI Plushies;
    [Header("Menu")]
    [SerializeField] private GameObject MenuHolder = null;
    //[SerializeField] private Button RestartButton;
    //private TextMeshProUGUI RestartButtonText;
    [SerializeField] private Button QuitButton;
    private TextMeshProUGUI QuitButtonText;
    [Header("Player")]
    [SerializeField] private FirstPersonController Player;
    private void Awake()
    {
        // Add listeners to the buttons
        //RestartButton.onClick.AddListener(() => { Restart(); });
        QuitButton.onClick.AddListener(() => { Quit(); });
        // Grab their text components
        //RestartButtonText = RestartButton.GetComponentInChildren<TextMeshProUGUI>();
        QuitButtonText = QuitButton.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void Start()
    {
        DoPause(); // Re-enable everything
        // Reset the text
        //RestartButtonText.text = "Restart";
        QuitButtonText.text = "Quit";
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            DoPause();
        }
    }
    private void DoPause()
    {
        IsPaused = !IsPaused; // Flip the bool
        Time.timeScale = IsPaused ? 0 : 1; // Set the time scale to 0 if paused, 1 if not
        Player.enabled = !IsPaused; // Enable or disable the player
        UpdateUI();
        MenuHolder.SetActive(IsPaused); // Show or hide the menu
        Cursor.lockState = IsPaused ? CursorLockMode.None : CursorLockMode.Locked; // Show or hide the cursor
    }
    /*private void Restart()
    {
        // Reload the scene
        Debug.Log("Restarting game");
        RestartButtonText.text = "Restarting...";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }*/
    private void Quit()
    {
        // Quit the game
        Debug.Log("Quitting game");
        QuitButtonText.text = "Quitting...";
        Application.Quit();
    }
    private void UpdateUI()
    {
        Health.text = $"Health: {PlayerHealth.Instance.Health}/3";
        Plushies.text = $"Plushies: {Collectibles.plushiesCollected}/{Collectibles.plushiesNeedToWIn}";
    }
}