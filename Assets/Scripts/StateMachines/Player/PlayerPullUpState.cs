using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullUpState : PlayerBaseState
{
    private readonly int PullUpHash = Animator.StringToHash("PullUp");
    const float CrossFadeDurataion = 0.1f;

    private readonly Vector3 pullUpOffset = new Vector3(0, 2.305f, 0.65f);

    public PlayerPullUpState(PlayerStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(PullUpHash, CrossFadeDurataion);
    }
    

    public override void Tick(float deltaTime)
    {
        // 애니메이션이 모두 재생되지 않으면 리턴 

        if (GetNormalizedTime(stateMachine.Animator, "PullUp") < 1f) { return; }

        //Translate 먹히게 하려면 Controller 잠시 꺼줘야 함 
        stateMachine.Controller.enabled = false;

        //아래 local 포지션 숫자는 노가다로 찾아내야 함 -_-
        stateMachine.transform.Translate(pullUpOffset, Space.Self);

        // 이동 후에 다시 Controller 켜 주
        stateMachine.Controller.enabled = true;

        stateMachine.SwitchState(new PlayerFreeLookState(stateMachine, false));
    }

    public override void Exit()
    {
        stateMachine.Controller.Move(Vector3.zero);
        stateMachine.ForceReceiver.ReSetForce();
    }
}
