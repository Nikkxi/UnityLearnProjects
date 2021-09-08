using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator animator;
    private AudioSource audioSource;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtTrailParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    public float jumpForce = 650.0f;
    public float gravityMultiplier = 3.0f;
    public bool isOnGround = true;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.loop = true;

        Physics.gravity *= gravityMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRB.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            animator.SetTrigger("Jump_trig");

            audioSource.PlayOneShot(jumpSound, 1.0f);

            dirtTrailParticle.Stop();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtTrailParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game Over!");
            gameOver = true;
            explosionParticle.Play();
            dirtTrailParticle.Stop();

            audioSource.PlayOneShot(crashSound, 1.0f);

            animator.SetBool("Death_b", true);
            animator.SetInteger("DeathType_int", 1);
        }
    }
}
