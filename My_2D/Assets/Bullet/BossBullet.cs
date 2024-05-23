using UnityEngine;
using UnityEngine.SceneManagement;
namespace other
{
    public class BossBullet : MonoBehaviour {

        public float Speed = 10f;
        public float damageRadius = 0.3f; // 피해를 입힐 범위

        private void Start()
        {
            //생성으로부터 2초 후 삭제
            Destroy(gameObject, 4f);
        }

        private void Update()
        {
            DealDamageToBossInRange();
            //두번째 파라미터에 Space.World를 해줌으로써 Rotation에 의한 방향 오류를 수정함
            transform.Translate(Vector2.right * (Speed * Time.deltaTime), Space.Self);
        }
    private void DealDamageToBossInRange()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= damageRadius)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);

                // 총알 파괴
                Destroy(gameObject);
            }
        }
    }
    }
}
