using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Префабы")]
    [SerializeField] public Player player;
    [SerializeField] public GameObject firePrefab;
    [SerializeField] public GameObject enemyPrefab;
    [SerializeField] public GameObject bossPrefab;
    [SerializeField] public List<Enemy> enemies = new List<Enemy>();
    [SerializeField] public bool isStart;
    [SerializeField] private int enemyCounts = 5;
    [SerializeField] public GameObject panelPopUp;

    [Space]
    //Параметры игрока
    [Header("Параметры игрока")]
    [SerializeField] private float playerHealth = 300f;
    [SerializeField] private float playerDamage = 2.5f;
    [SerializeField] private float playerAttackSpeed = 2f;
    [SerializeField] private float attackDistance = 4f;

    [Space]
    //Параметры врага
    [Header("Параметры врага")]
    [SerializeField] private float enemyHealth = 10f;
    [SerializeField] private float enemyDamage = 2f;
    [SerializeField] private float enemyAttackSpeed = 1f;
    [SerializeField] private float enemyMoveSpeed = 0.5f;

    [Space]
    //Параметры босса
    [Header("Параметры босса")]
    [SerializeField] private float bossHealth = 100f;
    [SerializeField] private float bossDamage = 3f;
    [SerializeField] private float bossAttackSpeed = 1f;
    [SerializeField] private float bossMoveSpeed = 0.25f;

    [SerializeField] private float addSpawnX;
    [SerializeField] private float addSpawnY;

    [SerializeField] private float bossTime = 15f;

    void Start()
    {
        player.health = playerHealth;
        player.damage = playerDamage;
        player.attackSpeed = playerAttackSpeed;
        player.attackDistance = attackDistance;
        player.ballPrefab = firePrefab;
        player.gameManager = this;
    }


    void Update()
    {
        if (!isStart) { return; }

        bossTime -= Time.deltaTime;
        if (bossTime < 0f)
        {
            SpawnBoss();
            bossTime = 15f;
        }
        else if (enemies.Count < enemyCounts)
        {
            enemies.Add(SpawnEnemy());
        }
    }

    private Enemy SpawnEnemy()
    {
        //Выбираем рандомное расположение для врага
        float radius = attackDistance + Random.Range(0f, addSpawnX);
        float spawnY = attackDistance + Random.Range(0f, addSpawnY);

        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector2 spawnPosition = new Vector2(x, y);

        // Создаём врага и задаём его характеристики
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();

        enemy.gameManager = this;
        enemy.attackSpeed = enemyAttackSpeed;
        enemy.health = enemyHealth;
        enemy.damage = enemyDamage;
        enemy.moveSpeed = enemyMoveSpeed;

        return enemy;
    }

    private Boss SpawnBoss()
    {
        //Выбираем рандомное расположение для босса
        float radius = attackDistance + Random.Range(0f, addSpawnX);
        float spawnY = attackDistance + Random.Range(0f, addSpawnY);

        float angle = Random.Range(0f, Mathf.PI * 2f);

        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        Vector2 spawnPosition = new Vector2(x, y);

        // Создаём босса и задаём его характеристики
        GameObject enemyObject = Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
        Boss boss = enemyObject.GetComponent<Boss>();

        boss.gameManager = this;
        boss.attackSpeed = bossAttackSpeed;
        boss.health = bossHealth;
        boss.damage = bossDamage;
        boss.moveSpeed = bossMoveSpeed;

        return boss;
    }

    public void StartGame()
    {
        panelPopUp.SetActive(false);
        isStart = true;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}