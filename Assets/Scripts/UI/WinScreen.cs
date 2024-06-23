using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameObject wintext;
    public float delay;
    private Animator anim;

    private void Start()
    {
        wintext.SetActive(false);                      // Asetetaan ei aktiiviseksi alussa
        anim = GetComponent<Animator>();               // Animaatio
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            wintext.SetActive(true);                   // Asetetaan teksti aktiiviseksi
            StartCoroutine(Countdown());               // Aloitetaan laskenta
            anim.SetTrigger("open");                   // Asetetaan "open" h‰kin animaatiolle
        }
    }

    IEnumerator Countdown()
    {
        yield return new WaitForSeconds(delay);        // Viive -> kuinka kauan teksti n‰ytet‰‰n
        SceneManager.LoadScene(1);                     // Menn‰‰n takaisin main menuun
    }



}
