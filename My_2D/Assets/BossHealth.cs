using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 200; // 최대 체력
    private int currentHealth = 200;   // 현재 체력

    public UnityAction OnHealthChanged; // 체력이 변경될 때 호출할 이벤트

    public Slider healthSlider; // 연결할 UI Slider

    private void Start()
    {
        currentHealth = maxHealth; // 게임 시작 시 최대 체력으로 초기화

        // healthSlider를 Scene에서 찾아서 할당
        healthSlider = GameObject.FindWithTag("BossHealthSlider").GetComponent<Slider>();

        // 보스 체력 UI 초기화
        UpdateHealthSlider();
    }

    // 피해를 입는 메서드
    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // 현재 체력에서 피해만큼 감소

        // 체력이 0 이하로 떨어지면 사망 처리
        if (currentHealth <= 0)
        {
            Die(); // 사망 처리 메서드 호출
        }

        // 체력이 변경되었음을 알리는 이벤트 호출
        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke();
        }

        // 체력 UI 업데이트
        UpdateHealthSlider();
    }

    // 보스 사망 처리 메서드
    private void Die()
    {
        Debug.Log("Boss is defeated!");
        // 보스가 죽었을 때 필요한 처리를 여기에 추가 (예: 애니메이션 재생, 게임 오버 등)
        Destroy(gameObject); // 보스 오브젝트 파괴 (혹은 비활성화 등)
    }

    // 현재 체력을 반환하는 Getter 메서드
    public int CurrentHealth
    {
        get { return currentHealth; }
    }

    // 체력 슬라이더 업데이트 메서드
    private void UpdateHealthSlider()
    {
        // 현재 체력을 슬라이더 값으로 설정 (0 ~ 1 사이 값으로 변환)
        float healthRatio = (float)currentHealth / maxHealth;
        healthSlider.value = healthRatio;
    }
}
