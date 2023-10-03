using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float timeDelayLoad = 2f;
    [SerializeField] AudioClip sfxDead;
    [SerializeField] AudioClip sfxSucess;
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
                    StartCoroutine(LoadNextLevel());
                    Debug.Log("Finish");
                    break;
                }
        }
    }

    IEnumerator LostControl()
    {
        if (!movement.lostControl)
        {
            audioSource.PlayOneShot(sfxDead);
        }
        movement.lostControl = true;
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
        audioSource.PlayOneShot(sfxSucess);
        yield return new WaitForSeconds(timeDelayLoad);
        if (!movement.lostControl)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
