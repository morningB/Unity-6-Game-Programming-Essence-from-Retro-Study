using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab; // 탄알 생성
    public float spawnRateMin = 0.5f; // 최소 시간
    public float spawnRateMax = 3f; // 최대 시간
    
    private Transform target;
    private float spawnRate;
    private float timeAfterSpawn;

    void Start()
    {
        timeAfterSpawn = 0f; // 반환 시간 초기화
        
        spawnRate = Random.Range(spawnRateMin,spawnRateMax);
        // Player의 transform을 찾고 할당함.
        // 모든 오브젝트를 찾기 때문에 start에서만 사용하기를 추천
        // 처음 한번만 얻는 것처럼 보이지만, PlayerController의 값을 계속 참조하기에 값이 변한다.
        target = FindFirstObjectByType<PlayerController>().transform;
        
        //target = GameObject.Find("Player").GetComponent<Transform>();
        
        
    }

    
    void Update()
    {
        timeAfterSpawn += Time.deltaTime;

        if(timeAfterSpawn >= spawnRate)
        {
            timeAfterSpawn = 0;
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            //LookAt : 회전 함수, target을 보도록 회전시킨다.
            bullet.transform.LookAt(target);
            
            spawnRate = Random.Range(spawnRateMin, spawnRateMax);
        }
    }
}
