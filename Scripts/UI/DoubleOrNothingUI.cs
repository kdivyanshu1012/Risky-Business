using UnityEngine;
using TMPro;

public class DoubleOrNothingUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;

    [Header("UI Text")]
    [SerializeField] private TMP_Text potText;
    [SerializeField] private TMP_Text resultText;

    private DoubleOrNothingMachine currentMachine;

    public void Open(DoubleOrNothingMachine machine, int pot, string result)
    {
        // VERY IMPORTANT
        currentMachine = machine;

        potText.text = "Current Pot: " + pot;
        resultText.text = result;

        uiPanel.SetActive(true);
    }

    public void Close()
    {
        currentMachine = null;

        uiPanel.SetActive(false);
    }

    public void GambleAgain()
    {
        if (currentMachine == null)
        {
            Debug.LogError("No machine connected to UI!");
            return;
        }

        DoubleOrNothingMachine machine = currentMachine;

        Close();

        machine.GambleAgain();
    }

    public void CashOut()
    {
        if (currentMachine == null)
        {
            Debug.LogError("No machine connected to UI!");
            return;
        }

        DoubleOrNothingMachine machine = currentMachine;

        Close();

        machine.CashOut();
    }
}