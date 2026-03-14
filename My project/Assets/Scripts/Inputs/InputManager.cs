using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    #region Variables

    public static InputManager instance;
    public bool OptionsInput { get; private set; }  // escape
    public bool SpawnCell1Input { get; private set; } // 1
    public bool SpawnCell2Input { get; private set; } // 2
    public bool SpawnCell3Input { get; private set; } // 3
    public bool SpawnCell4Input { get; private set; } // 4
    public bool TestInput { get; private set; } // T
    public Vector2 MousePosition { get; private set; }
    public bool StopGameInputs { get; set; } // menu pause
    public Vector2 MouseRelativeToBrainPosition =>
Camera.main
    .ScreenToWorldPoint(new Vector3(MousePosition.x, MousePosition.y, transform.position.z)) -
BrainManager.instance.transform.position;

    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        MousePosition = Input.mousePosition;
    }

    public void OnOptionsInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            OptionsInput = true;
        }
        if (context.canceled)
        {
            OptionsInput = false;
        }
    }

    public void OnSpawnCell1Input(InputAction.CallbackContext context)
    {
        if (context.started && !StopGameInputs)
        {
            SpawnCell1Input = true;
        }
        if (context.canceled && !StopGameInputs)
        {
            SpawnCell1Input = false;
        }
    }

    public void OnSpawnCell2Input(InputAction.CallbackContext context)
    {
        if (context.started && !StopGameInputs)
        {
            SpawnCell2Input = true;
        }
        if (context.canceled && !StopGameInputs)
        {
            SpawnCell2Input = false;
        }
    }

    public void OnSpawnCell3Input(InputAction.CallbackContext context)
    {
        if (context.started && !StopGameInputs)
        {
            SpawnCell3Input = true;
        }
        if (context.canceled && !StopGameInputs)
        {
            SpawnCell3Input = false;
        }
    }

    public void OnSpawnCell4Input(InputAction.CallbackContext context)
    {
        if (context.started && !StopGameInputs)
        {
            SpawnCell4Input = true;
        }
        if (context.canceled && !StopGameInputs)
        {
            SpawnCell4Input = false;
        }
    }

    public void OnTestInput(InputAction.CallbackContext context)
    {
        if (context.started && !StopGameInputs)
        {
            TestInput = true;
        }
        if (context.canceled && !StopGameInputs)
        {
            TestInput = false;
        }
    }

    public void MousePositionInput(InputAction.CallbackContext context)
    {
        MousePosition = context.ReadValue<Vector2>();
    }

    public void UseOptionsInput() => OptionsInput = false;
    public void UseTestInput() => TestInput = false;
}
