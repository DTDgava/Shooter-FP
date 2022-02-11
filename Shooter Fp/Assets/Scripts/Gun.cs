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
    float allammo = 21;

    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    Quaternion poscamera;

    private float nextTimeToFire = 0f;
    public Recoil Recoil_Script;
    public Text ammoText;

    public void Start()
    {

        this.currentAmmo = this.maxAmmo;
        this.ammoText.text = this.currentAmmo + " | " + this.allammo;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 1)
        {
            this.ammoText.text = this.currentAmmo + " | " + this.allammo;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 1)
        {
            this.ammoText.text = this.currentAmmo + " | " + this.allammo;
        }
        
        if (this.isReloading)
            return;
        if(currentAmmo <= 0 && Input.GetKeyDown(KeyCode.R))
        {
            this.ammoText.text = "R" + " | " + this.allammo;
            if (allammo < maxAmmo)
            {
                this.currentAmmo = (int)this.allammo;
                this.allammo = 0;
            }
            else
            {
                this.allammo -= this.maxAmmo;
            }
            StartCoroutine(Reload());
            return;
            
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
            
        }

    }

    IEnumerator Reload ()
    {
        this.isReloading = true;
        this.ammoText.text = "R" + " | " + this.allammo;
        yield return new WaitForSeconds(reloadTime);
        if (this.allammo > this.maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        this.isReloading = false;
        this.ammoText.text = this.currentAmmo + " | " + this.allammo;
    }

    public void Shoot()
    {
        Recoil_Script.RecoilFire();
        muzzleflash.Play();

        this.currentAmmo--;

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
        this.ammoText.text = this.currentAmmo + " | " + this.allammo;
    }
}
