using UnityEngine;
using TMPro;
using System.Data.SqlTypes;
using System.Collections;

public class GunSystem : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public float fireRate, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap, burstAmount;
    public bool allowButtonHold, allowBurst, allowDifferentMode, burstNormalMode, burstAutoMode, autoNormalMode, isShotgun;
    int bulletsLeft, bulletsShot;

    //bools
    bool shooting, readyToShoot, reloading;
    bool allowInvoke = true;

    [Header("Reference")]
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy,whatIsEnviroment;
    public Input fire, reload, swapMode, zoom;

    [Header("Graphics")]
    public GameObject muzzleFlash, ImpactGraphics;
    public TextMeshProUGUI text;
    public Animator animator;

    private void Awake()
    {
        
        bulletsLeft = magazineSize;
        readyToShoot = true;
        reloading= false;
    }
    private void Update()
    {
        MyInput();

        //SetText
        if (isShotgun == true)
            text.SetText(bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap);
        else 
            text.SetText(bulletsLeft + " / " + magazineSize);
    }

    private void OnEnable()
    {
        reloading = false;
        //animator.SetBool("Reloading", false);
    }

    private void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Reload
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());

        //Shoot
        if (isShotgun == true) burstAmount = bulletsPerTap * 2;
        

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && allowBurst == false)
        {
            bulletsShot = bulletsPerTap;
            Shoot();

        }
        else if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && allowBurst == true)
        {
            bulletsShot = burstAmount;
            Shoot();

        }

        //Swap fire mode
        if (allowDifferentMode == true && Input.GetKeyDown(KeyCode.C))
        {
            //burst to normal fire mode
            if (burstNormalMode == true && allowBurst == false)
                allowBurst = true;
            else if (burstNormalMode == true && allowBurst == true) allowBurst = false;
            //burst to auto fire mode
            if(burstAutoMode == true && allowBurst == false)
            {
                allowBurst = true;
                allowButtonHold = false;
            }
            else if (burstAutoMode == true && allowBurst == true)
            {
                allowBurst = false;
                allowButtonHold = true;
            }
            //auto to normal fire mode
            if (autoNormalMode == true && allowButtonHold == false)
                allowButtonHold = true;
            else if(autoNormalMode == true && allowButtonHold == true)
                allowButtonHold = false;

        }
    }

    private void Shoot()
    {
        
        readyToShoot = false;

        //Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);
        float z = Random.Range(-spread, spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, z);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, whatIsEnemy))
        {
            Target target = rayHit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                Instantiate(ImpactGraphics, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            }

        }
        Instantiate(muzzleFlash, attackPoint.position, Quaternion.LookRotation(direction));

        //Graphics
        if (Physics.Raycast(fpsCam.transform.position,direction, out rayHit, range, whatIsEnviroment) && !Physics.Raycast(fpsCam.transform.position, direction, range, whatIsEnemy))
        {
            //Instantiate(bulletHoleGraphics, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }

        bulletsLeft--;
        bulletsShot--;
        if (allowInvoke)
        {
            Invoke("ResetShot", timeBetweenShots);
            allowInvoke = false;
        }
        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);
    }
    private void ResetShot()
    {
        readyToShoot = true;
        allowInvoke = true;
    }

    IEnumerator Reload()
    {
        reloading = true;
        //animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);
        bulletsLeft = magazineSize;
        //animator.SetBool("Reloading", false);
        //yield return new WaitForSeconds(0.25f);
        reloading = false;
    }


}
