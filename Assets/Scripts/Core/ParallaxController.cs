using UnityEngine;
using UnityEngine.UIElements;

public class ParallaxController : MonoBehaviour
{
    private float startpos, length;
    public GameObject cam;
    public float parallaxEffect; // Efektin nopeus kameraan nähden

    private void Start()
    {
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect; // Lasketaan liikutuksen etäisyys kameraan ( 0=liikkuu kameran mukana, 1= ei liiku, 0.5 = puolet)
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

        // Jos tausta saavuttaa lopun -> säädetään positio infinite scrolling
        if(movement > startpos + length)
        {
           startpos += length;
        }
        else if(movement < startpos - length)
        {
            startpos -= length;
        }
    }
}
