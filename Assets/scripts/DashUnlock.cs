using UnityEngine;

public class DashUnlock : AbilityUnlock
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<playermovement>())
        {
            collision.gameObject.GetComponent<playermovement>().DashUnlocked = true;
            Destroy(gameObject);
        }
    }
}
