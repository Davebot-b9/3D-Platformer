using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector3 _respawnPosition;
    public GameObject deathEffect;
    public GameObject pickupEffect;
    public int currentCoins;
    public int soundToPlay;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _respawnPosition = PlayerController.instance.transform.position;
        
        AddCoins(0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpase();
        }
    }

    public void Respawn()
    {
        StartCoroutine(RespawnWaiter());
        HealthManager.instance.PlayerKIlled();
        AudioManager.instance.PlaySfx(soundToPlay);
    }

    private IEnumerator RespawnWaiter()
    {
        //Desaparece el Personaje al caer al Killzone
        PlayerController.instance.gameObject.SetActive(false);
        //DESACTIVA EL CONTROL DE LA CAMARA
        CameraController.instance.cmBrain.enabled = false;
        //Pantalla negra aparece
        UIManager.instance.fadeToBlack = true;
        //Instanciamos el efecto de muerte
        var transform1 = PlayerController.instance.transform;
        Instantiate(deathEffect, transform1.position + new Vector3(0f, 1f, 0f),
            transform1.rotation);
        //Esperamos 2 segundos antes de reaparecer
        yield return new WaitForSeconds(2f);
        //Pantalla negra desaparece
        UIManager.instance.fadeFromBlack = true;
        //Reaparecemos despues de 2 segundos en la posición inicial
        PlayerController.instance.transform.position = _respawnPosition;
        //ACTIVA EL CONTROL DE LA CAMARA
        CameraController.instance.cmBrain.enabled = true;
        //El jugador se ve visible
        PlayerController.instance.gameObject.SetActive(true);
        HealthManager.instance.ResetHealth();
    }

    public void SetSpawnPoint(Vector3 newSpawnPoint)
    {
        _respawnPosition = newSpawnPoint;
    }

    public void AddCoins(int coinsToAdd)
    {
        currentCoins += coinsToAdd;
        UIManager.instance.coinText.text = "" + currentCoins;
    }

    public void PauseUnpase()
    {
        if (UIManager.instance.pauseScreen.activeInHierarchy)
        {
            UIManager.instance.pauseScreen.SetActive(false);
            Time.timeScale = 1f;
            
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UIManager.instance.pauseScreen.SetActive(true);
            UIManager.instance.CloseOptions();
            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
