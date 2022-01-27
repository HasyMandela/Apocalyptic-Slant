using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] GunSwitch gunSwitch;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] Image heart;
    [SerializeField] PlayerScript player;
    void Update(){
        ammoText.text = gunSwitch.GetCurrentGun().GetComponent<GunScript>().currentAmmo.ToString();
        heart.fillAmount = player.health/100;
    }

}
