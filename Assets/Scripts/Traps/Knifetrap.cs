using System.Collections;
using UnityEngine;

public class Knifetrap : MonoBehaviour
{

    [SerializeField] private float damage;

    [Header("Knifetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activateTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; // Kun ansa triggeroitu
    private bool activate;  // Kun ansa on aktivoitu

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attacksound;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(!triggered)
            {
                StartCoroutine(ActivateKnifetrap());
            }
            if (activate)
                collision.GetComponent<Health>().Damage(damage);
        }
    }
    private IEnumerator ActivateKnifetrap()
    {
        // kun ansa triggeröityy väri vaihtuu punaiseksi
        triggered = true;
        spriteRend.color = Color.red;

        // Viive, ansan aktivointi, animointi ja värin vaihto normaaliin
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(attacksound);
        spriteRend.color = Color.white; //Väri tavalliseksi                         
        activate = true;
        anim.SetBool("activated", true);

        //deaktivointi ja reset
        yield return new WaitForSeconds(activateTime);
        activate = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
}

