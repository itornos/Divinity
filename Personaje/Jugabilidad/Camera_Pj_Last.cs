using UnityEngine;

public class Camera_Pj_Last : MonoBehaviour
{
    [SerializeField] GameObject brazos;
    [SerializeField] GameObject camaraFPS;
    [SerializeField] GameObject camaraTP;
    [SerializeField] Texture2D cursor;

    [SerializeField] float xRotation = 0f;
    [SerializeField] float yRotation = 0f;
    [SerializeField] float sensitivity;

    [SerializeField] bool playing;

    [SerializeField] public float speed = 0f;

    void Start()
    {
        sensitivity = 700f;
        Cursor.lockState = CursorLockMode.Locked;
        playing = true;
    }

    void Update()
    {
        if (!playing) {
            return;
        }

        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");
        Rotacion(y, x);
    }

    private void Rotacion(float y,float x) 
    {
        xRotation += y;
        xRotation = Mathf.Clamp(xRotation, -80f, 70f);

        yRotation += x;

        Quaternion thisGameObjectQuaternion = Quaternion.Euler(0f, yRotation, 0f); 
        Quaternion brazosQuaternion = Quaternion.Euler(xRotation, 0f, 0f);

        // Aplicar la rotación al objeto
        transform.localRotation = Quaternion.Lerp(transform.rotation, thisGameObjectQuaternion, sensitivity * Time.deltaTime);
        brazos.transform.localRotation = Quaternion.Lerp(transform.rotation, brazosQuaternion, sensitivity * Time.deltaTime);


    }

    public void cambiarEstado(bool estado) {
        if (estado)
        {
            Cursor.lockState = CursorLockMode.Locked;           
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.SetCursor(cursor, new Vector2(0,0) , CursorMode.Auto);
        }

        playing = estado;
    }

    public void cambioCamara() {
        if (camaraFPS.activeSelf)
        {
            camaraFPS.SetActive(false);
            camaraTP.SetActive(true);
        }
        else {
            camaraFPS.SetActive(true);
            camaraTP.SetActive(false);
        }
    }
}
