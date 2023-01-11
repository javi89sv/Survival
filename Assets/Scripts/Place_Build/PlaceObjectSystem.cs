using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectSystem : MonoBehaviour
{
    public static PlaceObjectSystem instance;

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
    [SerializeField] ParticleSystem particleBuildSuccess;

    RaycastHit hit;
    [SerializeField] float rangeBuild;
    [SerializeField] LayerMask layer;

    Vector3 grid = new Vector3(1.25f, 1.5f, 1.25f);

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
                tempGO.name = currentGO.name;
                if(tempGO.GetComponent<BoxCollider>())
                    tempGO.GetComponent<BoxCollider>().isTrigger = true;
                if(tempGO.GetComponent<MeshCollider>())
                    tempGO.GetComponent<MeshCollider>().isTrigger = true;
                tempGO.AddComponent<TempPlaceObject>();
                tempGO.GetComponent<TempPlaceObject>().green = canBuild;
                tempGO.GetComponent<TempPlaceObject>().red = cantBuild;
                existTemp = true;
            }

            if (currentGO.typePlace == typePlace.normal)
            {
                NormalPlace();
            }
            if (currentGO.typePlace == typePlace.edge)
            {
                EdgePlace();
            }

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
                go.name = currentGO.name;
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

    private void EdgePlace()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rangeBuild))
        {
            tempGO.transform.position = new Vector3(Mathf.Round(hit.point.x / grid.x) * grid.x, Mathf.Round(hit.point.y / grid.y) * grid.y, Mathf.Round(hit.point.z / grid.z) * grid.z);

            if (Input.GetKeyDown(KeyCode.R))
            {
                tempGO.transform.Rotate(0, 90, 0, Space.World);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(tempGO.gameObject);
                existTemp = false;
                isBuilding = false;
                tempGO = null;
                currentGO = null;
                
            }

            if (Input.GetMouseButtonDown(0) && tempGO.GetComponent<TempPlaceObject>().isBuildable)
            {
               
                var go = Instantiate(currentGO.prefab, tempGO.transform.position, tempGO.transform.rotation);
                go.name = currentGO.name;
                particleBuildSuccess.transform.position = go.transform.position;
                particleBuildSuccess.Play();
                go.GetComponent<MeshRenderer>().material = successBuild;
                go.AddComponent<BuildObject>();
                go.GetComponent<BuildObject>().maxDurability = currentGO.durability;
                if (go.GetComponent<BoxCollider>())
                {
                    go.GetComponent<BoxCollider>().isTrigger = false;
                }                
                if (go.GetComponent<MeshCollider>())
                {
                    go.GetComponent<MeshCollider>().isTrigger = false;
                }              
                if (go.GetComponent<ItemPickUp>())
                {
                    go.GetComponent<ItemPickUp>().enabled = false;
                }
                isBuilding = false;
                Destroy(tempGO.gameObject);
                tempGO = null;
                existTemp = false;
                
            }

        }
    }
}
