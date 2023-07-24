using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public int soundToPlay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlaySfx(soundToPlay);
            HealthManager.instance.Hurt();
        }
    }
}
