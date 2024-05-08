using UnityEngine;

namespace Shot
{
    public class SpinShot : MonoBehaviour
    {
        // 회전되는 속도
        public float turnSpeed;

        // 발사될 총알 프리팹
        public GameObject bulletPrefab;

        // 총알 발사 위치를 가리키는 빈 오브젝트
        public Transform bulletSpawnPoint;

        public float spawnInterval = 0.5f;
        private float spawnTimer;

        private void Update()
        {
            // 기본 회전
            transform.Rotate(Vector3.forward * (turnSpeed * Time.deltaTime));

            // 생성 간격 처리
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnBullet();
                spawnTimer = 0f;
            }
        }

        private void SpawnBullet()
        {
            if (bulletSpawnPoint == null || bulletPrefab == null)
            {
                Debug.LogWarning("Bullet spawn point or bullet prefab is not assigned.");
                return;
            }

            // 총알을 발사 위치에서 생성
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            Destroy(bullet, 3f); // 일정 시간 후에 총알 파괴
        }
    }
}
