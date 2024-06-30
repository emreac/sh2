using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpriteRenderer hackingArea;
    public Animator playerAnimator;
    public PlayerController playerController;
    public GameObject macComp;
    public Animator macAnimator;
    public float hackingSpeed;

    //UI
    public Image countdownPie;


    [Header("Settings")]
    [SerializeField] private float detectionRadius;
    // Start is called before the first frame update
    void Start()
    {
        hackingArea = GetComponent<SpriteRenderer>();

        // Set the initial alpha to 1 (fully opaque)
        Color color = hackingArea.color;
        color.a = 0.3f;
        hackingArea.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        DetectBuildings();
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
    private void OnTriggerExit(Collider other)
    {
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
        }
    }
    public void SetAsHacked()
    {
        Debug.Log("Building Hacked! > NEXT LEVEL");

    }
}
