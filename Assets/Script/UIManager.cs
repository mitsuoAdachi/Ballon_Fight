using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _txtScore;

    [SerializeField]
    private Text _txtInfo;

    [SerializeField]
    CanvasGroup _canvasGroupInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// スコア表示を更新
    /// </summary>
    /// <param name="score"></param>
    public void UpdateDisplayScore(int _score)
    {
        _txtScore.text = _score.ToString();
    }

    /// <summary>
    /// ゲームオーバー表示
    /// </summary>
    ///
    public void DisplayGameOverInfo()
    {
        _canvasGroupInfo.DOFade(1.0f, 1.0f);
        _txtInfo.DOText("Game Over...", 1.0f);
    }
}
