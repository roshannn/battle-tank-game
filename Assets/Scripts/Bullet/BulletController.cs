using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletExplosionParticle;
    [SerializeField]
    private AudioClip bulletExplode;
    
    float damage;


    private void Start()
    {
        
        damage = BulletService.Instance.SetDamage();
        Debug.Log("Damage of bullet is " + damage);
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        gameObject.SetActive(false);
        SoundManager.Instance.PlaySoundAtTrack1(bulletExplode, 1f, 64, true);
        GameObject shellExplosion = ObjectPooler.SharedInstance.GetPooledObject("bulletexplosion");
        shellExplosion.SetActive(true);
        shellExplosion.GetComponent<ParticleSystem>().Play();
        shellExplosion.SetActive(false);
        IDamageable _damage = collision.gameObject.GetComponent<IDamageable>();
        if (_damage != null)
        {
            _damage.TakeDamage(damage);
        }
    }

}



    