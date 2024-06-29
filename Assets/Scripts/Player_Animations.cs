using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animations : MonoBehaviour
{
    private Animator _anim;
    private Player_Colisions _coli;
    private Player_Movement _movement;
    private Health_System health;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _coli = GetComponent<Player_Colisions>();
        _movement = GetComponent<Player_Movement>();
        health = GetComponent<Health_System>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        Set_Animation_Valuse();
    }

    private void Set_Animation_Valuse()
    {
        _anim.SetInteger("IsRunning", (int)_movement.playerInput.x);
        _anim.SetBool("IsJumping", _movement.can_Jump);
        _anim.SetBool("OnGround", _coli.onGround);
        _anim.SetFloat("yVelocity", _movement.rb.velocity.y);
        _anim.SetBool("Attack", _movement.isAttacking);
        _anim.SetBool("IsAlive", health.isAlive);

    }
}
