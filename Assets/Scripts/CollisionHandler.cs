using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeDelayLoad = 2f;
    [SerializeField] AudioClip sfxDead;
    [SerializeField] AudioClip sfxSucess;
    [SerializeField] ParticleSystem crashEffect;
    [SerializeField] ParticleSystem sucessEffect;

    [HideInInspector] public bool isTransitioningNextLevel = false;
    [HideInInspector] public bool isTransitioningReloadLevel = false;
    AudioSource audioSource;

    Movement movement;

    void Start()
    {
        movement = GetComponent<Movement>();
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Obstacles":
                {
                    if (isTransitioningReloadLevel) return;
                    isTransitioningReloadLevel = true;
                    Debug.Log("Obstacles");
                    StartCoroutine(LostControl());
                    break;
                }
            case "Fuel":
                {
                    other.gameObject.SetActive(false);
                    Destroy(other.gameObject);
                    Debug.Log("Fuel");
                    break;
                }
            case "Start":
                {
                    Debug.Log("Start");
                    break;
                }
            case "Finish":
                {
                    if (isTransitioningNextLevel) return;
                    isTransitioningNextLevel = true;
                    StartCoroutine(LoadNextLevel());
                    Debug.Log("Finish");
                    break;
                }
        }
    }

    IEnumerator LostControl()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(sfxDead);
        crashEffect.Play();
        movement.lostControl = true;
        movement.StopThrustEffect();
        yield return new WaitForSeconds(timeDelayLoad);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            nextSceneIndex = 0;
        }
        audioSource.Stop();
        sucessEffect.Play();
        audioSource.PlayOneShot(sfxSucess);
        movement.StopThrustEffect();
        yield return new WaitForSeconds(timeDelayLoad);
        if (!movement.lostControl)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
