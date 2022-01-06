using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/
    public static UIManager instance;
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------SeralizeFields----------------------------------------------------------------------------------------------------------*/
    [SerializeField]
    private GameObject building1;

    [SerializeField]
    private GameObject parentForBuildings;

    [SerializeField]
    private GameObject building2;

    [SerializeField]
    private GameObject _buildingLevelPanel;

    [SerializeField]
    private GameObject _areYouSurePanel;

    [SerializeField]
    private TMP_Text _buildingLevelValue;
    /*----------------------------------------------------------------------------------------------------SeralizeFields----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------Unity Function----------------------------------------------------------------------------------------------------------*/
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Update()
    {
        BuildingLevelPanel();
        AreYouSurePanel();
    }
    /*----------------------------------------------------------------------------------------------------Unity Function----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------My Private Function----------------------------------------------------------------------------------------------------------*/
    private void BuildingLevelPanel() 
    {
        if (GameManager.instance.tempObject != null)
        {
            if (GameManager.instance.buildingLevelPanelVisible && GameManager.instance.canClickableBuilding)
            {
                GameManager.instance.canClickableBuilding = false;
                Vector3 position = Camera.main.WorldToScreenPoint(GameManager.instance.tempObject.transform.position + (Vector3.up * 3));
                _buildingLevelPanel.transform.position = position;
                _buildingLevelPanel.SetActive(true);
                _buildingLevelValue.SetText(GameManager.instance.tempObject.GetComponent<Building>().buildingLevel + "");
                GameManager.instance.areYouSurePanelVisible = false;
            }
        }
    }

    private void AreYouSurePanel() 
    {
        if (GameManager.instance.areYouSurePanelVisible && GameManager.instance.canClickableBuilding)
        {
            GameManager.instance.canClickableBuilding = false;
            Vector3 position = Camera.main.WorldToScreenPoint(GameManager.instance.tempObject.transform.position + (Vector3.up * 3));
            _areYouSurePanel.transform.position = position;
            GameManager.instance.buildingLevelPanelVisible = false;
            _areYouSurePanel.SetActive(true);
        }

    }

    /*----------------------------------------------------------------------------------------------------My Private Function----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------My Public Function----------------------------------------------------------------------------------------------------------*/

    public void Building1ButtunAction()
    {
        _buildingLevelPanel.SetActive(false);
        GameManager.instance.InstantiateBuilding(building1, parentForBuildings);
    }

    public void Building2ButtunAction()
    {
        _buildingLevelPanel.SetActive(false);
        GameManager.instance.InstantiateBuilding(building2, parentForBuildings);
    }
    

    public void LevelingUpButtonAction() 
    {
        if (GameManager.instance.tempObject != null)
        {
            GameManager.instance.tempObject.GetComponent<Building>().buildingLevel += 1;
            int buildingLevel = GameManager.instance.tempObject.GetComponent<Building>().buildingLevel;
            _buildingLevelValue.SetText(buildingLevel + "");
            GameManager.instance.Save();
        }
    }

    public void MoveTheBuildingButtonAction() 
    {
        
        _buildingLevelPanel.SetActive(false);
        GameManager.instance.portableBuilding = GameManager.instance.tempObject;
    }

    public void YesButton() 
    {
        _areYouSurePanel.SetActive(false);
    }

    public void NoButton()
    {

        _areYouSurePanel.SetActive(false);
        GameManager.instance.portableBuilding = GameManager.instance.tempObject;
    }
    /*----------------------------------------------------------------------------------------------------My Public Function----------------------------------------------------------------------------------------------------------*/
}
