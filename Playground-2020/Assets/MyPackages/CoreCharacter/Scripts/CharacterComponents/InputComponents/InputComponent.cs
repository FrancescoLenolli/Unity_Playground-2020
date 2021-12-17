using System;
using UnityEngine;

namespace CoreCharacter
{
    public class InputComponent : CharacterComponent
    {
        private Vector2 moveInput = Vector2.zero;
        private Vector2 rotateInput = Vector2.zero;
        private Action<Vector3> onMovementInput;
        private Action<Vector2> onRotationInput;
        private Action onActionJump;
        private Action onAction1;
        private Action onAction2;
        private Action onContinuousActionJump;
        private Action onContinuousAction1;
        private Action onContinuousAction2;

        public Action<Vector3> OnActionMove { get => onMovementInput; set => onMovementInput = value; }
        public Action<Vector2> OnActionRotate { get => onRotationInput; set => onRotationInput = value; }
        public Action OnActionJump { get => onActionJump; set => onActionJump = value; }
        public Action OnAction1 { get => onAction1; set => onAction1 = value; }
        public Action OnAction2 { get => onAction2; set => onAction2 = value; }
        public Action OnContinuousActionJump { get => onContinuousActionJump; set => onContinuousActionJump = value; }
        public Action OnContinuousAction1 { get => onContinuousAction1; set => onContinuousAction1 = value; }
        public Action OnContinuousAction2 { get => onContinuousAction2; set => onContinuousAction2 = value; }

        private void Update()
        {
            HandleMovement(GetMoveInput());
            HandleRotation();
            HandleAction(Input.GetButtonDown("Jump"), onActionJump);
            HandleAction(Input.GetButtonDown("Fire3"), onAction1);
            HandleAction(Input.GetButtonDown("Fire2"), onAction2);
            HandleAction(Input.GetButton("Jump"), onContinuousActionJump);
            HandleAction(Input.GetButton("Fire3"), onContinuousAction1);
            HandleAction(Input.GetButton("Fire2"), onContinuousAction2);
        }

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
        }

        private void HandleMovement(Vector2 input)
        {
            moveInput = input;

            // Check to avoid faster diagonal movement
            if (moveInput.magnitude > 1)
                moveInput /= moveInput.magnitude;

            onMovementInput?.Invoke(ModifyInput(new Vector3(moveInput.x, moveInput.y, 0)));
        }

        private void HandleRotation()
        {
            onRotationInput?.Invoke(rotateInput);
        }

        private void HandleAction(bool check, Action action)
        {
            if (check)
                action?.Invoke();
        }

        private Vector2 GetMoveInput()
        {
            Vector2 moveInput = Vector2.zero;

            if (characterValues.movementWithAcceleration)
                moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            else
                moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            return moveInput;
        }

        private Vector3 ModifyInput(Vector3 input)
        {
            Vector3 result = Vector3.zero;

            switch (characterValues.inputType)
            {
                case InputType.XZAxis:
                    result = new Vector3(input.x, 0, input.y);
                    break;
                case InputType.XYAxis:
                    result = input;
                    break;
                case InputType.XAxis:
                    result = new Vector3(input.x, 0, 0);
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
