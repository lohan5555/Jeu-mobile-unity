using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class AnimatorFixer : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FixAnimator());
    }

    private IEnumerator FixAnimator()
    {
        // attendre 1-2 frames pour laisser Unity initialiser la sc�ne
        yield return null;
        yield return null;

        if (animator != null)
        {
            animator.enabled = false;
            yield return null;
            animator.enabled = true;
            animator.Rebind();
            animator.Update(0f);
        }
    }
}