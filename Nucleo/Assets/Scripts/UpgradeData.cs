using UnityEngine;

[CreateAssetMenu(fileName = "NewUpgrade", menuName = "Nucleo/Upgrade")]
public class UpgradeData : ScriptableObject
{
    public string upgradeName;
    public string description;

    public UpgradeType upgradeType;

    public float value;
}
