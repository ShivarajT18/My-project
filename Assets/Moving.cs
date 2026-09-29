using UnityEngine;

public class Moving : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 8f;

    public LayerMask groundLayer;
    public float groundRadius = 0.25f;

    private Rigidbody2D rb;
    private Transform groundCheck;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        // Auto create GroundCheck
        groundCheck = transform.Find("GroundCheck");
        if (groundCheck == null)
        {
            GameObject gc = new GameObject("GroundCheck");
            gc.transform.SetParent(transform);
            gc.transform.localPosition = new Vector3(0, -0.6f, 0);
            groundCheck = gc.transform;
        }
    }

    void Start()
    {
        rb.gravityScale = 3f;     // 🔴 MUST be > 0
        rb.freezeRotation = true;
    }

    void Update()
    {
        // Left / Right movement
        float h = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(h * moveSpeed, rb.linearVelocity.y);

        // Ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            groundRadius,
            groundLayer
        );

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}
