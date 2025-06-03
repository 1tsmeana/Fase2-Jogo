using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public HeartSystem heart;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Personagem")
        {
            heart.TakeDamage(1); // Aplica 1 de dano
        }
    }
}
