using UnityEngine;
using TMPro;
using System.Data.SqlTypes;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine.VFX;

public class GunSystem : MonoBehaviour
{
    [Header("Stats")]
    //public int damage;
    //public float fireRate, spread, range, reloadTime, timeBetweenShots;
    //public int magazineSize, bulletsPerTap, burstAmount;
    //public bool allowButtonHold, allowBurst, allowDifferentMode, burstNormalMode, burstAutoMode, autoNormalMode, isShotgun;
    int bulletsLeft, bulletsShot;
    [SerializeField] private float basefirerate;
    //bools
    bool shooting, readyToShoot, reloading;
    bool allowInvoke = true;

    [Header("Reference")]
    [SerializeField] private Weapons Weapon;
    public Camera fpsCam;
    public Transform attackPoint;
    public RaycastHit rayHit;
    public LayerMask whatIsEnemy,whatIsEnviroment;
    public Input fire, reload, swapMode, zoom;

    [Header("Graphics")]
    public GameObject ImpactGraphics;
    public VisualEffect muzzleFlash;
    public TextMeshProUGUI text;
    public Animator animator;

    public void Play(VFXEventAttribute eventAttribute)
    {
        
    }

    private void Awake()
    {
        
        bulletsLeft = Weapon.magazineSize;
        readyToShoot = true;
        reloading= false;
        basefirerate = Weapon.timeBetweenShots;
    }
    private void Update()
    {
        MyInput();

        //SetText
        if (Weapon.isShotgun == true)
            text.SetText(bulletsLeft / Weapon.bulletsPerTap + " / " + Weapon.magazineSize / Weapon.bulletsPerTap);
        else 
            text.SetText(bulletsLeft + " / " + Weapon.magazineSize);
    }

    private void OnEnable()
    {
        reloading = false;
        //animator.SetBool("Reloading", false);
    }

    private void MyInput()
    {
        if (Weapon.allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
        else shooting = Input.GetKeyDown(KeyCode.Mouse0);


        //Reload
        if (Input.GetKeyDown(KeyCode.R))
            StartCoroutine(Reload());

        //Shoot
        //if (Weapon.isShotgun == true) Weapon.burstAmount = Weapon.bulletsPerTap * 2;
        

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && Weapon.allowBurst == false)
        {
            Weapon.timeBetweenShots = basefirerate;
            bulletsShot = Weapon.bulletsPerTap;
            Shoot();

        }
        else if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && Weapon.allowBurst == true)
        {
            Weapon.timeBetweenShots = Weapon.fireRate;
            bulletsShot = Weapon.burstAmount;
            Shoot();

        }

        //Swap fire mode
        if (Weapon.allowDifferentMode == true && Input.GetKeyDown(KeyCode.C))
        {
            //burst to normal fire mode
            if (Weapon.burstNormalMode == true && Weapon.allowBurst == false)
                Weapon.allowBurst = true;
            else if (Weapon.burstNormalMode == true && Weapon.allowBurst == true) Weapon.allowBurst = false;
            //burst to auto fire mode
            if(Weapon.burstAutoMode == true && Weapon.allowBurst == false)
            {
                Weapon.allowBurst = true;
                Weapon.allowButtonHold = false;
            }
            else if (Weapon.burstAutoMode == true && Weapon.allowBurst == true)
            {
                Weapon.allowBurst = false;
                Weapon.allowButtonHold = true;
            }
            //auto to normal fire mode
            if (Weapon.autoNormalMode == true && Weapon.allowButtonHold == false)
                Weapon.allowButtonHold = true;
            else if(Weapon.autoNormalMode == true && Weapon.allowButtonHold == true)
                Weapon.allowButtonHold = false;

        }
    }

    private void Shoot()
    {
        
        readyToShoot = false;

        //Spread
        float x = Random.Range(-Weapon.spread, Weapon.spread);
        float y = Random.Range(-Weapon.spread, Weapon.spread);
        float z = Random.Range(-Weapon.spread, Weapon.spread);

        //Calculate direction with spread
        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, z);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, Weapon.range, whatIsEnemy))
        {
            Target target = rayHit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(Weapon.damage);
                Instantiate(ImpactGraphics, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            }

        }
        //Instantiate(muzzleFlash, attackPoint.position, Quaternion.LookRotation(direction));

        //Graphics
        if (Physics.Raycast(fpsCam.transform.position,direction, out rayHit, Weapon.range, whatIsEnviroment) && !Physics.Raycast(fpsCam.transform.position, direction, Weapon.range, whatIsEnemy))
        {
            Instantiate(ImpactGraphics, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            //Instantiate(muzzleFlash, attackPoint.position, Quaternion.identity);
        }
        muzzleFlash.Play();

        bulletsLeft--;
        bulletsShot--;
        if (allowInvoke)
        {
            Invoke("ResetShot", Weapon.timeBetweenShots);
            allowInvoke = false;
        }
        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", Weapon.timeBetweenShots);
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
        yield return new WaitForSeconds(Weapon.reloadTime);
        bulletsLeft = Weapon.magazineSize;
        //animator.SetBool("Reloading", false);
        //yield return new WaitForSeconds(0.25f);
        reloading = false;
    }


}
