using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollider : MonoBehaviour
{
    private int dano = 1;
    private bool jaDeuDano = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.isTrigger && collision.CompareTag("Player"))
        {
            if (!jaDeuDano)
            {
                Player p = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
                p.dano(dano);
                jaDeuDano = true;
            }
        }
    }
}
