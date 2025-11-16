using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerSpawnManager : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(SpawnWhenReady());
    }

    IEnumerator SpawnWhenReady()
    {
        PlayerInventory playerInv = null;
        while (playerInv == null)
        {
            playerInv = FindAnyObjectByType<PlayerInventory>();
            yield return null;
        }

        GameObject player = playerInv.gameObject;

        SpawnPoint spawn = null;
        while (spawn == null)
        {
            spawn = FindAnyObjectByType<SpawnPoint>();
            yield return null;
        }

        player.transform.position = spawn.transform.position;
        player.transform.rotation = spawn.transform.rotation;
    }
}
