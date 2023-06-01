using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class SpawnNewItem : MonoBehaviour
{
    public GameObject com;
    public GameObject pec;
    public GameObject rar;
    public GameObject uni;
    public GameObject leg;
    GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        int numeroItems = Random.Range(0, 1000);
        if (numeroItems == 777)
        {
            numeroItems = 4;

        }
        else if (numeroItems < 600 || numeroItems > 700)
        {
            numeroItems = 1;
        }
        else if (numeroItems <= 700 && numeroItems > 625)
        {
            numeroItems = 2;

        }
        else if (numeroItems <= 625 && numeroItems >= 600)
        {
            numeroItems = 3;

        }

        for (int i = 0; i < numeroItems; i++)
        {

            int numero = Random.Range(0, 1000);
            if (numero == 777)
            {
                item = Instantiate(leg, transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (numero <= 400)
            {
                item = Instantiate(com, transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (numero <= 700)
            {
                item = Instantiate(pec, transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (numero <= 900)
            {
                item = Instantiate(rar, transform.position, new Quaternion(0, 0, 0, 0));
            }
            else if (numero <= 980)
            {
                item = Instantiate(uni, transform.position, new Quaternion(0, 0, 0, 0));
            }

            Rigidbody rb = item.GetComponent<Rigidbody>();

            // Genera un vector aleatorio en el plano XZ
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            Vector3 jumpDirection = new Vector3(randomDirection.x, 1, randomDirection.y);

            // Combina la dirección aleatoria con una componente de dirección en el eje Y
            jumpDirection += Vector3.up * 2f;

            // Aplica una fuerza en la dirección aleatoria
            rb.AddForce(jumpDirection.normalized * 100f, ForceMode.Impulse);

            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
