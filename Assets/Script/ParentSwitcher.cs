using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentSwitcher : MonoBehaviour
{
    private string player = "Player";

    // このスクリプトがアタッチされているゲームオブジェクトのコライダーと、他のゲームオブジェクトのコライダーが接触している間ずっと接触判定を行うメソッド
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == player)
        {
            collision.transform.SetParent(transform);
        }
    }
    // このスクリプトがアタッチされているゲームオブジェクトのコライダーと、他のゲームオブジェクトのコライダーとが離れた際に判定を行うメソッド
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == player)
        {
            collision.transform.SetParent(null);
        }
    }
}
