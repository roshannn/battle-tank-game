using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    
    public void Explode(ParticleSystem explosionParticle)
    {
        explosionParticle.transform.parent = null;
        explosionParticle.Play();
        Destroy(explosionParticle.gameObject, 2f);
    }
}