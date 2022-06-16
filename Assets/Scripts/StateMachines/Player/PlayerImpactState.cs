using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerImpactState : PlayerBaseState
{

    private readonly int ImpactHash = Animator.StringToHash("Impact");
    const float CrossFadeDurataion = 0.1f;
    float duration = 0.7f;
    public PlayerImpactState (PlayerStateMachine stateMachine): base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(ImpactHash, CrossFadeDurataion);
        
        Debug.Log("ImpactState");
    }

    public override void Tick(float deltaTime)
    {
        // 중력 적용 위해
        Move(deltaTime);

        duration -= deltaTime;
        if(duration <= 0)
        {
            ReturnToLocomotion();
        }


    }

    public override void Exit()
    {
        
    }

    
}
