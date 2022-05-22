using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperCollider : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _back;
    [SerializeField]
    private BackGroundScroll[] _backType;

    [SerializeField]
    private float _distance = 10f;

    private void Start()
    {
        for (int i = 0; i < _back.Length; i++)
            _backType[i] = _back[i].GetComponent<BackGroundScroll>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _back[2].transform.position = new Vector2(transform.position.x, _back[0].transform.position.y + _distance);
            _back[1].transform.position = new Vector2(transform.position.x, _back[0].transform.position.y - _distance);
        }
        //for (int i = 0; i < _back.Length; i++)
        //    if (collision.gameObject.tag == "Player" &&  _backType[i]._underCollision==false)
        //        //_backType[i]._backType == BackGroundScroll.BackType.center &&
        //    {
        //        Debug.Log("機能はしている");
        //        _back[2].transform.position = new Vector2(transform.position.x, transform.position.y /_distance);
        //    }
        //if (collision.gameObject.tag == "Player" && _backType[1]._underCollision[2] == false)
        //{
        //    Debug.Log("up機能はしている");
        //    _back[2].transform.position = new Vector2(transform.position.x, _back[1].transform.position.y - _distance);
        //}

        //if (collision.gameObject.tag == "Player" && _backType[1]._underCollision[2] == false)
        //{
        //    Debug.Log("up機能はしている");
        //    _back[2].transform.position = new Vector2(transform.position.x, _back[1].transform.position.y - _distance);
        //}

    }
}
