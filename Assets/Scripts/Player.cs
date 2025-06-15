using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float inputH;
    private float inputV;
    private bool moving;
    private Vector3 destinyPoint;
    private Vector3 lastInput;
    private Vector3 interactionPoint;
    private Collider2D frontCollider; // Indica el collider que tenemos por delante
    private Animator anim;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float interactionRadius;

    private bool interacting;

    public bool Interacting { get => interacting; set => interacting = value; }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = GameManager.Instance.lastSavedPosition;
        anim.SetFloat("inputH", GameManager.Instance.lastSavedRotation.x);
        anim.SetFloat("inputV", GameManager.Instance.lastSavedRotation.y);
    }

    // Update is called once per frame
    void Update()
    {
        ReadInputs();

        MovementAndAnimations();
    }

    private void MovementAndAnimations()
    {
        // Ejecuto movimiento solo si estoy en una casilla y solo si hay input
        if (!interacting && !moving && (inputH != 0 || inputV != 0))
        {
            anim.SetBool("walking", true);
            anim.SetFloat("inputH", inputH);
            anim.SetFloat("inputV", inputV);
            // Actualizo cuál fue mi último input, cuál va a ser mi punto de destino y cuál es mi punto de interacción
            lastInput = new Vector3(inputH, inputV, 0);
            destinyPoint = transform.position + lastInput;
            interactionPoint = destinyPoint;

            frontCollider = ThrowCheck();

            if (!frontCollider)
            {
                StartCoroutine(Move());
            }
        }
        else if (inputH == 0 && inputV == 0)
        {
            anim.SetBool("walking", false);
        }
    }

    private void ReadInputs()
    {
        if (inputV == 0)
        {
            inputH = Input.GetAxisRaw("Horizontal");
        }

        if (inputH == 0)
        {
            inputV = Input.GetAxisRaw("Vertical");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ThrowInteraction();
        }
    }

    IEnumerator Move()
    {
        moving = true;
        while (transform.position != destinyPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinyPoint, movementSpeed * Time.deltaTime);
            yield return null;
        }
        // Ante un nuevo destino, necesito refrescar de nuevo el punto de interacción
        interactionPoint = transform.position + lastInput;
        moving = false;
    }

    private Collider2D ThrowCheck()
    {
        return Physics2D.OverlapCircle(interactionPoint, interactionRadius);
    }

    private void ThrowInteraction()
    {
        frontCollider = ThrowCheck();
        if (frontCollider != null)
        {
            if (frontCollider.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact();
            }  
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(interactionPoint, interactionRadius);
    }
}
