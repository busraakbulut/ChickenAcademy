using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    InputActionReference movementControl;
    [SerializeField]
    InputActionReference jumpControl;
    [SerializeField]
    float playerSpeed = 2.0f;
    [SerializeField]
    float jumpHeight = 1.0f;
    [SerializeField]
    float gravityValue = -9.81f;
    [SerializeField]
    float rotationSpeed=4;

    CharacterController controller;
    Vector3 playerVelocity;
    bool groundedPlayer;
    Transform cameraMainTransform;

    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraMainTransform = Camera.main.transform;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector2 movement = movementControl.action.ReadValue<Vector2>();
        Vector3 move = new Vector3(movement.x, 0, movement.y);
        move = cameraMainTransform.forward * move.z + cameraMainTransform.right * move.x;
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        

        // Changes the height position of the player..
        if (jumpControl.action.triggered && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if (movement!=Vector2.zero)
        {
            float targetAngle = Mathf.Atan2(movement.x, movement.y) * Mathf.Rad2Deg+cameraMainTransform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0f, targetAngle, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);
        }


    }


    void OnEnable()
    {
        movementControl.action.Enable();
        jumpControl.action.Enable();
    }

    void OnDisable()
    {
        movementControl.action.Disable();
        jumpControl.action.Disable();
    }

}
