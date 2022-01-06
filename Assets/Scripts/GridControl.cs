using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControl : MonoBehaviour
{
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/
    public static bool gridControl;
    /*----------------------------------------------------------------------------------------------------Public Variables----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------Unity Functions----------------------------------------------------------------------------------------------------------*/
    private void Start()
    {
        gridControl = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
            gridControl = false;
        else
            gridControl = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 6)
            gridControl = true;
    }
    /*----------------------------------------------------------------------------------------------------Unity Functions----------------------------------------------------------------------------------------------------------*/
}
