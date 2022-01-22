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
    [SerializeField] GameObject[] thompsonShootingSound;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire){
            nextTimeToFire = Time.time + 1/fireRate;
            Instantiate(thompsonShootingSound[Random.Range(0, thompsonShootingSound.Length)], transform.position, Quaternion.identity);
            Shoot();
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
