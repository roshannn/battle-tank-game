using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private Slider healthSlider;

    private EnemyService enemyService;
    private float health;
    private float turnSpeed;
    private float movementSpeed;
    private float damage;
    public MeshRenderer[] tankParts;

    //AudioVisual
    [SerializeField]
    private ParticleSystem tankExplosionParticle;
    [SerializeField]
    private AudioClip tankExplosionAudio;
    [SerializeField]
    private ExplosionController explosionController;

    private void Start()
    {

    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            GameService.Instance.noOfEnemies -= 1;
            
            explosionController.Explode(tankExplosionParticle);
            SoundManager.Instance.PlayEnemyTrack(tankExplosionAudio, 1, 10);
            Destroy(gameObject);
        }
    }
    public void InitializeValues(EnemyScriptable enemyType)
    {
        health = enemyType.health;
        healthSlider.maxValue = health;
        turnSpeed = enemyType.turnSpeed;
        movementSpeed = enemyType.movementSpeed;
        damage = enemyType.damage;
        for(int i = 0; i < tankParts.Length; i++)
        {
            tankParts[i].material = enemyType.material;
        }
    }
}
