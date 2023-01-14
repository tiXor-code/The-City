using TheCity.EnemyAI;
using TheCity.TowerAI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TheCity.UI
{
    public class NodeUI : MonoBehaviour
    {
        public static NodeUI instance;

        public GameObject ui;
        public GameObject shopUI;

        public static Image shopImage;
        public Color notEnoughMoney;

        public TextMeshProUGUI upgradeCost;
        public Button upgradeButton;

        public TextMeshProUGUI sellAmount;
        public Button sellButton;

        //public TurretBlueprint standardTurret;
        //public TurretBlueprint crystalTurret;

        private Node target;

        private void Awake()
        {
            instance = this;
        }

        public void SetTarget(Node _target)
        {
            this.target = _target;

            transform.position = target.GetBuildPosition();

            if (target != null && target.turret != null)
            {
                shopUI.SetActive(false);

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
            
            if(target.turret == null)
            {
                ui.SetActive(false);

                //buildCost[0].text = "$" + target.turretBlueprint.cost;
                

                shopUI.SetActive(true);
            }    
        }

        public void Hide()
        {
            ui.SetActive(false);
            shopUI.SetActive(false);
        }

        public void Upgrade()
        {
            target.UpdateTurret();
            BuildManager.instance.DeselectNode();
        }

        public void Sell()
        {
            target.SellTurret();
            BuildManager.instance.DeselectNode();
        }

        public void BuildTurret(TurretBlueprint turret)
        {
            target.BuildTurret();
            BuildManager.instance.DeselectNode();
        }
    }
}