using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    public EnemyController enemyController;

    public virtual void OnStateEnter()
    {
        this.enabled = true;
    }
    public virtual void OnStateExit()
    {
        this.enabled = false;
    }


    public void ChangeState(EnemyStates newState)
    {
        if (enemyController.currentState != null)
            enemyController.currentState.OnStateExit();

        enemyController.currentState = newState;
        enemyController.currentState.OnStateEnter();
    }
}