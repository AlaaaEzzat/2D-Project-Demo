using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour
{
    [SerializeField] private GameObject potionEffect;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            int health = collision.gameObject.GetComponent<Health_System>().health; 

            if (health < 5) 
            {
                collision.gameObject.GetComponent<Health_System>().health += 1;
            }

            if (potionEffect.gameObject.activeInHierarchy == true)
            {
                potionEffect.gameObject.GetComponent<ParticleSystem>().Play();
            }
            else if (potionEffect.gameObject.activeInHierarchy == false)
            {
                potionEffect.gameObject.SetActive(true);
            }
            Destroy(this.gameObject);
        }
    }
}
