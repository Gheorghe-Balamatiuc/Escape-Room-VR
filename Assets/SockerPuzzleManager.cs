using UnityEngine;

public class SocketPuzzleManager : MonoBehaviour
{
    public Transform socket1; // Poziția unde verifici mingea
    public Transform socket2; // Poziția unde verifici bricheta
    public Transform socket3; // Poziția unde verifici mărul

    public float detectionRadius = 0.2f;
    public DoorOpener doorOpener;

    private bool puzzleSolved = false;

    void Update()
    {
        if (puzzleSolved) return;

        bool s1 = IsObjectNearby(socket1.position, "TennisBall");
        bool s2 = IsObjectNearby(socket2.position, "CandleLighter");
        bool s3 = IsObjectNearby(socket3.position, "Apple");

        Debug.Log($"[CheckSocket] S1: {s1}, S2: {s2}, S3: {s3}");

        if (s1 && s2 && s3)
        {
            Debug.Log("✅ Puzzle rezolvat. Se deschide usa!");
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
                Debug.Log($"[IsObjectNearby] Gasit {tag} langa {position}");
                return true;
            }
        }

        return false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (socket1 != null) Gizmos.DrawWireSphere(socket1.position, detectionRadius);
        if (socket2 != null) Gizmos.DrawWireSphere(socket2.position, detectionRadius);
        if (socket3 != null) Gizmos.DrawWireSphere(socket3.position, detectionRadius);
    }
}
