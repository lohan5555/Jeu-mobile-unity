using UnityEngine;
using UnityEngine.UIElements;

public class ShowMobileInput : MonoBehaviour
{
    void Start()
    {
        // Cherche le premier UIDocument actif dans la sc�ne
        UIDocument doc = Object.FindFirstObjectByType<UIDocument>();
        if (doc == null)
        {
            Debug.LogError("[ShowMobileInput] Aucun UIDocument trouv� dans la sc�ne !");
            return;
        }

        var root = doc.rootVisualElement;
        var mobileInput = root.Q<VisualElement>("MobileInput");

        if (mobileInput != null)
        {
            mobileInput.style.display = DisplayStyle.Flex;
        }
        else
        {
            Debug.LogWarning("[ShowMobileInput] �l�ment #MobileInput introuvable !");
        }

        var bossHealthBar = root.Q<VisualElement>("BossHealthBarContainer");
        if (bossHealthBar != null)
        {
            bossHealthBar.style.display = DisplayStyle.None;
        }
        else
        {
            Debug.LogWarning("[ShowMobileInput] BossHealthBarContainer introuvable (nom incorrect ?)");
        }
    }
}
