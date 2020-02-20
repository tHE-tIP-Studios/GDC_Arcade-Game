using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), (typeof(CapsuleCollider2D)))]
public class CharacterController : MonoBehaviour
{
    [Foldout("Movement Parameters")]
    [SerializeField] private float moveSpeed = 20;
    [Foldout("Movement Parameters")]
    [SerializeField] private LayerMask groundMask = default;
    [Foldout("Movement Parameters")]
    [SerializeField] private float jumpSpeed = 100;
    [Foldout("Movement Parameters")]
    [SerializeField] private float jumpSustain = 0.0f;
    [Foldout("Movement Parameters")]
    [SerializeField] private float coyoteTime = 0.1f;
    [Foldout("Movement Parameters")]
    [Range(0.0f, 1.0f)]
    [SerializeField] private float switchDirectionDrag = 0.0f;
    [Foldout("Movement Parameters")]
    [SerializeField] private float gravityJumpMultiplier = 4.0f;

    [Header("References")]
    [SerializeField] private GameObject groundPoint = null;

    [Header("Controls")]
    [SerializeField] private string xAxis = "Horizontal";
    [SerializeField] private string jump = "Jump";

    private Collider2D coyoteCol = default;
    private ContactFilter2D groundContact = default;
    private Rigidbody2D rb = default;
    private Vector2 currentVelocity = default;
    private Vector2 movementDir = default;
    private float jumpTime = default;
    private bool jumpPressed = false;
    private float timeOfLanding = 0.0f;

    private Vector2 groundPosition
    {
        get
        {
            return (groundPoint) ? groundPoint.transform.position : transform.position;
        }
    }

    private Vector2 velocity
    {
        get
        {
            return rb.velocity;
        }
        set
        {
            rb.velocity = value;
        }
    }

    private float gravity
    {
        get
        {
            return rb.gravityScale;
        }
        set
        {
            rb.gravityScale = value;
        }
    }

    public bool isGrounded
    {
        get
        {
            if (coyoteCol)
            {
                if (coyoteCol.enabled)
                {
                    Collider2D[] colliders = new Collider2D[32];
                    return (Physics2D.OverlapCollider
                        (coyoteCol, groundContact, colliders) > 0);
                }
            }

            Vector2 gp = groundPosition;
            return Physics2D.OverlapPoint(gp, groundMask);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (groundPoint) coyoteCol = groundPoint.GetComponent<Collider2D>();
        else coyoteCol = null;

        groundContact = new ContactFilter2D();
        groundContact.SetLayerMask(groundMask);
    }

    private void FixedUpdate()
    {
        GoFast();
        GoUp();
    }

    private void GoFast()
    {
        currentVelocity = velocity;

        if (Mathf.Abs(movementDir.x) > 0.01f)
        {
            currentVelocity.x = movementDir.x * moveSpeed;
        }
        else
        {
            currentVelocity.x *= (1.0f - switchDirectionDrag);
        }

        if (movementDir.y > 0.0f)
        {
            Debug.Log("Jump");
            currentVelocity.y = jumpSpeed;

            movementDir.y = 0.0f;
        }

        velocity = currentVelocity;
        currentVelocity = velocity;
    }

    private void GoUp()
    {
        if (jumpPressed)
        {
            if(currentVelocity.y > 0.01f)
            {
                if ((Time.time - jumpTime) > jumpSustain)
                {
                    gravity = gravityJumpMultiplier;
                }
            }
            else gravity = gravityJumpMultiplier;
        }
        else gravity = gravityJumpMultiplier;

        if (Mathf.Abs(currentVelocity.y) > 0.01f)
        {
            coyoteCol.enabled = false;            
        }
        else
        {
            coyoteCol.enabled = true;

            if (Physics2D.OverlapCircle(groundPosition, 1.0f, groundMask))
            {
                timeOfLanding = Time.time;
            }
            else
            {
                if ((Time.time - timeOfLanding) > coyoteTime)
                {
                    coyoteCol.enabled = false;
                }
            }
        }
    }

    void Update()
    {
        bool grounded = isGrounded;
        Vector2 right = transform.right;

        movementDir.x = Input.GetAxis(xAxis);
        if (grounded) gravity = 1.0f;

        if (Input.GetButtonDown(jump))
        {
            if(Mathf.Abs(currentVelocity.y) < 0.01f)
            {
                if (grounded)
                {
                    Debug.Log("Jump");
                    movementDir.y = 1.0f;
                    jumpTime = Time.time;
                    gravity = 1.0f;
                    jumpPressed = true;

                    // Play jump sound
                }
            }
        }
        else if (Input.GetButton(jump))
        {
            jumpPressed = true;
        }
        else
        {
            jumpPressed = false;
        }

        if (Vector2.Dot(right, movementDir) < 0.0f)
        {
            if (movementDir.x > 0.01f) transform.rotation = Quaternion.identity;
            else if (movementDir.x < -0.01f) transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
    }
}
