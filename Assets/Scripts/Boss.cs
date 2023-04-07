using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss: Creature
{

    [SerializeField]
    public float health;
    public float damage { get; set; }
    public float attackSpeed { get; set; }
    public Vector2 position { get; set; }
    public GameManager gameManager { get; set; }
    public float moveSpeed { get; set; }
    public Player player { get; set; }
    float distanceToPlayer = 0f;
    float attackDistance { get; set; } = 2f;
    float nextAttackTime { get; set; } = 0f;

    private void Move()
    {
        // Движение к игроку
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
    }

    public override void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Метод, вызываемый при смерти врага.
    protected override void Die()
    {
        Destroy(gameObject);
    }

    //Метод, вызываемый для атаки персонажа
    protected override void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackSpeed;

            player.TakeDamage(damage);
        }
    }

    public void Start()
    {
        player = gameManager.player;
    }

    // Метод, обновляющий состояние врага.
    void Update()
    {
        if (!gameManager.isStart) { return; }

        distanceToPlayer = Vector2.Distance(GetPosition(), player.GetPosition());

        if (distanceToPlayer <= attackDistance)
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    public override Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}
