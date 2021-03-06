using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    Attack attack;
    float previousFrameTime;
    bool alreadyAppliedForce;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex): base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }
    public override void Enter()
    {
        stateMachine.Weapon.SetAttack(attack.Damage, attack.Knockback);
        // Attacking State 진입과 동시에 애니메이션 교체 
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime(stateMachine.Animator);

        // 애니메이션이 재생 중이라면,
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce(normalizedTime);
            }
            
            // Attacking 버튼을 누른 상태라면
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
            }
        }
        
        else 
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

        previousFrameTime = normalizedTime;

    }

    private void TryApplyForce(float normalizedTime)
    {
        if(alreadyAppliedForce) return;
        stateMachine.ForceReceiver.AddForce(stateMachine.transform.forward*attack.Force);
        alreadyAppliedForce = true;
    }

    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) return;
        
        // 설정한 ComboAttackTime 보다 진행 시간이 짧으면 return
        if (normalizedTime < attack.ComboAttackTime) return;

        // 그렇지 않으면 ComboStateIndex에 따라 새로운 애니메이션 진입
        stateMachine.SwitchState(new PlayerAttackingState (stateMachine, attack.ComboStateIndex));
    }

    

}
