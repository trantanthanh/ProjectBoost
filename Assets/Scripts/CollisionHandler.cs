using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Obstacles":
                {
                    Debug.Log("Obstacles");
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
                    Debug.Log("Finish");
                    break;
                }
        }
    }
}
