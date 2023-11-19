using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpHeight = 5f;
    [SerializeField] float climbingSpeed = 1f;
    [SerializeField] Vector2 deathJump = new Vector2(5f, 5f);
    float originalGravityScale;
    bool isAlive = true;

    Rigidbody2D myRigidbody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        originalGravityScale = myRigidbody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return;  }
        Run();
        Jump();
        ClimbLadder();
        CheckForDirection();
        Die();
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            myAnimator.SetTrigger("isDead");
            myRigidbody.velocity = new Vector2(deathJump.x, deathJump.y);
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    private void Run()
    {
        float controlThrow = Input.GetAxis("Horizontal") * moveSpeed;
        myRigidbody.velocity = new Vector2(controlThrow, myRigidbody.velocity.y);

        bool isRunning = (Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon);
        myAnimator.SetBool("isRunning", isRunning);
    }

    private void Jump()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }

        if (Input.GetButtonDown("Jump"))
        {
            Vector2 jumpVelocity = new Vector2(0f, jumpHeight);
            myRigidbody.velocity += jumpVelocity;
        }
    }

    private void ClimbLadder()
    {
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladders")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = originalGravityScale;
            return;
        }

        myRigidbody.gravityScale = 0f;
        float controlThrow = Input.GetAxis("Vertical") * climbingSpeed;
        myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, controlThrow);
        bool isClimbing = (Mathf.Abs(controlThrow) > Mathf.Epsilon);
        myAnimator.SetBool("isClimbing", isClimbing);
    }

    private void CheckForDirection()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        if (playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

}
