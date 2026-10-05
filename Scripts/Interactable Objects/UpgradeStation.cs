using UnityEngine;
public class UpgradeStation : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject upgradeUI;
    public void interact()
    {
        upgradeUI.SetActive(true);
        Debug.Log("Enabled");
    }
}
