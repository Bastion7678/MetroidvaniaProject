using System.Collections;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.InputSystem;

public class playermovement : MonoBehaviour
{
    public float movespeed;
    public float jumpforce;
    public bool isHoldingJump;
    public bool isJumpHeld;
    public bool isGrounded;
    public bool canJumpAgain;

    [SerializeField]
    private Transform foot;
    [SerializeField]
    private LayerMask groundMask;

    private Rigidbody2D rigidBody;
    private Vector2 move;

    private bool CheckGrounding()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(foot.position, Vector2.down, 1.0f, groundMask);

        return hit;
    }
    public void Awake()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        rigidBody.linearVelocity = new Vector2(context.ReadValue<Vector2>().x * movespeed, rigidBody.linearVelocity.y);
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

        rigidBody.AddForce(jumpforce * Vector2.up, ForceMode2D.Impulse);

    }

    void JumpCondition()
    {
       
        
           bool isOnGround = IsGrounded(1.1f);

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
                    else if (isJumpHeld)
                    {
                        rigidBody.AddForce(new Vector2(0, jumpforce));
                    }
                }
            }
        
    }

    private void FixedUpdate()
    {
        JumpCondition();
    }

    public void MoveInput(InputAction.CallbackContext context)
    {

    }

    public void JumpInput(InputAction.CallbackContext context)
    {

    }

    public void Walking()
    {

    }

    public bool IsGrounded(float distance)
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(this.transform.position, Vector2.down, distance, groundMask);
        Debug.DrawRay(this.transform.position, Vector2.down, Color.red);

        return hit;
    }

    public void ReturnToStable()
    {

    }
    
    IEnumerator DelayedJump()
    {
        yield return new WaitForSeconds(.15f);
        canJumpAgain = true;
    }



}
