using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class factLoader : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private ListView listView;
    public PlayerInventory playerInventory;
    private MonsterEntry[] allMonsterData;
    private List<MonsterEntry> visibleFacts = new List<MonsterEntry>();

    IEnumerator Start()
    {
        // Attendre que GameManager ait chargé les données
        while (!GameManager.Instance.dataReady)
            yield return null;

        var root = uiDocument.rootVisualElement;
        listView = root.Q<ListView>("monsterList");

        MonsterDataList list = MonsterDataLoader.LoadData("monsterData.json");

        if (list == null)
        {
            Debug.LogError("Impossible de charger MonsterData. Pas de connexion ?");
            yield break;
        }

        allMonsterData = list.data;

        // Crée la vue d’un élément (le visuel d’une ligne)
        listView.makeItem = () =>
        {
            var label = new Label();
            label.AddToClassList("monster-item");
            return label;
        };

        listView.bindItem = (element, i) =>
        {
            (element as Label).text = visibleFacts[i].text;
        };

        listView.fixedItemHeight = 60;
        listView.selectionType = SelectionType.None;

        RefreshUI();
    }


    public void RefreshUI()
    {
        visibleFacts.Clear();

        foreach (int factId in playerInventory.getFacts())
        {
            if (factId >= 0 && factId < allMonsterData.Length)
                visibleFacts.Add(allMonsterData[factId]);
        }

        //on supp les doublons
        visibleFacts = visibleFacts.Distinct().ToList();

        listView.itemsSource = visibleFacts;
        listView.Rebuild();
    }
}
