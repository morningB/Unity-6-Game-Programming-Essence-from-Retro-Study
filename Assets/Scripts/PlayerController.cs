using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private Animator animator;

    public float speed = 8f;
    public float jump = 100f;
    private bool isJump;

    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); // AnimatorGhost가 붙어있다고 가정
        isJump = false;
    }

    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 moveVelocity = new Vector3(xSpeed, playerRigidBody.linearVelocity.y, zSpeed);
        playerRigidBody.linearVelocity = moveVelocity;

        // 캐릭터 회전
        Vector3 flatVelocity = new Vector3(xSpeed, 0, zSpeed);

        if (flatVelocity.sqrMagnitude > 0.01f)
        {
            transform.rotation = Quaternion.LookRotation(flatVelocity);
          
        }
       
        if (Input.GetKeyDown(KeyCode.Space) && !isJump)
        {
            playerRigidBody.linearVelocity = new Vector3(0,jump,0);
            isJump = true;
        }
        
    }

    public void Die()
    {
        gameObject.SetActive(false);
        FindFirstObjectByType<GameManager>().EndGame();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Plane"))
        {
            isJump = false;
        }
    }
}
