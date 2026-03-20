using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerhealth : MonoBehaviour
{
    public int health = 10;

    public Vector2 respawnPoint;
    public void takeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Handle player death (e.g., respawn, game over, etc.)
            Debug.Log("Player has died.");
            die();
        }


    }

    public void die()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1, 0, 0, 1);

        StartCoroutine(Death());
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(0.5f);
        this.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

        health = 10;
        this.transform.position = respawnPoint;
    }

    public void leavegame(InputAction.CallbackContext context)
    {
        Application.Quit();
    }
}
