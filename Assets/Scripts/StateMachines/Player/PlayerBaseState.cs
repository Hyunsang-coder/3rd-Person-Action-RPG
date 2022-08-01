using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBaseState : State
{
    protected PlayerStateMachine stateMachine;

    public PlayerBaseState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    
    // 모션이 없어도 중력 표현을 위해 필요!
    protected void Move(float deltatime)
    {
        Move(Vector3.zero, deltatime);
    }


    protected void Move(Vector3 motion, float deltatime)
    {
        stateMachine.Controller.Move((motion + stateMachine.ForceReceiver.Movement) * deltatime);
    }

    protected void FaceTarget()
    {   
        if (stateMachine.Targeter.CurrentTarget == null) return;

        Vector3 lookPos = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        lookPos.y= 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(lookPos);
    }

    protected void ReturnToLocomotion()
    {
        if (stateMachine.Targeter.CurrentTarget != null)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        else
        {
            stateMachine.SwitchState(new PlayerFreeLookState(stateMachine));
        }
    }
}
