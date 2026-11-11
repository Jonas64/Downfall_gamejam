using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private enum Facing { Down, Up, Side }

    [Header("Movement")]
    public float moveSpeed = 7f;

    private Rigidbody2D rigidBody;
    private Vector2 moveInput;

    // The side animations (PlayerRunLeft / PlayerIdleLeft) are drawn facing LEFT.
    // At rotation 0 the player therefore faces left. We rotate 180 degrees to face right.
    // If your sprite is actually drawn facing right, change this to true.
    private bool isFacingRight = false;

    private Facing facing = Facing.Down;
    private Animator animator;
    private string currentState;
    private bool isDead = false;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Top-down movement: no gravity, and don't let physics rotate the player.
        rigidBody.gravityScale = 0f;
        rigidBody.freezeRotation = true;
    }

    void Update()
    {
        //Are we dead? Then the rest of Update doesn't run and we can't move.
        if (isDead == true)
        {
            return;
        }

        ReadInput();
        UpdateFacing();
        Flip();
        UpdateAnimations();
    }

    void ReadInput()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        // Read movement input (WASD + arrow keys).
        float x = 0f;
        float y = 0f;

        if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
        {
            x -= 1f;
        }

        if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
        {
            x += 1f;
        }

        if (keyboard.sKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            y -= 1f;
        }

        if (keyboard.wKey.isPressed || keyboard.upArrowKey.isPressed)
        {
            y += 1f;
        }

        // Normalize so diagonal movement isn't faster than straight movement.
        moveInput = new Vector2(x, y).normalized;
    }

    // Remember which way the player is facing, so the right idle animation
    // is used when the player stops. Sideways wins when moving diagonally.
    private void UpdateFacing()
    {
        if (moveInput.x != 0f)
        {
            facing = Facing.Side;
        }
        else if (moveInput.y > 0f)
        {
            facing = Facing.Up;
        }
        else if (moveInput.y < 0f)
        {
            facing = Facing.Down;
        }
    }

    void FixedUpdate()
    {
        //Are we dead? Then the rest of FixedUpdate doesn't run and we stop physics working on us.
        if (isDead == true)
        {
            return;
        }

        // Control movement in all directions.
        rigidBody.linearVelocity = moveInput * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Are we dead? Can't collide with stuff if dead.
        if (isDead == true)
        {
            return;
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collision to enemy");

            StartCoroutine(PlayerDeath());
        }
    }

    // Mirrors the left-facing animations when walking right.
    private void Flip()
    {
        if (isFacingRight && moveInput.x < 0f || !isFacingRight && moveInput.x > 0f)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private IEnumerator PlayerDeath()
    {
        if (isDead == true)
        {
            yield break;
        }

        isDead = true;

        // Clear any stored input.
        moveInput = Vector2.zero;

        // Stop the player's movement.
        rigidBody.linearVelocity = Vector2.zero;

        // Only plays if you add a state called "PlayerDeath" to the Animator.
        ChangeAnimation("PlayerDeath");

        yield return new WaitForSeconds(1);

        //Restart level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateAnimations()
    {
        bool isMoving = moveInput != Vector2.zero;

        // State names must match the Animator exactly.
        // Right-facing uses the Left animations (mirrored by Flip).
        string state;

        switch (facing)
        {
            case Facing.Up:
                state = isMoving ? "PlayerRunUp" : "PlayerIdleUp";
                break;
            case Facing.Side:
                state = isMoving ? "PlayerRunLeft" : "PlayerIdleLeft";
                break;
            default:
                state = isMoving ? "PlayerRunDown" : "PlayerIdleDown";
                break;
        }

        ChangeAnimation(state);
    }

    private void ChangeAnimation(string newState)
    {
        if (currentState == newState)
        {
            return;
        }

        // Skip states that don't exist in the Animator, to avoid warnings spamming the Console.
        if (!animator.HasState(0, Animator.StringToHash(newState)))
        {
            return;
        }

        animator.Play(newState);
        currentState = newState;
    }
}