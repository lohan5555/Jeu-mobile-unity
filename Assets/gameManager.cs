using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class gameManager : MonoBehaviour
{
    void Awake()
    {
        SceneManager.LoadScene("level_One");
    }

}
