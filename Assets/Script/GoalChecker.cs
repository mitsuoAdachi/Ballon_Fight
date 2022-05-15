using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoalChecker : MonoBehaviour
{
    public float _moveSpeed = 0.01f;
    private float _stopPos = 6.5f;
    private bool isGoal;
    private GameDirector gameDirector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > _stopPos)
        {
            transform.position += new Vector3(-_moveSpeed, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && isGoal == false)
        {
            isGoal = true;
            Debug.Log("ゲームクリア");

            // PlayerControllerの情報を取得
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            // PlayerControllerの持つ、UIManagerの変数を利用して、GenerateResultPopUpメソッドを呼び出す。引数にはPlayerControllerのcoinCountを渡す
            playerController._uiManager.GenerateResultPopUp(playerController._coinPoint);

            gameDirector.GoalClear();
        }
    }

    public void SetUpGoalHouse(GameDirector gameDirector)
    {
        this.gameDirector = gameDirector;
    }
}