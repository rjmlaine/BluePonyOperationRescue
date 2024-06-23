using UnityEngine;

public class canadaGoose : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float collidierDistance;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Attack Sound")]
    [SerializeField] private AudioClip attacksound;

    // Referencess

    private Animator anim;
    private Health playerHealth;
    private Patrol enemyPatrol;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<Patrol>();
    }

    private void Update()
        {
            cooldownTimer += Time.deltaTime;


            // Attack only when player is seen
            if (Playersee())
            {
                if (cooldownTimer >= attackCooldown && playerHealth.currentHealth > 0)
                {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
                SoundManager.instance.PlaySound(attacksound);
            }
            }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !Playersee();

         }

    private bool Playersee()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collidierDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * collidierDistance, 
        new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
         // If player is in the range 

          if (Playersee())
           playerHealth.Damage(damage);

        
    }

}
