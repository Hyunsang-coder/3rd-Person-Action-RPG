using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    
    private readonly int DieHash = Animator.StringToHash("Die");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;

    public EnemyDeadState(EnemyStateMachine stateMachine): base(stateMachine){}
    public override void Enter()
    {
       stateMachine.Ragdoll.RagDollToggle(true);
       stateMachine.Weapon.gameObject.SetActive(false);
       GameObject.Destroy(stateMachine.Target);
    }
   
    public override void Tick(float deltaTime){}
    public override void Exit(){}
}
