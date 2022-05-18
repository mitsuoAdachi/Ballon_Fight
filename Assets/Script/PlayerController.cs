using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // キー入力用の文字列指定(InputManagerの入力を判定するための文字列)
    private string horizontal = "Horizontal", jump = "Jump";
    // コンポーネントの取得用
    private Rigidbody2D rb;
    // 移動速度
    public float moveSpeed;

    // 向きの設定に利用する
    private float scale;

    private Animator anime;

    private float limitPosX = 9.5f, limitPosY = 4.45f;

    private bool isGameover = false;

    private int ballonCount;

    // ジャンプ・浮遊力
    public float jumpPower;

    [SerializeField, Header("Linecast用 地面判定レイヤー")]
    private LayerMask groundLayer;

    [SerializeField]
    private StartChecker _startChecker;

    public bool isGrounded, isFirstGenerateBallon;

    public GameObject[] ballons;
    public int maxBalllonCount;
    public Transform[] ballonTrans;
    public GameObject ballonPrefab;

    public float generateTime;
    public bool isGenerating;

    public float knockbackPower;

    public int _coinPoint;

    public UIManager _uiManager;

    [SerializeField]
    private AudioClip knockbackSE;
    [SerializeField]
    private AudioClip coinGetSE;


    [SerializeField]
    private GameObject knockbackEffectPrefab;

    [SerializeField]
    private Joystick joystick;

    [SerializeField]
    private Button btnJump;

    [SerializeField]
    private Button btnDetach;

    [SerializeField]
    private ParticleSystem _beam;


    // Start is called before the first frame update
    void Start()
    {
        // 必要なコンポーネントを取得して用意した変数に代入
        rb = GetComponent<Rigidbody2D>();

        scale = transform.localScale.x;

        anime = GetComponent<Animator>();

        // 配列の初期化(バルーンの最大生成数だけ配列の要素数を用意する
        ballons = new GameObject[maxBalllonCount];

        //jumpボタンが押されたらOnClickJump関数が呼び出される
        btnJump.onClick.AddListener(OnClickJump);
        //DetachOrGenerateボタンが押されたらOnClickDetachOrGenerate関数が呼び出される
        btnDetach.onClick.AddListener(OnClickDetachOrGenerate);

    }

    // Update is called once per frame
    void Update()
    {
        // 地面接地  Physics2D.Linecastメソッドを実行して、Ground Layerとキャラのコライダーとが接地している距離かどうかを確認し、
        // 接地しているなら true、接地していないなら false を戻す
        isGrounded = Physics2D.Linecast(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, groundLayer);
        // Sceneビューに Physics2D.LinecastメソッドのLineを表示する
        Debug.DrawLine(transform.position + transform.up * 0.4f, transform.position - transform.up * 0.9f, Color.red, 1.0f);

        // Ballons配列変数の最大要素数が 0 以上なら = インスペクターでBallons変数に情報が登録されているなら
        if (ballons[0] != null)
        {

            //ジャンプ
            // InputManager の Jump の項目に登録されているキー入力を判定する
            if (Input.GetButtonDown(jump))
            {
                Jump();
            }

            // 接地していない(空中にいる)間で、落下中の場合
            if (isGrounded == false && rb.velocity.y < 0.15f)
            {
                // 落下アニメを繰り返す
                anime.SetTrigger("Fall");
            }
        }
        else
        {
            Debug.Log("バルーンがない。ジャンプ不可");
        }

        // Velocity.y の値が 5.0f を超える場合(ジャンプを連続で押した場合)
        if (rb.velocity.y > 5.0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 5.0f);
        }

        // 地面に接地していて、バルーンが生成中ではない場合
        if(isGrounded==true&&isGenerating==false)
        {
            // Qボタンを押したら
            if(Input.GetKeyDown(KeyCode.Q))
            {
                // バルーンを１つ作成する
                StartCoroutine(GenerateBallon());
            }
        }

        if (Input.GetKey(KeyCode.B))
        {
            OnBeam();
        }
    }
    /// <summary>
    /// ジャンプと空中
    /// </summary>
    private void Jump()
    {
        // キャラの位置を上方向へ移動させる(ジャンプ・浮遊)
        rb.AddForce(transform.up * jumpPower);
        // Jump(Up + Mid) アニメーションを再生する
        anime.SetTrigger("Jump");
    }

    void FixedUpdate()
    {
        if (isGameover == true)return;
        //移動
        Move();
    }
    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
#if UNITY_EDITOR
        float x = Input.GetAxis(horizontal);
        //x = joystick.Horizontal;
#else
        float x = joystick.Horizontal;
