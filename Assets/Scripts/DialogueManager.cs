using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    [SerializeField] private GameObject dialogueScreen;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    [Header("Buttons")]
    [SerializeField] private Button nextButton;
    [SerializeField] private Button closeButton;

    private DialogueData currentDialogue;
    private int currentLine;

    public bool IsOpen => dialogueScreen != null && dialogueScreen.activeSelf;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (nextButton != null) nextButton.onClick.AddListener(NextLine);
        if (closeButton != null) closeButton.onClick.AddListener(CloseDialogue);

        if (dialogueScreen != null) dialogueScreen.SetActive(false);

        if (nextButton != null) nextButton.gameObject.SetActive(false);
        if (closeButton != null) closeButton.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        DisableButtons();
    }

    void DisableButtons()
    {
        if (nextButton != null) nextButton.onClick.RemoveAllListeners();
        if (closeButton != null) closeButton.onClick.RemoveAllListeners();
    }

    public void StartDialogue(DialogueData dialogue)
    {
        if (dialogue == null) return;
        if (dialogue.lines == null || dialogue.lines.Length == 0) return;

        currentDialogue = dialogue;
        currentLine = 0;

        if (dialogueScreen != null) dialogueScreen.SetActive(true);
        if (nextButton != null) nextButton.gameObject.SetActive(true);
        if (closeButton != null) closeButton.gameObject.SetActive(true);

        ShowCurrentLine();
    }

    public void NextLine()
    {
        if (currentDialogue == null) return;

        currentLine++;

        if (currentLine >= currentDialogue.lines.Length)
        {
            CloseDialogue();
            return;
        }

        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        DialogueLine line = currentDialogue.lines[currentLine];

        VolumeController.Instance.PlaySound(line.charSFX);
        if (dialogueText != null) dialogueText.text = line.text;
        if (portraitImage != null) portraitImage.sprite = line.portrait;
    }

    public void CloseDialogue()
    {
        currentDialogue = null;
        currentLine = 0;

        if (dialogueScreen != null) dialogueScreen.SetActive(false);
        if (nextButton != null) nextButton.gameObject.SetActive(false);
        if (closeButton != null) closeButton.gameObject.SetActive(false);
    }
}