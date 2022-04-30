using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChecker : MonoBehaviour
{
    private MoveObject _moveObject;

    // Start is called before the first frame update
    void Start()
    {
        _moveObject = GetComponent<MoveObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetInitialSpeed() {
        _moveObject.moveSpeed = 0.005f;
    }
}
