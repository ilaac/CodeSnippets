using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("Gun Stats")]
    public float damage;
    public float range;
    public float fireRate;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;

    [Header("HipFireRecoil")]
    [SerializeField] public float _recoilX;
    [SerializeField] public float _recoilY;
    [SerializeField] public float _recoilZ;

    [Header("AimFireRecoil")]
    [SerializeField] public float _aimRecoilX;
    [SerializeField] public float _aimRecoilY;
    [SerializeField] public float _aimRecoilZ;

    [Header("Recoil Settings")]
    [SerializeField] public float _snappiness;
    [SerializeField] public float _returnSpeed;

    [Header("Visuals and Sound")]
    public ParticleSystem muzzleFlash;
    public ParticleSystem BulletCasing;
    public GameObject impactEffect;
    public LineRenderer lineRendererPrefab;
    public Transform trailSpawnPoint;
    [SerializeField] private GunSoundScar _ScarSound;
    [SerializeField] private ReloadSound _ReloadSound;

    [Header("References")]
    public Recoil recoil;
    public Aim aim;
    public Camera fpsCam;
    public Camera cam;
    public MagazineUI MagUI;

    private float nextTimeToFire;
    public bool isShooting;
    public bool isReloading;

    private Vector3 originalPosition;
    private Vector3 recoilSmoothDampVelocity;

    private void Start()
    {
        Transform cameraRecoilTransform = transform.Find("CameraRotation/CameraRecoil");
        cam = Camera.main;
        originalPosition = transform.localPosition;
        isShooting = false;
        isReloading = false;
        currentAmmo = maxAmmo; // Set initial ammo to max
        MagUI.UpdateMagazineText(currentAmmo);
    }

    void Update()
    {
        if (PauseManager.Instance.IsPaused)
        {
            return;
        }

        //Left click to shoot if you have above 1 ammo and if the firerate allows it
        if (!isReloading && Input.GetButton("Fire1") && Time.time >= nextTimeToFire && currentAmmo > 0)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        //Initialize reload
        if (!aim.isADS && Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            Reload();
        }

        SmoothRecoilMovement();
    }

    private void Shoot()
    {
        //ShotSound
        _ScarSound.ScarGunSound();

        isShooting = true;

        //visual feedback for shooting
        
        muzzleFlash.Play();
        BulletCasing.Play();
        recoil.RecoilFire();
        
        //Creates a linerenderer so you know where you're shooting
        LineRenderer lineRenderer = Instantiate(lineRendererPrefab);
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, trailSpawnPoint.position);

        //Shooting a raycast that also spawns a hiteffect at the point of inpact
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            lineRenderer.SetPosition(1, hit.point);

            GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impact, 1.2f);

            // Apply damage to the hit object
            IDamageable isDamageable = hit.transform.GetComponent<IDamageable>();
            if (isDamageable != null)
            {
                isDamageable.Damage(damage);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, fpsCam.transform.position + fpsCam.transform.forward * range);
        }

        //Gets rid of the linerenderer and updates current ammo logic + visual
        Destroy(lineRenderer.gameObject, 0.05f);
        currentAmmo--;
        MagUI.UpdateMagazineText(currentAmmo);
    }

    private void Reload()
    {
        //Reload logic
        isReloading = true;
        _ReloadSound.PlayReloadSound();
        Invoke("CompleteReload", reloadTime);
        GetComponent<Animator>().Play("Reload");
        MagUI.ReloadMagazineText(maxAmmo);
    }

    private void CompleteReload()
    {
        //Updates ammo and allows player to shoot again
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    private void SmoothRecoilMovement()
    {
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition, originalPosition, ref recoilSmoothDampVelocity, _returnSpeed);
    }
}
