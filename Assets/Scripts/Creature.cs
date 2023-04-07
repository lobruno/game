using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Creature: MonoBehaviour
{
    // Метод, обрабатывающий получение урона.
    public virtual void TakeDamage(float damage)
    {
        
    }

    // Метод, вызываемый при смерти персонажа.
    protected virtual void Die()
    {
        // реализация для конкретного класса
    }

    //Метод, вызываемый для атаки персонажа
    protected virtual void Attack()
    {
        // реализация для конкретного класса
    }

    // Метод, возвращающий позицию персонажа на экране.
    public virtual Vector2 GetPosition()
    {
        return new Vector2();
    }
 }
