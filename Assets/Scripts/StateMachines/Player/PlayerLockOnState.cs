using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLockOnState : PlayerBaseState
{
    public PlayerLockOnState(PlayerStateMachine stateMachine): base(stateMachine) {}
    
    bool isLockon;
    public override void Enter()
    {
        stateMachine.InputReader.LockOnEvent += LockOn;
    }

    public override void Tick(float deltaTime)
    {
        
    }

    public override void Exit()
    {
        stateMachine.InputReader.LockOnEvent -= LockOn;
    }
    void LockOn()
    {
        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
    } 


}
