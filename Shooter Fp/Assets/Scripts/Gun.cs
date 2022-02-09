using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour 
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;
    float allammo = 200;

    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    Quaternion poscamera;

    private float nextTimeToFire = 0f;
    public Recoil Recoil_Script;
    public Text ammoText;

    public void Start()
    {
        
        currentAmmo = maxAmmo;
        ammoText.text = currentAmmo + " | " + allammo;
    }
    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        if(currentAmmo <= 0)
        {
            allammo -= maxAmmo;
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
        }

    }

    IEnumerator Reload ()
    {
        isReloading = true;
        ammoText.text = "R" + " | " + allammo;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
        ammoText.text = currentAmmo + " | " + allammo;
    }

    public void Shoot()
    {
        Recoil_Script.RecoilFire();
        muzzleflash.Play();

        currentAmmo--;

        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {

            TBA_ENEMY target = hit.transform.GetComponent<TBA_ENEMY>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
        ammoText.text = currentAmmo + " | " + allammo;
    }
}
