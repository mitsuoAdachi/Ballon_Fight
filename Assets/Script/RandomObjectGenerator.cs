using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject[] objPrefab;

    [SerializeField]
    private Transform generateTran;

    [Header("生成までの待機時間")]
    public Vector2 waitTimeRange;

    private float waitTime;
    private float timer;

    private bool isActivate;
    private GameDirector gameDirector;

    // Start is called before the first frame update
    void Start()
    {
        SetGenerateTime();
    }
    private void SetGenerateTime()
    {
        waitTime = Random.Range(waitTimeRange.x, waitTimeRange.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActivate == false)
        {
            return;
        }
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            timer = 0;
            RandomGenerateObject();
        }
    }

    private void RandomGenerateObject()
    {
        int randomIndex = Random.Range(0, objPrefab.Length);
        GameObject obj = Instantiate(objPrefab[randomIndex], generateTran);

        float randomPosY = Random.Range(-4.0f, 4.0f);

        obj.transform.position = new Vector2(obj.transform.position.x, obj.transform.position.y + randomPosY);

        SetGenerateTime();

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
