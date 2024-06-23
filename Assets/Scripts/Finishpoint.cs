using UnityEngine;

public class Finishpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneController.instance.Nextlevel(); // Kun pelaaja osuu menn‰‰n seuraavalle tasolle
        }
    }
}
