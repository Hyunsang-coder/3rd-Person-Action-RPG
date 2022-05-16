using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine): base(stateMachine) {}

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");

    private readonly int TargetingForwardHash = Animator.StringToHash("TargetingForward");

    private readonly int TargetingRightHash = Animator.StringToHash("TargetingRight");
    
    bool isLockon;
    public override void Enter()
    {
        stateMachine.InputReader.TargetingEvent += OnCancel;
        stateMachine.Animator.Play(TargetingBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        if (stateMachine.Targeter.CurrentTarget == null)
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }

        Vector3 movement = CanculateMovement();
        Move((movement + stateMachine.ForceReceiver.Movement)*stateMachine.TargetingMovementSpeed, deltaTime);

        
        UpdateAnimator(deltaTime);
        FaceTarget();


    }



    public override void Exit()
    {
        stateMachine.Targeter.CurrentTarget = null;
        stateMachine.InputReader.TargetingEvent -= OnCancel;
    }
    void OnCancel()
    {
        stateMachine.Targeter.Cancel();
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    } 

    Vector3 CanculateMovement()
    {
        Vector3 movement = new Vector3();
        movement += stateMachine.transform.right * stateMachine.InputReader.MovementValue.x;
        movement += stateMachine.transform.forward * stateMachine.InputReader.MovementValue.y;

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

}
