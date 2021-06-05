using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    private GameObject BulletPrefab;

    private float fireSpeed;

    public BulletScriptable[] bulletList;

    private BulletScriptable bulletType;

    private void Start()
    {
        GetBulletType();
    }

    private void GetBulletType()
    {
        int rand = Random.Range(0, bulletList.Length);
        bulletType = bulletList[rand];
        fireSpeed = bulletType.speed;
    }
    public void Fire(Transform fireTransform)
    {
        GameObject bullet = Instantiate(bulletType.bulletPref, fireTransform.position, fireTransform.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = fireSpeed * fireTransform.forward ;
    }
}
