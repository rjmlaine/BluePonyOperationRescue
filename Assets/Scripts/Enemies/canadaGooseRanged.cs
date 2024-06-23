
using UnityEngine;

public class canadaGooseRanged : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] bullets;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float collidierDistance;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Knife Sound")]
    [SerializeField] private AudioClip knifesound;

    // Referenssit

    private Animator anim;
    private Patrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<Patrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;


        // Hyˆk‰t‰‰n vai kun pelaaja on n‰kyy
        if (Playersee())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("knife_attack");   // K‰ytet‰‰n ranged gooseen
                anim.SetTrigger("boss_attack");    // K‰ytet‰‰n boss gooseen 

            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !Playersee();

    }

    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(knifesound);
        cooldownTimer = 0;
        bullets[FindKnifes()].transform.position = firepoint.position;
        bullets[FindKnifes()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindKnifes()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;

    }


    private bool Playersee()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collidierDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collidierDistance,
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
