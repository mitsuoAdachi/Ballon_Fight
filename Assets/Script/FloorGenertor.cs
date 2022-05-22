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

    private GameDirector _gameDirector;

    private bool isActivate;

    void Update()
    {
        if(isActivate == false)
        {
            return;
        }
        _timer += Time.deltaTime;

        if (_timer >= _waitTime) {
            _timer = 0;

            GeneratorFloor();
        }
    }

    private void GeneratorFloor() {
        GameObject obj = Instantiate(_floorPrefab, _generateTrans);

        float _randomPosY = Random.Range(-10f, 10);
        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + _randomPosY);

        _gameDirector.GenerateCount++;
    }

    public void SetUpGenerator(GameDirector gameDirector)
    {
        this._gameDirector = gameDirector;
    }

    /// <summary>
    /// 生成状態のオン/オフを切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    public void SwitchActivation(bool isSwitch)
    {
        isActivate = isSwitch;
   }
}

