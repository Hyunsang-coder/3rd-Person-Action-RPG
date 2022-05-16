using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTargetingState : PlayerBaseState
{
    public PlayerTargetingState(PlayerStateMachine stateMachine): base(stateMachine) {}

    private readonly int TargetingBlendTreeHash = Animator.StringToHash("TargetingBlendTree");
    
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
        Move(movement*stateMachine.TargetingMovementSpeed, deltaTime);

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

}
