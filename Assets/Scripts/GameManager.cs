using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/
    public static GameManager instance;
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------HideInInspectors----------------------------------------------------------------------------------------------------------*/
    [HideInInspector] public GameObject tempObject;

    [HideInInspector] public GameObject portableBuilding;

    [HideInInspector] public bool buildingLevelPanelVisible;

    [HideInInspector] public bool areYouSurePanelVisible;

    [HideInInspector] public bool canClickableBuilding;
    /*----------------------------------------------------------------------------------------------------HideInInspectors----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------Serializefields----------------------------------------------------------------------------------------------------------*/
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private LayerMask _buildingLayer;

    [SerializeField] private GameObject _buildingParent;
    /*----------------------------------------------------------------------------------------------------Serializefields----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------Private Variables----------------------------------------------------------------------------------------------------------*/
    private Grid grid;
    /*----------------------------------------------------------------------------------------------------Private Variables----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------Unity Functions----------------------------------------------------------------------------------------------------------*/
    private void Start()
    {
        Load();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        grid = FindObjectOfType<Grid>();
    }

    private void Update()
    {
        dragAndDropTheBuilding(); 
    }
    /*----------------------------------------------------------------------------------------------------Unity Functions----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------My Private Functions----------------------------------------------------------------------------------------------------------*/
    private void dragAndDropTheBuilding() 
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //Drag the Building
        if (Input.GetMouseButtonDown(0))
        {
            canClickableBuilding = true;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _buildingLayer))
            {
                tempObject = hit.collider.gameObject;
                tempObject.GetComponent<Rigidbody>().useGravity = false;
                buildingLevelPanelVisible = true;
               
                Save();
            }
            else
                buildingLevelPanelVisible = false;
        }


        //Drop the Building
        if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == 3 && portableBuilding != null)
        {
            var finalPosition = grid.GetNearestPointOnGrid(hit.point) + new Vector3(0, 2, 0);
            portableBuilding.transform.position = finalPosition;
        
            if (Input.GetMouseButton(0) && GridControl.gridControl)
            {
                tempObject = portableBuilding;
                tempObject.GetComponent<Rigidbody>().useGravity = true;
                areYouSurePanelVisible = true;
                portableBuilding = null;
                
                Save();
            }

        }
        else
            areYouSurePanelVisible = false;
    
    }
    /*----------------------------------------------------------------------------------------------------My Private Functions----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------My Public Functions----------------------------------------------------------------------------------------------------------*/
    public void InstantiateBuilding(GameObject building, GameObject parent)
    {
        portableBuilding = Instantiate(building, new Vector3(25,1,-12), Quaternion.identity);
        portableBuilding.transform.SetParent(parent.transform);
    }

    public void Save() 
    {
        Building[] ObjectsInScene = FindObjectsOfType<Building>();

        SaveableObjectInScene ObjectData = new SaveableObjectInScene
        {
            SaveableObjects = new SaveableObject[ObjectsInScene.Length]
        };

        for (int i = 0; i < ObjectData.SaveableObjects.Length; i++)
        {
            ObjectData.SaveableObjects[i] = new SaveableObject
            {
                WorldPosition = ObjectsInScene[i].transform.position,
                WorldRotation = ObjectsInScene[i].transform.rotation,
                ID = ObjectsInScene[i].ID,
                BuildingLevel = ObjectsInScene[i].buildingLevel
            }; 
        }
        SaveSystem.Save(ObjectData, "Objects");
    }

    public void Load()
    {
        SaveSystem.Load(out SaveableObjectInScene LoadedObjectData, "Objects");

        Building[] ObjectsInScene = FindObjectsOfType<Building>();

        for (int i = 0; i < ObjectsInScene.Length; i++)
        {
            Destroy(ObjectsInScene[i].gameObject);
        }

        for (int i = 0; i < LoadedObjectData.SaveableObjects.Length; i++) 
        {
            tempObject= Instantiate
            (
                SaveableObjectLibrary.SaveableObjects[LoadedObjectData.SaveableObjects[i].ID],
                LoadedObjectData.SaveableObjects[i].WorldPosition, 
                LoadedObjectData.SaveableObjects[i].WorldRotation
            );
            tempObject.transform.SetParent(_buildingParent.transform);
            tempObject.GetComponent<Building>().buildingLevel = LoadedObjectData.SaveableObjects[i].BuildingLevel;
        }
    }

    /*----------------------------------------------------------------------------------------------------My Public Functions----------------------------------------------------------------------------------------------------------*/
}
