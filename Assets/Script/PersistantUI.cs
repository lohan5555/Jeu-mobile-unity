using UnityEngine;

public class PersistentUI : MonoBehaviour
{
    private static PersistentUI instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Garde ce GameObject (et tous ses enfants) entre les scènes
        }
        else
        {
            Destroy(gameObject); // Supprime les doublons si on recharge une scène
        }
    }
}
