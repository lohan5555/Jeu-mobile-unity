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

    public void LoadDialogueForCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string jsonPath = "";

        switch (currentScene.name)
        {
            case "level_One":
                PlayerInventory playerInv = FindAnyObjectByType<PlayerInventory>();

                if (playerInv != null)
                {
                    string invContent = playerInv.items.Count > 0
                        ? string.Join(", ", playerInv.items)
                        : "Inventaire vide";

                    Debug.Log($"[DialogueManager] Inventaire du joueur : {invContent}");

                    if (playerInv.items.Contains(0))
                    {
                        Debug.Log("[DialogueManager] Joueur possède l'objet 0 -> dialogue alternatif !");
                        jsonPath = "dialogue_1/dialogues_2";
                    }
                    else
                    {
                        jsonPath = "dialogue_1/dialogues_1";
                    }
                }
                else
                {
                    Debug.LogWarning("[DialogueManager] Aucun PlayerInventory trouvé dans la scène !");
                    jsonPath = "dialogue_1/dialogues_1";
                }
                break;

            case "level_Three":
                jsonPath = "dialogue_3/dialogues_1";
                break;

            default:
                Debug.LogError("[DialogueManager] Pas de dialogues JSON défini pour cette scène !");
                return;
        }

        TextAsset jsonText = Resources.Load<TextAsset>(jsonPath);

        if (jsonText != null)
        {
            dialogues = JsonUtility.FromJson<DialogueData>(jsonText.text);
            Debug.Log($"[DialogueManager] JSON chargé avec succès depuis : {jsonPath}");
        }
        else
        {
            Debug.LogError($"[DialogueManager] Impossible de charger le JSON depuis Resources : {jsonPath}");
        }
    }
}
