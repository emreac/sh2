using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public bool hacked;
    public LineRenderer lineRenderer;
    public SpriteRenderer hackingArea;
    public Animator playerAnimator;
    public PlayerController playerController;
    public GameObject macComp;
    public Animator macAnimator;
    public float hackingSpeed;
    public AudioSource computerSound;
   
    public AudioSource hackingProcess;
    public AudioSource hackDoneSound;
    public Collider buildingCollider;
    

    public RandomSoundPlayer soundPlayer;

    //UI
    public Image countdownPie;


    [Header("Settings")]
    [SerializeField] private float detectionRadius;
    // Start is called before the first frame update
    void Start()
    {

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
 

    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Building")
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
        countdownPie.fillAmount += value;
        if(countdownPie.fillAmount >= 1)
        {
            SetAsHacked();
            if (buildingCollider != null)
            {
                buildingCollider.enabled = false;
                Debug.Log("Collider disabled.");
            }
        }
       
    }
    public void SetAsHacked()
    {
        Debug.Log("Building Hacked! > NEXT LEVEL");
        hacked = true;
        hackDoneSound.Play();
        hackingProcess.Stop();
       
        // Disable the collider to prevent further interactions
      
    }


}
