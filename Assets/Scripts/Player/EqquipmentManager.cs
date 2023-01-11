
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EqquipmentManager : MonoBehaviour
{
    public static EqquipmentManager Instance;


    [SerializeField] LayerMask ignoreLayers;
    [SerializeField] float timeDelayAnim;
    [SerializeField] GameObject camera_player;

    [HideInInspector]
    public GameObject currentWeapon;

    private float currentcoolDown;
    private RaycastHit hit;
    private Animator anim;


    private void Awake()
    {
        Instance = this;
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {

        currentcoolDown += Time.deltaTime;

        if (currentWeapon != null)
        {
            var currWeapon = currentWeapon.GetComponent<EquippmentStats>();

            if (Input.GetMouseButton(0) && currentcoolDown >= currWeapon.itemObject.fireRate && !EventSystem.current.IsPointerOverGameObject())
            {
                Invoke("HitMelee", timeDelayAnim);
                anim.SetTrigger("Hit");
                currentcoolDown = 0f;
            }
        }
    }

    private void HitMelee()
    {
        
        if (currentWeapon == true)
        {
            int damageWeapon;
            float range = currentWeapon.GetComponent<EquippmentStats>().itemObject.range;

            if (Physics.Raycast(camera_player.transform.position, camera_player.transform.forward, out hit, range, ~ignoreLayers))
            {
                Vector3 hitPoint = hit.point;

                Resources resources = hit.collider.GetComponent<Resources>();

                var hitable = hit.collider.GetComponent<IHitable>();

                if (resources)
                {

                    if (resources.typeResources == typeResources.wood)
                    {
                        damageWeapon = currentWeapon.GetComponent<EquippmentStats>().itemObject.CalculateDamageWood();
                        hitable.TakeDamage(damageWeapon, hitPoint);
                    }
                    if (resources.typeResources == typeResources.mineral)
                    {
                        damageWeapon = currentWeapon.GetComponent<EquippmentStats>().itemObject.CalculateFarmMineral();
                        hitable.TakeDamage(damageWeapon, hitPoint);
                    }
                }
                else
                {
                    damageWeapon = currentWeapon.GetComponent<EquippmentStats>().itemObject.atkBonus;
                    hitable.TakeDamage(damageWeapon, hitPoint);
                }
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


