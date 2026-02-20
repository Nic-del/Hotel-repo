using UnityEngine;

public class ElevatorDoor : MonoBehaviour
{
    public Transform leftDoor;
    public Transform rightDoor;

    public Vector3 leftOpenOffset = new Vector3(-1f, 0, 0);
    public Vector3 rightOpenOffset = new Vector3(1f, 0, 0);

    public float speed = 2f;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;

    private bool open = false;

    void Start()
    {
        leftClosedPos = leftDoor.localPosition;
        rightClosedPos = rightDoor.localPosition;
    }

    void Update()
    {
        Vector3 leftTarget =
            open ? leftClosedPos + leftOpenOffset : leftClosedPos;

        Vector3 rightTarget =
            open ? rightClosedPos + rightOpenOffset : rightClosedPos;

        leftDoor.localPosition =
            Vector3.Lerp(leftDoor.localPosition, leftTarget, Time.deltaTime * speed);

        rightDoor.localPosition =
            Vector3.Lerp(rightDoor.localPosition, rightTarget, Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            open = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            open = false;
    }
}
