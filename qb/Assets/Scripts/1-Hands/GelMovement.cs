using System.Collections.Generic;
using UnityEngine;

public class GelMovement : MonoBehaviour
{
    #region Attributes
    public ParticleSystem mainParticleSyst;
    private ParticleSystem splatterParticleSyst;
    private List<ParticleCollisionEvent> collisionEvents;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        collisionEvents = new List<ParticleCollisionEvent>();
        splatterParticleSyst = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Let's emit");
        ParticlePhysicsExtensions.GetCollisionEvents(mainParticleSyst, other, collisionEvents);
        for (int i = 0; i < collisionEvents.Count; i++)
        {
            Debug.Log("Let's emit");
            EmitAtLocation(collisionEvents[i]);
        }
    }

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticleSyst.transform.position = particleCollisionEvent.intersection;
        splatterParticleSyst.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        splatterParticleSyst.Play();
    }
}
