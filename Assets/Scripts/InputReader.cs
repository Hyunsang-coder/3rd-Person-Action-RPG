using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour, Controls.IPlayerActions
{
    public Vector2 MovementValue { get; private set; }
    
    public bool IsAttacking{get; private set;}

    public event Action JumpEvent;
    public event Action DodgeEvent;

    public event Action TargetingEvent;


    // Control 선언 필요 
    private Controls controls;

    private void Start()
    {
        // 오브젝트화 + 콜백 세팅 + 플레이어 컨트롤 스킴 활성화
        controls = new Controls();
        controls.Player.SetCallbacks(this);

        controls.Player.Enable();
    }

    private void OnDestroy()
    {
        controls.Player.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        JumpEvent?.Invoke();
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        DodgeEvent?.Invoke();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        
    }

    public void OnTargeting(InputAction.CallbackContext context)
    {
        if (!context.performed) { return; }

        TargetingEvent?.Invoke();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            IsAttacking = true;
        }
        else if (context.canceled) 
        {
            IsAttacking = false;
        }
    }
}
