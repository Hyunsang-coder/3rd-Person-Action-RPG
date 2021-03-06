using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{

    public EnemyIdleState(EnemyStateMachine stateMachine): base(stateMachine){}

    
    private readonly int LocomotionHash = Animator.StringToHash("Locomotion");
    private readonly int SpeedHash = Animator.StringToHash("Speed");

    private const float CrossFadeDuration = 0.1f;
    private const float AnimatorDampTime = 0.1f;
    

    public override void Enter()
    {
        stateMachine.Animator.CrossFadeInFixedTime(LocomotionHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime)
    {
        Move(deltaTime);
        if (IsInChaseRange())
        {
            stateMachine.SwitchState(new EnemyChasingState(stateMachine));
            return;
        }
        stateMachine.Animator.SetFloat(SpeedHash, 0, AnimatorDampTime, deltaTime);
        
        
        
    }
    public override void Exit()
    {
    }

    
}
