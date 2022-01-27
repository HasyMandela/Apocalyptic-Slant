using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class ExplosionScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject explosionEffect;
    [SerializeField] GameObject explosionSound;
    [SerializeField] float slomoTime;
    [SerializeField] float slomoSpeed;
    MeshRenderer meshRenderer;
    void Start(){
        meshRenderer = GetComponent<MeshRenderer>();
    }
    //[SerializeField] PostProcessVolume volume;
    void Update()
    {
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        Instantiate(explosionSound, transform.position, Quaternion.identity);
        meshRenderer.enabled = false;
        for (float i = slomoTime; i > 0; i += 0)
        {
            Time.timeScale = slomoSpeed;
        }
        Time.timeScale = 1;
        Destroy(gameObject);
    }

}
