using UnityEngine;

public class BulletService : MonoSingletonGeneric<BulletService>
{
    private float fireSpeed;

    public float damage;

    public BulletScriptable[] bulletList;

    public BulletScriptable bulletType;

    [SerializeField]
    private AudioClip bulletFire;


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
        SoundManager.Instance.PlaySoundAtTrack1(bulletFire, 1f, 64, true);
        GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject("bullet");
        if (bullet != null)
        {
            bullet.transform.position = fireTransform.position;
            bullet.transform.rotation = fireTransform.rotation;
            bullet.SetActive(true);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.velocity = fireSpeed * fireTransform.forward;            
        }
        //GameObject bullet = Instantiate(bulletType.bulletPref, fireTransform.position, fireTransform.rotation);
        //Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        //bulletRigidbody.velocity = fireSpeed * fireTransform.forward;
    }
    
}
