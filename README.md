### 유니티의 참조와 값

| 참조 타입 | 값 타입 |
| --- | --- |
| class | c# 내장 변수 (bool, int, floot 등) |
| unity 모든 component | struct 구조체(Vector3, Color 등) |
| 작성하는 c# 스크립트 |  |

### 브로드캐스팅 (Broadcasting or 이벤트 시스템)

- **한 번의 메시지로 여러 객체에게 동시 전달**되는 방식
- 모든 `Component`는 `MonoBehaviour`를 상속받는다.

**작동 원리**

- Unity에서 특정 이름을 가진 메서드를 가진 모든 `MonoBehaviour`에게 자동으로 메시지를 전달
    
    → 예: 메시지를 받을 수 있는 컴포넌트가 100개라면, 100개 모두 전달받음
    
- **자신과 관련 없는 메시지**라면 해당 컴포넌트는 **무시**함
- 메시지를 보내는 쪽은 **수신자에 대해 알 필요 없음**
    
    → 수신자도 **누가 메시지를 보냈는지 알 수 없음**
    

**장점**

- 컴포넌트 간 **독립성** 유지
- 구조가 단순하고 확장성이 높음

**발신자와 수신자가 서로를 알 필요 없음**

- 메시지를 보내는 쪽은 *누가 받는지* 모르고
- 받는 쪽도 *어디서 왔는지* 알지 못함

**유니티 내 대표적인 브로드캐스트 메서드**

- 유니티 이벤트 메서드 : 브로드캐스팅 되기 위해 구현된 메서드
    - **대표적인 예:**
    - `Start()`, `Update()`, `OnTriggerEnter()` 등

### 인스펙터 창 주요 컴포넌트

- Transform
    - 오브젝트의 **Position**, **Rotation**, **Scale**를 담당
    - **부모 오브젝트 정보**(숨겨져 있음)
- Mesh Filter
    - 오브젝트의 **형태(모양)** 를 결정하는 컴포넌트
    - 어떤 3D **Mesh 데이터를 렌더링할지 지정**

### Class

- 멤버 **변수**와 멤버 **함수**로 조합
- class(타입)로 생성한 객체를 Instance라고도 함.

```csharp
Human human = new Human(); // 인스턴스 생성
```

- 오브젝트 (Object)
    - 상태와 기능으로 이루어짐
    - 인스턴스를 포함하는 더 넓은 개념
    - `p1`, `p2`, `enemy` 등 모든 동작하는 객체


### 유니티는 **왼손 좌표계**를 사용

- **X축**: 오른쪽이 +, 왼쪽이 −
- **Y축**: 위쪽이 +, 아래쪽이 −
- **Z축**: X축을 기준으로 왼쪽이 +, 오른쪽이 − (오브젝트의 전방)

---

## 월드 위치와 로컬 위치

### 1. 전역 위치 (World Position)

- 씬 전체에서의 **절대 좌표**
- 부모 유무와 관계없이, **월드 공간** 기준
    
    `transform.position`
    

### 2. 로컬 위치 (Local Position)

- **부모 오브젝트를 기준**으로 한 상대 위치
- 부모의 좌표계(Local Space)를 기준으로 계산됨
    
    `transform.localPosition`
    

### 예시

- 부모 A의 위치: `(5, 0, 0)`
- 자식 B의 로컬 위치: `(2, 0, 0)`
    
    → 자식 B의 전역 위치는 `(7, 0, 0)`
    

---

## 회전 개념

- 회전은 축을 기준으로 이루어진다
- `Transform.Rotate()` 또는 `Quaternion.Euler()`로 조작 가능
- 로컬 회전과 전역 회전을 구분해야 함

### 그리드 단위

- 1칸 = 1미터 단위로 배치
- 실제 물리 크기를 기준으로 구현 → 충돌 처리에 영향을 줌

---

### Dodge 게임

> Player 만들기
> 
- Player를 만들고 태그를 Player를 설정.
    - Rigidbody는 **움직이는 대상에만** 적용
    - 너무 많은 오브젝트에 적용하면 **계산 비용 증가**

> 충돌 설정
> 
- 오브젝트의 모양대로 충돌을 하고 싶다면 Mesh Collider 사용.
- 제약을 줘서 물리적 상호작용으로부터 위치나 회전을 막음. (직접적인 변경은 됨)
    
    ![스크린샷 2025-04-17 오전 9.31.25.png](attachment:cdeae33d-1ce6-4346-baa7-15167cc70962:스크린샷_2025-04-17_오전_9.31.25.png)
    
