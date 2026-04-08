using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xClampRange = 3f;
    [SerializeField] float zClampRange = 3f;
    [SerializeField] float jumpHeight = 2f;
    [SerializeField] float jumpDuration = 0.8f;
    [SerializeField] float groundY = 0f; 
    Vector2 movement;
    Rigidbody rigidBody;
    bool isJumping = false;
    float jumpTimer = 0f;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        MovePosition();
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && !isJumping)
        {
            isJumping = true;
            jumpTimer = 0f;
        }
    }

    void MovePosition()
    {
        Vector3 currentPosition = rigidBody.position;

        Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (moveSpeed * Time.fixedDeltaTime);

        float y = groundY;

        if (isJumping)
        {
            jumpTimer += Time.fixedDeltaTime;

            float normalizedTime = jumpTimer / jumpDuration;

            if (normalizedTime >= 1f)
            {
                normalizedTime = 1f;
                isJumping = false;
            }

            y = groundY + 4f * jumpHeight * normalizedTime * (1f - normalizedTime);
        }

        newPosition.y = y;
        newPosition.x = Mathf.Clamp(newPosition.x, -xClampRange, xClampRange);
        newPosition.z = Mathf.Clamp(newPosition.z, -zClampRange, zClampRange);

        rigidBody.MovePosition(newPosition);
    }

}