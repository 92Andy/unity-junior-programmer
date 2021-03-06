using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    private AudioSource audioPlayer;

    public ParticleSystem explosionParticle;
    public ParticleSystem dirtSplatterParticle;

    public AudioClip jumpSound;
    public AudioClip crashSound;

    private bool isOnGround = true;
    public bool isGameOver = false;

    public float jumpForce = 10f;
    public float gravityModifier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        audioPlayer = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && !isGameOver)
        {
            PlayerJump();
        }
    }

    private void PlayerJump()
    {
        audioPlayer.PlayOneShot(jumpSound, 1);
        dirtSplatterParticle.Stop();
        playerRB.AddForce(Vector3.up * jumpForce, mode: ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtSplatterParticle.Play();
        } else if(collision.gameObject.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game over!");
        isGameOver = true;
        dirtSplatterParticle.Stop();
        audioPlayer.PlayOneShot(crashSound, 1);
        explosionParticle.Play();
        playerAnim.SetInteger("DeathType_int", 1);
        playerAnim.SetBool("Death_b", true);
    }
}
