using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject lillyPad = null;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnLillyPad", 2f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //SpawnFunction
    private void SpawnLillyPad()
    {
        Instantiate(lillyPad);
    }
}
