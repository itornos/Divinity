using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HealthPj : MonoBehaviour
{
    //VIDA
    [SerializeField] const float MAXHEALTH = 200f;
    [SerializeField] float CurrentHealth;
    [SerializeField] const float CRITICALHEALTHRATIO = 20f;

    //;)
    [SerializeField] UnityAction<float, GameObject> OnDamaged;
    [SerializeField] UnityAction<float> OnHealed;
    [SerializeField] UnityAction OnDie;

    //VARIABLE RECUPERACION VIDA
    [SerializeField] const float TIMETORECOVERY = 4f;
    [SerializeField] float recovery;
    [SerializeField] const float percentRecovery = 10f;

    //CONTROL DE MUERTE
    [SerializeField] bool IsDead; 
    [SerializeField] float tiempoAparicion;
    [SerializeField] float currentTiempoAparicion;
    [SerializeField] Unity.FPS.Game.Actor vistoPorEnemigo;

    //CUPULA MUERTE
    [SerializeField] GameObject cupula;
    [SerializeField] Transform posicionSpawnCupula;

    //CONTROL DE POST MUERTE
    [SerializeField] bool IsPostAparicion;
    [SerializeField] float tiempoPostAparicion;
    [SerializeField] float currentPostAparicion;

    //CONTROL TIEMPO HASTA LA RECUPERACION
    [SerializeField] bool GraceTime;
    [SerializeField] float countGraceTime;

    //STATS ARMADURA
    [SerializeField] float statDureza;
    [SerializeField] float statRecuperacion;

    //MUESTRA VIDA EN PANTALLA
    [SerializeField] Slider sliderVida;

    //ENNOVE CON LA ANIMACION DE MUERTE LA QUE HE LIAO PA LA PUTA ANIMACION DE MIERDA
    [SerializeField] Animator currentAnim;
    [SerializeField] RuntimeAnimatorController muelto;
    [SerializeField] RuntimeAnimatorController vivo;

    public bool Invincible { get; set; }

    public bool IsCritical() => CurrentHealth <= CRITICALHEALTHRATIO;

    void Start()
    {
        currentPostAparicion = tiempoPostAparicion;
        CurrentHealth = MAXHEALTH;
        recovery = TIMETORECOVERY-statRecuperacion;
        currentTiempoAparicion = tiempoAparicion;
    }

    void Update()
    {
        //DEVUELVEME A LA VIDA :)
        if (IsDead)
        {
            currentTiempoAparicion -= Time.deltaTime;
            if (currentTiempoAparicion <= 0) {
                currentAnim.SetBool("muelto", false);
                currentTiempoAparicion = tiempoAparicion;
                CurrentHealth = MAXHEALTH;
                IsPostAparicion = true;
                IsDead = false; 
                sliderVida.value = CurrentHealth;
            }
            return;
        }

        //TIEMPO MOVIMIENTO LIBRE HASTA DETECCION DE LOS ENEMIGOS :x
        if (IsPostAparicion) {
            currentPostAparicion -= Time.deltaTime;
            if (currentPostAparicion <= 0)
            {
                vistoPorEnemigo.Affiliation = 1;
                IsPostAparicion = false; 
                currentPostAparicion = tiempoPostAparicion;
            }
        }

        //¿PUEDE RECUPERAR VIDA? >:|
        if (GraceTime)
        {
            CurrentHealth = MAXHEALTH;
            countGraceTime -= Time.deltaTime;
            if (countGraceTime <= 0f)
            {
                GraceTime = false;
            }
            else
            {
                return;
            }
        }

        //RECUPERANDO VIDA :D
        if (CurrentHealth != 200f)
        {
            recovery -= Time.deltaTime + ((Time.deltaTime * percentRecovery) / 100);
            if (recovery <= 0f)
            {
                CurrentHealth += Time.deltaTime * (percentRecovery + (statRecuperacion*10));
                sliderVida.value = CurrentHealth;
                if (CurrentHealth > MAXHEALTH)
                {
                    CurrentHealth = MAXHEALTH;
                    sliderVida.value = CurrentHealth;
                    recovery = TIMETORECOVERY-statRecuperacion;
                }
            }
        }
    }

    public void Heal(float healAmount)
    {
        float healthBefore = CurrentHealth;
        CurrentHealth += healAmount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MAXHEALTH);

        // call OnHeal action
        float trueHealAmount = CurrentHealth - healthBefore;
        if (trueHealAmount > 0f)
        {
            OnHealed?.Invoke(trueHealAmount);
        }

        sliderVida.value = CurrentHealth;
    }

    public void TakeDamage(float damage, GameObject damageSource)
    {
        /*if (Invincible)
            return;
        */
        float reduccion = ((statDureza * 10) / 100) * damage;
        damage -= reduccion;
        float healthBefore = CurrentHealth;
        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0f, MAXHEALTH);
        recovery = TIMETORECOVERY - statRecuperacion;
        // call OnDamage action
        float trueDamageAmount = healthBefore - CurrentHealth;
        if (trueDamageAmount > 0f)
        {
            OnDamaged?.Invoke(trueDamageAmount, damageSource);
        }

        sliderVida.value = CurrentHealth;
        HandleDeath();
    }

    public void Kill()
    {
        CurrentHealth = 0f;

        // call OnDamage action
        OnDamaged?.Invoke(MAXHEALTH, null);

        HandleDeath();
    }

    void HandleDeath()
    {
        if (IsDead)
            return;

        // call OnDie action
        if (CurrentHealth <= 0f)
        {
            IsDead = true;
            OnDie?.Invoke();
            Muelto();
            Instantiate(cupula, posicionSpawnCupula.position, posicionSpawnCupula.rotation);
        }
    }

    public void setStatDureza(float valor) {
        statDureza += valor;
        if(statDureza > 1f)statDureza= 1f;
    }

    public void setStatRecuperacion(float valor)
    {
        statRecuperacion += valor;
        if (statRecuperacion > 1f) statRecuperacion = 1f;
    }

    public void reset()
    {
        statRecuperacion = 0f;
        statDureza = 0f;
    }

    public void noMuelto() {
        currentAnim.runtimeAnimatorController = vivo;
    }

    public void Muelto()
    {
        currentAnim.runtimeAnimatorController = muelto;
    }

    public Slider SliderVida { get => sliderVida; set => sliderVida = value; }
}
