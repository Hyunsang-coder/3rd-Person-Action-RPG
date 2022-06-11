using UnityEngine;
public abstract class EnemyBaseState : State
{
    protected EnemyStateMachine stateMachine;
    
    public EnemyBaseState(EnemyStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    protected bool IsInChaseRange()
    {
        //float distanceToPlayer = Vector3.Distance(stateMachine.transform.position, stateMachine.Player.transform.position);
        //위의 함수가 간단하지만 퍼포먼스를 위해 루트 계산을 안 하려면, 
        float distanceToPlayerSqr = (stateMachine.Player.transform.position - stateMachine.transform.position).sqrMagnitude;

        return distanceToPlayerSqr <= stateMachine.PlayerChasingRange*stateMachine.PlayerChasingRange;
    }

    
    protected void Move(float deltatime)
    {
        stateMachine.Controller.Move(stateMachine.ForceReceiver.Movement*deltatime);
    }

    protected void Move(Vector3 motion, float deltatime)
    {
        stateMachine.Controller.Move(motion * deltatime);
    }



}
