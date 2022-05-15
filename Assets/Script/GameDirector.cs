using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public GoalChecker _goalHousePrefab;
    public PlayerController _playerController;
    public FloorGenertor[] floorGenerators;
    public RandomObjectGenerator[] randomObjectGenerators;
    public AudioManager _audio;


    private bool isSetUp;
    private bool isGameUp;

    private int generateCount;

    // generateCount 変数のプロパティ
    public int GenerateCount
    {
        set
        {
            generateCount = value;
            Debug.Log("生成数/クリア目標数：" + generateCount + "/" + _clearCount);

            if (generateCount >= _clearCount)
            {
                GenerateGoal();
                GameUp();
            }
        }
        get
        {
            return generateCount;
        }
    }

    public int _clearCount;

    void Start()
    {
        StartCoroutine(_audio.PlayBGM(0));

        isGameUp = false;
        isSetUp = false;

        SetUpFloorGenerators();
        StopGenerators();
    }

    private void SetUpFloorGenerators()
    {
        for(int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SetUpGenerator(this);
        }
    }
    void Update()
    {
        if (_playerController.isFirstGenerateBallon && isSetUp == false)
        {
            isSetUp = true;
            ActivateGenerators();
            StartCoroutine(_audio.PlayBGM(1));
        }
    }

    private void GenerateGoal()
    {
        GoalChecker goalHouse = Instantiate(_goalHousePrefab);
        goalHouse.SetUpGoalHouse(this);
    }

    public void GameUp()
    {
        isGameUp = true;
        StopGenerators();
    }

    private void StopGenerators()
    {
        for(int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(false);
        }
        for (int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(false);
        }
    }

    private void ActivateGenerators()
    {
        for(int i = 0; i < randomObjectGenerators.Length; i++)
        {
            randomObjectGenerators[i].SwitchActivation(true);
        }
        for(int i = 0; i < floorGenerators.Length; i++)
        {
            floorGenerators[i].SwitchActivation(true);
        }
    }
    public void GoalClear()
    {
        StartCoroutine(_audio.PlayBGM(2));
    }
}
