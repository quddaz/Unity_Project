using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shot
{
    public class CircleShotGotoShot : MonoBehaviour
    {
        // 발사될 총알 프리팹
        public GameObject BulletPrefab;

        // 발사 위치와 방향 설정을 위한 프리팹 오프셋
        public Vector3 SpawnOffset;
        public Vector3 BulletDirection;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Shot();
        }

        private void Shot()
        {
            // Target 방향으로 발사될 총알 수록
            List<Transform> bullets = new List<Transform>();

            // 360도를 기준으로 원형으로 총알 발사
            for (int i = 0; i < 360; i += 13)
            {
                // 총알 생성
                GameObject temp = Instantiate(BulletPrefab);

                // 일정 시간 후 삭제
                Destroy(temp, 2f);

                // 총알 위치 설정 (프리팹 기준으로 설정)
                temp.transform.position = transform.position + SpawnOffset;

                // 총알 방향 설정 (프리팹 방향 기준으로 설정)
                temp.transform.rotation = Quaternion.Euler(0, 0, i) * Quaternion.Euler(BulletDirection);

                // 발사된 총알 수록
                bullets.Add(temp.transform);
            }

            // 총알을 Target 방향으로 이동시킴
            StartCoroutine(BulletToTarget(bullets));
        }

        private IEnumerator BulletToTarget(List<Transform> objects)
        {
            // 일정 시간 후에 실행
            yield return new WaitForSeconds(0.5f);

            // 모든 총알에 대해 대상(Target)을 향하도록 회전
            foreach (Transform bullet in objects)
            {
                Vector3 targetDirection = (Target.position - bullet.position).normalized;
                float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                bullet.rotation = Quaternion.Euler(0, 0, angle);
            }

            // 데이터 해제
            objects.Clear();
        }
    }
}
