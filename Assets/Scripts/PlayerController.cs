using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private GameManager gameManager;
    private float powerupStrength = 15.0f;
    public float speed = 5.0f;
    public bool hasPowerup;
    public GameObject powerupIndicator;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        if (transform.position.y < -3)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.GameOver();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
          if (other.CompareTag("Powerup"))
            {
                hasPowerup = true;
                Destroy(other.gameObject);
                StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
            }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
            {
                Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
                Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();
                Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);
                Debug.Log("Player collided with " + collision.gameObject + " with powerup set to " + hasPowerup);
                enemyRigidBody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
    }
}
