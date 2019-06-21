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
        //Vector3 position_temp = new Vector3(0,0,10.0f);
        //borderLeft = Camera.current.ViewportToWorldPoint(position_temp).x;
        //position_temp.x = 1;
        //borderRight = Camera.current.ViewportToWorldPoint(position_temp).x;


    }

    // Update is called once per frame
    void Update()
    {
        ActKeyProcess();
        ActBoderBind();


    }

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

    void ActJumpPlayer(float x)
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed * x), ForceMode2D.Impulse);

    }

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
        Vector2 movement = new Vector2(moveHorizontal * moveSpeed, rb2d.velocity.y);
        rb2d.velocity = movement;
    }

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



        //屏幕边缘定义为屏幕border相邻5%个width的空间
        //if(transform.position.x - borderLeft < 0.05)
        //{
        //    transform.position = new Vector3(borderRight, transform.position.y, transform.position.z);
        //}
        //if (borderRight - transform.position.x < 0.05)
        //{
        //    transform.position = new Vector3(borderLeft, transform.position.y, transform.position.z);
        //}

        //todo:borderRight竟然是0
        //print("transform.position.x:" + transform.position.x);
        //print("borderLeft:" + borderLeft);
        //print("view:" + Camera.main.WorldToViewportPoint(transform.position));
        //print("Camera.main.pixelWidth:" + Camera.main.scaledPixelWidth);

        //Vector3 pos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        //print(pos1);
    }
}
