using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public ParticleSystem explosionParticle;

    private Rigidbody enemyRb;
    private GameObject player;
    private GameManager gameManager;
    private Vector3 lookDirection;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (player != null)
        {
              lookDirection = (player.transform.position - transform.position).normalized;
        }
        
        enemyRb.AddForce(lookDirection * speed);
        //Debug.Log("Update of Enemy" + transform.position.y);
        if (transform.position.y < -3)
        {
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(1);
       Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(1);
        }
    }
}
