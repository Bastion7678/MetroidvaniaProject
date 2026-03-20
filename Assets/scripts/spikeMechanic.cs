using UnityEngine;

public class spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        Debug.Log("Spike has been hit!");
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has hit the spikes!");
            collision.transform.position = collision.GetComponent<playermovement>().lastSavelocation;

            collision.GetComponent<playerhealth>().takeDamage(10);
        }
    }

}
