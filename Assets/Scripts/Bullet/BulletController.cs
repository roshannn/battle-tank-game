using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem bulletExplosionParticle;
    [SerializeField]
    private ExplosionController explosionController;
    [SerializeField]
    private AudioClip bulletExplode;
    [SerializeField]
    private AudioClip bulletFire;
    float damage;


    private void Start()
    {
        SoundManager.Instance.PlaySoundAtTrack1(bulletFire, 1f, 64, true);
        damage = BulletService.Instance.SetDamage();
        Debug.Log("Damage of bullet is " + damage);
    }
    private void OnCollisionEnter(Collision collision)
    {
        explosionController.Explode(bulletExplosionParticle);
        SoundManager.Instance.PlaySoundAtTrack1(bulletExplode, 1f, 64, true);

        Destroy(gameObject);

        IDamageable _damage = collision .gameObject.GetComponent<IDamageable>();
        if (_damage != null)
        {
            _damage.TakeDamage(damage);
        }
    }



}



    