using UnityEngine;
using UnityEngine.UI;

public class Levelbar : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private Image uifillimage;                 // Liukuva kuva

    [Header("Player and end references")]
    [SerializeField] private Transform playerTransform;         // Referenssi pelaajaan
    [SerializeField] private Transform endlineTransform;        // Referenssi loppu pisteeseen

    private Vector3 endlinePosition;
    private float fulldistance;

    private void Start()
    {
        endlinePosition = endlineTransform.position;
        fulldistance = GetDistance();
    }

    private float GetDistance()
    {
        return Vector3.Distance(playerTransform.position, endlinePosition);
    }

    private void UpdateProgressFill(float value)
    {
        uifillimage.fillAmount = value;                         // Kuvan täytön määrä
    }

    private void Update()
    {
        if (playerTransform.position.x <= endlinePosition.x) 
        {
            float newDistance = GetDistance();
            float progressValue = Mathf.InverseLerp(fulldistance, 0f, newDistance);

            UpdateProgressFill(progressValue);
        }
   
    }
}
