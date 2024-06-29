using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_System : MonoBehaviour
{
    private Player_Movement mov;
    private void Start()
    {
        mov = GetComponent<Player_Movement>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && mov.isAttacking)
        {
            collision.gameObject.GetComponent<Enemy_AI>().Enemy_Take_Damage(1);
        }
    }
}
