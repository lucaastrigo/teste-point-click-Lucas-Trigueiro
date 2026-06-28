using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIPause : MonoBehaviour
{
    [SerializeField] private string menuSceneName;
    [SerializeField] private Slider loadingBar;

    [Header("Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button menuButton;
    [SerializeField] private Button unfocusButton;
    [SerializeField] private Button openInventoryButton;
    [SerializeField] private Button closeInventoryButton;

    [Header("Screens")]
    [SerializeField] private GameObject HUDScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject inventoryScreen;
    
    private void Start()
    {
        if (pauseButton != null) pauseButton.onClick.AddListener(PauseGame);
        if (resumeButton != null) resumeButton.onClick.AddListener(ResumeGame);
        if (optionsButton != null) optionsButton.onClick.AddListener(Options);
        if (backButton != null) backButton.onClick.AddListener(Back);
        if (menuButton != null) menuButton.onClick.AddListener(MenuGame);
        if (unfocusButton != null) unfocusButton.onClick.AddListener(Unfocus);
        if (openInventoryButton != null) openInventoryButton.onClick.AddListener(Open);
        if (closeInventoryButton != null) closeInventoryButton.onClick.AddListener(Close);

        if (HUDScreen != null) HUDScreen.SetActive(true);
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (optionsScreen != null) optionsScreen.SetActive(false);
        if (loadingScreen != null) loadingScreen.SetActive(false);
        if (inventoryScreen != null) inventoryScreen.SetActive(false);

        if (unfocusButton != null) unfocusButton.gameObject.SetActive(false);
        if (openInventoryButton != null) openInventoryButton.gameObject.SetActive(true);
        if (closeInventoryButton != null) closeInventoryButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        DisableButtons();
    }

    void DisableButtons()
    {
        if (pauseButton != null) pauseButton.onClick.RemoveAllListeners();
        if (resumeButton != null) resumeButton.onClick.RemoveAllListeners();
        if (optionsButton != null) optionsButton.onClick.RemoveAllListeners();
        if (backButton != null) backButton.onClick.RemoveAllListeners();
        if (menuButton != null) menuButton.onClick.RemoveAllListeners();
        if (unfocusButton != null) unfocusButton.onClick.RemoveAllListeners();
    }

    private void SetGamePause(bool paused)
    {
        VolumeController.Instance.PlaySound("ui-sfx");
        Time.timeScale = paused ? 0 : 1;

        if (HUDScreen != null) HUDScreen.SetActive(!paused);
        if (pauseScreen != null) pauseScreen.SetActive(paused);

        SetUnfocusButtonVisible(CameraController.Instance.IsFocused && !paused);
    }

    void PauseGame()
    {
        SetGamePause(true);
    }

    void ResumeGame()
    {
        SetGamePause(false);
    }

    private void MenuGame()
    {
        VolumeController.Instance.PlaySound("ui-sfx");
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        Time.timeScale = 1;

        AsyncOperation operation = SceneManager.LoadSceneAsync(menuSceneName);

        if (loadingScreen != null) loadingScreen.SetActive(true);
        if (HUDScreen != null) HUDScreen.SetActive(false);
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (optionsScreen != null) optionsScreen.SetActive(false);

        while (!operation.isDone)
        {
            if (loadingBar != null) loadingBar.value = operation.progress;
            yield return null;
        }
    }

    private void SetOptionsVisible(bool visible)
    {
        VolumeController.Instance.PlaySound("ui-sfx");
        if (pauseScreen != null) pauseScreen.SetActive(!visible);
        if (optionsScreen != null) optionsScreen.SetActive(visible);
    }

    private void Options()
    {
        SetOptionsVisible(true);
    }

    private void Back()
    {
        SetOptionsVisible(false);
    }

    private void Unfocus()
    {
        VolumeController.Instance.PlaySound("unfocus");
        CameraController.Instance.Unfocus();
        SetUnfocusButtonVisible(false);
    }

    private void SetInventory(bool open)
    {
        VolumeController.Instance.PlaySound("ui-sfx");
        if (inventoryScreen != null) inventoryScreen.SetActive(open);
        if (openInventoryButton != null) openInventoryButton.gameObject.SetActive(!open);
        if (closeInventoryButton != null) closeInventoryButton.gameObject.SetActive(open);
    }

    void Open()
    {
        SetInventory(true);
    }

    void Close()
    {
        SetInventory(false);
    }

    public void SetUnfocusButtonVisible(bool visible)
    {
        if (unfocusButton != null) unfocusButton.gameObject.SetActive(visible);
    }
}
