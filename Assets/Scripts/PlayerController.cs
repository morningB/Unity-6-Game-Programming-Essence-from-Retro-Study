using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    // 플레이어의 이동에 사용될 컴포넌트
    private Rigidbody playerRigidBody;
    // 플레이어 속력
    public float speed = 8f;


    void Start()
    {
        //gameObject에서 RigidBody Component를 찾아서 할당.
        playerRigidBody = GetComponent<Rigidbody>();        
    }

    
    void Update()
    {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");

        float xSpeed = xInput * speed;
        float zSpeed = zInput * speed;

        Vector3 newVelocity = new Vector3(xSpeed,0,zSpeed);
        playerRigidBody.linearVelocity = newVelocity;
    }
    public void Die()
    {
        gameObject.SetActive(false);

        GameManager gameManager = FindFirstObjectByType<GameManager>();
        gameManager.EndGame();
    
    }
}
