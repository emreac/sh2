using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG;
using DG.Tweening;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SocialPlatforms.Impl;

public class BuildingManager : MonoBehaviour
{
    //Score
    public float score;

    //TS
    public bool isUserCompleteLevel;

    public bool hacked;
    public LineRenderer lineRenderer;
    public SpriteRenderer hackingArea;
    public Animator playerAnimator;
    public PlayerController playerController;
    public GameObject macComp;
    public Animator macAnimator;
    public float hackingSpeed;
    public AudioSource computerSound;
    public AudioSource winSound;
    public AudioSource gameMusic;
    public AudioSource startSound;

   
    public AudioSource hackingProcess;
    public AudioSource hackDoneSound;
    public AudioSource gateSound;
    public Collider buildingCollider;
    public Collider gate1Collider;
    public Collider gate2Collider;
    public Animator gateLight1;
    public Animator gateLight2;
    

    public RandomSoundPlayer soundPlayer;

    //UI
    public Image countdownPieBuilding;
    public Image countdownPieGate1;
    public Image countdownPieGate2;

    public GameObject winUI;
    public GameObject loseUI;


    [Header("Settings")]
    [SerializeField] private float detectionRadius;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitStartSound());
        buildingCollider = GetComponent<Collider>();
        hackingArea = GetComponent<SpriteRenderer>();

        // Set the initial alpha to 1 (fully opaque)
        Color color = hackingArea.color;
        color.a = 0.3f;
        hackingArea.color = color;
    }

    // Update is called once per frame
    void Update()
    {
   
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, detectionRadius);
    }
    */
    public void DetectBuildings()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, detectionRadius);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Building")) // Assuming the player is the object triggering the event
        {
           // Debug.Log("building found!");
            // Set the GameObject to inactive
            lineRenderer.gameObject.SetActive(false);
            Color color = hackingArea.color;
            color.a = 0.8f;
            hackingArea.color = color;
            
            playerAnimator.SetBool("isHacking", true);
            macComp.SetActive(true);
            macAnimator.SetBool("macOpen", true);
            HackingCountDown(hackingSpeed*Time.deltaTime);

        }

        if (other.CompareTag("GateBox"))
        {
           
            Color color = hackingArea.color;
            color.a = 0.8f;
            hackingArea.color = color;

            playerAnimator.SetBool("isHacking", true);
            macComp.SetActive(true);
            macAnimator.SetBool("macOpen", true);
            HackingCountDownGate1(hackingSpeed * Time.deltaTime);
            
        }

        if (other.CompareTag("GateBox2"))
        {
            
            Color color = hackingArea.color;
            color.a = 0.8f;
            hackingArea.color = color;

            playerAnimator.SetBool("isHacking", true);
            macComp.SetActive(true);
            macAnimator.SetBool("macOpen", true);
            HackingCountDownGate2(hackingSpeed * Time.deltaTime);

        }

    }



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Building" || other.gameObject.tag == "GateBox" || other.gameObject.tag=="GateBox2")
        {
            soundPlayer.PlayRandomSound();
            computerSound.Play();
            hackingProcess.Play();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        soundPlayer.enabled = false;
        computerSound.Stop();
        hackingProcess.Stop();
        Color color = hackingArea.color;
        color.a = 0.3f;
        hackingArea.color = color;
        lineRenderer.gameObject.SetActive(true);
        playerAnimator.SetBool("isHacking", false);
        macComp.SetActive(false);
    }

    public void HackingCountDown(float value)
    {
        countdownPieBuilding.fillAmount += value;
        if(countdownPieBuilding.fillAmount >= 1)
        {
            SetAsHacked();
            if (buildingCollider != null)
            {
                buildingCollider.enabled = false;
                //Debug.Log("Collider disabled.");
            }
        }
   
       
    }

    public void HackingCountDownGate1(float value)
    {
        countdownPieGate1.fillAmount += value;
        if (countdownPieGate1.fillAmount >= 1)
        {
            SetAsGateHacked();
            if (gate1Collider != null)
            {
                gate1Collider.enabled = false;
                Debug.Log("Collider disabled.");
            }
        }
    }

    public void HackingCountDownGate2(float value)
    {
        countdownPieGate2.fillAmount += value;
        if (countdownPieGate2.fillAmount >= 1)
        {
            SetAsGate2Hacked();
            if (gate2Collider != null)
            {
                gate2Collider.enabled = false;
                Debug.Log("Collider disabled.");
            }
        }
    }
    public void SetAsHacked()
    {
        StartCoroutine(WaitStopGame());
        StartCoroutine(WaitWinSound());
        gameMusic.Stop();
        //Debug.Log("Building Hacked! > NEXT LEVEL");
        hacked = true;
        hackDoneSound.Play();
        hackingProcess.Stop();
        winUI.SetActive(true);
        // Disable the collider to prevent further interactions
      
    }

    public void SetAsGateHacked()
    {
        TinySauce.OnGameFinished(isUserCompleteLevel, score);
        gateLight1.SetBool("isGreen", true);
        gateSound.Play();
        DOTween.Play("Door1");
        DOTween.Play("Door2");

       // Debug.Log("Gate Hacked!");
        hacked = true;
        hackDoneSound.Play();
        hackingProcess.Stop();

    }
    public void SetAsGate2Hacked()
    {
        isUserCompleteLevel = true;
        gateSound.Play();
        gateLight2.SetBool("isGreen", true);
        //TS
        TinySauce.OnGameFinished(isUserCompleteLevel, score);

        DOTween.Play("Door3");
        DOTween.Play("Door4");
       // Debug.Log("Gate Hacked!");
        hacked = true;
        hackDoneSound.Play();
        hackingProcess.Stop();

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {

            SceneManager.LoadScene("Level1");
        }
    }

    IEnumerator WaitWinSound()
    {
        yield return new WaitForSeconds(0.5f);
        winSound.Play();
    }
    IEnumerator WaitStartSound()
    {
        yield return new WaitForSeconds(0.3f);
        startSound.Play();
    }
    IEnumerator WaitStopGame()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0f;
    }
}
