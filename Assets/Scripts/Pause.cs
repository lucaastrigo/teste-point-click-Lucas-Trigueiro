using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    [SerializeField] private string menuSceneName;
    [SerializeField] private Slider loadingBar;

    [Header("Buttons")]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button backButton;
    [SerializeField] private Button menuButton;

    [Header("Screens")]
    [SerializeField] private GameObject HUDScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject loadingScreen;
    
    private void Start()
    {
        if (pauseButton != null) pauseButton.onClick.AddListener(PauseGame);
        if (resumeButton != null) resumeButton.onClick.AddListener(ResumeGame);
        if (optionsButton != null) optionsButton.onClick.AddListener(Options);
        if (backButton != null) backButton.onClick.AddListener(Back);
        if (menuButton != null) menuButton.onClick.AddListener(MenuGame);

        if (HUDScreen != null) HUDScreen.SetActive(true);
        if (pauseScreen != null) pauseScreen.SetActive(false);
        if (optionsScreen != null) optionsScreen.SetActive(false);
        if (loadingScreen != null) loadingScreen.SetActive(false);
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
    }

    private void SetGamePause(bool paused)
    {
        Time.timeScale = paused ? 0 : 1;

        if (HUDScreen != null) HUDScreen.SetActive(!paused);
        if (pauseScreen != null) pauseScreen.SetActive(paused);
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
        StartCoroutine(LoadMenu());
    }

    private IEnumerator LoadMenu()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(menuSceneName);

        if (loadingScreen != null) loadingScreen.SetActive(true);
        if (HUDScreen != null) pauseScreen.SetActive(HUDScreen);
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
}
