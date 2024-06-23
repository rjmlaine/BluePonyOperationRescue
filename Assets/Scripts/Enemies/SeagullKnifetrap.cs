using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class SeagullKnifetrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;  // Hyökkäyksestä toipumis aika
    [SerializeField] private Transform firepoint;   // Ampumis piste
    [SerializeField] private GameObject[] knifes;   // Ammukset 
    private float cooldowntimer;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attacksound;


    private void Attack()
    {
        cooldowntimer = 0;
        SoundManager.instance.PlaySound(attacksound);
        knifes[findKnife()].transform.position = firepoint.position;
        knifes[findKnife()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int findKnife()
    {
        for (int i = 0; i < knifes.Length; i++)
        {
            if (!knifes[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    private void Update()
    {
        cooldowntimer += Time.deltaTime;

        if (cooldowntimer >= attackCooldown)
            Attack();

    }
}
