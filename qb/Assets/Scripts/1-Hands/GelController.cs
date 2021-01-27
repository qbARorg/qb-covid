using System.Collections.Generic;
using UnityEngine;

public class GelController : MonoBehaviour
{
    #region Attributes

    public ARShooting _ARShooting;
    private GameObject[] particleSystems;
    private ParticleSystem splatterParticleSyst;
    private ParticleSystem mainParticleSyst;
    private List<ParticleCollisionEvent> collisionEvents;
    private float time;

    #endregion

    #region Unity3D

    void Start()
    {
        time = 0;
        if(_ARShooting == null) _ARShooting = GameObject.Find("Scene").GetComponent<ARShooting>();
        Debug.Log(_ARShooting);
        particleSystems = GameObject.FindGameObjectsWithTag("ParticleSyst");
        foreach (GameObject ps in particleSystems)
        {
            if(ps.name.StartsWith("Splatter"))
            {
                splatterParticleSyst = ps.GetComponent<ParticleSystem>();
                break;
            }
        }
        mainParticleSyst = this.GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    private void OnParticleCollision(GameObject other)
    {
        if(mainParticleSyst != null && other != null)
        {
            ParticlePhysicsExtensions.GetCollisionEvents(mainParticleSyst, other, collisionEvents);
            for (int i = 0; i < collisionEvents.Count; i++)
            {
                EmitAtLocation(collisionEvents[i]);
            }
        }
        if (/*other.transform.parent.name.StartsWith("Controller") && */other.gameObject.tag == "Virus")
        {
            other.SetActive(false);
            _ARShooting.IncrementEliminatedVirus(time);
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

    #region Public Methods
    
    

    #endregion
}
