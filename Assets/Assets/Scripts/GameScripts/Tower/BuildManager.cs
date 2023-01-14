using System;
using TheCity.EnemyAI;
using TheCity.GameMaster;
using TheCity.UI;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace TheCity.TowerAI
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager instance;

        void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("More than one BuildManager in scene!");
                return;
            }
            instance = this;
        }

        public GameObject buildEffect;
        public GameObject sellEffect;      

        private TurretBlueprint turretToBuild;
        private Node selectedNode;

        public NodeUI nodeUI;

        public bool CanBuild { get { return turretToBuild != null; } }
        public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }

        public void SelectNode(Node node)
        {
            if(selectedNode != null)//== node)
            {
                DeselectNode();
                return;
            }
            if (node.transform.position.y >= 1)
            {
                DeselectNode();
                return;
            }

            selectedNode = node;
            turretToBuild = null;

            nodeUI.SetTarget(node);
        }

        public void DeselectNode()
        {
            selectedNode = null;
            nodeUI.Hide();
        }

        public void SelectTurretToBuild(TurretBlueprint turret)
        {
            turretToBuild = turret;
            selectedNode = null;

            DeselectNode();
        }

        public void BuildTurret()
        {
            selectedNode.BuildTurret(GetTurretToBuild());
        }

        public TurretBlueprint GetTurretToBuild()
        {
            return turretToBuild;
        }        
    }
}