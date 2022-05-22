using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderCollider : MonoBehaviour
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _back[1].transform.position = new Vector2(transform.position.x, _back[2].transform.position.y + _distance);
            _back[0].transform.position = new Vector2(transform.position.x, _back[2].transform.position.y - _distance);
        }
    }
}
