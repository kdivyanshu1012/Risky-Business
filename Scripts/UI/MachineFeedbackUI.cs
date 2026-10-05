using UnityEngine;
using System.Collections;
using TMPro;

public class MachineFeedbackUI : MonoBehaviour
{
    [SerializeField] private GameObject feedbackCanvas;
    [SerializeField] private TMP_Text feedbackText;
    [SerializeField] private float displayTime = 1.5f;

    private Coroutine hideRoutine;

    
    public void ShowNotEnoughMoney(int amountNeeded)
    {
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
        }

        feedbackText.text = "NEED " + amountNeeded + " MORE COINS";

        feedbackCanvas.SetActive(true);

        hideRoutine = StartCoroutine(HideRoutine());
    }

    private IEnumerator HideRoutine()
    {
        yield return new WaitForSeconds(displayTime);

        feedbackCanvas.SetActive(false);

        hideRoutine = null;
    }
    public void ShowWin(int payout)
    {
        ShowFeedback("YOU WIN! " + payout + " COINS");
    }


    public void ShowLose()
    {
        ShowFeedback("YOU LOSE!");
    }


    private void ShowFeedback(string message)
    {
        if (hideRoutine != null)
        {
            StopCoroutine(hideRoutine);
        }

        feedbackText.text = message;

        feedbackCanvas.SetActive(true);

        hideRoutine = StartCoroutine(HideRoutine());
    }
}
