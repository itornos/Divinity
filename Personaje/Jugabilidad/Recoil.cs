using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Recoil : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject recoil;

    [SerializeField] bool apuntando;

    //rotacion hombros
    [SerializeField] Vector3 currentRotacion;
    [SerializeField] Vector3 targetRotation;

    //rotacion Weapon
    [SerializeField] Vector3 currentRotacionWeapon;
    [SerializeField] Vector3 targetRotationWeapon;
    [SerializeField] float zRotation = 0f;
    [SerializeField] float yRotation = 0f;
    [SerializeField] float xRotation = 0f;
    [SerializeField] float maxY;
    [SerializeField] float maxZ;
    [SerializeField] float maxX;

    [SerializeField] private Transform weaponTransform; // Transform del arma

    //Disparo sin apuntar
    [SerializeField] private float recoilX;
    [SerializeField] private float recoilY;
    [SerializeField] private float recoilZ;

    //Apuntando
    [SerializeField] private float AdsRecoilX;
    [SerializeField] private float AdsRecoilY;
    [SerializeField] private float AdsRecoilZ;

    //velocidad movimiento retroceso
    [SerializeField] private float snappiness;
    [SerializeField] private float snappinessWeapon;
    [SerializeField] private float returnSpeed;

    private void Start()
    {
        apuntando = false;
        recoil = GameObject.Find("Recoil");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        //RECOIL CAMARA
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotacion = Vector3.Slerp(currentRotacion, targetRotation, snappiness * Time.deltaTime);
        recoil.transform.localRotation = Quaternion.Euler(currentRotacion);

        //RECOIL WEAPON
        targetRotationWeapon = Vector3.Lerp(targetRotationWeapon, Vector3.zero, returnSpeed * Time.deltaTime);
        currentRotacionWeapon = Vector3.Slerp(currentRotacionWeapon, targetRotationWeapon, snappinessWeapon * Time.deltaTime);
        weaponTransform.localRotation = Quaternion.Euler(currentRotacionWeapon);
    }

    public void recoilFire() {
        float x = apuntando ? AdsRecoilX : recoilX;
        float y = apuntando ? AdsRecoilY : recoilY;
        float z = apuntando ? AdsRecoilZ : recoilZ;

        z = Random.Range(-z, z);
        y = Random.Range(-y, y);
        targetRotation += new Vector3(x, y, z);

        zRotation += z;
        yRotation += y;
        xRotation += x;
        yRotation = Mathf.Clamp(yRotation, -maxY, maxY);
        zRotation = Mathf.Clamp(zRotation, -maxZ, maxZ);
        xRotation = Mathf.Clamp(xRotation, -maxX, maxX);

        targetRotationWeapon += new Vector3(xRotation, yRotation, zRotation);
    }

    public void apuntar(bool v)
    {
        apuntando = v;
        animator.SetBool("apuntar", v);
    }
}
