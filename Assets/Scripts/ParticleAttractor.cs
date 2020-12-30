using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleAttractor : MonoBehaviour
{
    private ParticleSystem.Particle[] allParticles;
    public Transform destination;
    //public float distanceNeeded = 0.5f;
    public ParticleSystem myParticleSystem;

    void Awake()
    {
        myParticleSystem = this.GetComponent<ParticleSystem>();

        if (myParticleSystem == null)
        {
            Debug.Log("No particle system attached to particle attractor script");
            this.enabled = false;
        }

        allParticles = new ParticleSystem.Particle[myParticleSystem.main.maxParticles];
    }

    private void Start()
    {
        StartCoroutine(GoToPlayer());
    }

    private void Update()
    {
        int numParticles = myParticleSystem.GetParticles(allParticles);
        if (goToTarget)
        {

            for (int i = 0; i < numParticles; i++)
            {
                ParticleSystem.Particle particle = allParticles[i];

                //particle.position = Vector3.Lerp(particle.position, destination.position, (particle.startLifetime - particle.remainingLifetime) / particle.startLifetime);
                particle.position = Vector2.MoveTowards(particle.position, destination.position, 20 * Time.deltaTime);
                allParticles[i] = particle;
            }
        }


        myParticleSystem.SetParticles(allParticles, numParticles);
    }

    bool goToTarget = false;
    IEnumerator GoToPlayer()
    {
        yield return new WaitForSeconds(2f);
        goToTarget = true;
    }
}
