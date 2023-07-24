using System;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject cpOn, cpOff;

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
        AudioManager.instance.PlaySfx(soundToPlay);
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetSpawnPoint(transform.position);
    
            Checkpoint[] allCp = FindObjectsOfType<Checkpoint>();
            foreach (var t in allCp)
            {
                t.cpOff.SetActive(true);
                t.cpOn.SetActive(false);
            }
            cpOff.SetActive(false);
            cpOn.SetActive(true);
        }
    }
}
