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
        stateMachine.InputReader.LockOnEvent += CancelTargeting;
        stateMachine.Animator.Play(TargetingBlendTreeHash);
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log(stateMachine.Targeter.target.name);
    }

    public override void Exit()
    {
        stateMachine.Targeter.target = null;
        stateMachine.InputReader.LockOnEvent -= CancelTargeting;
    }
    void CancelTargeting()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    } 

}
