using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public static BuildSystem instance;

    [SerializeField] Transform childCamera;

    [SerializeField] Transform floorBuild;
    public PlaceableObject currentGO;
    public PlaceableObject tempGO;
    public bool isBuilding;
    public bool existTemp;
    public bool hasObstacle;

    [SerializeField] Material canBuild;
    [SerializeField] Material cantBuild;
    [SerializeField] Material successBuild;

    RaycastHit hit;
    [SerializeField] float rangeBuild;
    [SerializeField] LayerMask layer;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isBuilding)
        {
            if (!existTemp)
            {
                tempGO = Instantiate(currentGO, currentGO.prefab.transform.position, Quaternion.identity);
                tempGO.prefab.AddComponent<TempBuildObject>();
                tempGO.prefab.GetComponent<TempBuildObject>().green = canBuild;
                tempGO.prefab.GetComponent<TempBuildObject>().red = cantBuild;
                existTemp = true;
            }

            if(currentGO.typePlace == typePlace.normal)
            {
                NormalPlace();
            }
            if(currentGO.typePlace == typePlace.edge)
            {

            }
            
        }
    }


    private void NormalPlace()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rangeBuild, layer))
        {
            tempGO.prefab.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (Input.GetKeyDown(KeyCode.R))
            {
                tempGO.prefab.transform.Rotate(0, 90, 0, Space.World);
            }

            if (Input.GetMouseButtonDown(0) && tempGO.prefab.GetComponent<TempBuildObject>().isBuildable)
            {
                var go = Instantiate(currentGO.prefab, tempGO.prefab.transform.position, tempGO.prefab.transform.rotation);
                go.GetComponent<MeshRenderer>().material = successBuild;
                go.GetComponent<BoxCollider>().isTrigger = false;
                isBuilding = false;
                tempGO = null;
                existTemp = false;
            }

        }
    }
}
