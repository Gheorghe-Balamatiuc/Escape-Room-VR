using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

[RequireComponent(typeof(XRBaseInteractable))]
public class ButtonColorChange : MonoBehaviour
{
    public TextMeshPro textMeshPro;
    public int digit;

    private PasswordManager passwordManager;
    private Color originalColor = Color.white;
    private XRBaseInteractable interactable;

    void Awake()
    {
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(OnHoverEntered);
        interactable.hoverExited.AddListener(OnHoverExited);
    }

    void Start()
    {
        passwordManager = FindObjectOfType<PasswordManager>();

        if (textMeshPro == null)
            textMeshPro = GetComponentInChildren<TextMeshPro>();

        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color;
            Debug.Log($"Button {gameObject.name} initialized with digit {digit}");
        }
        else
        {
            Debug.LogWarning($"TextMeshPro not found on {gameObject.name}");
        }
    }

    private void OnDestroy()
    {
        interactable.hoverEntered.RemoveListener(OnHoverEntered);
        interactable.hoverExited.RemoveListener(OnHoverExited);
    }

    private void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log($"Hover entered on button {gameObject.name} with digit {digit}");

        if (textMeshPro != null)
            textMeshPro.color = Color.blue;

        if (passwordManager != null)
            passwordManager.PressDigit(digit);
    }

    private void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log($"Hover exited on button {gameObject.name}");

        if (textMeshPro != null)
            textMeshPro.color = originalColor;
    }
}
