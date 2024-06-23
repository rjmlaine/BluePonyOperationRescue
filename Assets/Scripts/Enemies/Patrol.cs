
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Time behivior")]
    [SerializeField] private float waitTime;
    private float waitTimer;

    [Header("Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }

    // Partio alue
    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
             DirectionChange();
        }
    }

    // Suunnan vaihto
    private void DirectionChange()
    {
        anim.SetBool("moving", false);

        waitTimer += Time.deltaTime;
        
        if(waitTimer > waitTime)
        movingLeft = !movingLeft; // k‰‰nnet‰‰n value
    }

    // Liikutus suunta
    private void MoveInDirection(int _direction)
    {
        waitTimer = 0;
        anim.SetBool("moving", true);
        
        // Suuntaa vihollisen
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);

        // Liikuttaa suuntaus suuntaan
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
