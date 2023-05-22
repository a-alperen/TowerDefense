using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Upgrades : MonoBehaviour
{
    public int upgradeId;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI damageCostText;
    public TextMeshProUGUI healthCostText;
    public TextMeshProUGUI speedCostText;
    public TextMeshProUGUI rangeCostText;

    public void BuyDamageUpgrade() => MarketManager.Instance.BuyUpgrade("Damage", upgradeId);
    public void BuyHealthUpgrade() => MarketManager.Instance.BuyUpgrade("Health", upgradeId);
    public void BuySpeedUpgrade() => MarketManager.Instance.BuyUpgrade("Speed", upgradeId);
    public void BuyRangeUpgrade() => MarketManager.Instance.BuyUpgrade("Range", upgradeId);
}
