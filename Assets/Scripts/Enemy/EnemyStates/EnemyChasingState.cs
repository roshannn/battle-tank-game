using UnityEngine;
using UnityEngine.Scripting;
public class EnemyChasingState : EnemyStates
{
    private bool canChase;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        Debug.Log("In chasing mode");
        enemyController.activeState = EnemyState.Chasing;
        Chase();
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.GetComponent<TankController>() != null)
        {
            enemyController.SetTankController(collider.gameObject.GetComponent<TankController>());
            ChangeState(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (enemyController.activeState == EnemyState.Attacking || !canChase) return;


        if (other.gameObject.GetComponent<TankController>() != null)
        {
            Chase();
        }
            

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TankController>() != null)
        {
            ChangeState(enemyController.patrollingState);
        }
    }

    async private void Chase()
    {
        canChase = false;

        enemyController.navMeshAgent.isStopped = true;
        enemyController.navMeshAgent.ResetPath();
        enemyController.navMeshAgent.SetDestination(enemyController .GetTankTransform().position);
        await new WaitForSeconds(2f);

        canChase = true;
    }
}