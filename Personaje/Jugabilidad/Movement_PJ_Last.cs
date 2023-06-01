using UnityEngine;

public class Movement_PJ_Last : MonoBehaviour
{
    [SerializeField] CharacterController movementController;
    [SerializeField] Animator anim;
    [SerializeField] bool isDead;
    [SerializeField] bool isPlaying;

    ///MOVIMIENTO DEL PJ
    [SerializeField] float X_Vector;
    [SerializeField] float Z_Vector;

    ///GRAVEDAD DEL PJ
    [SerializeField] const float gravedad = -19.62f;
    [SerializeField] Vector3 pjGravedad;
    [SerializeField] Transform SueloCheck;
    [SerializeField] float distanciaSuelo = 0.4f;
    [SerializeField] LayerMask layerSuelo;
    [SerializeField] bool isGrounded;

    ///SALTO
    [SerializeField] float saltoAltura = 1.5f;
    [SerializeField] bool salto;
    [SerializeField] bool saltando;

    ///VELOCIDAD DEL PERSONAJE DEPENDIENDO DE LAS SITUACIONES
    [SerializeField] float velocity;
    [SerializeField] float velocityBOOSTCROUCH;//Se reduce de forma progresiva
    [SerializeField] const float WALK = 4.5f;
    [SerializeField] const float CROUCH = 3.5f;
    [SerializeField] const float RUN = 7.5f;
    [SerializeField] const float BOOSTCROUCH = 12f;
    [SerializeField] float statMove;

    ///SITUACIONES AGACHADO
    [SerializeField] bool boostcrouched;
    [SerializeField] bool iscrouched;

    ///SITUACIONES CORRIENDO
    [SerializeField] bool isrunning;

    [SerializeField] AudioSource pasosAudio;
    [SerializeField] AudioSource saltoAudio;
    [SerializeField] AudioSource caidaAudio;

    // Start is called before the first frame update
    void Start()
    {
        iscrouched = false;
        salto = false;
        isPlaying = true;
        saltando = false;

        X_Vector = 0;
        Z_Vector = 0;

        velocity = WALK;
    }

    private void Update()
    {
        if (!isPlaying) return;

        gravedadSalto();
        movement();
    }

    private void gravedadSalto() {
        isGrounded = Physics.CheckSphere(SueloCheck.position, distanciaSuelo, layerSuelo);

        if (!saltando && isGrounded)
        {
            caidaAudio.Play();
        }
        saltando = isGrounded;

        if (isGrounded && pjGravedad.y < 0)
        {
            pjGravedad.y = -2f;
        }
    }

    private void movement() {
        float normalize_Vector_X = 0;
        float normalize_Vector_Z = 0;

        if ((X_Vector == 1 || X_Vector == -1) && (Z_Vector == 1 || Z_Vector == -1))
        {
            normalize_Vector_X = X_Vector / 1.5f;
            normalize_Vector_Z = Z_Vector / 1.5f;
        }
        else
        {
            normalize_Vector_X = X_Vector;
            normalize_Vector_Z = Z_Vector;
        }

        if (normalize_Vector_X == 0 && normalize_Vector_Z == 0)
        {
            if (iscrouched)
            {
                velocity = CROUCH;
            }
            else
            {
                velocity = WALK;
            }

            isrunning = false;
        }
        else if (normalize_Vector_Z <= 0 && isrunning) {
            isrunning = false;
            velocity = WALK;
        }

        if (boostcrouched)
        {
            velocityBOOSTCROUCH -= Time.deltaTime * 7f;

            Vector3 move = transform.right * normalize_Vector_X + transform.forward * normalize_Vector_Z;
            movementController.Move(move * (statMove + velocityBOOSTCROUCH) * Time.deltaTime);//Movimento Objeto

            if (velocityBOOSTCROUCH < CROUCH || normalize_Vector_X == 0 && normalize_Vector_Z == 0)
            {
                if (iscrouched)
                {
                    velocity = CROUCH;
                    boostcrouched = false;
                    isrunning = false;
                }
            }
        }
        else 
        {
            Vector3 move = transform.right * normalize_Vector_X + transform.forward * normalize_Vector_Z;
            movementController.Move(move * (statMove + velocity) * Time.deltaTime);//Movimento Objeto
        }


        if (isGrounded && salto)
        {
            pjGravedad.y = Mathf.Sqrt(saltoAltura * -2f * gravedad);
            salto = false;
            saltoAudio.Play();
        }

        pjGravedad.y += gravedad * Time.deltaTime;
        movementController.Move(pjGravedad * Time.deltaTime);//Gravedad del Objeto

        movementAnimation(normalize_Vector_X, normalize_Vector_Z);//Animacion
    }

