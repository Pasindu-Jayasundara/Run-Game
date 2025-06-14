using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class CharacterMovement : MonoBehaviour
{

    private Animator animationController;
    private Transform playerTransform;
    private Rigidbody playerRigidbody;

    private float runSpeed = 0.1f;
    private float laneChangeSpeed = 5f;
    private float safeDistance = 5f;
    private float jumpForce = 6f;

    private bool isJumping = false;
    private bool isPaused = false;

    public TextMeshProUGUI coinCount;
    public TextMeshProUGUI wonCoinCount;
    public GameObject pauseBtn;
    public GameObject playPausePanel;
    public GameObject goToMenuPanel;

    private int collectedCoinCount;

    void Start()
    {
        Time.timeScale = 1;

        animationController = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        playerRigidbody = GetComponent<Rigidbody>();

        animationController.SetBool("Breath", false);
        animationController.SetBool("Run", true);
    }

    void FixedUpdate()
    {
        if (!isPaused)
        {

            // Reset jump state when grounded (you may want to implement a ground check)
            if (playerRigidbody.velocity.y <= 0)

            {
                isJumping = false;
                animationController.SetBool("Run", true);
                animationController.SetBool("Jump", false);
            }

            float move = Input.GetAxis("Horizontal");

            if (playerTransform.position.x > -safeDistance && playerTransform.position.x < safeDistance)
            {
                playerTransform.position += new Vector3(move * laneChangeSpeed, 0, 1) * runSpeed;
            }
            else
            {
                playerTransform.position += new Vector3(0, 0, 1) * runSpeed;
            }


            // Jumping
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                FindObjectOfType<AudioManager>().JumpAudio();

                isJumping = true;
                animationController.SetBool("Run", false);
                animationController.SetBool("Jump", true);

                playerRigidbody.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Gold Coin"))
        {
            FindObjectOfType<AudioManager>().CoinCollectAudio();

            Destroy(other.gameObject);

            string coinsStr = coinCount.GetParsedText();
            if (int.TryParse(coinsStr, out collectedCoinCount))
            {
                collectedCoinCount++;
            }
            else
            {
                collectedCoinCount = 0; 
            }
            coinCount.SetText(collectedCoinCount.ToString());

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            FindObjectOfType<AudioManager>().PlayGameEndAudio();

            Time.timeScale = 0;
            pauseBtn.SetActive(false);
            goToMenuPanel.SetActive(true);
            wonCoinCount.SetText("Won: "+collectedCoinCount.ToString()+" coins");
        }
    }

    public void PauseGame()
    {
        isPaused = true;

        FindObjectOfType<AudioManager>().PauseAudio(); 

        playPausePanel.SetActive(true);
        pauseBtn.SetActive(false);

        animationController.SetBool("Run", false);
        animationController.SetBool("Breath", true);

    }

    public void PlayGame()
    {
        isPaused = false;

        FindObjectOfType<AudioManager>().ResumeAudio();

        playPausePanel.SetActive(false);
        pauseBtn.SetActive(true);

        animationController.SetBool("Run", true);
        animationController.SetBool("Breath", false);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
