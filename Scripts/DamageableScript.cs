using UnityEngine;

public class DamageableScript : MonoBehaviour
{
    public float health;
    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f){
            Die();
        }
    }
    void Die(){
        Destroy(gameObject);
    }
}
