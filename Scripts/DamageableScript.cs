using UnityEngine;

public class DamageableScript : MonoBehaviour
{
    public float health;
    [SerializeField] private GameObject[] spawnAfterDie;
    [SerializeField] private int amountSpawned;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            for (int i = 0; i < amountSpawned; i++)
            {
                Instantiate(spawnAfterDie[Random.Range(0, spawnAfterDie.Length)], transform.position, Quaternion.identity);
            }
            Die();
        } 
    }
    void Die(){
        Destroy(gameObject);
    }
}