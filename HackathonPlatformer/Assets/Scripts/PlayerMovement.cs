using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    private float dirx = 0f;
    private bool dJump = true;
    [SerializeField]private float moveSpeed = 7f;
    [SerializeField]private float jumpForce = 14f;
    
    private enum MovementState {idle, running, jumping, falling, hurt, dubJump}
    private MovementState state = MovementState.idle;

    [SerializeField] private AudioSource jumpSFX;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (anim.GetInteger("state") != 4)
        {
            Movement();
            UpdateAnimationState();
        }

    }

    private void Movement()
    {
        dirx = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirx * moveSpeed, rb.velocity.y);

        if(isGrounded())
        {
            dJump = true;
        }
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            jumpSFX.Play();
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        else if (Input.GetButtonDown("Jump") && dJump)
        {
            jumpSFX.Play();
            dJump = false;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
    private void UpdateAnimationState()
    {
        if (dirx > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirx < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else 
        {
            state = MovementState.idle;
        }
        if (rb.velocity.y > .1f && dJump)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y > .1f)
        {
            state = MovementState.dubJump;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }
        anim.SetInteger("state", (int)state);
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }
}
