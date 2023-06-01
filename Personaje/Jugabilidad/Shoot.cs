using Unity.FPS.Game;
using UnityEngine;
using TMPro;

public class Shoot : MonoBehaviour
{

    [SerializeField] float danno;
    [SerializeField] float range;
    [SerializeField] int tamannoCargador;
    [SerializeField] float cadencia; // balas por minuto
    [SerializeField] float tiempoRecarga;

    [SerializeField] Camera camaraFps;
    [SerializeField] bool disparando;

    [SerializeField] float tiempoUltimoDisparo = 0f;
    [SerializeField] int balas;
    [SerializeField] Recoil recoil;

    [SerializeField] bool isRelouding;

    [SerializeField] AudioSource shootSound;
    [SerializeField] AudioSource reloadSound;

    [SerializeField] Animator anim;

    [SerializeField] TextMeshProUGUI textAmmo;

    private void Start()
    {
        tiempoRecarga = 1f;
        recoil = GetComponent<Recoil>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isRelouding) {
            return;
        }

        if (disparando && Time.time - tiempoUltimoDisparo >= 60f / cadencia && balas != 0)
        {
            fire();
            balas--;
            TextAmmo.SetText(balas+"");
            tiempoUltimoDisparo = Time.time;
        }
    }

    void fire()
    {
        RaycastHit hit;
        if (Physics.Raycast(camaraFps.transform.position, camaraFps.transform.forward, out hit, 1000)) {
            float distancia = hit.distance;
            float dannoFinal = danno;
            if (distancia > range) {
                distancia -= range;
                dannoFinal -= ((distancia / danno) * 3);
            }
            Damageable t = hit.transform.GetComponent<Damageable>();
            if (t != null)
            {
                t.InflictDamage(dannoFinal, false, gameObject);
            }
            else if(hit.transform.gameObject.tag == "destruible"){
                Destroy(hit.transform.gameObject);
            }
        }

        shootSound.Play();
        recoil.recoilFire();
    }
    public void reload() {
        if (tamannoCargador != balas) { 
            isRelouding = true;
        }
    }

    public void recargaAnim()
    {
        reloadSound.Play();
        GetComponent<Animator>().SetBool("recarga", false);
    }

    public void ejectaRecarga() {
        balas = tamannoCargador;
        TextAmmo.SetText(balas + "");
        isRelouding = false;
    }

    public bool Disparando { get => disparando; set => disparando = value; }
    public int Balas { get => balas; set => balas = value; }
    public int TamannaCargador { get => tamannoCargador; set => tamannoCargador = value; }

    public TextMeshProUGUI TextAmmo { get => textAmmo; set => textAmmo = value; }

    public void setDanno(int danno)
    {
        this.danno = danno;
    }

    public void setRango(int range)
    {
        this.range = range;
    }

    public void setCadencia(int cadencia)
    {
        this.cadencia = cadencia;
    }

    public void setTamnnoCargador(int tamannoCargador)
    {
        this.tamannoCargador = tamannoCargador;
        balas = tamannoCargador;
        isRelouding = false;
    }

    public void setVelocidadRecarga(float bonus)
    {
        tiempoRecarga += bonus/5;
        anim.SetFloat("VelocidadRecarga", tiempoRecarga);
        anim.SetFloat("VelocidadRecarga", tiempoRecarga);
    }

    public void reset()
    {
        tiempoRecarga = 1;
        isRelouding = false;
    }

    public void iniciaTexto() {
        textAmmo.SetText(balas + "");
    }
}
