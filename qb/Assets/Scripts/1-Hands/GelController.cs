using System.Collections.Generic;
using UnityEngine;

public class GelController : MonoBehaviour
{
    #region Attributes
    private ParticleSystem splatterParticleSyst;
    private ParticleSystem mainParticleSyst;
    private List<ParticleCollisionEvent> collisionEvents;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        splatterParticleSyst = GameObject.FindGameObjectWithTag("ParticleSyst").GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        mainParticleSyst = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    private void EmitAtLocation(ParticleCollisionEvent particleCollisionEvent)
    {
        splatterParticleSyst.transform.position = particleCollisionEvent.intersection;
        splatterParticleSyst.transform.rotation = Quaternion.LookRotation(particleCollisionEvent.normal);
        splatterParticleSyst.Play();
    }
}