- Position Y를 하면 위로 오르는 것을 막음
- Rotation X, Z를 하면
    - **X축 회전 제한:** 앞뒤로 기울거나 구르는 동작 없음
    - **Z축 회전 제한:** 옆으로 기울거나 굴러가는 회전 없음
    
- PlayerController 생성
    - 스크립트
        
        ```csharp
        using UnityEngine;
        
        public class PlayerController : MonoBehaviour
        {
            
            // 플레이어의 이동에 사용될 컴포넌트
            public Rigidbody playerRigidBody; 
            // 플레이어 속력
            public float speed = 8f;
        
            void Start()
            {
                
            }
         
            void Update()
            {
                if(Input.GetKey(KeyCode.UpArrow) == true)
                {
                    playerRigidBody.AddForce(0,0,speed);
                }
                else if(Input.GetKey(KeyCode.DownArrow) == true)
                {
                    playerRigidBody.AddForce(0,0,-1 * speed);
                }
                else if(Input.GetKey(KeyCode.RightArrow) == true)
                {
        	         playerRigidBody.AddForce(speed,0,0);   
                }
                else if(Input.GetKey(KeyCode.LeftArrow) == true)
                {
                    playerRigidBody.AddForce(-1 * speed, 0,0);
                }
            }
        }
        
        ```
        
        - GetKeyDown : 누르는 순간 true
        - GetKeyUp : 떼는 순간 ture
        - KeyCode.UpArrow를 == “A” 대신 사용 이유.
            - 반환 타입이 enum임
            - 자유롭게 UpArrow에 마음대로 매핑가능.
                
                ![스크린샷 2025-04-17 오전 10.22.25.png](attachment:e35a0363-2413-4c8d-bfe3-14fb04700665:스크린샷_2025-04-17_오전_10.22.25.png)
                
    - 비활성화되어 모든 Component도 꺼짐.
    
    ```csharp
     public void Die()
    {  //활성화
            gameObject.SetActive(false);
    }
    ```
    
    - `gameObject` :
        - 모든 컴포넌트가 지님
        - 오브젝트에 할당하여 불러올 수 있음.
    - `GameObject` : 오브젝트 타입을 의미하는 클래스임
    - 스크립트 보완(void Update())
        
        ```csharp
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
        
        ```
        
        - 만약 GetComponent`<Rigidbody>`가 없다면 하나씩 다 구현해야됨.
            - GetComponentRigidbody(); GetComponentCollider(); 등등
        - Addforce와 LinearVelocity의 차이 : 관성 무시
        - Awake : start 하기 전에 초기화시켜야해야함.
        - `Awake` → `OnEnable` → `Start`
        - 만약 다른 스크립트에서 Player의 정보를 가져오고 싶다면?
            - PlayerController를 Public으로 변경.
            
            ```csharp
            using UnityEngine;
            
            public class  Enemy : MonoBehaviour
            {
                private Rigidbody rb;
                
                void Start()
                {
            		  
            	    rb = GameObject.Find("Player").GetComponent<Rigidbody>(); 
            	    //rb = GameObject.Find("Player").GetComponent<PlayerController>().playerRigidBody;    
            	  }
                
            }
            
            ```
            

### 프리팹(PreFab)

- 미리 만들어진 오브젝트 파일
- 비슷한 게임 오브젝트를 여러개 만들 때 유용
    1. 오브젝트를 생성함
    2. 프로젝트 창으로 드래그함
    3. 속이 차있는 파란색 큐브 모양으로 바뀜

> Bullet 만들기
> 
- Is Trigger 체크
    
    ![스크린샷 2025-04-17 오후 3.24.14.png](attachment:07a8b44f-3ea4-4596-a2b7-7a53e92cf7ca:스크린샷_2025-04-17_오후_3.24.14.png)
    
