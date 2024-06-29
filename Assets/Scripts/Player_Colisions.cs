using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player_Colisions : MonoBehaviour
{
    public bool onGround {  get; private set; }
    public bool onWater { get; private set; }

    [SerializeField] private float _collision_Radius;
    [SerializeField] private Vector2 _ground_Offset;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private LayerMask _waterLayer;

    void Start()
    {
        
    }

    void Update()
    {
        Check_Player_Collision();
    }

    private void Check_Player_Collision()
    {
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + _ground_Offset, _collision_Radius, _groundLayer);
        onWater = Physics2D.OverlapCircle((Vector2)transform.position + _ground_Offset, _collision_Radius, _waterLayer);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + _ground_Offset, _collision_Radius);
    }
}
