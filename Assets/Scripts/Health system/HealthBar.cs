using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image totalhealtbar;
    [SerializeField] private Image currenthealtbar;


    private void Start()
    {
        totalhealtbar.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currenthealtbar.fillAmount = playerHealth.currentHealth / 10;
    }
}
