using UnityEngine;

public class PlayerRespaw : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; // Checkpoint efekti kun checkpointtiin osuu
    private Transform currentCheckpoint;                // Tallentaa viimisimm‰n checkpointin
    private Health playerHealt;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealt = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        // Katsotaan onko checkpointia saatavilla
        if (currentCheckpoint == null)
        {
            uiManager.Gameover(); // N‰ytet‰‰n game over

            return; // ei suoriteta loppu funktiota
        }


        transform.position = currentCheckpoint.position; // liikuttaa pelaajan checkpoint kohtaan
        playerHealt.Respawn();                           // Tallentaa pelaajan el‰m‰t ja resetoi animaatiot

        // Liikutetaan kamera checkpointiin
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    // Checkpointin aktivointi
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Checkpoint")
        {
            currentCheckpoint = collision.transform;                    // tallenetaan aktiivinen checkpoint nykyisen‰
            SoundManager.instance.PlaySound(checkpointSound);           // Checkpoint ‰‰ni efekti
            collision.GetComponent<Collider2D>().enabled = false;       // Deaktivoidaan checkpointin collidier
            collision.GetComponent<Animator>().SetTrigger("active");    // Aktivoidaan checkpointin animaatio
        }
    }
}
