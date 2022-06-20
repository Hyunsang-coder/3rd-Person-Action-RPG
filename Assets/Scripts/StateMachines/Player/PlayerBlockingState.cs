using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingState : PlayerBaseState
{
    private readonly int BlockingHash = Animator.StringToHash("Block");

    private const float CrossFadeDuration = 0.1f;

    public PlayerBlockingState(PlayerStateMachine stateMachine): base(stateMachine){}
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(BlockingHash, CrossFadeDuration);
    }

   

    public override void Tick(float deltaTime)
    {

        if (!stateMachine.InputReader.IsBlocking)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }
        
    }

     public override void Exit()
    {
       
    }
}
