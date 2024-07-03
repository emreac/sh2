using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public AudioSource failedSound;
    public GameObject loseUI;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("game over!");
            failedSound.Play();
            Time.timeScale = 0f;
            loseUI.SetActive(true);
        }
    }
   
  
}
