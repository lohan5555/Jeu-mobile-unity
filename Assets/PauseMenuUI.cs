using UnityEngine;
using UnityEngine.UIElements;

public class PauseMenuUI : MonoBehaviour
{
    private VisualElement root;
    private VisualElement pauseMenu;
    private VisualElement pauseButton;
    private Button resumeButton;

    private bool isPaused = false;

    void Awake()
    {
        var uiDocument = GetComponent<UIDocument>();
        root = uiDocument.rootVisualElement;

        pauseMenu = root.Q<VisualElement>("PauseMenu");
        pauseButton = root.Q<VisualElement>("ButtonPause");
        resumeButton = root.Q<Button>("ButtonResume");

        // Sécurité
        if (pauseButton != null)
            pauseButton.RegisterCallback<ClickEvent>(OnPauseClicked);
        if (resumeButton != null)
            resumeButton.clicked += OnResumeClicked;
    }

    private void OnPauseClicked(ClickEvent evt)
    {
        TogglePause();
    }

    private void OnResumeClicked()
    {
        TogglePause();
    }

    private void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Debug.Log("⏸️ Pause activée");
            Time.timeScale = 0f;
            pauseMenu.style.display = DisplayStyle.Flex;
        }
        else
        {
            Debug.Log("▶️ Jeu repris");
            Time.timeScale = 1f;
            pauseMenu.style.display = DisplayStyle.None;
        }
    }
}
