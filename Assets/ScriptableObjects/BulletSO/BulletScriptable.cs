using UnityEngine;

[CreateAssetMenu(fileName = "BulletScriptableObject", menuName = "ScriptableObject/Bullet/NewBulletScriptableObject")]
public class BulletScriptable : ScriptableObject
{
    public BulletType bulletType;

    public GameObject bulletPref;

    public float speed;
    public float damage;

}
