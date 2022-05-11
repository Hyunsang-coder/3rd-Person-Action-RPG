using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOnState : PlayerBaseState
{
    public PlayerLockOnState(PlayerStateMachine stateMachine): base(stateMachine) {}
    
    bool isLockon;
    public override void Enter()
    {
        stateMachine.InputReader.LockOnEvent += CancelLockOn;
    }

    public override void Tick(float deltaTime)
    {
        Debug.Log(stateMachine.Targeter.target.name);
    }

    public override void Exit()
    {
        stateMachine.Targeter.target = null;
        stateMachine.InputReader.LockOnEvent -= CancelLockOn;
    }
    void CancelLockOn()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    } 

}
