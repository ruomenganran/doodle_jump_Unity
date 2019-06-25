using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSettings:MonoBehaviour
{
    public score_script score;
    public int diffculty_ratio = 1;
    private static int diffculty = 0;
    // Start is called before the first frame update
    void Start()
    {
        DiffcultyProcess();
    }

    // Update is called once per frame
    void Update()
    {
        DiffcultyProcess();
    }

    void DiffcultyProcess()
    {
        float min_offset;
        float max_offset;
        min_offset = 0.25f - (Camera.main.aspect - 0.48f) / 13;
        min_offset = min_offset > 0.25f ? 0.25f : min_offset;
        min_offset = min_offset < 0.15f ? 0.15f : min_offset;

        max_offset = 0.5f - (Camera.main.aspect - 0.48f) / 6.5f;
        max_offset = max_offset > 0.5f ? 0.5f : max_offset;
        max_offset = max_offset < 0.3f ? 0.3f : max_offset;

        diffculty = diffculty_ratio * score.score / 2000;
        diffculty = diffculty > 200 ? 200 : diffculty;
        //这个数值在1：1的画面中比较合适，要根据画面长宽比 调整
        //float min_height = (1.50f - 0.20f) / 200f * (float)diffculty + 0.20f;
        //float max_height = (1.70f - 0.70f) / 200f * (float)diffculty + 0.40f;
        float min_height = (1.50f - 0.20f) / 200f * (float)diffculty + min_offset;
        float max_height = (1.70f - 0.70f) / 200f * (float)diffculty + max_offset;

        normalTile.minHeight = min_height;
        normalTile.maxHeight = max_height;
        brokenTile.minHeight = min_height;
        brokenTile.maxHeight = max_height;
        oneTimeOnly.minHeight = min_height;
        oneTimeOnly.maxHeight = max_height;

        if (diffculty > 0)
        {
            brokenTile.weight = diffculty / 10;
            oneTimeOnly.weight = diffculty / 10;
        }
        else
        {
            brokenTile.weight = 1;
            oneTimeOnly.weight = 1;
        }
        
    }

    /// <summary>
    /// 下列为bars生成的参数
    /// 最小高度和最大高度经测试，最低难度为0.2-0.4，最高难度为1.5-1.7
    /// </summary>
    [Serializable]
    public class NormalTile
    {
        //随机最小高度
        public float minHeight;
        //随机最大高度
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class BrokenTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class OneTimeOnly
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class SpringTile
    {
        public float minHeight;
        public float maxHeight;
        public float weight;
    }

    [Serializable]
    public class MovingHorizontally
    {
        public float minHeight;
        public float maxHeight;
        //移动距离
        public float distance;
        //速度
        public float speed;
        public float weight;
    }

    [Serializable]
    public class MovingVertically
    {
        public float minHeight;
        public float maxHeight;
        public float distance;
        public float speed;
        public float weight;
    }

    public NormalTile normalTile = new NormalTile();
    public BrokenTile brokenTile = new BrokenTile();
    public OneTimeOnly oneTimeOnly = new OneTimeOnly();
    public SpringTile springTile = new SpringTile();
    public MovingHorizontally movingHorizontally = new MovingHorizontally();
    public MovingVertically movingVertically = new MovingVertically();
}
