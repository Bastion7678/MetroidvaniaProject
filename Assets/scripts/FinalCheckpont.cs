using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalCheckpont : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
