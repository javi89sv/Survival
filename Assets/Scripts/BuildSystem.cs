using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildSystem : MonoBehaviour
{

    [SerializeField] Transform childCamera;

    [SerializeField] Transform floorBuild;
    [SerializeField] GameObject currentGO;

    [SerializeField] Material canBuild;
    [SerializeField] Material cantBuild;
    [SerializeField] Material successBuild;

    RaycastHit hit;
    [SerializeField] float rangeBuild;
    [SerializeField] LayerMask layer;

    private void Update()
    {
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, rangeBuild, layer))
        {
            currentGO.GetComponent<MeshRenderer>().material = canBuild;
            currentGO.GetComponent<BoxCollider>().isTrigger = true;
            currentGO.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);

            if (Input.GetKeyDown(KeyCode.R))
            {
                currentGO.transform.Rotate(0, 90, 0, Space.World);
            }

            if (Input.GetMouseButtonDown(0))
            {
                var go = Instantiate(currentGO, currentGO.transform.position, Quaternion.identity);
                go.GetComponent<MeshRenderer>().material = successBuild;
                go.GetComponent<BoxCollider>().isTrigger = false;
            }

        }
    }



}
