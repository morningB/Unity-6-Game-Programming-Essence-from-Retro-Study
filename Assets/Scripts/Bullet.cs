using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private Rigidbody bulletRigidBody;
    
    // start에 구현한 이유는 탄알은 여러개 발사하기에 처음만 설정해주면 됨.
    void Start()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
        
        bulletRigidBody.linearVelocity = speed * transform.forward;
        
        // 3초 뒤에 파괴
        Destroy(gameObject,3f);
    }

    void OnTriggerEnter(Collider other)
    {   

        // Player 태그를 아까 변경함.
        if(other.tag == "Player")
        {
            // 충돌한 상대방 게임 오브젝트에 PlayerController에 없으면 null이 할당.
            PlayerController playerController = other.GetComponent<PlayerController>();
            // 게임 오브젝트가 존재하면
            if(playerController != null)
            {
                playerController.Die();
            }
        }
    }
   
}
