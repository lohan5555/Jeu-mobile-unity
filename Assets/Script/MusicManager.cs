using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    private AudioSource audioSource;

    [Header("Musique par sc�ne")]
    public AudioClip menuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;

    [Header("Musique sp�ciale")]
    public AudioClip bossMusic;

    private Coroutine fadeCoroutine;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayMusicForScene(scene.name);
    }

    public void PlayMusicForScene(string sceneName)
    {
        AudioClip clip = null;

        switch (sceneName)
        {
            case "menuPrincipal": clip = menuMusic; break;
            case "loadingScene": clip = menuMusic; break;
            case "level_One": clip = level1Music; break;
            case "level_Two": clip = level2Music; break;
            case "level_Three": clip = level3Music; break;
            default: Debug.LogWarning("[MusicManager] Aucune musique d�finie pour cette sc�ne."); break;
        }

        if (clip != null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeToMusic(clip));
        }
    }

    public void PlayBossMusic()
    {
        if (bossMusic == null)
        {
            Debug.LogWarning("[MusicManager] BossMusic non assign�e !");
            return;
        }

        if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);
        fadeCoroutine = StartCoroutine(FadeToMusic(bossMusic));
    }

    public void StopBossMusicAndResumeLevel()
    {
        // On r�cup�re la sc�ne actuelle et on relance la musique de niveau
        PlayMusicForScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    private System.Collections.IEnumerator FadeToMusic(AudioClip newClip)
    {
        float t = 0f;
        float fadeTime = 1f;
        float startVolume = audioSource.volume > 0 ? audioSource.volume : 1f;

        // Fade out
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeTime);
            yield return null;
        }

        audioSource.clip = newClip;
        audioSource.Play();

        // Fade in
        t = 0f;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, startVolume, t / fadeTime);
            yield return null;
        }
    }
}
