using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class Health_System : MonoBehaviour
{
    private Player_Colisions col;
    public int health;
    public float recoverTime;
    public int maxHealth;
    public int minHealth;
    public bool isDamaged = false;
    public bool isAlive = true;
    [SerializeField] private Image[] lives;
    [SerializeField] private Sprite fullLive, emptyLive;
    public Vector2 spawnPos;

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Player_Colisions>();
        maxHealth = 5;
        minHealth = 0;
        health = maxHealth;
    }

    private void Update()
    {
        Player_Death();
        spriteManager();
    }

    public void Player_Take_Damage(int damage)
    {
        health -= damage;
        isDamaged = true;
        Debug.Log("Player is damaged" + health);
    }

    private void spriteManager()
    {
        for (int i = 0; i < lives.Length; i++)
        {
            if (i < health)
            {
                lives[i].sprite = fullLive;
            }
            else
            {
                lives[i].sprite = emptyLive;
            }
        }
    }

    public void Player_Death()
    {
        if (health < minHealth)
        {
            isAlive = false;
            StartCoroutine(Respawn_Player());
        }
    }

    IEnumerator Respawn_Player()
    {
        yield return new WaitForSeconds(2);
        isAlive = true;
        this.transform.position = spawnPos;
        health = maxHealth;
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.gameObject.CompareTag("Water") && col.onGround == false) || collision.gameObject.CompareTag("DeadZone"))
        {
            isAlive = false;
            StartCoroutine(Respawn_Player());
        }
    }
}
