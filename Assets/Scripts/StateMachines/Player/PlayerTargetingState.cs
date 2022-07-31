using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    Vector2 dodingDirectionInput;
    float remainingDodgeTime;

    public PlayerTargetingState(PlayerStateMachine stateMachine): base(stateMachine) {}

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");

    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    
    const float CrossFadeDurataion = 0.1f;
    bool isLockon;
    public override void Enter()
    {
        stateMachine.InputReader.TargetingEvent += OnCancel;
        stateMachine.InputReader.DodgeEvent += OnDodge;
        stateMachine.InputReader.JumpEvent += OnJump;

        stateMachine.Animator.CrossFadeInFixedTime(TargetingBlendTreeHash, CrossFadeDurataion);

    }

    public override void Tick(float deltaTime)
    {

        if (stateMachine.InputReader.IsAttacking)
        {
            stateMachine.SwitchState(new PlayerAttackingState(stateMachine, 0));
            return;
        }

        if (stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerBlockingState(stateMachine));
            return;
        }

        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
            return;
        }

        Vector3 movement = CanculateMovement(deltaTime);
        Move((movement + stateMachine.Gravity.Movement)*stateMachine.TargetingMovementSpeed, deltaTime);

        
        UpdateAnimator(deltaTime);
        FaceTarget();

    }



    
    void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    }

    void OnDodge()
    {
        stateMachine.SetDodgeTime(Time.deltaTime);
        dodingDirectionInput = stateMachine.InputReader.MovementValue;
        remainingDodgeTime = stateMachine.DodgeDuration;
    }

    void OnJump()
    {
        stateMachine.SwitchState(new PlayerJumpState(stateMachine));
    }

    Vector3 CanculateMovement(float deltaTime)
    {
        Vector3 movement = new Vector3();
        if (remainingDodgeTime > 0)
        {
            movement += stateMachine.transform.right * dodingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
            movement += stateMachine.transform.forward * dodingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

            remainingDodgeTime = Mathf.Max(remainingDodgeTime -deltaTime, 0f);
        }
        else
        {
            
            movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
            movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

        }
        return movement;
    }


    void UpdateAnimator(float deltaTime)
    {   
        if (stateMachine.InputReader.MovementValue.y == 0)
        {
            stateMachine.Animator.SetFloat(TargetingForwardHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float forwardValue = stateMachine.InputReader.MovementValue.y > 0? 1 : -1;
            stateMachine.Animator.SetFloat(TargetingForwardHash, forwardValue, 0.1f, deltaTime);
        }

        if (stateMachine.InputReader.MovementValue.x == 0)
        {
            stateMachine.Animator.SetFloat(TargetingRightHash, 0, 0.1f, deltaTime);
        }
        else
        {
            float rightValue = stateMachine.InputReader.MovementValue.x > 0? 1 : -1;
            stateMachine.Animator.SetFloat(TargetingRightHash, rightValue, 0.1f, deltaTime);
        }
    }

    public override void Exit()
    {
        //stateMachine.Targeter.CurrentTarget = null;
        stateMachine.InputReader.TargetingEvent -= OnCancel;
        stateMachine.InputReader.DodgeEvent -= OnDodge;
        stateMachine.InputReader.JumpEvent -= OnJump;
    }
}
