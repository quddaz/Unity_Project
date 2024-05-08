using System.Collections;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 5.5f;

    private Vector3 originalPosition;
    private float targetY;
    private bool movingUp = true;

    void Start()
    {
        originalPosition = transform.position;
        targetY = originalPosition.y + moveDistance;
    }

    void Update()
    {
        if (movingUp)
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

            if (transform.position.y >= targetY)
            {
                transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
                movingUp = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

            if (transform.position.y <= originalPosition.y)
            {
                transform.position = originalPosition;
                movingUp = true;
            }
        }
    }

    public void MoveBulletToTarget(Transform bulletTransform, Transform target)
    {
        StartCoroutine(MoveBulletCoroutine(bulletTransform, target));
    }

    private IEnumerator MoveBulletCoroutine(Transform bulletTransform, Transform target)
    {
        Vector3 start = bulletTransform.position;
        Vector3 direction = (target.position - start).normalized;
        float speed = 10f;

        while (Vector3.Distance(bulletTransform.position, target.position) > 0.1f)
        {
            bulletTransform.Translate(direction * speed * Time.deltaTime);
            yield return null;
        }

        Destroy(bulletTransform.gameObject);
    }
}
