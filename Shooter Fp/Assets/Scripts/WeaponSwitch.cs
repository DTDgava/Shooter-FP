using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour
{
    public Camera MainCamera;
    public int selectedWeapon = 0;
    public GameObject CameraWeapon;
    public Sprite[] Targets;
    public Image Target;
    bool gunboolean;


    // Start is called before the first frame update
    void Start()
    {
        SelectWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        int previousthing = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }
        if (previousthing != selectedWeapon)
        {
            SelectWeapon();
        }
        if (selectedWeapon == 0)
        {
            Target.sprite = Targets[0];
            if(Input.GetButtonDown("Fire2"))
            {
                CameraWeapon.SetActive(true);
            }
            Target.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            if (Input.GetButtonDown("Fire2"))
            {
                if (gunboolean == true)
                {
                    MainCamera.fieldOfView = 60;
                    gunboolean = false;
                }
                else if (gunboolean == false)
                {
                    MainCamera.fieldOfView = 45;
                    gunboolean = true;

                }
            }
            CameraWeapon.SetActive(false);
            Target.sprite = Targets[1];
            Target.transform.localScale = new Vector3(0.1f, 0.2f, 0.2f);
        }

}
    void SelectWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
