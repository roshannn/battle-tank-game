using UnityEngine;

public class EnemyAttackingState : EnemyStates
{
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        enemyController.activeState = EnemyState.Attacking;
    }
    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TankController>() != null)
        {
            enemyController.navMeshAgent.isStopped = true;
            enemyController.navMeshAgent.ResetPath();
            ChangeState(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<TankController>() != null)
        {
            Vector3 lookDir = other.transform.position - enemyController.transform.position;
            if (lookDir != new Vector3(0, 0, 0))
                RotateTowardsTarget();

            enemyController.Attack();
        }
    }

    private void RotateTowardsTarget()
    {
        enemyController.transform.LookAt(enemyController.GetTankTransform());
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TankController>() != null)
        {
            ChangeState(enemyController.chasingState);
        }
    }
}