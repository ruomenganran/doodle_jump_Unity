using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_script : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform border;
    private Transform bg;
    public Transform camera;
    //public GameObject camera_main;
    void Start()
    {
        //border = GetComponentInChildren<GameObject>();
        border = GetComponent<Transform>().Find("topbar");
        bg = GetComponent<Transform>().Find("bg-grid");
        Resize();
    }

    // Update is called once per frame
    void Update()
    {
        //跟随镜头移动
        transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, transform.position.z);
    }
    void Resize()
    {
        
        float width = bg.GetComponent<SpriteRenderer>().bounds.size.x;
        float high = bg.GetComponent<SpriteRenderer>().bounds.size.y;
        float targetWidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;
        //float targetHigh = (Camera.main.orthographicSize) * 2 / Screen.height * Screen.width;
        float targetHigh = targetWidth / Camera.main.aspect; ;
        Vector3 scale = bg.localScale;
        scale.x = targetWidth / width;
        scale.y = targetHigh / high;
        if(scale.x>= scale.y)
        {
            scale.y = scale.x;
        }
        else
        {
            scale.x = scale.y;
        }
        bg.localScale = scale;

        width = border.GetComponent<SpriteRenderer>().bounds.size.x;
        targetWidth = Camera.main.orthographicSize * 2 / Screen.height * Screen.width;
        scale = border.localScale;
        scale.x = targetWidth / width;
        border.localScale = scale;
    }
}
