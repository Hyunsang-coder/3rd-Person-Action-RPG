using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangingState : PlayerBaseState
{
    private Vector3 ledgeForward;
    private Vector3 closestPoint;
    public PlayerHangingState(PlayerStateMachine stateMachine, Vector3 ledgeForward, Vector3 closestPoint) : base(stateMachine)
    {
        this.ledgeForward = ledgeForward;
        this.closestPoint = closestPoint;
    }

    private readonly int HangingHash = Animator.StringToHash("Hanging");

    const float CrossFadeDurataion = 0.1f;

    public override void Enter()
    {
        stateMachine.transform.rotation = Quaternion.LookRotation(ledgeForward, Vector3.up);

        stateMachine.Controller.enabled = false;
        stateMachine.transform.position = closestPoint - (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position);
        Debug.Log("closestPoint: " + closestPoint + "gap: " + (stateMachine.LedgeDetector.transform.position - stateMachine.transform.position));
        stateMachine.Controller.enabled = true;

        stateMachine.Animator.CrossFadeInFixedTime(HangingHash, CrossFadeDurataion);
    }

    
    public override void Tick(float deltaTime)
    {


        if (stateMachine.InputReader.MovementValue.y > 0)
        {
            
            stateMachine.SwitchState(new PlayerPullUpState(stateMachine));
        }

        else if (stateMachine.InputReader.MovementValue.y < 0)
        {
            // 불필요한 중력이 누적되거나 적용되지 않도록 리셋
            stateMachine.Controller.Move(Vector3.zero);
            stateMachine.ForceReceiver.ReSetForce();

            stateMachine.SwitchState(new PlayerFallingState(stateMachine));
        }

        
    }

    public override void Exit()
    {

    }
}
