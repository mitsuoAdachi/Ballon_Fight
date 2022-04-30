using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Ballon : MonoBehaviour
{
    private PlayerController _playerController;

    private Tweener _tweener;

    public void SetUpBallon(PlayerController _playerController)
    {
        this._playerController = _playerController;

        Vector3 scale = transform.localScale;

        // 現在のScaleを0にして画面から一時的に非表示にする
        transform.localScale = Vector3.zero;

        // だんだんバルーンが膨らむアニメ演出
        transform.DOScale(scale, 2).SetEase(Ease.InBounce);

        // 左右にふわふわさせる
        _tweener = transform.DOLocalMoveX(0.02f, 0.2f).SetEase(Ease.Flash).SetLoops(-1, LoopType.Yoyo);


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _tweener.Kill();

            _playerController.DestroyBallon();
        }
    }
}