#endif
        // 水平(横)方向への入力受付
        // InputManager の Horizontal に登録されているキーの入力があるかどうか確認を行う

        // x の値が 0 ではない場合 = キー入力がある場合
        if (x!=0)
        {
            // velocity(速度)に新しい値を代入して移動
            rb.velocity = new Vector2(x * moveSpeed, rb.velocity.y);

            // temp 変数に現在の localScale 値を代入
            Vector3 temp = transform.localScale;
            // 現在のキー入力値 x を temp.x に代入
            temp.x = x;
            // 向きが変わるときに小数になるとキャラが縮んで見えてしまうので整数値にする
            if(temp.x>0)
            {
                // 数字が0よりも大きければすべて1にする
                temp.x = scale;
            }
            else
            {
                // 数字が0よりも小さければすべて-1にする
                temp.x = -scale;
            }
            // キャラの向きを移動方向に合わせる
            transform.localScale = temp;

            // 待機状態のアニメの再生を止めて、走るアニメの再生への遷移を行う
            //Idle アニメーションを false にして、待機アニメーションを停止する
            anime.SetBool("Idle", false);
            //　Runアニメーションに対して、0.5f の値を情報として渡す。遷移条件が greater 0.1 なので、0.1 以上の値を渡すと条件が成立してRun アニメーションが再生される
            anime.SetFloat("Run", 0.5f); 
        }
        else
        {
            // 左右の入力がなかったら横移動の速度を0にしてすぐに停止させる
            rb.velocity = new Vector2(0, rb.velocity.y);

            // 走るアニメの再生を止めて、待機状態のアニメの再生への遷移を行う
            anime.SetFloat("Run", 0.0f);
            anime.SetBool("Idle", true);
        }

        //移動範囲の制限
        float posX = Mathf.Clamp(transform.position.x, -limitPosX, limitPosX);
        float posY = Mathf.Clamp(transform.position.y, -limitPosY, limitPosY);

        transform.position = new Vector2(posX, posY);
    }

    private IEnumerator GenerateBallon()
    {
        // すべての配列の要素にバルーンが存在している場合には、バルーンを生成しない
        if(ballons[1] != null)
        {
            yield break;
        }
        // 生成中状態にする
        isGenerating = true;

        if (isFirstGenerateBallon == false)
        {
            isFirstGenerateBallon = true;
            _startChecker.SetInitialSpeed();
        }

        // １つめの配列の要素が空なら
        if(ballons[0]==null)
        {
            // 1つ目のバルーン生成を生成して、1番目の配列へ代入
            ballons[0] = Instantiate(ballonPrefab, ballonTrans[0]);

            ballons[0].GetComponent<Ballon>().SetUpBallon(this);
        }
        else
        {
            // 2つ目のバルーン生成を生成して、2番目の配列へ代入
            ballons[1] = Instantiate(ballonPrefab, ballonTrans[1]);

            ballons[1].GetComponent<Ballon>().SetUpBallon(this);

        }
        //バルーンの数を増やす
        ballonCount++;

        // 生成時間分待機
        yield return new WaitForSeconds(generateTime);

        // 生成中状態終了。再度生成できるようにする
        isGenerating = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized;
            transform.position += direction * knockbackPower;

            AudioSource.PlayClipAtPoint(knockbackSE, transform.position);
            GameObject knockbackEffect = Instantiate(knockbackEffectPrefab, collision.transform.position,Quaternion.identity);

            // エフェクトを 0.5 秒後に破棄。生成したタイミングで変数に代入しているので、削除の命令が出せる
            Destroy(knockbackEffect, 0.5f);
        }
    }
    public void DestroyBallon()
    {
        if (ballons[1] != null)
        {
            Destroy(ballons[1]);
        }
        else if(ballons[0]!=null)
        {
            Destroy(ballons[0]);
        }

        //バルーンの数を減らす
        ballonCount--;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            _coinPoint += collision.gameObject.GetComponent<Coin>().point;
            _uiManager.UpdateDisplayScore(_coinPoint);

            AudioSource.PlayClipAtPoint(coinGetSE, transform.position);

            Destroy(collision.gameObject,0.5f);
        }

    }
    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public void GameOver()
    {
        isGameover = true;
        Debug.Log(isGameover);
        _uiManager.DisplayGameOverInfo();
    }

    private void OnClickJump()
    {
        if(ballonCount > 0)
        {
            Jump();
        }
    }

    private void OnClickDetachOrGenerate()
    {
        if(isGrounded == true && ballonCount < maxBalllonCount && isGenerating == false)
        {
            StartCoroutine(GenerateBallon());
        }
    }

    public void OnBeam()
    {
        _beam.Play();
    }

}
