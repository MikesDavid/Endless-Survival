using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //[SerializeField] private Testing testing;
    public static List<Target> enemyList = new List<Target>();


    [SerializeField]private LevelSystemManager levelSystemManager;
    public float health = 50f;

    private void Awake()
    {
        enemyList.Add(this);
        levelSystemManager = FindObjectOfType<LevelSystemManager>();
    }
    private void FixedUpdate()
    {
        Debug.Log(health);
    }

    public void TakeDamage(float amount )
    {
        health -= amount;

        if (health <= 0f)
        {
            levelSystemManager.enemyKilled++;

            Die();

        }
    }


    public void Die()
    {

        Destroy(gameObject);
    }

}
