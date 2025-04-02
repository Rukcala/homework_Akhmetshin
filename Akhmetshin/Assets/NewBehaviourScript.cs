using UnityEngine;

public class CubeController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Ground Check")]
    public float groundCheckDistance = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheckPoint;

    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (groundCheckPoint == null)
            groundCheckPoint = transform;
    }

    void Update()
    {
        // Проверка земли через луч
        isGrounded = Physics.Raycast(groundCheckPoint.position,
                                   Vector3.down,
                                   groundCheckDistance,
                                   groundLayer);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            Debug.Log("Прыжок выполнен!"); // Для отладки
        }

        // Движение
        float moveX = Input.GetAxis("Horizontal") * moveSpeed;
        float moveZ = Input.GetAxis("Vertical") * moveSpeed;
        rb.velocity = new Vector3(moveX, rb.velocity.y, moveZ);
    }

    // Визуализация луча для отладки
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(groundCheckPoint.position,
                       groundCheckPoint.position + Vector3.down * groundCheckDistance);
    }
}