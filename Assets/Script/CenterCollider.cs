using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _back;
    [SerializeField]
    private BackGroundScroll[] _backType;

    [SerializeField]
    private float _distance = 5f;

    private void Start()
    {
        for (int i = 0; i < _back.Length; i++)
            _backType[i]=_back[i].GetComponent<BackGroundScroll>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _back[0].transform.position = new Vector2(transform.position.x, _back[1].transform.position.y + _distance);
            _back[2].transform.position = new Vector2(transform.position.x, _back[1].transform.position.y - _distance);
        }
        ////if(collision.gameObject.tag == "Player" && _backType[1]._underCollision[2] == true)
        //if(collision.gameObject.tag == "Player")
        //{
        //    Debug.Log("down機能はしている");
        //    _back[2].transform.position = new Vector2(transform.position.x, _back[0].transform.position.y+_distance);
        //}

        ////if (collision.gameObject.tag == "Player" && _backType[1]._underCollision[2] == false && _backType[0]._underCollision[1] == true)
        //if (collision.gameObject.tag == "Player")
        //    {
        //    Debug.Log("down機能はしている2");
        //    _back[1].transform.position = new Vector2(transform.position.x, _back[2].transform.position.y + _distance);
        //}

    }
}