- 스크립트
    
    ```csharp
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
                PlayerController playerController 
    			            = other.GetComponent<PlayerController>();
    			            // other를 안하면 bullet의 playController가 가져와짐
                // 게임 오브젝트가 존재하면
                if(playerController != null)
                {
                    playerController.Die();
                }
            }
        }
    }
    ```
    
    - `transform.forward` : 앞방향
    
    충돌 처리 함수
    
    - `OnTriggerEnter(Collider other)`  서로 그대로 통과, Is Trigger를 체크해야됨
        - Collision이 아닌 이유 : 상세한 충돌 정보가 필요 없음.
    - `OnCollisionEntecr(Collision collision)` 서로 튕겨냄, Is Trigger를 해제해야됨
        - Stay : 충돌하는 동안
        - Exit : 충돌하고 분리되는 순간
    - 상대 오브젝트의 정보, 충돌 지점, 표현의 방향을 서로 전달함
    - Is Trigger를 하나의 오브젝트만 해도 OnTrigger가 실행됨.
        
        → 여기에 Player에 IsTrigger를 하지 않는 이유는 Player는 벽과 바닥에 통과되면 안됨.
        
    
- Prefab을 오브젝트로 만들고 그 오브젝트를 변경했다면 그림 1처럼 됨.
- 변경사항은 파란색으로 보임(그림 2)
    
    ![그림 1](attachment:3b6d8993-be89-433e-ba3b-a3c490b630c0:스크린샷_2025-04-17_오후_5.00.15.png)
    
    그림 1
    
    ![그림 2](attachment:8003728f-c36b-404e-b2ff-a36f59b521b7:스크린샷_2025-04-17_오후_5.01.34.png)
    
    그림 2
    
- Revert는 되돌리기, Apply는 변경 적용

> BulletSpawner
> 
- 스크립트
    
    ```
    using UnityEngine;
    
    public class BulletSpawner : MonoBehaviour
    {
        public GameObject bulletPrefab; // 탄알 생성
        public float spawnRateMin = 0.5f; // 최소 시간
        public float spawnRateMax = 3f; // 최대 시간
        
        private Transform target; // 대상(Player)의 위치
        private float spawnRate; // 생성 주기
        private float timeAfterSpawn; // 생성 후 지난 시간
    
        void Start()
        {
            timeAfterSpawn = 0f; // 반환 시간 초기화
            
            spawnRate = Random.Range(spawnRateMin,spawnRateMax);
            // Player의 transform을 찾고 할당함.
            // 모든 오브젝트를 찾기 때문에 start에서만 사용하기를 추천
            target = FindFirstObjectByType<PlayerController>().transform;
            
        }
      
        void Update()
        {
            timeAfterSpawn += Time.deltaTime;
    
            if(timeAfterSpawn >= spawnRate)
            {
                timeAfterSpawn = 0;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.LookAt(target);
                spawnRate = Random.Range(spawnRateMin, spawnRateMax);
            }
        }
    }
    
    ```
    
    - `target = FindFirstObjectByType<PlayerController>().transform;`
        - 씬에 있는 모든 오브젝트를 검색해 해당 타입의 오브젝트를 가져옴.
        
        → Start에 구현하는 이유 :  처음 한번만 얻는 것처럼 보이지만, PlayerController의 값을 계속 **참조**하기에 값이 변한다.
        
        → `Tansform`은 참조 타입
        
        - 실제로 값을 업데이트할 때는 target을 사용할 때인`bullet.transform.LookAt(target);`
    - `Debug.Log(target.position.x + " " + target.position.y + " "+ target.position.y);`
        
        ![로그로 확인한 transform](attachment:0068c9e9-71fc-4487-be57-c06bcf7ab98e:스크린샷_2025-04-17_오후_5.18.44.png)
        
        로그로 확인한 transform
        
    - `Instantiate(원본, 위치, 회전)` : 탄알 복제 시 사용(오브젝트를 생성할 때 주로 사용)
        - GameObject를 return
    - `bullet.transform.LookAt(target);`  : LookAt : 회전 함수, target을 보도록 회전만시킨다.

> Rotator
> 

```csharp
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float rotationSpeed = 60f;

    void Update()
    {
        transform.Rotate(0f,rotationSpeed * Time.deltaTime,0f);
    }
}

```

- Time.deltaTime을 안하면 폭풍 회전함.
- 그 이유는 1프레임에 60도를 회전함
- 각 컴퓨터만의 주사율에 따라 달라짐

- UI 관리하는 GameManager.cs
- `PlayerPrefs` (로컬 저장소) 사용 이유
    - 별도 저장 파일이나 DB 없이, 키-값 형태로 쉽게 저장/불러오기 가능
    - **플레이어의 장치(Local)에 저장되며**, 앱을 꺼도 유지됨
