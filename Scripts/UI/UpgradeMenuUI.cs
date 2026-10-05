using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenuUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject UpgradeUI;

    [Header("Luck")]
    [SerializeField] private Slider luckSlider;
    [SerializeField] private TMP_Text luckCostText;

    [Header("Movement Speed")]
    [SerializeField] private Slider movementSpeedSlider;
    [SerializeField] private TMP_Text movementSpeedCostText;

    [Header("Crate Cooldown Speed")]
    [SerializeField] private Slider crateCooldownSlider;
    [SerializeField] private TMP_Text crateCooldownCostText;

    [Header("Crate Reward")]
    [SerializeField] private Slider crateRewardSlider;
    [SerializeField] private TMP_Text crateRewardCostText;


    private void OnEnable()
    {
        refreshUI();

        if (EconomyManager.Instance != null)
            EconomyManager.Instance.onCoinsChanged += refreshUI;
    }

    private void OnDisable()
    {
        if (EconomyManager.Instance != null)
            EconomyManager.Instance.onCoinsChanged -= refreshUI;
    }


    public void increaseLuck()
    {
        AudioManager.Instance.PlaySFX(4);
        PlayerStats.Instance.increasePlayerLuck();
        refreshUI();
    }

    public void increaseMovementSpeed()
    {
        AudioManager.Instance.PlaySFX(5);
        PlayerStats.Instance.increaseMovementSpeed();
        refreshUI();
    }

    public void decreaseCrateCooldown()
    {
        AudioManager.Instance.PlaySFX(4);
        PlayerStats.Instance.decreaseCrateCooldown();
        refreshUI();
    }

    public void increaseCrateReward()
    {
        AudioManager.Instance.PlaySFX(5);
        PlayerStats.Instance.increaseCrateReward();
        refreshUI();
    }


    private void refreshUI()
    {
        if (PlayerStats.Instance == null)
            return;

        PlayerStats stats = PlayerStats.Instance;

        // Sliders
        luckSlider.maxValue = stats.MaxLuckLevel;
        luckSlider.value = stats.LuckLevel;

        movementSpeedSlider.maxValue = stats.MaxMovementSpeedLevel;
        movementSpeedSlider.value = stats.MovementSpeedLevel;

        crateCooldownSlider.maxValue = stats.MaxCrateCooldownLevel;
        crateCooldownSlider.value = stats.CrateCooldownLevel;

        crateRewardSlider.maxValue = stats.MaxCrateRewardLevel;
        crateRewardSlider.value = stats.CrateRewardLevel;


        // Costs
        if (stats.LuckLevel >= stats.MaxLuckLevel)
        { luckCostText.text = "MAX"; AudioManager.Instance.PlaySFX(9); }
        else
            luckCostText.text = stats.LuckUpgradeCost + " COINS";


        if (stats.MovementSpeedLevel >= stats.MaxMovementSpeedLevel)
        { AudioManager.Instance.PlaySFX(9); movementSpeedCostText.text = "MAX"; }
        else
            movementSpeedCostText.text =
                stats.MovementSpeedUpgradeCost + " COINS";


        if (stats.CrateCooldownLevel >= stats.MaxCrateCooldownLevel)
        { AudioManager.Instance.PlaySFX(9); crateCooldownCostText.text = "MAX"; }
        else
            crateCooldownCostText.text =
                stats.CrateCooldownUpgradeCost + " COINS";


        if (stats.CrateRewardLevel >= stats.MaxCrateRewardLevel)
        { AudioManager.Instance.PlaySFX(9); crateRewardCostText.text = "MAX"; }
        else
            crateRewardCostText.text =
                stats.CrateRewardUpgradeCost + " COINS";
    }


    public void close()
    {
        AudioManager.Instance.PlaySFX(4);
        UpgradeUI.SetActive(false);
    }
}