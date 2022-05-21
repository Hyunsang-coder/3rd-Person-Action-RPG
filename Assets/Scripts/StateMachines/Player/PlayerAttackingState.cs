using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : PlayerBaseState
{
    Attack attack;
    float previousFrameTime;

    public PlayerAttackingState(PlayerStateMachine stateMachine, int attackIndex): base(stateMachine)
    {
        attack = stateMachine.Attacks[attackIndex];
    }
    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(attack.AnimationName, attack.TransitionDuration);
        Debug.Log("AttackState: " + attack.AnimationName + " ComboIndex: " + attack.ComboStateIndex);
    }


    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        FaceTarget();

        float normalizedTime = GetNormalizedTime();

        if (normalizedTime > previousFrameTime && normalizedTime < 1f)
        {
            if (stateMachine.InputReader.IsAttacking)
            {
                TryComboAttack(normalizedTime);
                Debug.Log("Trying to comboattack?");
            }
        }
        // 강의 듣기 전 임의로 한 부분
        else if(!stateMachine.Animator.IsInTransition(0))
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

        previousFrameTime = normalizedTime;

    }

    
    public override void Exit()
    {
        
    }

    private void TryComboAttack(float normalizedTime)
    {
        if (attack.ComboStateIndex == -1) return;

        if (normalizedTime < attack.ComboAttackTime) return;

        stateMachine.SwitchState(new PlayerAttackingState (stateMachine, attack.ComboStateIndex));
        Debug.Log("State Switched?");
    }

    private float GetNormalizedTime()
    {
        AnimatorStateInfo currentInfo = stateMachine.Animator.GetCurrentAnimatorStateInfo(0);
        AnimatorStateInfo nextInfo = stateMachine.Animator.GetNextAnimatorStateInfo(0);

        // 공격 애니메이션으로 전환 중
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
