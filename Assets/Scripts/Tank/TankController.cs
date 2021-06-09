using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
public class TankController: MonoBehaviour, IDamageable
{
    //UI
    public Joystick joystick;
    [SerializeField] private Slider healthSlider;
    
    //Input
    private float vertical;
    private float horizontal;

    //Bullet
    private BulletScriptable bulletScriptable;

    //Components
    private Rigidbody rigidBody;
    public MeshRenderer[] tankParts;
    
    //ScriptableObjectItems
    private TankType tankType;
    private float health;
    private float fireRate;
    private float movementSpeed;
    private float turnSpeed;

    //WorldSpecifics
    public Transform fireTransform;
    public Transform tankTransform;
    public Transform turretTransform;

    //AudioVisual
    [SerializeField]
    private ParticleSystem tankExplosionParticle;
    [SerializeField]
    private ExplosionController explosionController;
    [SerializeField]
    private AudioClip tankExplosion;
    [SerializeField]
    private AudioClip tankIdle;
    [SerializeField]
    private AudioClip tankDriving;


    private void Start()
    {
        bulletScriptable = BulletService.Instance.bulletType;
        rigidBody = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
       
        GetInput();
    }

    
    private void GetInput()
    {
        vertical = joystick.Vertical;
        horizontal = joystick.Horizontal;
        if (CrossPlatformInputManager.GetButtonDown("Fire"))
        {

            BulletService.Instance.Fire(fireTransform, bulletScriptable);
        }
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void PlayEngineSounds()
    {
        if (vertical != 0)
        {
            
            SoundManager.Instance.MovingSoundTrack(tankDriving, 0.1f, 256, false);
        }
        else
        {
            SoundManager.Instance.MovingSoundTrack(tankIdle, 0.1f, 256, false);
        }
    }
    private void FixedUpdate()
    {
        
        Move(vertical);
        PlayEngineSounds();
        Turn(horizontal); 
    }

    private void Turn(float horizontal)
    {
        float turn = horizontal * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    private void Move(float vertical)
    {
        Vector3 movement = transform.forward * vertical * movementSpeed * Time.deltaTime;
        rigidBody.MovePosition(rigidBody.position + movement);
    }

    public void SetValues(TankScriptable tankScriptable)
    {
        health = tankScriptable.health;
        healthSlider.maxValue = health;
        tankType = tankScriptable.tankType;
        movementSpeed = tankScriptable.movementSpeed;
        turnSpeed = tankScriptable.turnSpeed;
        for (int i= 0; i < tankParts.Length; i++)
        {
            tankParts[i].material = tankScriptable.material;
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthSlider.value = health;
        if (health <= 0)
        {
            SoundManager.Instance.PlaySoundAtTrack1(tankExplosion, 1, 10);
            explosionController.Explode(tankExplosionParticle);
            Destroy(gameObject);
        }
    }
}
