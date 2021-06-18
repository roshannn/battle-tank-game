using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Slider healthSlider;

    private float health;
    private float turnSpeed;
    private float movementSpeed;
    private float timer;
    private float canFire = 0f;
    

    protected float damage;
    private float fireRate;
    private float patrollingRadius;
    private float patrolTime;
    private float attackDistance;

    private float scaleMultiplier;

    private BulletScriptable bulletType;
    public MeshRenderer[] tankParts;
    public Transform fireTransform;

    //states
    public EnemyPatrollingState patrollingState;
    public EnemyChasingState chasingState;
    public EnemyAttackingState attackingState;
    [SerializeField] private EnemyState initialState;
    public EnemyState activeState;
    public EnemyStates currentState;

    //AudioVisual
    [SerializeField]
    private GameObject tankExplosionParticle;
    [SerializeField]
    private AudioClip tankExplosionAudio;
    private TankController tankController;
    public NavMeshAgent navMeshAgent;

    private void Start()
    {
        InitializeState();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void Attack()
    {
        if (canFire < Time.time)
        {
            canFire = fireRate + Time.time;
            BulletService.Instance.Fire(fireTransform, bulletType);
        }
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
    public Transform GetTankTransform()
    {
        return tankController.transform;
    }

    private void InitializeState()
    {
        switch (initialState)
        {
            case EnemyState.Attacking:
                currentState = attackingState;
                break;
            case EnemyState.Chasing:
                currentState = chasingState;
                break;
            case EnemyState.Patrolling:
                currentState = patrollingState;
                break;
            default:
                currentState = null;
                break;
        }
        currentState.OnStateEnter();
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            GameService.Instance.noOfEnemies -= 1;
            EventService.Instance.InvokeEnemyKilledEvent();
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        SoundManager.Instance.PlayEnemyTrack(tankExplosionAudio, 1, 10);
        gameObject.SetActive(false);
    }

    public void InitializeValues(EnemyScriptable enemyType)
    {
        bulletType = enemyType.bulletType;
        health = enemyType.health;
        healthSlider.maxValue = health;
        turnSpeed = enemyType.turnSpeed;
        movementSpeed = enemyType.movementSpeed;
        fireRate = enemyType.fireRate;
        patrollingRadius = enemyType.patrollingRadius;
        patrolTime = enemyType.patrolTime;
        attackDistance = enemyType.attackDistance;
        SetScale(enemyType.ScaleMultiplier);
        for(int i = 0; i < tankParts.Length; i++)
        {
            tankParts[i].material = enemyType.material;
        }
    }

    private void SetScale(float scaleMultiplier)
    {
        gameObject.transform.localScale *= scaleMultiplier;
    }

    public void Patrol()
    {
        timer += Time.deltaTime;
        if (timer > patrolTime)
        {
            SetPatrolingDestination();
            timer = 0;
        }
    }

    private void SetPatrolingDestination()
    {
        Vector3 newDestination = GetRandomPosition();
        navMeshAgent.SetDestination(newDestination);
    }

    private Vector3 GetRandomPosition()
    {
        Vector3 randDir = UnityEngine.Random.insideUnitSphere * patrollingRadius;
        randDir += EnemyService.Instance.enemyType.tankPref.transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, patrollingRadius, NavMesh.AllAreas);
        return navHit.position;
    }
}
