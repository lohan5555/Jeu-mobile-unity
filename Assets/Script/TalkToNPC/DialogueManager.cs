using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;  // Singleton

    public DialogueData dialogues;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {

        // Récupérer la scène active
        Scene currentScene = SceneManager.GetActiveScene();
        // Déterminer le chemin du fichier JSON selon la scène
        string jsonPath = "";

        switch (currentScene.name)
        {
            case "level_One":
                jsonPath = "dialogue_1/dialogues_1";
                break;
            case "level_Three":
                jsonPath = "dialogue_3/dialogues_1";
                break;
            default:
                Debug.LogError("[DialogueManager] Pas de dialogues JSON défini pour cette scène !");
                return;
        }

        // Charger le JSON depuis Resources
        TextAsset jsonText = Resources.Load<TextAsset>(jsonPath);
        if (jsonText != null)
        {
            dialogues = JsonUtility.FromJson<DialogueData>(jsonText.text);
            Debug.Log("[DialogueManager] JSON chargé avec succès !");
        }
        else
        {
            Debug.LogError("[DialogueManager] Impossible de charger dialogues.json depuis Resources !");
        }
    }
}
