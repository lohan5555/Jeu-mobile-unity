using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject player;
    public GameObject mobileUI;

    // Cache pour stocker les dialogues et les dataMonster
    public Dictionary<string, DialogueData> dialoguesCache = new Dictionary<string, DialogueData>();
    public Dictionary<string, string> jsonCache = new Dictionary<string, string>();

    [SerializeField] private string baseUrl = "https://billyboy16.github.io/unity-dialogues/";
    [SerializeField] private GameObject RetryButton;
    public bool dataReady = false; //quand le gameManager à fini de charger les facts, on informe factLoader qu'il peut les récupérer pour le grimoirUI


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        StartCoroutine(LoadEverything());
    }

    IEnumerator LoadEverything()
    {
        bool monsterOk = false;
        yield return StartCoroutine(LoadAllMonsterData(result => monsterOk = result));

        if (!monsterOk)
            yield break;

        bool dialoguesOk = false;
        yield return StartCoroutine(LoadAllDialogues(result => dialoguesOk = result));

        if (!dialoguesOk)
            yield break;

        SceneManager.LoadScene("level_One");
        dataReady = true;
    }



    IEnumerator LoadAllMonsterData(System.Action<bool> callback)
    {
        Debug.Log("[GameManager] Téléchargement de monsterData...");

        string url = baseUrl + "monsterData.json";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                jsonCache["monsterData.json"] = request.downloadHandler.text;
                Debug.Log("[GameManager] monsterData chargé !");
                callback(true);
                yield break;
            }
            else
            {
                Debug.LogError("[GameManager] ERREUR monsterData : " + request.error);
                RetryButton?.SetActive(true);
                callback(false);
                yield break;
            }
        }
    }



    IEnumerator LoadAllDialogues(System.Action<bool> callback)
    {
        Debug.Log("[GameManager] Téléchargement des dialogues...");

        string[] dialogueFiles = {
            "dialogue_1/dialogues_1.json",
            "dialogue_1/dialogues_2.json",
            "dialogue_2/dialogues_1.json",
            "dialogue_2/dialogues_2.json",
            "dialogue_3/dialogues_1.json"
        };

        foreach (string file in dialogueFiles)
        {
            string url = baseUrl + file;

            using (UnityWebRequest request = UnityWebRequest.Get(url))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Erreur téléchargement : {file}");
                    RetryButton?.SetActive(true);
                    callback(false);
                    yield break;
                }

                try
                {
                    dialoguesCache[file] = JsonUtility.FromJson<DialogueData>(request.downloadHandler.text);
                }
                catch
                {
                    Debug.LogError($"Erreur parsing JSON : {file}");
                    RetryButton?.SetActive(true);
                    callback(false);
                    yield break;
                }
            }
        }

        Debug.Log("[GameManager] Tous les dialogues sont chargés !");
        callback(true);
    }



    public void RegisterPlayer(GameObject p)
    {
        //on lie le Player au gameManager
        player = p;
    }

    public void RegisterUI(GameObject ui)
    {
        //on lie l'UI au gameManager
        mobileUI = ui;
    }

    public void RetryDownload()
    {
        if (RetryButton != null)
            RetryButton.SetActive(false); // cache le bouton

        StartCoroutine(LoadEverything()); // relance le téléchargement
    }

}
