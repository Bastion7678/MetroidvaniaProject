using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class playermovement : MonoBehaviour
{
    public float movespeed;
    public float jumpforce;
    public bool isHoldingJump;
    public bool isJumpHeld;
    public bool isGrounded;
    public bool canJumpAgain;
    private float horizontal;
    private float speed = 8f;

    [SerializeField] private Transform foot;
    [SerializeField] private Transform jump;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private TrailRenderer tr;

    private Rigidbody2D rigidBody;
    private Vector2 move;

    public bool isDead;

    public Vector2 MoveAmount;

    [Header("Dashing")]
    public bool DashUnlocked;
    private bool isDashing;
    private float dashpower = 24f;
    public bool canDash;

    private void Update()
    {

    }

    public void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        canJumpAgain = true;
    }

    public void Move(InputAction.CallbackContext context)
    {
       MoveAmount = context.ReadValue<Vector2>();
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHoldingJump = true;
            Debug.Log("Press has started");
        }
        if (context.performed)
        {
            isHoldingJump = false;
            Debug.Log("Held for longer");
        }
        isJumpHeld = context.ReadValue<float>() > 0;

        //rigidBody.AddForce(jumpforce * Vector2.up, ForceMode2D.Impulse);

    }

    void JumpCondition()
    {
        bool isOnGround = IsGrounded();

            if (isJumpHeld)
            {
                if (isOnGround)
                {
                    if (canJumpAgain)
                    {
                        rigidBody.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                            canJumpAgain = false;
                            StartCoroutine(DelayedJump());
                    }
                    else if (isHoldingJump)
                    {
                        rigidBody.AddForce(new Vector2(0, jumpforce));
                    }
                }
            }
        
    }

    private void FixedUpdate()
    {
        JumpCondition();

        if (!isDead)
        {
            Walking(); 
        }

    }

    public void Walking()
    {
        rigidBody.linearVelocity = new Vector2(MoveAmount.x * movespeed, rigidBody.linearVelocity.y);
    }

    public bool IsGrounded()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundMask);
    }
   
    public void ReturnToStable()
    {

    }


    public void Dash(InputAction.CallbackContext context)
    {
        if (DashUnlocked)
        {
            if (context.started)
            {
                if (canDash)
                {
                   rigidBody.AddForce(new Vector2(dashpower * transform.localScale.x, 50), ForceMode2D.Impulse);
                   canDash = false;
                   StartCoroutine(DashDelay());
                }
                
            }
        }
    }
    
    IEnumerator DashDelay()
    {
        yield return new WaitForSeconds(1f);
        canDash = true;
    }


    IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(.15f);
        canJumpAgain = true;
    }
}
