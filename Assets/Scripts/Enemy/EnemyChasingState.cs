using UnityEngine;
using System.Collections;
public class EnemyChasingState : EnemyStates
{
    private bool canChase;
    public override void OnStateEnter()
    {
        base.OnStateEnter();
        enemyController.activeState = EnemyState.Chasing;
        StartCoroutine(Chase());
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<TankController>() != null)
        {
            enemyController.SetTankController(other.gameObject.GetComponent<TankController>());
            ChangeState(this);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (enemyController.activeState == EnemyState.Attacking || !canChase) return;


        if (other.gameObject.GetComponent<TankController>() != null)
        {
            StartCoroutine(Chase());
        }
            

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<TankController>() != null)
        {
            ChangeState(enemyController.patrollingState);
        }
    }
    
    private IEnumerator Chase()
    {
        canChase = false;

        enemyController.navMeshAgent.isStopped = true;
        enemyController.navMeshAgent.ResetPath();
        enemyController.navMeshAgent.SetDestination(enemyController.GetTankTransform().position);
        yield return new WaitForSeconds(2);

        canChase = true;

    }
}