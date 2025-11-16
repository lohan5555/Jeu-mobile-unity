using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory: MonoBehaviour
{
    public List<int> items = new List<int>();
    public List<int> facts = new List<int>();

    public void AddItem(int itemId)
    {
        items.Add(itemId);
    }

    public void AddFacts(int factId)
    {
        facts.Add(factId);
    }

    public void clear()
    {
        FindAnyObjectByType<factLoader>().RefreshUI();
        items.Clear();
    }

    public List<int> getFacts()
    {
        return facts;
    }
}
