using UnityEngine;
using UnityEngine.SceneManagement;

public class Niveau : MonoBehaviour
{
    public string NomDeScene;

    public void AllerAuNiveau()
    {
        SceneManager.LoadScene(NomDeScene);
    }

    private void OnTriggerEnter(Collider other)
    {

        PlayerInventory playerInv = FindAnyObjectByType<PlayerInventory>();

        if (playerInv != null)
        {
            string invContent = playerInv.items.Count > 0
                ? string.Join(", ", playerInv.items)
                : "Inventaire vide";

            Debug.Log($"[DialogueManager] Inventaire du joueur : {invContent}");

            if (playerInv.items.Contains(0))
            {
                AllerAuNiveau();
            }
        }
        
    }
}
