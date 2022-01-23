using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    [SerializeField] private GameObject[] guns;
     private int gunNum;
     private int gunNumInfo;
    // Dictionary<int, GameObject> gunInfo = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
         gunNum = 1;
        // for (int i = 0; i < guns.Length; i++){
        //     gunInfo.Add(i+1, guns[i]);
        //     Debug.Log(gunInfo);
        // }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1)){
            Switch();
        }
    }
    void Switch(){
        if (gunNum != guns.Length){
            gunNum += 1;
        }
        else {
            gunNum = 1;
        }
        for (int i = 0; i < guns.Length; i++){
            if (i != gunNum-1){
                guns[i].SetActive(false);
            } else if (i == gunNum-1){
                guns[i].SetActive(true);
                gunNumInfo = i;
            }
        }
    }
    public GameObject GetCurrentGun(){
        return guns[gunNumInfo];
    }
}
