using TheCity.EnemyAI;
using TheCity.GameMaster;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TheCity.TowerAI
{
    public class Node : MonoBehaviour
    {
        public Color hoverColor;
        public Color notEnoughMoneyColor;
        public Vector3 positionOffset = new(0f, 0.5f, 0f);
        


        [HideInInspector]
        public GameObject turret;
        [HideInInspector]
        public TurretBlueprint turretBlueprint;
        [HideInInspector]
        public bool isUpgraded = false;

        private Renderer rend;
        private Color startColor;

        BuildManager buildManager;
        Shop shop;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            startColor = rend.material.color;

            buildManager = BuildManager.instance;
            shop = Shop.instance;

        }

        private void Update()
        {

            //OnTouch();

            //OnClick();
               
        }

        private void OnClick()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            buildManager.SelectNode(this);
            if (this == null)
                return;

            if (!buildManager.CanBuild)
                return;

            BuildTurret(buildManager.GetTurretToBuild());
        }

        private void OnTouch()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;
                //else
                //{

                //if (turret != null)
                //{
                buildManager.SelectNode(this);
                if (this == null)
                    return;
                //    return;
                //}
                //else buildManager.SelectNodeToBuildOn(this);

                if (!buildManager.CanBuild)
                    return;

                BuildTurret(buildManager.GetTurretToBuild());
                //}
            }
        }

        public Vector3 GetBuildPosition()
        {
            return transform.position + positionOffset;
        }

        void OnMouseDown()
        {
            //if (EventSystem.current.IsPointerOverGameObject())
            //    return;

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
                    return;
                else
                {

                    //if (turret != null)
                    //{
                    buildManager.SelectNode(this);
                    if (this == null)
                        return;
                    //    return;
                    //}
                    //else buildManager.SelectNodeToBuildOn(this);

                    if (!buildManager.CanBuild)
                        return;

                    BuildTurret(buildManager.GetTurretToBuild());
                }
        }

        public void BuildTurret()
        {
            BuildTurret(buildManager.GetTurretToBuild());
        }

        public void BuildTurret(TurretBlueprint blueprint)
        {
            if (PlayerStats.Money < blueprint.cost)
            {
                Debug.Log("Not enough money to build that!");
                //shop.NotEnoughMoney();
                return;
            }

            PlayerStats.Money -= blueprint.cost;

            GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            turretBlueprint = blueprint;

            Debug.Log("Turret built");
        }

        public void UpdateTurret()
        {
            if (PlayerStats.Money < turretBlueprint.upgradeCost)
            {
                Debug.Log("Not enough money to upgrade that!");
                
                return;
            }

            PlayerStats.Money -= turretBlueprint.upgradeCost;

            // Get rid of the old turret:
            Destroy(turret);

            // Build a new one:
            GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity);
            turret = _turret;

            GameObject effect = Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            isUpgraded = true;

            Debug.Log("Turret upgraded");
        }

        public void SellTurret()
        {
            PlayerStats.Money += turretBlueprint.GetSellAmount();

            GameObject effect = Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(effect, 5f);

            Destroy(turret);
            turretBlueprint = null;
        }

        //void OnMouseEnter()
        //{
        //    if (EventSystem.current.IsPointerOverGameObject())
        //        return;

        //    //if (!buildManager.CanBuild)
        //    //    return;

        //    //if (buildManager.HasMoney)
        //    //{
        //        rend.material.color = hoverColor;
        //    //}
        //    //else
        //    //{
        //    //    rend.material.color = notEnoughMoneyColor;                    
        //    //}
        //}

        void OnMouseExit()
        {
            rend.material.color = startColor;
        }
    }
}