using UnityEngine;

public class Knifeholder : MonoBehaviour
{
    [SerializeField] private Transform enemy;

    private void Update()
    {
    transform.localScale = enemy.localScale;
    }
}
