using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private string gameplaySceneName;
    [SerializeField] private Slider loadingBar;

    [Header("Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [Header("Screens")]
    [SerializeField] private GameObject mainScreen;
    [SerializeField] private GameObject optionsScreen;
    [SerializeField] private GameObject loadingScreen;
    
    private void Start()
    {
        if (playButton != null) playButton.onClick.AddListener(Play);
        if (optionsButton != null) optionsButton.onClick.AddListener(Options);
        if (quitButton != null) quitButton.onClick.AddListener(Quit);
        if (backButton != null) backButton.onClick.AddListener(Back);

        if (mainScreen != null) mainScreen.SetActive(true);
        if (optionsScreen != null) optionsScreen.SetActive(false);
        if (loadingScreen != null) loadingScreen.SetActive(false);
    }

    private void OnDestroy()
    {
        DisableButtons();
    }

    void DisableButtons()
    {
        if (playButton != null) playButton.onClick.RemoveAllListeners();
        if (optionsButton != null) optionsButton.onClick.RemoveAllListeners();
        if (quitButton != null) quitButton.onClick.RemoveAllListeners();
        if (backButton != null) backButton.onClick.RemoveAllListeners();
    }

    private void Play()
    {
        VolumeController.Instance.PlaySound("ui-sfx");
        StartCoroutine(LoadGameplay());
    }

    private IEnumerator LoadGameplay()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(gameplaySceneName);

        if (loadingScreen != null) loadingScreen.SetActive(true);
        if (mainScreen != null) mainScreen.SetActive(false);
        if (optionsScreen != null) optionsScreen.SetActive(false);

        while (!operation.isDone)
        {
            if (loadingBar != null) loadingBar.value = operation.progress;
            yield return null;
        }
    }

    private void Quit()
    {
        VolumeController.Instance.PlaySound("ui-sfx");
        Application.Quit();
    }

    private void SetOptionsVisible(bool visible)
    {
        VolumeController.Instance.PlaySound("ui-sfx");
        if (mainScreen != null) mainScreen.SetActive(!visible);
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
