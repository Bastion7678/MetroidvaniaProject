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

    private Rigidbody2D rb;
    private Vector2 move;

    private bool CheckGrounding()
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(foot.position, Vector2.down, 1.0f, groundMask);

        return hit;
    }
    public void Awake()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        rb.linearVelocity = new Vector2(context.ReadValue<Vector2>().x * movespeed, rb.linearVelocity.y);
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

        rb.AddForce(jumpforce * Vector2.up, ForceMode2D.Impulse);

    }

    void JumpCondition()
    {
        if (this.gameobject.GetComponent<PlayerStats().currently == false && this.gameobject.GetComponet<PlayerStats>.AbleToMove == true)
        {
            isOnGround = IsGrounded(1.1f);

            if (isJumpHeld)
            {
                if (isOnGround)
                {
                    if (canJumpAgain)
                    {
                        rb.AddForce(new Vector2(0, jumpforce), ForceMode2D.Impulse);
                        canJumpAgain = false;
                        StartCoroutine(DelayedJump());
                    }
                    else if (isJumpHeld)
                    {
                        rb.AddForce(new Vector2(0, jumpforce));
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        JumpCondition();
    }

    public void MoveInput(InputAction.CallbackContext context)

    public void JumpInput(InputAction.CallbackContext context)

    public void Walking()

    public bool IsGrounded(float distance)
    {
        RaycastHit2D hit;

        hit = Physics2D.Raycast(this.transform.position, Vector2.down, distance, groundMask);
        Debug.DrawRay(this.transform.position, Vector2.down, Color.red);

        return hit;
    }

    public void ReturnToStable

    IEnumerator playermovement.DelayedJump()
    {
        yield return new WaitForSeconds(.15f);
        canJumpAgain = true;
    }



}
