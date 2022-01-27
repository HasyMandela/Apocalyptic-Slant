using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GunScript : MonoBehaviour
{
    public float damage;
    public float range;
    public float fireRate;
    float nextTimeToFire;
    [SerializeField] ParticleSystem muzzleFlashEffect;
    [SerializeField] GameObject impactEffect;
    [SerializeField] GameObject bloodEffect;
    [SerializeField] Camera fpsCam;
    [SerializeField] GameObject[] shootingSound;
    [SerializeField] LayerMask enemyMask;

    [SerializeField] LayerMask explosionMask;
    [SerializeField] LayerMask notEnemyMask;
    public int sets;
    [SerializeField] int ammo;
    public int currentAmmo;
    [SerializeField] GameObject reloadSound;
    [SerializeField] private Image crosshair;
    [SerializeField] private Color whenTouchedEnemy;
    [SerializeField] private Color whenNotTouchedEnemy;
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
        if (Input.GetKeyDown(KeyCode.R) & sets > 0 && currentAmmo != ammo){
            Instantiate(reloadSound, transform.position, Quaternion.identity);
            currentAmmo = ammo;
            sets--;
        }
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, enemyMask))
        {
            crosshair.color = whenTouchedEnemy;
        }
        else if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, notEnemyMask))
        {
            crosshair.color = whenNotTouchedEnemy;
        }
        else
        {
            crosshair.color = whenNotTouchedEnemy;
        }
    }

    //Check it we shoot
    void Shoot()
    {
        muzzleFlashEffect.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, notEnemyMask))
        {
            Debug.Log(hit.transform.name);
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 60);
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, enemyMask)){
            Debug.Log(hit.transform.name);
            DamageableScript target = hit.transform.GetComponent<DamageableScript>();
            if (PlayerScript.Instance.isGround == false)
            {
                target.TakeDamage(damage * 1.5f);
            } else if (PlayerScript.Instance.isGround)
            {
                target.TakeDamage(damage);
            }
            GameObject impactGO = Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
            impactGO.transform.parent = hit.transform;
            Destroy(impactGO, 60);
        }
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, explosionMask))
        {
            Debug.Log(hit.transform.name);
            ExplosionScript explosion = hit.transform.GetComponent<ExplosionScript>();
            Instantiate(explosion.gameObject, hit.transform.position, Quaternion.identity);
        }
    }
}
