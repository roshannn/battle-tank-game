using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy/NewEnemyScriptableObject")]

public class EnemyScriptable : ScriptableObject
{
    public EnemyType enemyType;

    public GameObject tankPref;

    public float health;

    public float movementSpeed;

    public float turnSpeed;

    public float fireRate;

    public float patrollingRadius;
    
    public float patrolTime;
    
    public float attackDistance;

    public BulletScriptable bulletType;

    public Material material;

    public float ScaleMultiplier = 1;
}