    private void movementAnimation(float moveX, float moveZ) {
        if (salto || !isGrounded) {
            pasosAudio.Stop();
            anim.SetInteger("movimiento", 6);//Salto
            anim.SetInteger("moveBrazo", 3);
            return;
        }

        if (moveX == 0 && moveZ == 0)
        {
            pasosAudio.Stop();
            if (velocity == CROUCH) {
                anim.SetInteger("movimiento", 5);//Agachar
                anim.SetInteger("moveBrazo", 0);//Quieto
                return;
            }

            anim.SetInteger("movimiento", 0);//Quieto
            anim.SetInteger("moveBrazo", 0);
            return;
        }

        if (!pasosAudio.isPlaying) pasosAudio.Play();

        switch (velocity)
        {
            case WALK:
                anim.SetInteger("movimiento", 1);//Andar
                anim.SetInteger("moveBrazo", 1);
                pasosAudio.pitch = 0.8f;
                return;
            case CROUCH:
                anim.SetInteger("movimiento", 2);//Andar agachado
                anim.SetInteger("moveBrazo", 1);
                pasosAudio.pitch = 0.7f;
                return;
            case RUN:
                anim.SetInteger("movimiento", 3);//Correr
                anim.SetInteger("moveBrazo", 2);
                pasosAudio.pitch = 1f;
                return;
            case BOOSTCROUCH:
                anim.SetInteger("movimiento", 4);//Deslizamiento
                anim.SetInteger("moveBrazo", 4);
                pasosAudio.pitch = 0.3f;
                return;
            default:
                return;
        }
    }

    public void crouched()
    {
        if (!iscrouched)
        {
            if (isrunning)
            {
                velocity = BOOSTCROUCH;
                velocityBOOSTCROUCH = BOOSTCROUCH;
                boostcrouched = true;
            }
            else
            {
                velocity = CROUCH;
            }

            iscrouched = true;
        }
        else
        {
            if (isrunning)
            {
                velocity = RUN;
            }
            else {
                velocity = WALK;
            }
            
            iscrouched = false;
            boostcrouched = false;
        }
    }

    public void jump()
    {
        salto = true;
        iscrouched = false;
        boostcrouched = false;
        iscrouched = false;
        if (isrunning)
        {
            velocity = RUN;
        }
    }

    public void run()
    {
        if (Z_Vector == 1)
        {
            if (velocity == RUN)
            {
                isrunning = false;
                velocity = WALK;
            }
            else
            {
                if (iscrouched || boostcrouched)
                {
                    iscrouched = false;
                    iscrouched = false;
                    boostcrouched = false;

                }

                velocity = RUN;
                isrunning = true;
            }
        }
    }

    public void left(int i)
    {
        if (X_Vector == 1 && i == 0) return;

        if (X_Vector == 1 && i == -1)
        {
            X_Vector = 0;
            return;
        }

        if (X_Vector == 0 && i == 0)
        {
            X_Vector = 1;
            return;
        }

        X_Vector = i;
    }

    public void right(int i)
    {

        if (X_Vector == -1 && i == 0) return;

        if (X_Vector == -1 && i == 1)
        {
            X_Vector = 0;
            return;
        }

        if (X_Vector == 0 && i == 0)
        {
            X_Vector = -1;
            return;
        }

        X_Vector = i;
    }

    public void up(int i)
    {
        if (Z_Vector == -1 && i == 0) return;

        if (Z_Vector == -1 && i == 1)
        {
            Z_Vector = 0;
            return;
        }

        if (Z_Vector == 0 && i == 0)
        {
            Z_Vector = -1;
            return;
        }

        Z_Vector = i;
    }

    public void down(int i)
    {
        if (Z_Vector == 1 && i == 0) return;

        if (Z_Vector == 1 && i == -1)
        {
            Z_Vector = 0;
            return;
        }

        if (Z_Vector == 0 && i == 0)
        {
            Z_Vector = 1;
            return;
        }

        Z_Vector = i;
    }

    public void setStatVelocity(float statMove) {
        this.statMove += statMove;
        if (this.statMove > 1f) this.statMove = 1f;
    }

    public void cambiarEstado(bool estado)
    {
        isPlaying = estado;
    }

    public float getVelocity() {
        return velocity;
    }

    public bool running() {
        return isrunning;
    }

    public void reset()
    {
        statMove = 0f;
    }
}
