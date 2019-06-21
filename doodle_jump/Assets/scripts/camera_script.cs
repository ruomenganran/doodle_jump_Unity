using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_script : MonoBehaviour
{
    public doodle_script player;
    public GameObject background;
    private float offset;
    private Rigidbody2D rb2d;
    public float cameraRollSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        offset = transform.position.y - (player.transform.position.y / 2);
    }

    // Update is called once per frame
    void Update()
    {
        //此处代码效果：小人的落点离屏幕中心越远，camera向上滚动的速度越快
        if(player.refreshCamera == true)
        {
            float factor_speed = 0f;
            factor_speed = (player.transform.position.y - transform.position.y) * cameraRollSpeed;
            factor_speed = factor_speed < 0 ? 0 : factor_speed;
            rb2d.velocity = new Vector2(0, factor_speed);
        }
        
    }
}
