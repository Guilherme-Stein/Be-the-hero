using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocaDePonto : MonoBehaviour
{
    //Player
    private Collider playerCollider;



    private float linhaAtual;
    public float velocidadeLinha;
    private Vector3 posicaoAlvoVertical;

    [SerializeField]
    private float deadZone = 100.0f;

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private Vector2 swipeDelta, startTouch;
    private bool isDraging = false;
    private float sqrDeadZone;

    #region public properties
    public bool Tap {get {return tap;}} 
    public bool SwipeLeft {get {return swipeLeft;}} 
    public bool SwipeRight {get {return swipeRight;}} 
    public bool SwipeUp {get {return swipeUp;}} 
    public bool SwipeDown {get {return swipeDown;}} 
    public Vector2 SwipeDelta {get {return swipeDelta;}} 
    #endregion

    private void Start(){
        sqrDeadZone = deadZone * deadZone;
        linhaAtual = transform.position.x;
        posicaoAlvoVertical = new Vector3(transform.position.x, transform.position.y, 6.5f);

        //Collider do player caindo
        playerCollider = GetComponent<Collider>();

    }

    private void Reset() {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
    private void Update() {
        tap = swipeLeft = swipeRight = false;

        #region Standlone Input
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            tap = true;
            startTouch = Input.mousePosition;
        }else if (Input.GetMouseButtonUp(0))
        {
            isDraging = true;
            Reset();
        }
        #endregion
        #region  Mobile Input
        if (Input.touches.Length > 0)
        {
           if (Input.touches[0].phase == TouchPhase.Began)
           {               
               isDraging = true;
               tap = true;
               startTouch = Input.touches[0].position;
           }else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
           {
                isDraging = false;
               Reset();
           }
        }
        #endregion

        swipeDelta = Vector2.zero;
        if (isDraging)
        {
            if (Input.touches.Length > 0){
                swipeDelta = Input.touches[0].position - startTouch;
            }else if(Input.GetMouseButton(0)){
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        if (swipeDelta.magnitude > 125)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;
            if (Mathf.Abs(x) > Mathf.Abs(y)){
                if(x < 0){
                    swipeLeft = true;
                    print("Esquerda");
                }else{
                    swipeRight = true;
                    print("Direita");
                }
                
            }else{
                if(y < 0){
                    swipeDown = true;
                    print("Baixo");
                }else{
                    swipeUp = true;
                    print("Cima");
                }
            }
            Reset();
        }

        if(swipeLeft){
            trocaDeLinha(-3.0000f);
        }else if(swipeRight){
            trocaDeLinha(3.0000f);
        }
                
    }

   private void FixedUpdate() {
       Vector3 posicaoAlvo = new Vector3(posicaoAlvoVertical.x, 
        transform.position.y, 
        posicaoAlvoVertical.z);
        
        transform.position = Vector3.MoveTowards(new Vector3(transform.position.x, 
        transform.position.y, 6.5f), 
        posicaoAlvo, 
        velocidadeLinha * Time.deltaTime);
   }


    void trocaDeLinha(float direcao){
        float linhaAlvo = linhaAtual + direcao;
        if (linhaAlvo < -5 || linhaAlvo > 5){
            return;
        }
        linhaAtual = linhaAlvo;
        posicaoAlvoVertical = new Vector3(linhaAtual, 0.0f, 6.5f);
    }

    private void OnTriggerEnter(Collider trigger) {
        if (trigger.gameObject.CompareTag("bonus")){
            mapGeneration.moedas++;
            FindObjectOfType<SoundManager>().Play("moeda");
            Destroy(trigger.gameObject);
        }

        if(trigger.gameObject.CompareTag("ObsInterior")){
            StartCoroutine(colisao(0.5f));
            PlayerMovement.Damage = true;;
        }

       

    }

    IEnumerator colisao(float esperar){
        playerCollider.enabled = false;
        yield return new WaitForSeconds(esperar);
        playerCollider.enabled = true;

    }

    

    
}
