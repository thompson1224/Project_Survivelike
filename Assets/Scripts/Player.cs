using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public Vector2 inputVec;
    public float speed = 3.0f;
    public SpriteRenderer spriter;
    public Animator animator;
    public Scanner scanner;
    
    private Rigidbody2D rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        scanner = GetComponent<Scanner>();
    }
    void Update()
    {
        // inputVec.x = Input.GetAxisRaw("Horizontal");
        // inputVec.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        // rigid.AddForce(inputVec);
        // rigid.velocity = inputVec;
        Vector2 nextVec = inputVec * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);
    }

    void OnMove(InputValue value)
    {
        inputVec = value.Get<Vector2>();
    }

    void LateUpdate()
    {
        animator.SetFloat("Speed", inputVec.magnitude);
        if (inputVec.x != 0)
        {
            spriter.flipX = inputVec.x < 0;
        }
    }
}
