using UnityEngine;

public class Recoil : MonoBehaviour
{
    private Vector3 currentRotation;
    private Vector3 targetRotation;

    private Aim aimScript;
    private Gun gunScript;

    void Start()
    {
        aimScript = GetComponentInChildren<Aim>();
        gunScript = GetComponentInChildren<Gun>();

        if (gunScript == null)
        {
            Debug.LogError("Gun script not found on the GameObject or its children.");
        }
    }

    void FixedUpdate()
    {
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, gunScript._returnSpeed * Time.deltaTime);
        currentRotation = Vector3.Slerp(currentRotation, targetRotation, gunScript._snappiness * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Euler(currentRotation);
    }
    
    public void RecoilFire()
    {
        if (aimScript != null)
        {
            float recoilX = aimScript.isADS ? gunScript._aimRecoilX : gunScript._recoilX;
            float recoilY = aimScript.isADS ? gunScript._aimRecoilY : gunScript._recoilY;
            float recoilZ = aimScript.isADS ? gunScript._aimRecoilZ : gunScript._recoilZ;

            targetRotation += new Vector3(recoilX, Random.Range(-recoilY, recoilY), Random.Range(-recoilZ, recoilZ));
        }
        else
        {
            Debug.LogError("Aim script is not attached or not found on the GameObject.");
        }
    }
}
