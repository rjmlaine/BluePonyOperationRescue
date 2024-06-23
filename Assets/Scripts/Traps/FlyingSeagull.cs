using System.Security.Cryptography;
using UnityEngine;

public class FlyingSeagull : EnemyDamage
{
    [Header("Seagull values")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkdelay;
    [SerializeField] private LayerMask playerLayer;
    private Vector3 destination;
    private float checktimer;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];

    [Header("Sound")]
    [SerializeField] private AudioClip hitsound;

    private void OnEnable()
    {
        Stop();
    }


    // Liikutetaan lokkia vain jos pelaaja näkyy
    private void Update()
    {
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checktimer += Time.deltaTime;
            if (checktimer > checkdelay)
                CheckForPlayer();
        }
    }

    // Katsotaan näkyykö pelaajaa
    private void CheckForPlayer()
    {
        CalculateDirectionss();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checktimer = 0;
            }
        }
    }

    // Suuntien laskenta
    private void CalculateDirectionss()
    {
        directions[0] = transform.right * range;    // Oikea
        directions[1] = -transform.right * range;   // vasen
        directions[2] = transform.up * range;       // ylös
        directions[3] = -transform.up * range;      // alas
    }

    private void Stop()
    {
        destination = transform.position; // asetetaan nykyinen paikka
        attacking = false;
    }

    // Collision hallinta

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(hitsound);
        base.OnTriggerEnter2D(collision);

        Stop(); // Pysäytetään lokki kun se osuu objekteihin


    }


}
