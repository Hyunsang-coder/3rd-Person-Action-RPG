using UnityEngine;
public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void Enter()
    {
    }

    public override void Tick(float deltaTime)
    {
    }
    public override void Exit()
    {
    }

}
