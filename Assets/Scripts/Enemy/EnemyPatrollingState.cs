
using UnityEngine;
public class EnemyPatrollingState : EnemyStates
{
    public override void OnStateEnter()
    {
        Debug.Log("Enemy in patrolling state");
        base.OnStateEnter();
        enemyController.activeState = EnemyState.Patrolling;
    }
    private void Update()
    {
        enemyController.Patrol();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
}