using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonHoverDebug : MonoBehaviour
{
    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        Debug.Log("Hover entered on button: " + gameObject.name);
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        Debug.Log("Hover exited on button: " + gameObject.name);
    }
}