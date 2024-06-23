using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Seuraava Kamera
    [SerializeField] private Transform player;      // Kohde mitä seurataan
    [SerializeField] private float camdistance;     // Kameran etäisyys
    [SerializeField] private float camspeed;        // Kameran nopeus
    private float lookAhead;
    private float currentPosX;

    private void Update()
    {
        // Seuraa pelaajaa
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        lookAhead = Mathf.Lerp(lookAhead, (camdistance * player.localScale.x), Time.deltaTime * camspeed);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        print("here");
        currentPosX = _newRoom.position.x;
    }

}
