using UnityEngine;
// Use on gameobjects which have particle system and needs to be
// destroyed after the particle's duration is over
// So we won't get useless gameobject hanging around the scene anymore
public class DestroyAfterParticleTimer : MonoBehaviour
{
    private ParticleSystem particle;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();

        if (particle == null)
            Debug.LogError("There is no particle system attached to this gameobject!");
        else
        {
            // Destroy this gameobject after duration is over.
            // Duration is multiplied by two in case particle lifetime
            // is higher than duration, so it won't look funny
            // when the gameobject is destroyed
            Destroy(gameObject, particle.main.duration * 2);
        }
    }
}