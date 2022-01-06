using UnityEngine;

public class Grid : MonoBehaviour
{
    /*----------------------------------------------------------------------------------------------------Private Variables----------------------------------------------------------------------------------------------------------*/
    private float size = 2f;
    /*----------------------------------------------------------------------------------------------------Private Variables----------------------------------------------------------------------------------------------------------*/

    /*----------------------------------------------------------------------------------------------------My Public Functions----------------------------------------------------------------------------------------------------------*/
    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {
        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / size);
        int yCount = Mathf.RoundToInt(position.y / size);
        int zCount = Mathf.RoundToInt(position.z / size);

        Vector3 result = new Vector3(
            (float)xCount * size,
            (float)yCount * size,
            (float)zCount * size);

        result += transform.position;
        return result;
    }
    /*----------------------------------------------------------------------------------------------------My Public Functions----------------------------------------------------------------------------------------------------------*/
}