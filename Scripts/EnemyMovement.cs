using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed;

    public float startHealth = 100;
    private float health;

    public int value = 50;

    //math variables
    [Header("Math Stuff")]
    public float enemyVar;
    public Text variableText;


    [Header("Unity Stuff")]
    public GameObject deathEffect;
    public Image healthBar;

    private bool isDead = false;

    private void Start()
    {
        speed = startSpeed;
        health = startHealth;
        variableText.text = enemyVar.ToString();
    }

    //change this for exact health
    public void TakeDamage(float amount, float dmgVar)
    {
        if(enemyVar == dmgVar)
        {
            health -= amount;

            healthBar.fillAmount = health / startHealth;

            if (health <= 0 && !isDead)
            {
                Die();
            }
        }
    }
    public void Slow(float amount, float dmgVar)
    {
        if(enemyVar == dmgVar)
        {
            speed = startSpeed * (1f - amount);
        }
    }

    void Die()
    {
        isDead = true;

        PlayerStats.Money += value;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;

        Destroy(gameObject);
    }
}
