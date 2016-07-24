using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SquidControl : MonoBehaviour {
    public LayerMask groundLayer;
    public GameObject gameOverPanel;

    Rigidbody2D rb2D;
    Animator animator;
    SpriteRenderer spriteRenderer;

    //public float jumpVelocity; // ジャンプの速度
    public float horizontalVelocity; // 横方向の速度(0~1)
    public float cameraSpeed;
    public float maxCharge;
    public float chargespeed;
    float charge = 0f;

    bool grounded = true;
    bool max = false;
    bool dead = false;

    Vector2 direction = new Vector2(0f, 0f);


	void Awake () {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        gameOverPanel.gameObject.SetActive(false);
	}
	
	void Update () {

        // なめらかにカメラを追従させる
        Vector3 pos = new Vector3(0f, transform.position.y, -19f); // イカのy座標を取り出す(z＝-19はイカとカメラの距離)
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, pos, cameraSpeed * Time.deltaTime); // カメラ目的の座標までなめらかに移動させる

        // イカを左右に移動させる
        direction.x = Input.GetAxis("Horizontal") * horizontalVelocity;
        rb2D.AddForce(direction);

        if (charge >= maxCharge)
        {
            max = true;
        }
        else
        {
            max = false;
        }

        // Fire1のキーが押されている間chargeを増加させる
        if (Input.GetButton("Fire1") && max == false)
        {
            charge += chargespeed; // 一度にチャージされる値をchargeSpeedで決めている
        }

        // Fire1のキーが離されたとき、地面に接してた場合
        if (Input.GetButtonUp("Fire1") && grounded)
        {
            Jump();

            charge = 0f; //chrgeのリセット     
              
        }

        // GetButtonUpが検知されずchargeがリセットされないことがあったので追加
        if (Input.GetButton("Fire1") == false)
        {
            if (charge != 0f)
            {
                charge = 0f;
            }
        }
        

        if (transform.position.x < -12.8) // 画面の左端に到達した場合画面の右端に座標を書き換える
        {
            Vector3 squidPos = transform.position;
            squidPos.x = 12.8f;
            transform.position = squidPos;
        }

        if (transform.position.x > 12.8) // 画面の右端に到達した場合画面の左端に座標を書き換える
        {
            Vector3 squidPos = transform.position;
            squidPos.x = -12.8f;
            transform.position = squidPos;
        }

        // Fire1が押されている間chargingをtrueにし続け、縮める動きを再生する
        animator.SetBool("charging", Input.GetButton("Fire1"));

        // チャージがmax時に点滅のアニメーションを再生する
        animator.SetBool("blink", max);

        // deadがtrueのときに死ぬアニメーションを表示する
        animator.SetBool("isDead", dead);

        //Debug.Log(charge);
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true; // 地面に接してる時groundedをtrueにする
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false; // 地面に接してる時groundedをfoalseにする
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        Time.timeScale = 0; // ゲームを停止

        gameOverPanel.gameObject.SetActive(true); // ゲームオーバーの表示をする

        Debug.Log("you are dead!!");

        dead = true;
    }

    public void Jump()
    {
        if (charge <= maxCharge) {

            rb2D.AddForce(Vector2.up * charge); // Velocityを直接書き換えて上方向に加速

        } else {

            rb2D.AddForce(Vector2.up * maxCharge); // Velocityを直接書き換えて上方向に加速

        }
    }
}
