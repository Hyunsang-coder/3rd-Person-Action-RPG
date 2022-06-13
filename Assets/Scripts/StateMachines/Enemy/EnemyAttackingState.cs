using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackingState : EnemyBaseState
{
    public EnemyAttackingState(EnemyStateMachine stateMachine): base(stateMachine){}
    private readonly int AttackHash = Animator.StringToHash("Attack");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    
    public override void Enter()
    {
       stateMachine.Weapon.SetAttack(stateMachine.AttackDamage);
       stateMachine.Animator.CrossFadeInFixedTime(AttackHash, CrossFadeDuration);
    }
   
    public override void Tick(float deltaTime)
    {
       
    }
     public override void Exit()
    {
       
    }

}
