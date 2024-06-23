
using UnityEngine;
using UnityEngine.UI;

public class Selection : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;   // Optiot
    [SerializeField] private AudioClip changesound;     // vaihto ‰‰ni
    [SerializeField] private AudioClip interactsound;   // vaihto ‰‰ni kun optio on valittu
    private RectTransform rect;
    private int currentPosition;                        // Mik‰ optio on valittuna

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        // Puukon valinta positio

        if(Input.GetKeyDown(KeyCode.UpArrow))  // kun nuoli n‰pp‰int‰ painetaan ylˆs
        {
            ChangePosition(-1);
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow)) // kun nuoli n‰pp‰int‰ painetaan alas
                ChangePosition(1);

        // Optiot
        if (Input.GetKeyDown(KeyCode.Return))  // kun painetaan iso Enter
            Interact();
    }

    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(_change !=0)
            SoundManager.instance.PlaySound(changesound);

        if (currentPosition < 0)
            currentPosition = options.Length - 1;
        else if (currentPosition > options.Length - 1)
            currentPosition = 0;

        // Y suunnan positio vaihto 
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactsound);

        // P‰‰sy n‰pp‰in komponenttiin ja funktion aktivointi
        options[currentPosition].GetComponent<Button>().onClick.Invoke();

    }
}
