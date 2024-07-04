using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour
{
    [SerializeField] int AmountOfEnemy;
    [SerializeField] int NextLevel;
    // Start is called before the first frame update
    void Start()
    {
        AmountOfEnemy = GameObject.FindGameObjectsWithTag("Enemy").Length;
    }

    public void DeadEnemy ()
    {
        AmountOfEnemy--;

        if(AmountOfEnemy == 0)
        {
            Application.LoadLevel(NextLevel);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
