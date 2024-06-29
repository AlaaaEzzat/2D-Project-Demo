using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{
    [SerializeField] private Transform[] points;
    [SerializeField] protected float patrolSpeed;
    [SerializeField] protected float waitTimeToNextPatrolPoint;
    [SerializeField] protected int currentPatrolPointIndex;
    [SerializeField] protected bool CoroutineOnce;
    [SerializeField] private int enemyHealth;
    public bool Dead  = false;
    private Animator anim;



    private void Start()
    {
        enemyHealth = 1;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Dead)
        {
            Patroling();
        }
    }

    public void Patroling()
    {
        //canAttack = false;

        if (transform.position.x != points[currentPatrolPointIndex].position.x)
        {
            if(transform.position.x < points[currentPatrolPointIndex].position.x)
            {
                this.transform.localScale = new Vector2(2 ,2);
            }
            else
            {
                this.transform.localScale = new Vector2(-2,2);
            }
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(points[currentPatrolPointIndex].position.x, transform.position.y), patrolSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(points[currentPatrolPointIndex].position.x, transform.position.y);
            if (CoroutineOnce == false)
            {
                CoroutineOnce = true;
                StartCoroutine(WaitPatrol());
            }
        }
    }

    IEnumerator WaitPatrol()
    {
        yield return new WaitForSeconds(waitTimeToNextPatrolPoint);
        if (currentPatrolPointIndex + 1 < points.Length)
        {
            currentPatrolPointIndex++;
        }
        else
        {
            currentPatrolPointIndex = 0;
        }
        CoroutineOnce = false;
    }

    public void Enemy_Take_Damage(int damage)
    {
        enemyHealth -= damage;
        Dead = true;
        anim.SetBool("Dead", Dead);
        StartCoroutine(Wait_Before_Death());
    }

    IEnumerator Wait_Before_Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health_System>().Player_Take_Damage(1);
        }
    }
}
