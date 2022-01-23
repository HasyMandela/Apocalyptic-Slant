using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GunScript : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;
    float nextTimeToFire;
    [SerializeField] ParticleSystem muzzleFlashEffect;
    [SerializeField] GameObject impactEffect;
    [SerializeField] Camera fpsCam;
    [SerializeField] GameObject[] shootingSound;
    public int sets;
    [SerializeField] int ammo;
    int currentAmmo;
    [SerializeField] GameObject reloadSound;

    // Start is called before the first frame update
    void Start(){
        currentAmmo = ammo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire && currentAmmo > 0){
            nextTimeToFire = Time.time + 1/fireRate;
            Instantiate(shootingSound[Random.Range(0, shootingSound.Length)], transform.position, Quaternion.identity);
            Shoot();
            currentAmmo--;
        }
        if (Input.GetKeyDown(KeyCode.R) & sets > 0){
            Instantiate(reloadSound, transform.position, Quaternion.identity);
            currentAmmo = ammo;
            sets--;
        }
    }

    //Check it we shoot
    void Shoot()
    {
        muzzleFlashEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            DamageableScript target = hit.transform.GetComponent<DamageableScript>();
            if (target != null){
                target.TakeDamage(damage);
            }
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2);
        }
    }
}
