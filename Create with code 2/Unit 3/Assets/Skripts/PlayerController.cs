using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRB;
    private Animator playerAnim;
    private bool isOnGround = true;
    public bool isGameOver = false;

    public float jumpForce = 10f;
    public float gravityModifier = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            PlayerJump();
        }
    }

    private void PlayerJump()
    {
        playerRB.AddForce(Vector3.up * jumpForce, mode: ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        } else if(collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Game over!");
            isGameOver = true;
        }
    }
}
