using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI rarityText;
    public Image cardImage;

    public UpgradeData upgrade;
    public UpgradeManager upgradeManager;

    public void Setup(UpgradeData data, UpgradeManager manager)
    {
        upgrade = data;
        upgradeManager = manager;

        titleText.text = data.upgradeName;
        descriptionText.text = data.description;
        rarityText.text = "(" + data.rarity.ToString() + ")";

        // Set card sprite based on rarity
        if (cardImage != null && manager != null)
        {
            Sprite raritySprite = manager.GetSpriteForRarity(data.rarity);
            if (raritySprite != null)
                cardImage.sprite = raritySprite;
        }
    }

    public void SelectUpgrade()
    {
        if (upgradeManager != null)
        {
            upgradeManager.ApplyUpgrade(upgrade);
        }
    }
}
