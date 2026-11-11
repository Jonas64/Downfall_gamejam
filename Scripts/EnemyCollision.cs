using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 2;
    public float knockbackForce = 6f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Push the player away from the enemy, with a small upward kick
            float side = Mathf.Sign(collision.transform.position.x - transform.position.x);
            Vector2 knockback = new Vector2(side, 0.5f).normalized * knockbackForce;

            playerHealth.TakeDamage(damage, knockback);
        }
    }
}