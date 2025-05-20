using UnityEngine;

public class Socket : MonoBehaviour
{
    public Transform attachPoint1; 
    public Transform attachPoint2; 
    public Transform attachPoint3; 

    public float detectionRadius = 0.05f;
    public DoorOpener doorOpener;

    private bool puzzleSolved = false;

    void Update()
    {
        if (puzzleSolved) return;

        bool s1 = IsObjectNearby(attachPoint1.position, "TennisBall");
        bool s2 = IsObjectNearby(attachPoint2.position, "CandleLighter");
        bool s3 = IsObjectNearby(attachPoint3.position, "Apple");

        if (s1 && s2 && s3)
        {
            doorOpener.OpenDoor();
            puzzleSolved = true;
        }
    }

    bool IsObjectNearby(Vector3 position, string tag)
    {
        Collider[] colliders = Physics.OverlapSphere(position, detectionRadius);

        foreach (Collider col in colliders)
        {
            if (col.CompareTag(tag))
            {
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (attachPoint1 != null) Gizmos.DrawWireSphere(attachPoint1.position, detectionRadius);
        if (attachPoint2 != null) Gizmos.DrawWireSphere(attachPoint2.position, detectionRadius);
        if (attachPoint3 != null) Gizmos.DrawWireSphere(attachPoint3.position, detectionRadius);
    }
}
