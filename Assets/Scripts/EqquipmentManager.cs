
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EqquipmentManager : MonoBehaviour
{
    public static EqquipmentManager Instance;
    public Weapon[] eqquipmentList;
    public GameObject bulletPrefab;
    public LayerMask canBeshot;

    [HideInInspector]
    public GameObject currentWeapon;
    private float currentcoolDown;
    public GameObject camera_player;

    public LayerMask ignoreLayers;
    private RaycastHit hit;


    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {

        currentcoolDown += Time.deltaTime;

        if (currentWeapon != null)
        {
            var currWeapon = currentWeapon.GetComponent<EquippmentStats>();

            if (Input.GetMouseButtonDown(0) && currentcoolDown >= currWeapon.itemObject.fireRate)
            {            
                Invoke("Hit", 0.6f);
                currentWeapon.GetComponent<EquippmentStats>().PlayAnim();
                currentcoolDown = 0f;
            }
        }
    }

    void Hit()
    {
        int damageWeapon = currentWeapon.GetComponent<EquippmentStats>().itemObject.atkBonus;

        if (Physics.Raycast(camera_player.transform.position, camera_player.transform.forward, out hit, 2f, ~ignoreLayers))
        {
            Vector3 hitPoint = hit.point;

            var impact = hit.collider.GetComponent<IHitable>();

            if (impact != null)
            {
                impact.TakeDamage(damageWeapon, hitPoint);
            }
        }
    }


    //private void Aim(bool isAiming)
    //{
    //    Transform anchor = currentWeapon.transform.Find("Anchor");
    //    Transform state_ads = currentWeapon.transform.Find("States/ADS");
    //    Transform state_hip = currentWeapon.transform.Find("States/Hip");

    //    if (isAiming)
    //    {
    //        //aim
    //        anchor.position = Vector3.Lerp(anchor.position, state_ads.position, Time.deltaTime * eqquipmentList[currentIndex].aimSpeed);
    //    }
    //    else
    //    {
    //        //hip
    //        anchor.position = Vector3.Lerp(anchor.position, state_hip.position, Time.deltaTime * eqquipmentList[currentIndex].aimSpeed);
    //    }
    //}


    //void Shoot()
    //{

    //    Transform spawn = transform.Find("Cameras/Normal Camera");

    //    //bloom
    //    Vector3 bloom = spawn.position + spawn.forward * 1000f;
    //    bloom += Random.Range(-eqquipmentList[currentIndex].bloom, eqquipmentList[currentIndex].bloom) * spawn.up;
    //    bloom += Random.Range(-eqquipmentList[currentIndex].bloom, eqquipmentList[currentIndex].bloom) * spawn.right;
    //    bloom -= spawn.position;
    //    bloom.Normalize();

    //    //cooldown
    //    currentcoolDown = eqquipmentList[currentIndex].firerate;

    //    //raycast
    //    RaycastHit hit = new RaycastHit();
    //    if (Physics.Raycast(spawn.position, bloom, out hit, 1000f, canBeshot))
    //    {
    //        GameObject newHole = Instantiate(bulletPrefab, hit.point + hit.normal * 0.001f, Quaternion.identity);
    //        newHole.transform.LookAt(hit.point + hit.normal);
    //        Destroy(newHole, 5f);

    //        //shooting other player on network

    //        if (hit.collider.gameObject.layer == 6)
    //        {
    //            //RPC call to damage Player
    //        }


    //    }

    //    //gun fx
    //    currentWeapon.transform.Rotate(-eqquipmentList[currentIndex].recoil, 0, 0);



    //}



}


