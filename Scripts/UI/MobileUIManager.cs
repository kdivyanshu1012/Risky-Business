using UnityEngine;

public class MobileUIManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileControls;

    private void Awake()
    {
        bool mobile =
            Application.isMobilePlatform ||
            SystemInfo.deviceType == DeviceType.Handheld;

        mobileControls.SetActive(mobile);
    }
}
