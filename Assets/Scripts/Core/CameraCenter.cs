using UnityEngine;

public class CameraCenter : MonoBehaviour
{

    public float Followspeed = 2f;  // Seuraamis nopeus
    public float yoffset = 1f;      // Offset y suuntaan
    public float xOffset = 1f;      // Offset x suuntaan
    public Transform target;        // Kohde mitä seurataan
    private float currentPosX;

    private void Update()
    {
        Vector3 newPos = new Vector3(target.position.x + xOffset, target.position.y + yoffset, -10f);
        transform.position = Vector3.Lerp(transform.position, newPos, Followspeed * Time.deltaTime);
    }

    public void MoveToNewRoom(Transform _newRoom)
    {
        print("here");
        currentPosX = _newRoom.position.x;
    }


}
