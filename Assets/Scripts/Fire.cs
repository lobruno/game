using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float damage { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //print("attack");
            collision.gameObject.GetComponent<Creature>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
