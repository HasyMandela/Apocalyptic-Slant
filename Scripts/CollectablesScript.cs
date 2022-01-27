using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablesScript : MonoBehaviour
{
    enum ItemType : int {
        Ammo = 0,
        Health = 1
    }
    [SerializeField] private GameObject collectSound;
    [SerializeField] private ItemType itemType;
    [SerializeField] private int healthAdd;
    // Start is called before the first frame update

    void OnTriggerEnter(Collider collider) {
        if (collider.CompareTag("Player")) {
            Instantiate(collectSound, transform.position, Quaternion.identity);
            if (itemType == ItemType.Ammo) {
                Ammo();
            }
            else if (itemType == ItemType.Health )
            {
                Health();
            }
            Destroy(gameObject);
        }
    } 
    void Ammo() {

        //Do something with the ammo when you collide with the player.
        Debug.Log("Collect Ammo");
        PlayerScript.Instance.gunSwitch.GetCurrentGun().GetComponent<GunScript>().sets += 2;
    }
    void Health()
    {
        if (PlayerScript.Instance.health + healthAdd < 100) 
        {
            PlayerScript.Instance.health += healthAdd;
        }
        else
        {
            PlayerScript.Instance.health = 100;
        }
    }
    
}
