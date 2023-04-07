using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Creature
{
    [SerializeField]
    public GameObject lightningPrefab;
    public float health;
    public float damage { get; set; }
    public float attackSpeed { get; set; }
    public float abilitySpeed { get; set; } = 6f;
    public Vector2 position { get; set; }
    public float attackDistance { get; set; }
    public GameObject ballPrefab { get; set; }
    public GameManager gameManager { get; set; }
    private float ballSpeed { get; set;  } = 5f;
    private float nextAttackTime { get; set; } = 0f;
    private float nextAbilityTime { get; set; } = 0f;

    private void Start()
    {
        
    }

    protected override void Attack()
    {
        if (Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackSpeed;

            // Создаем экземпляр префаба шара и получаем его компонент Rigidbody2D
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);
            Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
            ball.gameObject.GetComponent<Fire>().damage = damage;

            // Находим ближайшего врага и направляем шар в его сторону
            Enemy nearestEnemy = FindNearestEnemy();
            Vector2 direction = (nearestEnemy.transform.position - transform.position).normalized;
            ballRb.velocity = direction * ballSpeed;
        }
    }

    public override void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    // Метод, вызываемый при смерти игрока.
    protected override void Die()
    {
        gameManager.RestartGame();
    }

    // Метод, обрабатывающий использование способности молния.
    public void CastLightning()
    {
        if (Time.time > nextAbilityTime)
        {
            nextAbilityTime = Time.time + abilitySpeed;
            if (FindNearestEnemy() == null) { return; }

            GameObject _lightning = Instantiate(lightningPrefab, transform.position, Quaternion.identity);
            print("inst");
            Lightning lightning = _lightning.GetComponent<Lightning>();
            lightning.player = this;
            lightning.Use();
        }
    }

    // Метод, обновляющий состояние игрока.
    public void Update()
    {
        if (!gameManager.isStart) { return; }

        if (FindNearestEnemy() != null) { Attack(); }
    }

    private Enemy FindNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Enemy enemy in gameManager.enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < attackDistance && distance < nearestDistance)
            {
                nearestEnemy = enemy;
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }

    public override Vector2 GetPosition()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }
}

