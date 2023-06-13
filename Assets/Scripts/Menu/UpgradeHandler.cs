using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandler : MonoBehaviour
{
    public List<Upgrades> Upgrades;
    public Upgrades UpgradePrefab;
    public ScrollRect UpgradesScroll;
    public Transform UpgradesPanel;

    public string[] UpgradesNames;
    public float[,] UpgradesCost;
    public float[,] UpgradesCostMultiplier;
}
