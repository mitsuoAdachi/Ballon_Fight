using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _txtScore;

    [SerializeField]
    private Text _txtInfo;

    [SerializeField]
    CanvasGroup canvasGroupInfo;

    [SerializeField]
    private ResultPopUp resultPopUpPrefab;

    [SerializeField]
    private Transform canvasTran;

    [SerializeField]
    private Button btnInfo;

    [SerializeField]
    private Button btnTitle;

    [SerializeField]
    private Text lblStart;

    [SerializeField]
    private CanvasGroup canvasGroupTitle;
    private Tweener tweener;

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
        canvasGroupInfo.DOFade(1.0f, 1.0f);
        _txtInfo.DOText("Game Over...", 1.0f);

        btnInfo.onClick.AddListener(RestartGame);
    }

    public void GenerateResultPopUp(int score)
    {
        ResultPopUp resultPopUp = Instantiate(resultPopUpPrefab, canvasTran, false);

        resultPopUp.SetUpResultPopUp(score);
    }

    public void RestartGame()
    {
        btnInfo.onClick.RemoveAllListeners();

        string sceneName = SceneManager.GetActiveScene().name;

        canvasGroupInfo.DOFade(0, 1).OnComplete(() =>
        {
            Debug.Log("Restart");
            SceneManager.LoadScene(sceneName);
        });
    }
    private void Start()
    {
        SwitchDisplayTitle(true, 1);
        btnTitle.onClick.AddListener(OnClickTitle);
    }
    public void SwitchDisplayTitle(bool isSwitch, float alpha)
    {
        if (isSwitch) canvasGroupTitle.alpha = 0;

        canvasGroupTitle.DOFade(alpha, 1).SetEase(Ease.Linear).OnComplete(() =>
            { lblStart.gameObject.SetActive(isSwitch);
        });

        if (tweener == null)
        {
            tweener = lblStart.gameObject.GetComponent<CanvasGroup>().DOFade(0, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            tweener.Kill();
        }
    }
    private void OnClickTitle()
    {
        btnTitle.onClick.RemoveAllListeners();
        SwitchDisplayTitle(false, 0);
        StartCoroutine(DisplayGameStartInfo());
    }
    public IEnumerator DisplayGameStartInfo()
    {
        yield return new WaitForSeconds(0.5f);
        canvasGroupInfo.alpha = 0;
        canvasGroupInfo.DOFade(1, 0.5f);
        _txtInfo.text = "Game Start!";

        yield return new WaitForSeconds(1);
        canvasGroupInfo.DOFade(0, 0.5f);
        canvasGroupTitle.gameObject.SetActive(false);
    }
}
