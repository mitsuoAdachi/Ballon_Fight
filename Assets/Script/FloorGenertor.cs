using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenertor : MonoBehaviour
{
    [SerializeField]
    private GameObject _floorPrefab;

    [SerializeField]
    private Transform _generateTrans;

    [Header("生成までの待機時間")]
    public float _waitTime;

    private float _timer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _waitTime) {
            _timer = 0;

            GeneratorFloor();
        }
    }
    private void GeneratorFloor() {
        GameObject obj = Instantiate(_floorPrefab, _generateTrans);

        float _randomPosY = Random.Range(-4f, 4);
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + _randomPosY);
    }
}

