using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [Header("Moving parameters")]
    [SerializeField] private float speed;       // Liikkumis nopeus
    [SerializeField] private float jumpPower;   // Hypyn voima

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer; // Layer referenssit maalle
    [SerializeField] private LayerMask wallLayer;   // Layer referenssit sein‰lle

    [Header("Sound")]
    [SerializeField] private AudioClip jumpsound;  // Hypyn ‰‰net

    [Header("Air hanging Time")]
    [SerializeField] private float airtime; // S‰‰t‰‰ ilmassa olo aikaa ennen kuin hyp‰t‰‰n
    private float aircounter;               // Kuinka paljon aikaa menee kun ylitet‰‰n reuna

    [Header("Multi jumping")]
    [SerializeField] private int extrajumps; // S‰‰t‰‰ kuinka monta hyppy‰ voi tehd‰ putkeen
    private int jumpcounter;

    [Header("Wall jumping")] 
    [SerializeField] private float walljumpx; // S‰‰t‰‰ sein‰ hyppyvoimaa horisontaalisti
    [SerializeField] private float walljumpy; // S‰‰t‰‰ sein‰ hyppyvoimaa vertikaalisesti

    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalinput;


    private void Awake()
    {
        // Referenssit
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalinput = Input.GetAxis("Horizontal");

        // Pelaajan k‰‰ntˆ
        if (horizontalinput > 0.01f)
            transform.localScale = Vector3.one;

        else if (horizontalinput < -0.01f)
            transform.localScale = new Vector3(-1,1,1);


        // Animaatio parametrit
        anim.SetBool("walk", horizontalinput != 0);
        anim.SetBool("grounded", isgrounded());

        // Hyppy 
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // S‰‰dett‰v‰ hyppykorkeus
        if(Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
            body.velocity = new Vector2(body.velocity.x,body.velocity.y/2);

        // Sein‰hyppy
        if (onwall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 3;
            body.velocity = new Vector2(horizontalinput * speed, body.velocity.y);

            if (isgrounded())
            {
                aircounter = airtime;  // reset air counter
                jumpcounter = extrajumps; // reset jump counter -> extrajump value
            }
            else
                aircounter -= Time.deltaTime; // Aletaan laskea air counteria kun ei olla maassa 
        }
    }

    private void Jump()
    {
        if (aircounter < 0 && !onwall() && jumpcounter <= 0) return; // Jos ilma-ajastin on 0 tai v‰hemm‰n eik‰ se ole sein‰ss‰ eik‰ siin‰ ole ylim‰‰r‰isi‰ hyppyj‰ -> ei tehd‰ mit‰‰n

        SoundManager.instance.PlaySound(jumpsound);

        if (onwall())
            WallJump();
        else
        {
            if (isgrounded())
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            else
            {
                // Jos ei olla maassa ja air counter on isompi kuin 0 tehd‰‰n normaalihyppy
                if(aircounter > 0)
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                else
                {
                    if(jumpcounter > 0) // Jos ei ole extra hyppyj‰ lasketaan counteri
                    {
                      body.velocity = new Vector2(body.velocity.x, jumpPower);
                        jumpcounter--;
                    }
                }
            }

            // reset air counter 0 ett‰ v‰ltet‰‰n tupla hyppy
            aircounter = 0;
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * walljumpx, walljumpy));
    }


    // Normaali hyppy kun ollaan maassa
    private bool isgrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null ;
    }

    // Sein‰ hyppy
    private bool onwall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    // Hyˆkk‰ys
    public bool canAttack()
    {
        return horizontalinput == 0 && isgrounded() && !onwall();
    }
}