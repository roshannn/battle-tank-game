using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    private float fireSpeed;

    public float damage;

    public BulletScriptable[] bulletList;

    public BulletScriptable bulletType;

   

    private void Start()
    {
        GetBulletType();
    }

    private void GetBulletType()
    {
        int rand = Random.Range(0, bulletList.Length);
        bulletType = bulletList[rand];
        damage = bulletType.damage;
        fireSpeed = bulletType.speed;
    }

    public float SetDamage()
    {
        return damage;
    }
    public void Fire(Transform fireTransform,BulletScriptable bulletType)
    {    
        GameObject bullet = Instantiate(bulletType.bulletPref, fireTransform.position, fireTransform.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = fireSpeed * fireTransform.forward;
    }
    
}
