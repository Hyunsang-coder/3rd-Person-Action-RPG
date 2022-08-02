using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDodgeState : PlayerBaseState
{
    Vector2 dodgingDirectionInput;
    float remainingDodgeTime;

    public PlayerDodgeState(PlayerStateMachine stateMachine, Vector2 dodgeDirection) : base(stateMachine)
    {
        dodgingDirectionInput = dodgeDirection;
    }

    private readonly int DodgeBlendTreeHash = Animator.StringToHash("DodgeBlendTree");

    private readonly int DodgeForwardHash = Animator.StringToHash("DodgeForward");

    private readonly int DodgeRightHash = Animator.StringToHash("DodgeRight");

    const float CrossFadeDurataion = 0.1f;
    

    public override void Enter()
    {
        remainingDodgeTime = stateMachine.DodgeDuration;

        stateMachine.Animator.SetFloat(DodgeRightHash, dodgingDirectionInput.x);
        stateMachine.Animator.SetFloat(DodgeForwardHash, dodgingDirectionInput.y);

        stateMachine.Health.SetInvulnerable(true);
        stateMachine.Animator.CrossFadeInFixedTime(DodgeBlendTreeHash, CrossFadeDurataion);
        
        
    }

    public override void Tick(float deltaTime)
    {
        Vector3 movement = new Vector3();

        movement += stateMachine.transform.right * dodgingDirectionInput.x * stateMachine.DodgeLength / stateMachine.DodgeDuration;
        movement += stateMachine.transform.forward * dodgingDirectionInput.y * stateMachine.DodgeLength / stateMachine.DodgeDuration;

        Move((movement), deltaTime);
        FaceTarget();

        remainingDodgeTime -= deltaTime;

        if (remainingDodgeTime <= 0)
        {
            stateMachine.SwitchState(new PlayerTargetingState(stateMachine));
        }

    }


    public override void Exit()
    {
        stateMachine.Health.SetInvulnerable(false);
        Debug.Log("Exiting Dodge");
    }
}
