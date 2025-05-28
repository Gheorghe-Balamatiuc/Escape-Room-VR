using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PasswordManager : MonoBehaviour
{
    public TextMeshPro[] displayTexts; 
    [HideInInspector] public List<ButtonColorChange> buttonDigits = new List<ButtonColorChange>(); 

    private List<int> enteredSequence = new List<int>();
    private int currentIndex = 0;
    private string password = "2876";

    public DoorOpener doorOpener; 


    void Start()
    {
        Debug.Log("PasswordManager initialized");

        if (displayTexts != null && displayTexts.Length > 0)
        {
            foreach (var text in displayTexts)
            {
                if (text == null)
                {
                    Debug.LogWarning("One or more displayTexts is null!");
                    break;
                }
            }
        }

        ButtonColorChange[] foundButtons = GetComponentsInChildren<ButtonColorChange>(true);
        buttonDigits.AddRange(foundButtons);
        Debug.Log($"Found {buttonDigits.Count} button digits in children.");

        ResetPassword();
    }

    public void PressDigit(int digit)
    {
        Debug.Log($"Processing digit {digit} from button");

        if (currentIndex < 4)
        {
            enteredSequence.Add(digit);
            currentIndex++;
            Debug.Log($"Current sequence: {string.Join("", enteredSequence)}");

            if (displayTexts != null && displayTexts.Length > 0 && currentIndex <= displayTexts.Length && displayTexts[currentIndex - 1] != null)
            {
                displayTexts[currentIndex - 1].text = digit.ToString();
                displayTexts[currentIndex - 1].color = Color.blue;
            }

            if (currentIndex == 4)
            {
                CheckPassword();
            }
        }
    }

    private void CheckPassword()
    {
        string entered = string.Join("", enteredSequence);
        Debug.Log($"Checking password: {entered} vs {password}");

        if (entered == password)
        {
            StartCoroutine(ShowGreen());
        }
        else
        {
            StartCoroutine(ShowRed());
        }
    }

    private IEnumerator ShowGreen()
    {
        if (displayTexts != null)
        {
            foreach (var text in displayTexts)
            {
                if (text != null) text.color = Color.green;
            }
        }

        foreach (var button in buttonDigits)
        {
            if (button.textMeshPro != null)
                button.textMeshPro.color = Color.green;
        }

        Debug.Log("Password correct!");

        if (doorOpener != null)
        {
            Debug.Log("Opening door...");
            doorOpener.OpenDoor();
        }
        else
        {
            Debug.LogWarning("DoorOpener not assigned in PasswordManager.");
        }

        yield return null;
    }


    private IEnumerator ShowRed()
    {
        if (displayTexts != null)
        {
            foreach (var text in displayTexts)
            {
                if (text != null) text.color = Color.red;
            }
        }

        foreach (var button in buttonDigits)
        {
            if (button.textMeshPro != null)
                button.textMeshPro.color = Color.red;
        }

        Debug.Log("Password incorrect!");
        yield return new WaitForSeconds(3);
        ResetPassword();
    }

    private void ResetPassword()
    {
        enteredSequence.Clear();
        currentIndex = 0;

        if (displayTexts != null)
        {
            foreach (var text in displayTexts)
            {
                if (text != null)
                {
                    text.text = "";
                    text.color = Color.white;
                }
            }
        }

        foreach (var button in buttonDigits)
        {
            if (button.textMeshPro != null)
                button.textMeshPro.color = Color.white;
        }

        Debug.Log("Password reset");
    }
}
