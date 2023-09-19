using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 7;
    [SerializeField] private float jumpForce = 12;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float dashDuration = 1f;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] private Collider2D attackCollider;
    [SerializeField] private Animator anim;

    [SerializeField] private int obstacleLayerIndex = 6; 
    private Rigidbody2D _rb;
    private float coyoteTimeCounter;
    private bool isGrounded;
    private float jumpBufferCounter;
    private bool isSliding = false;
    private bool isJumping;
    private bool isRunning;
    private bool isAttacking;
    private bool isDashing;
    private Vector2 _currentForceVelocity;

    public float Speed => speed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        
    }

    private void OnEnable()
    {
        GameManager.instance.OnGameStarted += GameStart;
    }

    public void GameStart()
    {
        //_rb.velocity = Vector3.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.IsStarted) return;
        
        if (isGrounded)
        {
            if(isJumping) return;
            
            if (!isSliding)
            {
                anim.SetBool("Running", true);
            }
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            if (coyoteTimeCounter < -0-1f) return; 
            coyoteTimeCounter -= Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.DownArrow) && !isSliding) Slide();
        else if(Input.GetKeyUp(KeyCode.DownArrow)) StopSlide();

        if (Input.GetKeyDown(KeyCode.C) && !isDashing) StartCoroutine(Dash());

        if (Input.GetKeyDown(KeyCode.X) && !isSliding && !isAttacking)
        {
            Attack();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            if (jumpBufferCounter < -0.1f) return;
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f && !isJumping)
        {
            Jump();
            jumpBufferCounter = 0f;
            
            if (!Input.GetKeyDown(KeyCode.Z))
            {
                _rb.velocity = new Vector2(0, _rb.velocity.y * 0.5f);
            }
        }

        if (Input.GetKeyUp(KeyCode.Z) && _rb.velocity.y > 0f)
        {
            _rb.velocity = new Vector2(0, _rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
    }

    private void FixedUpdate()
    {
        //_rb.velocity = Vector2.right * speed * Time.fixedDeltaTime + Vector2.up * _rb.velocity.y;
           isGrounded = Physics2D.Raycast(groundCheck.position, -transform.up, 0.1f, groundLayer);
    }
    public bool GroundCheck()
    {
        return Physics2D.Raycast(groundCheck.position, -transform.up, 0.1f, groundLayer);
    }

    public void Attack()
    {
        isAttacking = true;
        anim.SetTrigger("Attack");
        attackCollider.enabled = true;
    }

    public void StopAttack()
    {
        isAttacking = false;
        attackCollider.enabled = false;
    }

    public void Jump()
    {
        isGrounded = false;
        anim.SetTrigger("Jump");
        anim.SetBool("Running", false);
        _rb.velocity = Vector2.up * jumpForce;
        
        
        StartCoroutine(JumpCooldown());
    }
    
    private IEnumerator JumpCooldown()
    {
        isJumping = true;
        yield return new WaitForSeconds(0.4f);
        isJumping = false;
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        var originalGravity = _rb.gravityScale;
        _rb.gravityScale = 0f;
        _rb.velocity = Vector2.zero;
        
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
        
        _rb.gravityScale = originalGravity;
    }

    public void Slide()
    {
        if (!isGrounded) return;
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 0.5f);
        GetComponent<CapsuleCollider2D>().offset = new Vector2(0, -0.25f);
        _rb.AddForce(Vector3.down * 2f);
        isSliding = true;
        anim.SetBool("Sliding", true);
        anim.SetBool("Running", false);
    }

    public void StopSlide()
    {
        if (!isSliding) return;
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.5f, 1f);
        GetComponent<CapsuleCollider2D>().offset = Vector2.zero;
        isSliding = false;

        anim.SetBool("Sliding", false);
        
        if(!isGrounded) anim.SetTrigger("Jump");
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == obstacleLayerIndex)
        {
            GameManager.instance.GameOver();
        }
    }
}

public enum InputType
{
    Jump,
    Dash,
    Slide,
    Attack
}
