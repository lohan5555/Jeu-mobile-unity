using UnityEngine;
using UnityEngine.UIElements;
using System.Collections.Generic;
using System.Linq;

public class factLoader : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private ListView listView;
    public PlayerInventory playerInventory;
    private MonsterEntry[] allMonsterData;
    private List<MonsterEntry> visibleFacts = new List<MonsterEntry>();

    void Start()
    {
        var root = uiDocument.rootVisualElement;
        listView = root.Q<ListView>("monsterList");

        allMonsterData = MonsterDataLoader.LoadData("monsterData").data;

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

        listView.fixedItemHeight = 40;
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
