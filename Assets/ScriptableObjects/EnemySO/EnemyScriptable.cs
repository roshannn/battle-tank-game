using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObject/Enemy/NewEnemyScriptableObject")]

public class EnemyScriptable : ScriptableObject
{
    public TankType tankType;

    public GameObject tankPref;

    public float health;

    public float movementSpeed;

    public float turnSpeed;

    public float fireRate;

    public float damage;

    public Material material;
}
