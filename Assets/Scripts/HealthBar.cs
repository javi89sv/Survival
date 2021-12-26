using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public GameObject image;

    Resources resources;

    public Vector3 offset;





    // Start is called before the first frame update
    void Start()
    {
        transform.position += offset;
        resources = GetComponentInParent<Resources>();
    }


    // Update is called once per frame
    void Update()
    {
        UpdateHealth();
        // transform.LookAt(2 * transform.position - Camera.main.transform.position);
        transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.Translate(transform.forward, Space.World);
    }

    public void UpdateHealth()
    {
        image.GetComponent<Image>().fillAmount = resources.health / resources.maxhealth;
    }
}
