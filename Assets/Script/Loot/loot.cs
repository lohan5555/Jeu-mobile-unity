using UnityEngine;

public class loot : MonoBehaviour
{
    public int id;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Item n°" + id + " touché par " + other + "!");

            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.AddItem(id);
            }

            Destroy(gameObject);
        }
    }
}
