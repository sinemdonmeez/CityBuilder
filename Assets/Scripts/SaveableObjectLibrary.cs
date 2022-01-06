using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveableObjectLibrary : MonoBehaviour
{
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/
    public static Dictionary<int, GameObject> SaveableObjects;
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------SerializeFields----------------------------------------------------------------------------------------------------------*/
    [SerializeField] private GameObject[] RegisteredObjects;
    /*----------------------------------------------------------------------------------------------------SerializeFields----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------Unity Functions----------------------------------------------------------------------------------------------------------*/
    private void Awake()
    {
        SaveableObjects = new Dictionary<int, GameObject>();

        for (int i = 0; i < RegisteredObjects.Length; i++) 
        {
            int IDToRegister = RegisteredObjects[i].GetComponent<Building>().ID;
            SaveableObjects.Add(IDToRegister, RegisteredObjects[i]);
        }
    }
    /*----------------------------------------------------------------------------------------------------Unity Functions----------------------------------------------------------------------------------------------------------*/
}