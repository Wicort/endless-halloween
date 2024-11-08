using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform cameraPosition;
    public float mouseSensivity = 5.0f;
    public float attackSpeed = 5f;
    public float movementSpeed = 5f;
    public float jumpHeight = 2f;
    public Animator animator;
    public float attackMaxTimeout = 1f;
    public Inventory Inventory;
    public float attackTimeout = 0f;

    private float verticalAngle = 0f;
    private float horizontalAngle = 0f;

    private float verticalSpeed = 0f;
    private float playerSpeed;
    private bool _playerIsMoving;

    private CharacterController characterController;


    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleMovement();
        HandleMouseLock();
        HandleAttack();
    }

    private void HandleMovement()
    {
        float speed = playerSpeed;

        float moveHorizontal = SimpleInput.GetAxis("Horizontal");
        float moveVertical = SimpleInput.GetAxis("Vertical");
        _playerIsMoving = moveHorizontal != 0 || moveVertical != 0;

        Vector3 moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        if (moveDirection.magnitude > 1f)
        {
            moveDirection.Normalize();
        }

        moveDirection = transform.TransformDirection(moveDirection);

        moveDirection *= speed * Time.deltaTime;

        if (characterController.isGrounded)
        {
            if (SimpleInput.GetButtonDown("Jump"))
            {
                verticalSpeed = Mathf.Sqrt(2f + jumpHeight * Mathf.Abs(Physics.gravity.y));
            }
        }

        verticalSpeed += Physics.gravity.y * Time.deltaTime;

        moveDirection.y = verticalSpeed * Time.deltaTime;

        if (animator != null)
        {
            animator.SetFloat("Strafe", moveHorizontal);
            animator.SetFloat("Forvard", moveVertical);
        }

        characterController.Move(moveDirection);
    }

    private void HandleMouseLock()
    {
        float mouseX = SimpleInput.GetAxis("Mouse X") * mouseSensivity;
        float mouseY = -SimpleInput.GetAxis("Mouse Y") * mouseSensivity;

        horizontalAngle += mouseX;
        verticalAngle = Mathf.Clamp(verticalAngle + mouseY, 10f, 20f);

        transform.localRotation = Quaternion.Euler(0f, horizontalAngle, 0f);
        cameraPosition.localRotation = Quaternion.Euler(verticalAngle, 0f, 0f);
    }

    private void HandleAttack()
    {
        if (attackTimeout > 0f) attackTimeout = Mathf.Clamp(attackTimeout - Time.deltaTime, 0, attackMaxTimeout);
        if (attackTimeout < 0f)
        {
            attackTimeout = 0f;
        }
        if (attackTimeout == 0f)
        {
            animator.SetFloat("Attacking", 0);
            playerSpeed = movementSpeed;
        }

        if (attackTimeout == 0f)
        {
            Collider[] _hitColliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1f);
            foreach (Collider hit in _hitColliders)
            {
                if (hit.TryGetComponent(out ItemSourceView source))
                {
                    int damage = 1;

                    if (source != null)
                    {
                        damage = source.GetDamage(damage, transform);

                        if (damage > 0)
                        {
                            attackTimeout = attackMaxTimeout;
                            animator.SetFloat("Attacking", _playerIsMoving ? .65f : 2f);
                            playerSpeed = attackSpeed;
                            Inventory.ChangeItemAmount(source.Resource.type, damage);
                        }
                    }

                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward + transform.up, 1f);
    }

}
