
using UnityEngine;

public class HealthCollect : MonoBehaviour
{
    [SerializeField] private float healthValue;

    [Header("Sound")]
    [SerializeField] private AudioClip collectsound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(collectsound);
            collision.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
