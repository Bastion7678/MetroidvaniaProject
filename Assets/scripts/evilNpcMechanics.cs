using UnityEngine;

public class evilNpcMechanics : MonoBehaviour
{
    [SerializeField]
    Transform postion1;
    [SerializeField]
    Transform postion2;
    private void Update()
    {
        this.transform.position = Vector3.Lerp(postion1.position, postion2.position, (Mathf.Cos(Time.time) + 1) / 2);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<playerhealth>().takeDamage(10);
        }
    }
}
