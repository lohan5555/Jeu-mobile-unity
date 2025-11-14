using UnityEngine;
using UnityEngine.SceneManagement;

public class ToLevel3 : MonoBehaviour
{
    public string SceneDestination;

    private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(SceneDestination);
    }
}
