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


    public class PlayerController : MonoBehaviour
    {
        [SerializeField] public float moveSpeed = 25f;
        private DiverControls inputActions;
        private Vector2 moveInput;
        private CharacterController diverController;
        private void Awake()
        {
            inputActions = new DiverControls();
            diverController = GetComponent<CharacterController>();
        }
        private void OnEnable()
        {
            inputActions.diver.Enable();
            inputActions.diver.move.performed += OnMoveInput;
            inputActions.diver.move.canceled += OnMoveStopped;
        }
        private void OnDisable()
        {
            inputActions.diver.move.performed -= OnMoveInput;
            inputActions.diver.move.canceled -= OnMoveStopped;
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
        private void MoveDiver()
        {
            Vector3 direction = new Vector3(moveInput.x, 25f, moveInput.y);
           diverController.Move(direction * moveSpeed * Time.deltaTime);
        }
    }
}
 
