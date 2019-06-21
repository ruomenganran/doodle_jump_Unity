using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameSettings:MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
