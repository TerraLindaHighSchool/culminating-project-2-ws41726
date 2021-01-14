using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{

    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    

    private void OnCollisionEnter(Collision collision)
    {
        //initiates explosion when colliding with player
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }

        if (collision.gameObject.CompareTag("Menemy"))
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        }



    }

}
