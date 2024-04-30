using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    public GameObject button;
    public GameObject ban;
    public GameObject nextPlayer;
    public GameObject nextWin;
    public GameObject nextBan;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            button.SetActive(true);
            nextPlayer.SetActive(true);
            if(ban != null)
            {
                ban.SetActive(false);
            }
            if(nextBan != null)
            {
                nextBan.SetActive(true);
            }
            nextWin.SetActive(true);
            gameObject.SetActive(false);
        }
    }

}
