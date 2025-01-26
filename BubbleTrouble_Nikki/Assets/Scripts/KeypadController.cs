using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections.Generic;
using System;
using System.Collections;

public class KeypadController : MonoBehaviour
{
    public List<int> correctPasscode = new List<int>();
    public List<int> inputPasscodeList = new List<int>();
    public Canvas canvas;
    [SerializeField] private TMP_InputField codeDisplay;
    [SerializeField] private float resetTime = 2f;
    [SerializeField] private string successText;

    [Space(5f)]
    [Header("Keypad Entry Events")]
    public UnityEvent onCorrectPasscode;
    public UnityEvent onIncorrectPasscode;
    public bool allowMultipleActivations = false;
    private bool hasUsedCorrectCode = false;

    public bool HasUsedCorrectCode { get { return hasUsedCorrectCode; } }

    public void UserNumberEntry(int selectionedNum)
    {
        if (inputPasscodeList.Count >= 4)
            return;

        inputPasscodeList.Add(selectionedNum);
        UpdateDisplay();

        if (inputPasscodeList.Count >= 4)
            StartCoroutine(CheckPasscodeAfterDelay(2f));
    }

    private void CheckPasscode()
    {
        for (int i = 0; i < correctPasscode.Count; i++)
        {
            if (inputPasscodeList[i] != correctPasscode[i])
            {
                IncorrectPasscode();
                return;
            }
        }
        correctPasscodeGiven();
    }

    private void correctPasscodeGiven()
    {
        if (allowMultipleActivations)
        {
            onCorrectPasscode.Invoke();
            StartCoroutine(ResetKeyCode());
            StartCoroutine(HideCanvasAfterDelay(2f));
        }
        else if (!allowMultipleActivations && !hasUsedCorrectCode)
        {
            onIncorrectPasscode.Invoke();
            hasUsedCorrectCode = true;
            codeDisplay.text = successText;
            StartCoroutine(HideCanvasAfterDelay(2f));
        }
    }

    private void IncorrectPasscode()
    {
        onIncorrectPasscode.Invoke();
        StartCoroutine(ResetKeyCode());
    }

    private void UpdateDisplay()
    {
        codeDisplay.text = null;
        for (int i = 0; i < inputPasscodeList.Count; i++)
        {
            codeDisplay.text += inputPasscodeList[i];
        }
        codeDisplay.caretPosition = codeDisplay.text.Length;
    }

    public void DeleteEntry()
    {
        if (inputPasscodeList.Count <= 0)
            return;

        var listposition = inputPasscodeList.Count - 1;
        inputPasscodeList.RemoveAt(listposition);
        UpdateDisplay();
    }

    private IEnumerator CheckPasscodeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        CheckPasscode();
    }

    IEnumerator ResetKeyCode()
    {
        yield return new WaitForSeconds(resetTime);
        inputPasscodeList.Clear();
        UpdateDisplay();  // Added this to clear the display after reset
    }

    IEnumerator HideCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canvas.gameObject.SetActive(false);
    }
}
