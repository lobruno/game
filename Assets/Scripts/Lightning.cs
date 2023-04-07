using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Lightning : MonoBehaviour
{
    public int damage = 10;
    public float range = 8f;
    public int numberOfTargets = 3;
    public float delayBetweenTargets = 0.2f;
    public Player player;

    public void Use()
    {
        StartCoroutine(ChainLightning());
    }

    private IEnumerator ChainLightning()
    {
        // Находим ближайшего врага
        Enemy nearestEnemy = FindNearestEnemy();

        for (int i = 0; i < numberOfTargets; i++)
        {
            if (nearestEnemy == null)
            {
                Destroy(this);
                break;
            }

            yield return StartCoroutine(MoveToTarget(nearestEnemy.transform.position, 1f));

            // Наносим урон ближайшему врагу
            nearestEnemy.TakeDamage(damage);

            // Находим следующего ближайшего врага
            nearestEnemy = FindNearestEnemy();
        }

        Destroy(gameObject);
    }

    private Enemy FindNearestEnemy()
    {
        Enemy nearestEnemy = null;
        float nearestDistance = float.MaxValue;

        foreach (Enemy enemy in player.gameManager.enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            
            if (distance < range && distance < nearestDistance)
            {
                print(distance);
                nearestEnemy = enemy.GetComponent<Enemy>();
                nearestDistance = distance;
            }
        }

        return nearestEnemy;
    }

    private IEnumerator MoveToTarget(Vector2 targetPosition, float moveDuration)
    {
        Vector2 startPosition = transform.position;
        float elapsedTime = 0.0f;

        while (elapsedTime < moveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / moveDuration);

            transform.position = Vector2.Lerp(startPosition, targetPosition, t);

            yield return null;
        }

        // точное положение целевой точки
        transform.position = targetPosition;
    }


}

