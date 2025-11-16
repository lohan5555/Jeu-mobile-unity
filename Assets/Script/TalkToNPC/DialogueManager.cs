using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
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
        string jsonKey = "";

        switch (currentScene.name)
        {
            case "level_One":
                var inv1 = FindAnyObjectByType<PlayerInventory>();
                jsonKey = inv1 != null && inv1.items.Contains(0)
                    ? "dialogue_1/dialogues_2.json"
                    : "dialogue_1/dialogues_1.json";
                break;

            case "level_Two":
                var inv2 = FindAnyObjectByType<PlayerInventory>();
                jsonKey = inv2 != null && inv2.items.Contains(1)
                    ? "dialogue_2/dialogues_2.json"
                    : "dialogue_2/dialogues_1.json";
                break;

            case "level_Three":
                jsonKey = "dialogue_3/dialogues_1.json";
                break;

            default:
                Debug.LogError("[DialogueManager] Aucun dialogue défini pour cette scène !");
                return;
        }

        if (GameManager.Instance.dialoguesCache.TryGetValue(jsonKey, out var data))
        {
            dialogues = data;
        }
        else
        {
            Debug.LogError($"[DialogueManager] Dialogue introuvable dans le cache : {jsonKey}");
        }
    }
}
