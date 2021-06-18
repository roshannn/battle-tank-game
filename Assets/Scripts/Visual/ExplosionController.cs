using UnityEngine;

public class ExplosionController : MonoSingletonGeneric<ExplosionController>
{

    public void InstantiateEffects(GameObject Effects, Vector3 position)
    {
        GameObject gameObject = Instantiate(Effects, position, Quaternion.identity);
        Destroy(gameObject, 1f);
    }
}