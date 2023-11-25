using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class HallManager : MonoBehaviour
{
    [SerializeField]
    public GameObject[] walls; //0-up 1-down 2-right 3-left
    [SerializeField]
    NavMeshSurface floor;

    public void UpdateWalls(bool[] openDir)
    {
        for(int l = 0; l < openDir.Length; l++)
        {
            walls[l].SetActive(!openDir[l]);
        }
    }

    public NavMeshSurface ShareMyNavmesh()
    {
        return floor;
    }
}
