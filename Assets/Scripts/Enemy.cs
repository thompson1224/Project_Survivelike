using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController[] animatorController;
    public Rigidbody2D target;

    private bool isLive;
    private Rigidbody2D rigid;
    private SpriteRenderer spriter;
    private Animator animator;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;
        
        Vector2 dirVector = target.position - rigid.position;
        Vector2 nextVector = dirVector.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void OnEnable()
    {
        target = GameManager.instance.player.GetComponent<Rigidbody2D>();
        isLive = true;
        health = maxHealth;
    }

    public void Init(SpawnData data)
    {
        animator.runtimeAnimatorController = animatorController[data.spriteType];
        speed = data.speed;
        maxHealth = data.health;
        health = data.health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet"))
            return;
        health -= collision.GetComponent<Bullet>().damage;

        if (health > 0)
        {
            // Live, Hit Action
        }
        else
        {
            // Die
            Dead();
        }
    }

    void Dead()
    {
        gameObject.SetActive(false);
    }
}
