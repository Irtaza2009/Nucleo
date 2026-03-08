using UnityEngine;
using TMPro;

public class UpgradeCard : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public UpgradeData upgrade;
    public UpgradeManager upgradeManager;

    public void Setup(UpgradeData data, UpgradeManager manager)
    {
        upgrade = data;
        upgradeManager = manager;

        titleText.text = data.upgradeName;
        descriptionText.text = data.description;
    }

    public void SelectUpgrade()
    {
        if (upgradeManager != null)
        {
            upgradeManager.ApplyUpgrade(upgrade);
        }
    }
}
