
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equippement : MonoBehaviour
{

    public Weapon[] eqquipmentList;
    public Transform weaponParent;
    public GameObject bulletPrefab;
    public LayerMask canBeshot;

    private GameObject currentWeapon;
    private int currentIndex;
    private float currentcoolDown;
    public GameObject camera_player;

    private float distanceMelee = 1.5f;
    public LayerMask ignoreLayers;

    public int health;
    public float cooldown;
    public int damage;

    RaycastHit hit;

    private void Update()
    {

        currentcoolDown += Time.deltaTime;

        //Preguntar si tenemos el arma al inventario y si pulsamos x tecla, eqquiparlo;
        //if (photonView.IsMine && Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    { photonView.RPC("Equip", RpcTarget.All, 0); }
        //}

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //Equip();
        }


        if (currentWeapon != null)
        {

            Aim((Input.GetMouseButton(1)));


            if (Input.GetMouseButtonDown(0) && currentcoolDown >= eqquipmentList[currentIndex].firerate)
            {
                if (eqquipmentList[currentIndex].type == type.ranged)
                {
                    Debug.Log("Shoot!!");
                    Shoot();
                    currentcoolDown = 0f;
                }
                else if (eqquipmentList[currentIndex].type == type.melee)
                {
                    Debug.Log("Hit!!");
                    currentWeapon.GetComponentInChildren<WeaponAnim>().PlayAnim();
                    Invoke("Hit", 0.4f);
                    currentcoolDown = 0f;
                }

            }
        }

    }
    public void Equip(int index)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon);
        }

        currentIndex = index;

        GameObject newEqquipment = Instantiate(eqquipmentList[index].prefab, weaponParent.position, weaponParent.rotation, weaponParent);
        newEqquipment.transform.localPosition = Vector3.zero;
        newEqquipment.transform.localEulerAngles = Vector3.zero;
        currentWeapon = newEqquipment;


        Destroy(newEqquipment.GetComponent<Rigidbody>());
        Destroy(newEqquipment.GetComponent<BoxCollider>());
        //newEqquipment.GetComponent<Rigidbody>().useGravity = false;
        //newEqquipment.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Dregrade(int amount)
    {
        health -= amount;
    }


    private void Aim(bool isAiming)
    {
        Transform anchor = currentWeapon.transform.Find("Anchor");
        Transform state_ads = currentWeapon.transform.Find("States/ADS");
        Transform state_hip = currentWeapon.transform.Find("States/Hip");

        if (isAiming)
        {
            //aim
            anchor.position = Vector3.Lerp(anchor.position, state_ads.position, Time.deltaTime * eqquipmentList[currentIndex].aimSpeed);
        }
        else
        {
            //hip
            anchor.position = Vector3.Lerp(anchor.position, state_hip.position, Time.deltaTime * eqquipmentList[currentIndex].aimSpeed);
        }
    }


    void Shoot()
    {

        Transform spawn = transform.Find("Cameras/Normal Camera");

        //bloom
        Vector3 bloom = spawn.position + spawn.forward * 1000f;
        bloom += Random.Range(-eqquipmentList[currentIndex].bloom, eqquipmentList[currentIndex].bloom) * spawn.up;
        bloom += Random.Range(-eqquipmentList[currentIndex].bloom, eqquipmentList[currentIndex].bloom) * spawn.right;
        bloom -= spawn.position;
        bloom.Normalize();

        //cooldown
        currentcoolDown = eqquipmentList[currentIndex].firerate;

        //raycast
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(spawn.position, bloom, out hit, 1000f, canBeshot))
        {
            GameObject newHole = Instantiate(bulletPrefab, hit.point + hit.normal * 0.001f, Quaternion.identity);
            newHole.transform.LookAt(hit.point + hit.normal);
            Destroy(newHole, 5f);

            //shooting other player on network

            if (hit.collider.gameObject.layer == 6)
            {
                //RPC call to damage Player
            }


        }

        //gun fx
        currentWeapon.transform.Rotate(-eqquipmentList[currentIndex].recoil, 0, 0);



    }


    void Hit()
    {

        int damageWeapon = eqquipmentList[currentIndex].damage;

        if (Physics.Raycast(camera_player.transform.position, camera_player.transform.forward, out hit, distanceMelee, ~ignoreLayers))
        {
            Vector3 impact = hit.point;

            Debug.Log("HIT!!");

            if (hit.transform.CompareTag("Resource"))
            {

                hit.collider.GetComponent<Resources>().particles.transform.position = impact;
                hit.collider.GetComponent<Resources>().particles.Play();

                hit.collider.GetComponent<Resources>().TakeDamage(damageWeapon);

                // Inventory.instance.DamageItem(GameManager.instance.weapon.GetComponent<InteractiveItem>().item);

                // Gathering();

            }

            if (hit.transform.CompareTag("Container"))
            {

                hit.collider.GetComponent<Container>().particles.transform.position = impact;
                hit.collider.GetComponent<Container>().particles.Play();

                hit.collider.GetComponent<Container>().TakeDamage(damageWeapon);

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


