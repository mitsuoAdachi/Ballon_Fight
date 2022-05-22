using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScroll : MonoBehaviour
{
    public enum BackType
    {
        upper,
        center,
        under
    }

    public BackType _backType;

    public bool[] _underCollision;

    [Header("背景画像のスクロール速度＝強制スクロールの速度")]
    public float scrollSpeed = 0.01f;

    [Header("画像のスクロール終了地点")]
    public float stopPosition = -16f;

    [Header("画像の再スタート地点")]
    public float restartPosition = 5.8f;

    void Update()
    {
        //画面の左方向にこのゲームオブジェクト(背景)の位置を移動する
        transform.Translate(-scrollSpeed, 0, 0);

        //このゲームオブジェクトの位置がstopPositonに到達したら
        if(transform.position.x<stopPosition)
        {
            //ゲームオブジェクトの位置を再スタート地点へ移動する
            transform.position = new Vector2(restartPosition, 0);
        }

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackUnder")
        {
            _underCollision[2] = true;
        }

        if (collision.gameObject.tag == "BackCenter")
        {
            _underCollision[1] = true;
        }

        if (collision.gameObject.tag == "BackUpper")
        {
            _underCollision[0] = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BackUnder")
        {
            _underCollision[2] = false;
        }

        if (collision.gameObject.tag == "BackCenter")
        {
            _underCollision[1] = false;
        }

        if (collision.gameObject.tag == "BackUpper")
        {
            _underCollision[0] = false;
        }

    }

}
