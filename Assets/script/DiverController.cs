using UnityEngine;

using UnityEngine.InputSystem;
public class DiverController : MonoBehaviour
{

    #region coder & project
    /// <summary>
    /// NSCC GAME2025 / 4086 / Procedural Gen III(B)/ Doucette,Matthew
    /// Unity:emrgent behavior
    /// Coder current script: Chris French Second Year NSCC Game Programming 
    /// Additions / annotations:
    /// </summary>
    #endregion


        [SerializeField] public float moveSpeed = .1f;
        [SerializeField] public float MinSpeed = 0f;
        [SerializeField] public float MaxSpeed = 15f;
    [SerializeField] public float Accelerate = 1f;
        private DiverControls inputActions;
        private Vector2 moveInput;
        private bool AccelerationInput;
         private CharacterController characterController;
    private void Awake()
        {
        AccelerationInput= false;  
        inputActions = new DiverControls();
        characterController = GetComponent<CharacterController>();
        }
        private void OnEnable()
        {
            inputActions.diver.Enable();
            inputActions.diver.move.performed += OnMoveInput;
            inputActions.diver.move.canceled += OnMoveStopped;
            inputActions.diver.Acceleration.performed += OnAccelerationInput;
            inputActions.diver.Acceleration.canceled += OnAccelerationopped;
    }
        private void OnDisable()
        {
            inputActions.diver.move.performed -= OnMoveInput;
            inputActions.diver.move.canceled -= OnMoveStopped;
            inputActions.diver.Acceleration.performed += OnAccelerationInput;
            inputActions.diver.Acceleration.canceled += OnAccelerationopped;
        inputActions.diver.Disable();
        }
        private void Update()
        {
            MoveDiver();
        }
        private void OnMoveInput(InputAction.CallbackContext context)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        private void OnMoveStopped(InputAction.CallbackContext context)
        {
            moveInput = Vector2.zero;
        }

    private void OnAccelerationInput(InputAction.CallbackContext context)
    {
        if (AccelerationInput == true)
        {
            if (moveSpeed < MaxSpeed )
            {
                moveSpeed = Accelerate++;
            }
        }
        else
        {

            moveSpeed =MaxSpeed;
        }
    }

        private void OnAccelerationopped(InputAction.CallbackContext context)
        {
        if (AccelerationInput == false)
        {
            if (moveSpeed > MinSpeed)
            {
                moveSpeed = Accelerate--;
            }
        }
        else
        {

            moveSpeed = MinSpeed;
        }
    }




    private void MoveDiver()
        {
            Vector3 direction = new Vector3(moveInput.x, moveSpeed, moveInput.y);
        characterController.Move(direction * moveSpeed * Time.deltaTime);
        }
    }

 
