using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale = 4.5f;
    public float rotateSpeed = 5f;
    //private bool canDoubleJump;

    private Vector3 _moveDirection;

    public CharacterController charController;
    public Camera playerCamera;
    public GameObject playerModel;

    public Animator animator;
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Grounded = Animator.StringToHash("Grounded");

    public bool isKnocking;
    public float knockBackLenght = .5f;
    private float _knockBackCounter;
    public Vector2 knockBackPower;

    public GameObject[] playerPieces;
    private void Awake()
    {
        instance = this;
    }

    /*void Start()
    {
        playerCamera = Camera.main; >> Forma 1 de ajustar la rotacion, haciendo de la variable playerCamera Privada en ves de publica
    }*/
    
    void Update()
    {
        if (!isKnocking)
        {
            float yStore = _moveDirection.y;
            //Movimiento
            //_moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            _moveDirection = (transform.forward * Input.GetAxisRaw("Vertical")) +
                             (transform.right * Input.GetAxisRaw("Horizontal"));
            _moveDirection.Normalize();
            _moveDirection = _moveDirection * moveSpeed;
            _moveDirection.y = yStore;

            //Salto
            if (charController.isGrounded)
            {
                _moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    _moveDirection.y = jumpForce;
                }
            }
            //Caida
            _moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            // Fluidez de movimiento
            charController.Move(_moveDirection * Time.deltaTime);
            //Rotacion
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0f, playerCamera.transform.rotation.eulerAngles.y, 0f);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(_moveDirection.x, 0f, _moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }
        }

        if (isKnocking)
        {
            _knockBackCounter -= Time.deltaTime;
            
            float yStore = _moveDirection.y;
            _moveDirection = (playerModel.transform.forward * knockBackPower.x);
            _moveDirection.y = yStore;
            
            _moveDirection.y += Physics.gravity.y * Time.deltaTime * gravityScale;
            
            charController.Move(_moveDirection * Time.deltaTime);

            if (_knockBackCounter <= 0)
            {
                isKnocking = false;
            }
        }
        //Animacion de movimiento y salto
        animator.SetFloat(Speed, Mathf.Abs(_moveDirection.x) + Mathf.Abs(_moveDirection.z));
        animator.SetBool(Grounded, charController.isGrounded);
    }

    public void Knockback()
    {
        isKnocking = true;
        _knockBackCounter = knockBackLenght;
        _moveDirection.y = knockBackPower.y;
        charController.Move(_moveDirection * Time.deltaTime);
    }
}
