using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandamTest : MonoBehaviour
{
    public GameObject[] itemPrefabs;

    
    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, itemPrefabs.Length);
        Debug.Log(index);

        GameObject item = Instantiate(itemPrefabs[index]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
