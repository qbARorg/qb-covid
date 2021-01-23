using System.Collections.Generic;
using UnityEngine;

public class GelController : MonoBehaviour
{
    #region Attributes

    public ParticleSystem splatterParticleSyst;

    private ParticleSystem mainParticleSyst;
    private List<ParticleCollisionEvent> collisionEvents;

    #endregion

    #region Unity3D

    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        mainParticleSyst = this.GetComponent<ParticleSystem>();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Deactivate");
        ParticlePhysicsExtensions.GetCollisionEvents(mainParticleSyst, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            EmitAtLocation(collisionEvents[i]);
        }

        if(other.transform.parent.name.StartsWith("Controller"))
        {
            Debug.Log("Deactivate");
            other.SetActive(false);
        }
    }

    #endregion

    #region Private Methods

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticleSyst.transform.position = particleCollisionEvent.intersection;
        splatterParticleSyst.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        splatterParticleSyst.Emit(1);
    }

    #endregion
}
