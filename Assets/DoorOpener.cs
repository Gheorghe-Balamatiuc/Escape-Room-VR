using UnityEngine;

public class DoorOpener : MonoBehaviour
{
    public Transform doorTransform; 
    public Vector3 openEulerAngles = new Vector3(0, 90, 0); 
    public Vector3 openLocalPosition = new Vector3(-0.4f, 0f, -2.45f); 
    public float openSpeed = 2f;

    private bool isOpening = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    void Start()
    {
        if (doorTransform == null)
        {
            if (transform.childCount > 0)
                doorTransform = transform.GetChild(0);
            else
                doorTransform = transform;
        }

        initialRotation = doorTransform.localRotation;
        targetRotation = initialRotation * Quaternion.Euler(openEulerAngles);

        initialPosition = doorTransform.localPosition;
        targetPosition = new Vector3(openLocalPosition.x, initialPosition.y, openLocalPosition.z);
    }

    void Update()
    {
        if (isOpening)
        {
            doorTransform.localRotation = Quaternion.Slerp(doorTransform.localRotation, targetRotation, Time.deltaTime * openSpeed);
            doorTransform.localPosition = Vector3.Lerp(doorTransform.localPosition, targetPosition, Time.deltaTime * openSpeed);

            if (
                Quaternion.Angle(doorTransform.localRotation, targetRotation) < 0.1f &&
                Vector3.Distance(doorTransform.localPosition, targetPosition) < 0.01f
            )
            {
                doorTransform.localRotation = targetRotation;
                doorTransform.localPosition = targetPosition;
                isOpening = false;
                Debug.Log("Door fully opened and moved.");
            }
        }
    }

    public void OpenDoor()
    {
        Debug.Log("True opening door");
        isOpening = true;
    }
}
