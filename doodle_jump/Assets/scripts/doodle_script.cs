using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doodle_script : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public int jumpSpeed;
    public int moveSpeed;
    public GameObject player;
    private float borderLeft;
    private float borderRight;
    public bool refreshCamera;
    private float player_revert_offset;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player_revert_offset = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        ActKeyProcess();
        ActBoderBind();


    }
    /// <summary>
    /// 小人下落碰到bar的边缘，跳起
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (rb2d.velocity.y < 0)
        {
            switch (collision.name)
            {
                case "p-green":
                    {
                        ActJumpPlayer(1);
                    }
                    break;
                case "p-brown":
                    {
                        ActJumpPlayer(1);
                        collision.gameObject.GetComponent<Animator>().SetBool("active", true);
                        //collision.enabled = false;
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -100f));
                    }
                    break;
                case "p-white":
                    {
                        ActJumpPlayer(1);
                        collision.gameObject.SetActive(false);
                    }
                    break;
            }
            refreshCamera = true;
            //小人落在bar上就让camera向上跟随
        }
        
    }
    /// <summary>
    /// 跳跃
    /// </summary>
    /// <param name="x"></param>
    void ActJumpPlayer(float x)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed * x), ForceMode2D.Impulse);

    }
    /// <summary>
    /// 按键处理
    /// </summary>
    void ActKeyProcess()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (moveHorizontal > 0)
        {
            transform.localScale = new Vector3(player_revert_offset, transform.localScale.y, transform.localScale.z);
            //此处作用是让小人实现左右翻转
            transform.rotation.Set(0, 0, 0, 0);
        }
        else if (moveHorizontal < 0)
        {
            transform.localScale = new Vector3(-player_revert_offset, transform.localScale.y, transform.localScale.z);
            transform.rotation.Set(0, 180, 0, 0);
        }
        else//无键盘输入，检测触摸屏
        {
            if(Input.touchCount > 0)
            {
                if((Input.touches[0].phase!=TouchPhase.Ended)&&(Input.touches[0].phase != TouchPhase.Canceled))
                {
                    if (Camera.main.WorldToViewportPoint(Input.touches[0].position).x > 0.5f)
                    {
                        transform.localScale = new Vector3(player_revert_offset, transform.localScale.y, transform.localScale.z);
                        //此处作用是让小人实现左右翻转
                        transform.rotation.Set(0, 0, 0, 0);
                    }
                    else
                    {
                        transform.localScale = new Vector3(-player_revert_offset, transform.localScale.y, transform.localScale.z);
                        transform.rotation.Set(0, 180, 0, 0);
                    }
                }
            }
        }
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb2d.velocity.y);
        rb2d.velocity = movement;
    }
    /// <summary>
    /// 小人到屏幕边缘处理
    /// </summary>
    void ActBoderBind()
    {
        //检测屏幕边缘。世界坐标系、观察坐标系、ViewPort、屏幕坐标系的相互转换。
        //viewpoint范围是每个轴0-1,对于的是视窗。
        float pos_viewport = 0f;
        pos_viewport = Camera.main.WorldToViewportPoint(transform.position).x;
        if(pos_viewport<0.001f)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0.999f, 0, 0)).x, 
                transform.position.y, transform.position.z);
        }
        else if (pos_viewport > 0.999f)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(0.001f, 0, 0)).x,
                transform.position.y, transform.position.z);
        }
    }
}
