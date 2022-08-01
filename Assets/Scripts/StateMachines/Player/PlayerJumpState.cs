using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerBaseState
{

    private readonly int JumpHash = Animator.StringToHash("Jump");
    const float CrossFadeDurataion = 0.1f;
    Vector3 momentum;

    
    public PlayerJumpState (PlayerStateMachine stateMachine): base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.ForceReceiver.Jump(stateMachine.JumpForce);

        momentum = stateMachine.Controller.velocity;
        momentum.y = 0;
        
        stateMachine.Animator.CrossFadeInFixedTime(JumpHash, CrossFadeDurataion);

        Debug.Log("JumpState");
    }


    public override void Tick(float deltaTime)
    {
        Move(momentum, deltaTime);

        //Debug.Log("Vertical Velocity is " + stateMachine.Controller.velocity);


        if (stateMachine.Controller.velocity.y <= 0)
        {
            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }
        

        FaceTarget();
    }

    public override void Exit()
    {
        
    }

    
}
