
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

        //Preguntar si tenemos el arma al inventario y si pulsamos x tecla, eqquiparlo;
        //if (photonView.IsMine && Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    { photonView.RPC("Equip", RpcTarget.All, 0); }
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    //Equip();
        //}


        if (currentWeapon != null)
        {
            var currWeapon = currentWeapon.GetComponent<EquippmentStats>();

            if (Input.GetMouseButtonDown(0) && currentcoolDown >= currWeapon.itemObject.fireRate)
            {

                Debug.Log("Hit!!");
                Invoke("Hit", 0.6f);
                currentWeapon.GetComponent<EquippmentStats>().PlayAnim();
                currentcoolDown = 0f;

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


    void Hit()
    {

        int damageWeapon = currentWeapon.GetComponent<EquippmentStats>().itemObject.atkBonus;

        if (Physics.Raycast(camera_player.transform.position, camera_player.transform.forward, out hit, 2f, ~ignoreLayers))
        {
            Vector3 impact = hit.point;

            if (hit.transform.CompareTag("Resource"))
            {

                hit.collider.GetComponent<Resources>().particlesGather.transform.position = impact;
                hit.collider.GetComponent<Resources>().particlesGather.Play();

                hit.collider.GetComponent<Resources>().TakeDamage(damageWeapon);

            }

            if (hit.transform.CompareTag("Container"))
            {

                hit.collider.GetComponent<LootBox>().particles.transform.position = impact;
                hit.collider.GetComponent<LootBox>().particles.Play();

                hit.collider.GetComponent<LootBox>().TakeDamage(damageWeapon);

            }
            if (hit.transform.CompareTag("Enemy"))
            {

                hit.collider.GetComponent<EnemyIA>().particles.transform.position = impact;
                hit.collider.GetComponent<EnemyIA>().particles.Play();

                hit.collider.GetComponent<EnemyIA>().TakeDamage(damageWeapon);

            }
        }
    }

}


