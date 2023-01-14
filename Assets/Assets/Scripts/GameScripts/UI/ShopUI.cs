using TheCity.TowerAI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheCity.UI
{
    public class ShopUI : MonoBehaviour
    {
        public GameObject ui;

        public TextMeshProUGUI upgradeCost;
        public Button upgradeButton;

        public TextMeshProUGUI sellAmount;
        public Button sellButton;

        private Node target;

        public void SetTarget(Node _target)
        {
            this.target = _target;

            transform.position = target.GetBuildPosition();

            if (!target.isUpgraded)
            {
                upgradeCost.text = "$" + target.turretBlueprint.upgradeCost;
                upgradeButton.interactable = true;
            }
            else
            {
                upgradeCost.text = "Turret Maximised";
                upgradeButton.interactable = false;
            }

            sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

            ui.SetActive(true);
        }

        public void BuildTurret()
        {

        }
    }
}