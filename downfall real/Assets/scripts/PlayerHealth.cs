using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxhealth = 5;

    public Animator animator;
    public Rigidbody2D rb;
    public float deathAnimationLength = 1f;
    public float hurtDuration = 0.4f;
    public Vector2 respawnPoint = new Vector2(0f, 0f);

    // Other scripts (e.g. movement) can check this to stop overriding the knockback
    public bool isHurt { get; private set; }
    public bool isDead { get; private set; }

    void Start()
    {
        health = maxhealth;

        if (animator == null) animator = GetComponent<Animator>();
        if (rb == null) rb = GetComponent<Rigidbody2D>();
    }

    // Keeps old calls working
    public void TakeDamage(int amount)
    {
        TakeDamage(amount, Vector2.zero);
    }

    public void TakeDamage(int amount, Vector2 knockback)
    {
        if (isDead || isHurt) return; // brief invincibility while hurt

        health -= amount;

        if (health <= 0)
        {
            StopAllCoroutines();
            StartCoroutine(Die());
        }
        else
        {
            StartCoroutine(Hurt(knockback));
        }
    }

    private IEnumerator Hurt(Vector2 knockback)
    {
        isHurt = true;

        // Husk animasjonen som spilte før skaden
        int previousState = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;

        animator.Play("PlayerHurt");

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(knockback, ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(hurtDuration);

        isHurt = false;

        // Gå tilbake til samme animasjon som før
        animator.Play(previousState);
    }

    private IEnumerator Die()
    {
        isDead = true;
        isHurt = false;

        Debug.Log("Die started. Waiting 3.5 seconds");

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false; // stops physics and collisions while dead
        }

        animator.Play("PlayerDeath");

        yield return new WaitForSeconds(3.5f);

        Debug.Log("Respawning now");

        transform.position = new Vector3(respawnPoint.x, respawnPoint.y, transform.position.z);

        if (rb != null) rb.simulated = true;

        health = maxhealth;
        isDead = false;

        animator.Play("PlayerIdleDown");
    }

    private static readonly int deathHash = Animator.StringToHash("PlayerDeath");

    void LateUpdate()
    {
        // Hvis spilleren er død, og noe annet har byttet animasjon, tving tilbake til PlayerDeath
        if (isDead && animator.GetCurrentAnimatorStateInfo(0).shortNameHash != deathHash)
        {
            animator.Play(deathHash);
        }
    }
}