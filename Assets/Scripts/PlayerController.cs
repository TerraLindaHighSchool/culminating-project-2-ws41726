using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private GameManager gameManager;
    private float powerupStrength = 15.0f;
    private float powerup2Strength = 5.0f;
    public float speed = 5.0f;
    public bool hasPowerup;
    public bool hasPowerup2;
    public GameObject powerupIndicator;
    public GameObject powerupIndicator2;
    public GameObject projectilePrefab;
    public GameObject projectilePrefab2;
    public GameObject projectilePrefab3;
    public GameObject projectilePrefab4;
    public ParticleSystem explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        gameManager = GameObject.Find("Game Manager").GetComponent <GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector3.forward * speed * forwardInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);
        powerupIndicator2.transform.position = transform.position + new Vector3(0, 0.8f, 0);

        //IF Player passes -3 game will end
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.GameOver();
        }

        //Superpower2 controls
        if (hasPowerup2)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
                Instantiate(projectilePrefab2, transform.position, projectilePrefab2.transform.rotation);
                Instantiate(projectilePrefab3, transform.position, projectilePrefab3.transform.rotation);
                Instantiate(projectilePrefab4, transform.position, projectilePrefab4.transform.rotation);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Destroys powerup and gives powerup ability to player  
        if (other.CompareTag("Powerup"))
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }

        //Destroys powerup and gives powerup ability to player    
        if (other.CompareTag("Powerup2"))
        {
            hasPowerup2 = true;
            Destroy(other.gameObject);
            StartCoroutine(Powerup2CountdownRoutine());
            powerupIndicator2.gameObject.SetActive(true);
        }
    }



    //Powerup Countdown routine
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    //Powerup2 Countdown routine
    IEnumerator Powerup2CountdownRoutine()
    {
        yield return new WaitForSeconds(5);
        hasPowerup2 = false;
        powerupIndicator2.gameObject.SetActive(false);
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        //launches enemy away from player when in contact with powerup
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
