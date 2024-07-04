using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBox : MonoBehaviour
{
    [SerializeField] int AmountOfBullets;
    [SerializeField] GameObject Effect;
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
        if(other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().AddBullets(AmountOfBullets);
            Instantiate(Effect, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }
}
