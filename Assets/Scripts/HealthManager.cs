using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int currentHealth, maxHealth;

    public float invincibleLength = 2f;
    private float _invinciCounter;

    public Sprite[] healthBarImages;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetHealth();
    }
    
    void Update()
    {
        if (_invinciCounter > 0)
        {
            // se hace invencible por un tiempo establecido al recibir daño
            _invinciCounter -= Time.deltaTime;
            // desaparecer al personaje al recibir daño
            for (int i = 0; i < PlayerController.instance.playerPieces.Length; i++)
            {
                if (Mathf.Floor(_invinciCounter * 5f) % 2 == 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
                else
                {
                    PlayerController.instance.playerPieces[i].SetActive(false);
                }
                //Reaparece el personaje despues de un tiempo establecido
                if (_invinciCounter <= 0)
                {
                    PlayerController.instance.playerPieces[i].SetActive(true);
                }
            }

        }
    }

    public void Hurt()
    {

        if (_invinciCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                GameManager.instance.Respawn();
            }
            else
            {
                PlayerController.instance.Knockback();
                _invinciCounter = invincibleLength;
            }
        }
        UpdateUI();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UIManager.instance.healthImage.enabled = true;
        UpdateUI();
    }

    public void AddHealth(int amountToHeal)
    {
        currentHealth += amountToHeal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        UIManager.instance.healthText.text = currentHealth.ToString();
        switch (currentHealth)
        {
            case 5:
                UIManager.instance.healthImage.sprite = healthBarImages[4];
                break;
            case 4:
                UIManager.instance.healthImage.sprite = healthBarImages[3];
                break;
            case 3:
                UIManager.instance.healthImage.sprite = healthBarImages[2];
                break;
            case 2:
                UIManager.instance.healthImage.sprite = healthBarImages[1];
                break;
            case 1:
                UIManager.instance.healthImage.sprite = healthBarImages[0];
                break;
            case 0:
                UIManager.instance.healthImage.enabled = false;
                break;
        }
    }

    public void PlayerKIlled()
    {
        currentHealth = 0;
        UpdateUI();
    }
}
