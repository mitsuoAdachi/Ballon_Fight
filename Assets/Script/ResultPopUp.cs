using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ResultPopUp : MonoBehaviour
{
    [SerializeField]
    private Text txtScore;
    [SerializeField]
    private CanvasGroup canvasGroup;
    [SerializeField]
    private CanvasGroup canvasGroupRestart;
    [SerializeField]
    private Button btnTitle;

    /// <summary>
    /// ResultPopUpの設定
    /// </summary>
    /// <param name="score"></param>
    public void SetUpResultPopUp(int score)
    {
        //最初に透明にする
        canvasGroup.alpha = 0;

        //徐々にResultPopupを表示
        canvasGroup.DOFade(1, 1);

        //スコアを表示
        txtScore.text = score.ToString();

        // リスタートのメッセージをゆっくりと点滅アニメさせる(学習済の命令です。復習しておきましょう)
        canvasGroupRestart.DOFade(0, 1).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);

        // ボタンにメソッドを登録
        btnTitle.onClick.AddListener(OnClickRestart);
    }

    /// <summary>
    /// ボタンを押した際の制御
    /// </summary>
    private void OnClickRestart()
    {
        // リザルト表示を徐々に非表示にする
        canvasGroup.DOFade(0, 1).SetEase(Ease.Linear);

        StartCoroutine(Restart());
    }
    /// <summary>
    /// 現在のシーンを再度読み込む
    /// </summary>
    /// <returns></returns>
    private IEnumerator Restart()
    {
        yield return new WaitForSeconds(1);

        // 現在のシーンの名前を取得
        string sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(sceneName);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
