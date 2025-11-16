using Unity.VisualScripting;
using UnityEngine;

public static class MonsterDataLoader
{
    public static MonsterDataList LoadData(string fileName)
    {
        if (GameManager.Instance.jsonCache.TryGetValue(fileName, out string jsonText))
        {
            return JsonUtility.FromJson<MonsterDataList>(jsonText);
        }

        Debug.LogError($"[MonsterDataLoader] JSON introuvable : {fileName}");
        return null;
    }
}


[System.Serializable]
public class MonsterEntry
{
    public int id;
    public string text;
}

[System.Serializable]
public class MonsterDataList
{
    public MonsterEntry[] data;
}
