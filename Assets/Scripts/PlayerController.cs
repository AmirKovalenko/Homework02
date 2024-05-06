using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

[RequireComponent(typeof(NPCController))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float jumpForce;
    [SerializeField] GameObject fireHazard;
    [SerializeField] GameObject waterHazard;
    [SerializeField] private Rigidbody playerBody;
    [SerializeField] public uint currentHP;
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private AudioSource trapAudio;
    [SerializeField] private AudioSource buttonPress;

    public Animation fallAnimation;
    public Animation fallRecoverAnimation;
    public Animator animator;
    public bool areControlsLocked;

    public uint CurrentHP { get => currentHP; }
    private uint maxHP = 30;
    private float immuneTimer;
    private float jumpCooldown = 0;
    private float rotationSpeed = 180;
    private float deathTimer;
    private bool pauseCheck;
    private bool onGround;
    public AmirSceneManager nextScene;
    private NPCController npcController;

    public void Awake()
    {
        playerBody = GetComponent<Rigidbody>();
        currentHP = maxHP;
        pauseCheck = false;
        areControlsLocked = false;
        onGround = true;
        deathTimer = 2.6f;
    }

    private void Update()
    {
        if (immuneTimer > 0)
            immuneTimer -= Time.deltaTime;
        if (jumpCooldown > 0)
            jumpCooldown -= Time.deltaTime;


        if (!playerBody.IsDestroyed())
        {
            if (!areControlsLocked)
            {
                animator.speed = 0.85f;
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

                if (Input.GetKeyDown(KeyCode.Space))
                    Jump();
            }
            if (Input.GetKeyDown(KeyCode.Escape))
                PauseMenu();

            if (currentHP <= 0)
            {
                areControlsLocked = true;
                animator.SetBool("IsFalling", true);
                deathTimer -= Time.deltaTime;
                if (deathTimer <= 0)
                {
                    if (nextScene.sceneCounter == 2)
                    {
                        SceneManager.LoadSceneAsync("LoseScene 1");
                        nextScene.LoseScene();
                    }
                    else if (nextScene.sceneCounter == 5)
                    {
                        SceneManager.LoadSceneAsync("LoseScene 2");
                        nextScene.LoseScene();
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Portal"))   //moves to win scene
        {
            if (nextScene.sceneCounter == 2)
            {
                SceneManager.LoadSceneAsync("WinScene 1");
                nextScene.WinScene();
                nextScene.sceneCounter = 5;

            }
            else if (nextScene.sceneCounter == 5)
            {
                SceneManager.LoadSceneAsync("WinScene 2");
                nextScene.WinScene();
                nextScene.sceneCounter = 7;

            }
        }

        else if (other.gameObject.CompareTag("NPC"))  //moves to lose scene
        {
            if (nextScene.sceneCounter == 2)
            {
                SceneManager.LoadSceneAsync("LoseScene 1");
                nextScene.LoseScene();
                nextScene.sceneCounter = 2;
            }
            else if (nextScene.sceneCounter == 5)
            {
                SceneManager.LoadSceneAsync("LoseScene 2");
                nextScene.LoseScene();
                nextScene.sceneCounter = 5;
            }
        }

        else if (other.gameObject.CompareTag("Trap"))  //sets the NPC's to move to your direction
        {
            NPCController.SetTarget(transform);
            trapAudio.Play();
            Debug.Log("Trap Activated!");
        }

        else if (other.gameObject.CompareTag("Water"))
        {
            if (immuneTimer <= 0)
            {
                Debug.Log(message: "entered water hazard!");
                animator.SetBool("IsFalling", true);
                animator.speed = 1.4f;
                areControlsLocked = true;
            }
        }
    }

    public void TakeDamage(uint damage)
    {
        currentHP -= damage;
        uiManager.hpText.text = currentHP.ToString();
        if (currentHP <= 0)
            playerBody.IsDestroyed();
    }
    public void TakeDamage(FireEnteredEventArgs fireData)
    {
        currentHP -= fireData.damageDealt;
        uiManager.hpText.text = currentHP.ToString();
        if (currentHP <= 0)
            playerBody.IsDestroyed();
    }    
    public void WaterEnter(WaterEnteredEventArgs waterData)
    {
        currentHP -= waterData.damageDealt;
        uiManager.hpText.text = currentHP.ToString();
        if (currentHP <= 0)
            playerBody.IsDestroyed();
    }
    public void BeginRecover()
    {
        animator.SetTrigger("IsRecovering");
    }
    public void EndRecovery()
    {
        animator.SetTrigger("CanWalk");
        animator.SetBool("IsFalling", false);

        areControlsLocked = false;
        animator.speed = 1;
        immuneTimer = 5;
    }
    public void Jump()
    {
        if (onGround)
        {
            playerBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            onGround = false;
            jumpCooldown = 0.8f;                
        }
        else
            if (jumpCooldown <= 0)
                onGround = true;
    }
    public void PauseMenu()
    {
        if (!pauseCheck)
        {
            areControlsLocked = true;
            pauseCheck = true;
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        }
        else if (pauseCheck)
        {
            areControlsLocked = false;
            pauseCheck = false;
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
        }   
    }
    public void SoundSettings()
    {
        soundMenu.SetActive(true);
        pauseMenu.SetActive(false);
        buttonPress.Play();
    }
    public void SoundSettingsExit()
    {
        soundMenu.SetActive(false);
        pauseMenu.SetActive(true);
        buttonPress.Play();
    }

    
}


