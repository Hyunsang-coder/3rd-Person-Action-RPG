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
        stateMachine.Weapon.SetAttack(attack.Damage);
        // Attacking State 진입과 동시에 애니메이션 교체 
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        // 애니메이션이 재생 중이라면,
        if (normalizedTime >= previousFrameTime && normalizedTime < 1f)
        {
            // Attacking 버튼을 누른 상태라면
            if (normalizedTime >= attack.ForceTime)
            {
                TryApplyForce(normalizedTime);
            }

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

    private float GetNormalizedTime()
    {
        // 0번째 레이어 = base layer
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        // 공격(Attack) 애니메이션으로 전환 중이면, 
        if(stateMachine.Animator.IsInTransition(0) && nextInfo.IsTag("Attack"))
        {
            return nextInfo.normalizedTime;
        }
        // 공격 애니메이션으로 전환 중이 아니고, 현재 애니메이션이 공격이면,
        else if(!stateMachine.Animator.IsInTransition(0) && currentInfo.IsTag("Attack"))
        {
            return currentInfo.normalizedTime;
        }
        else return 0;

    }

}
