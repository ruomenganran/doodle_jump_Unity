using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_script : MonoBehaviour
{
    public GameObject player;
    public float score_ratio = 5;
    public static Text txt;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        //绑定数据并初始化
        txt = GetComponent<Text>();
        txt.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        ScoreUpdate();
    }
    
    public void ScoreUpdate()
    {
        int temp = (int)(score_ratio * player.transform.position.y);
        if (temp > score)
        {
            score = temp;
        }
        txt.text = score.ToString();
    }
}
