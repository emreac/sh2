using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public SpriteRenderer hackingArea;

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
        
    }
    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Building")) // Assuming the player is the object triggering the event
        {
           // Debug.Log("building found!");
            // Set the GameObject to inactive
            lineRenderer.gameObject.SetActive(false);
            Color color = hackingArea.color;
            color.a = 1f;
            hackingArea.color = color;
        }    
    }
    private void OnTriggerExit(Collider other)
    {
        Color color = hackingArea.color;
        color.a = 0.3f;
        hackingArea.color = color;
        lineRenderer.gameObject.SetActive(true);
    }
}
