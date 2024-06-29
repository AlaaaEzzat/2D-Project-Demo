using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    private Player_Colisions coli;
    public Rigidbody2D rb {  get; private set; }


    [Header("Player Movement")]
    [SerializeField] private float player_Speed;
    [SerializeField] private float onWater_Speed;
    [SerializeField] private float accelerationForce;
    [SerializeField] private float deaccelerationTime;
    private float xVelocity = 0.0f;
    public Vector2 playerInput;

    [Header("Player Jump")]
    [SerializeField] private float jump_Force;
    public bool can_Jump {  get; private set; }
    public bool isAttacking { get; private set; }
    [SerializeField] private float fallMultiplyer;



    void Start()
    {
        coli = GetComponent<Player_Colisions>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        playerMovement();
    }

    void Update()
    {
        Handle_Player_Inputs();
        Flip_Character();
    }

    private void Handle_Player_Inputs()
    {
        if (Input.GetKey(KeyCode.A)) { playerInput.x = -1; }

        else if (Input.GetKey(KeyCode.D)) { playerInput.x = 1;}

        else { playerInput.x = 0;}

        if (Input.GetKeyDown(KeyCode.Space) && coli.onGround)
        {
            can_Jump = true;
        }

        if(Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            isAttacking = true;
            StartCoroutine((Wait_Some_Time()));
        }
    }

    private void playerMovement()
    {
        #region Handle player movement

        if (Mathf.Abs(rb.velocity.x) < player_Speed && playerInput.x != 0)
        {
            float acceleration = playerInput.x * accelerationForce;
            if (!coli.onWater)
            {
                rb.AddForce(Vector2.right * acceleration * Time.deltaTime, ForceMode2D.Impulse);
            }
            else
            {
                rb.AddForce(Vector2.right * onWater_Speed * playerInput.x * Time.deltaTime, ForceMode2D.Impulse);
            }

        }

        if(playerInput.x == 0 || (playerInput.x > 0 && rb.velocity.x < 0) || (playerInput.x < 0 && rb.velocity.x > 0))
        {
            float deacceleration = Mathf.SmoothDamp(rb.velocity.x, 0, ref xVelocity, deaccelerationTime);
            rb.velocity = new Vector2(deacceleration, rb.velocity.y);
        }

        #endregion


        #region Handle player JumpForce

        if (can_Jump)
        {
            rb.AddForce(Vector2.up * jump_Force, ForceMode2D.Impulse);
            can_Jump = false;
        }
        if(rb.velocity.y < 0 && !coli.onGround)
        {
            rb.gravityScale = fallMultiplyer;
        }
        else if (rb.velocity.y > 0 && !coli.onGround)
        {
            rb.gravityScale = 1.3f;
        }
        else if(coli.onGround)
        {
            rb.gravityScale = 1;
        }

        #endregion
    }

    private void Flip_Character()
    {
        if (playerInput.x != 0)
        {
            if(playerInput.x > 0)
            {
                this.transform.localScale = Vector2.one;
            }
            else
            {
                this.transform.localScale = new Vector2(-1, 1);
            }
        }
    }

    IEnumerator Wait_Some_Time()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }
}
