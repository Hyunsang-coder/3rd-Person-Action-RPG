using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    public EnemyIdleState(EnemyStateMachine stateMachine): base(stateMachine){}

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChaseRange())
        {
            Debug.Log("In Range");
            // transition to ChaseState;
            return;
        }
        stateMachine.Animator.SetFloat("Speed", 0, AnimatorDampTime, deltaTime);
        
        
        
    }
    public override void Exit()
    {
    }

    
}
