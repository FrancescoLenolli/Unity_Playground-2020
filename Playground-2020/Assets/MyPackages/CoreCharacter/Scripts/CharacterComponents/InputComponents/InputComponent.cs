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
        private Action onAction3;

        public Action<Vector3> OnActionMove { get => onMovementInput; set => onMovementInput = value; }
        public Action<Vector2> OnActionRotate { get => onRotationInput; set => onRotationInput = value; }
        public Action OnActionJump { get => onActionJump; set => onActionJump = value; }
        public Action OnAction1 { get => onAction1; set => onAction1 = value; }
        public Action OnAction2 { get => onAction2; set => onAction2 = value; }
        public Action OnAction3 { get => onAction3; set => onAction3 = value; }

        private void Update()
        {
            HandleMovement();
            //HandleRotation();
            HandleAction(Input.GetButtonDown("Fire1"), onActionJump);
            HandleAction(Input.GetButtonDown("Fire3"), onAction1);
            HandleAction(Input.GetButtonDown("Fire2"), onAction2);
            HandleAction(Input.GetButtonDown("Jump"), onAction3);
        }

        public override void SetUp(CharacterControl character)
        {
            base.SetUp(character);
        }

        private void HandleMovement()
        {
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = Input.GetAxis("Vertical");

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
