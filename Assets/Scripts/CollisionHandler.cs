using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
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
        movement.lostControl = true;
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            nextSceneIndex = 0;
        }
        yield return new WaitForSeconds(2);
        if (!movement.lostControl)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
