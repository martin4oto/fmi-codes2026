using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Krisnat
{
    public class InputManager : MonoBehaviour
    {
        #region Variables

        public static InputManager instance;
        public bool OptionsInput { get; private set; }  // escape
        public bool SpawnCell1Input { get; private set; } // 1
        public bool SpawnCell2Input { get; private set; } // 2
        public bool SpawnCell3Input { get; private set; } // 3
        public bool SpawnCell4Input { get; private set; } // 4
        public Vector2 MousePosition { get; private set; }
        public bool StopAllInputs { get; set; } // menu pause
        public float MouseRelativeToBrainPosition =>
    Camera.main
        .ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, transform.position.z)).x -
    BrainManager.instance.transform.position.x;

        #endregion

        private void Awake()
        {
            instance = this;
        }

        public void OnOptionsInput(InputAction.CallbackContext context)
        {
            if (context.started && !StopAllInputs)
            {
                OptionsInput = true;
            }
            if (context.canceled && !StopAllInputs)
            {
                OptionsInput = false;
            }
        }

        public void OnSpawnCell1Input(InputAction.CallbackContext context)
        {
            if (context.started && !StopAllInputs)
            {
                SpawnCell1Input = true;
            }
            if (context.canceled && !StopAllInputs)
            {
                SpawnCell1Input = false;
            }
        }

        public void OnSpawnCell2Input(InputAction.CallbackContext context)
        {
            if (context.started && !StopAllInputs)
            {
                SpawnCell2Input = true;
            }
            if (context.canceled && !StopAllInputs)
            {
                SpawnCell2Input = false;
            }
        }

        public void OnSpawnCell3Input(InputAction.CallbackContext context)
        {
            if (context.started && !StopAllInputs)
            {
                SpawnCell3Input = true;
            }
            if (context.canceled && !StopAllInputs)
            {
                SpawnCell3Input = false;
            }
        }

        public void OnSpawnCell4Input(InputAction.CallbackContext context)
        {
            if (context.started && !StopAllInputs)
            {
                SpawnCell4Input = true;
            }
            if (context.canceled && !StopAllInputs)
            {
                SpawnCell4Input = false;
            }
        }

        public void MousePositionInput(InputAction.CallbackContext context)
        {
            MousePosition = context.ReadValue<Vector2>();
        }

        public void UseOptionsInput() => OptionsInput = false;
    }
}
