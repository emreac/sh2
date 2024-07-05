using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public AudioSource failedSound;
    public GameObject loseUI;
    public BuildingManager buildingManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            buildingManager.isUserCompleteLevel = false;
            TinySauce.OnGameFinished(buildingManager.isUserCompleteLevel, buildingManager.score);

            Debug.Log("game over!");
            failedSound.Play();
            Time.timeScale = 0f;
            loseUI.SetActive(true);
        }
    }
   
  
}
