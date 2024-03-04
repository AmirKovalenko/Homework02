using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(NPCController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] private uint currentHP;
    [SerializeField] GameObject fireHazard;
    [SerializeField] GameObject waterHazard;

    public Animation fallAnimation;
    public Animation fallRecoverAnimation;
    public Animator Animator;
    public bool areControlsLocked;
    public float immuneTimer = 0;
    private bool isFalling;
    private bool onGround;
    private float rotationSpeed = 180;
    private uint maxHP = 100;

    public void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        currentHP = maxHP;
        areControlsLocked = false;
        isFalling = false;
    }

    private void Update()
    {
        if (immuneTimer > 0)
            immuneTimer -= Time.deltaTime;
        if (!playerBody.IsDestroyed())
        {
            if (!areControlsLocked)
            {
                float horizontalInput = Input.GetAxis("Horizontal");
                float verticalInput = Input.GetAxis("Vertical");
                Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
                movementDirection.Normalize();
                transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
                transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
                transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);

                if (movementDirection != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
                }

                if (Input.GetKeyDown(KeyCode.Space) && onGround)
                {
                    playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                    onGround = false;
                }
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Water"))
        {
            if (immuneTimer <= 0)
            {
                Debug.Log(message: "entered water hazard!");
                Animator.SetBool("IsFalling", true);
                Animator.speed = 1.3f;
                isFalling = true;
                areControlsLocked = true;
            }
        }
    }

    public void TakeDamage(FireEnteredEventArgs fireData)
    {
        currentHP -= fireData.damageDealt;
        if (currentHP <= 0)
            playerBody.IsDestroyed();
    }    

    public void TakeDamage(uint damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
            playerBody.IsDestroyed();
    }

    public void WaterEnter(WaterEnteredEventArgs waterData)
    {
        currentHP -= waterData.damageDealt;
        if (currentHP <= 0)
            playerBody.IsDestroyed();
    }

    //public void TakeDamage(uint damage)
    //{
    //    currentHP -= damage;
    //    if (currentHP <= 0)
    //        playerBody.IsDestroyed();
    //}

    public void BeginRecover()
    {
        Animator.SetTrigger("IsRecovering");
    }

    public void EndRecovery()
    {
        Animator.SetTrigger("CanWalk");
        Animator.SetBool("IsFalling", false);

        isFalling = false;
        areControlsLocked = false;
        Animator.speed = 1;
        immuneTimer = 5;
    }
}
