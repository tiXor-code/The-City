using TheCity.EnemyAI;
using TheCity.TowerAI;
using TheCity.UI;
using UnityEngine;

namespace TheCity.GameMaster
{
    public class Shop : MonoBehaviour
    {
        public static Shop instance;

        public TurretBlueprint standardTurret;
        public TurretBlueprint ballistaTurret;
        public TurretBlueprint crystalTurret;

        BuildManager buildManager;

        NodeUI nodeUI;
        Node node;

        private Renderer rend;
        private Color startColor;

        public Color notEnoughMoneyColor;

        private void Start()
        {
            buildManager = BuildManager.instance;
            nodeUI = NodeUI.instance;

            //rend = GetComponent<Renderer>();
            //startColor = rend.material.color;

            instance = this;
        }

        public void SelectStandardTurret()
        {
            buildManager.SelectTurretToBuild(standardTurret);
        }

        public void SelectCrystalTurret()
        {
            buildManager.SelectTurretToBuild(crystalTurret);
        }

        /// <summary>
        /// //////////////////////////////////////////////////////////
        /// </summary>

        public void BuildStandardTurret()
        {
            buildManager.SelectTurretToBuild(standardTurret);
            //node.BuildTurret(standardTurret);
            //buildManager.BuildTurret();
            nodeUI.BuildTurret(standardTurret);

        }
        
        public void BuildBallistaTurret()
        {
            buildManager.SelectTurretToBuild(ballistaTurret);
            //node.BuildTurret(standardTurret);
            //buildManager.BuildTurret();
            nodeUI.BuildTurret(ballistaTurret);

        }

        public void BuildCrystalTurret()
        {
            //buildManager.SelectTurretToBuild(crystalTurret);

            buildManager.SelectTurretToBuild(crystalTurret);
            //node.BuildTurret(standardTurret);
            //buildManager.BuildTurret();
            nodeUI.BuildTurret(crystalTurret);
        }

        public void NotEnoughMoney()
        {
            //rend.material.color = notEnoughMoneyColor;
        }
    }
}