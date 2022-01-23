using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColletablesScript : MonoBehaviour
{
    enum ItemType : int {
        Ammo = 0
    }
    [SerializeField] private ItemType itemType;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider){
        Debug.Log("Collide");
        if (collider.CompareTag("Player")){
            Debug.Log("Collide");
            if (itemType == ItemType.Ammo){
                Ammo(PlayerScript.Instance);
            }
        }
    }
    void Ammo(PlayerScript player){

        //Do something with the ammo when you collide with the player.
        Debug.Log("Collect Ammo");
        Destroy(gameObject);
        player.gunSwitch.GetCurrentGun().GetComponent<GunScript>().sets += 2;
    }
}
