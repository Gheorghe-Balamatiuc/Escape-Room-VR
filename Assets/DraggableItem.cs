using UnityEngine;

public class PlaceOnSocket : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Socket"))
        {
            // Caută copilul numit "Attach"
            Transform attach = other.transform.Find("Attach");
            if (attach != null)
            {
                Debug.Log($"[{gameObject.name}] Plasat în {attach.name}");

                // Muta obiectul sub Attach
                transform.SetParent(attach);

                // Ajustează poziția și rotația relativ la Attach
                transform.localPosition = Vector3.zero;
                transform.localRotation = Quaternion.identity;

                // Dezactivează Rigidbody și Collider dacă nu mai vrei să fie fizic activ
                Rigidbody rb = GetComponent<Rigidbody>();
                if (rb) rb.isKinematic = true;

                Collider col = GetComponent<Collider>();
                if (col) col.enabled = false;
            }
        }
    }
}
