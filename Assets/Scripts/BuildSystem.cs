using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{
    public static BuildSystem instance;

    [SerializeField] Transform childCamera;

    [SerializeField] Transform floorBuild;
    public PlaceableObject currentGO;
    public GameObject tempGO;
    public bool isBuilding;
    public bool existTemp;
    public bool hasObstacle;

    [HideInInspector]
    public InventorySlotUI itemAssignedSlot;

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
                tempGO = Instantiate(currentGO.prefab, currentGO.prefab.transform.position, Quaternion.identity);
                tempGO.GetComponent<BoxCollider>().isTrigger = true;
                tempGO.AddComponent<TempPlaceObject>();
                tempGO.GetComponent<TempPlaceObject>().green = canBuild;
                tempGO.GetComponent<TempPlaceObject>().red = cantBuild;
                existTemp = true;
            }

            if(currentGO.typePlace == typePlace.normal)
            {
                NormalPlace();
            }
            //if(currentGO.typePlace == typePlace.edge)
            //{

            //}
            
        }
    }


    private void NormalPlace()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rangeBuild, layer))
        {
            tempGO.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (Input.GetKeyDown(KeyCode.R))
            {
                tempGO.transform.Rotate(0, 90, 0, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                existTemp = false;
                isBuilding = false;
                tempGO = null;
                currentGO = null;  
            }

            if (Input.GetMouseButtonDown(0) && tempGO.GetComponent<TempPlaceObject>().isBuildable)
            {
                var go = Instantiate(currentGO.prefab, tempGO.transform.position, tempGO.transform.rotation);
                go.GetComponent<MeshRenderer>().material = successBuild;
                go.GetComponent<BoxCollider>().isTrigger = false;
                if (go.GetComponent<ItemPickUp>())
                {
                    go.GetComponent<ItemPickUp>().enabled = false;
                }
                isBuilding = false;
                Destroy(tempGO.gameObject);
                tempGO = null;
                existTemp = false;
                itemAssignedSlot.ClearSLot();
            }

        }
    }
}
