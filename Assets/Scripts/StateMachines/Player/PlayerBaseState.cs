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

    protected void Move(Vector3 motion, float deltatime)
    {
        stateMachine.Controller.Move(motion * deltatime);
    }

    protected void FaceTarget()
    {   
        if (stateMachine.Targeter.CurrentTarget == null) return;

        Vector3 targetDirection = stateMachine.Targeter.CurrentTarget.transform.position - stateMachine.transform.position;
        targetDirection.y= 0;
        stateMachine.transform.rotation = Quaternion.LookRotation(targetDirection);

        Debug.Log("TargetDirection is " + targetDirection);
    }
}
