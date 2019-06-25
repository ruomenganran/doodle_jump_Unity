using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class bar_manage : MonoSingleton<bar_manage>
{
    //tile池子
    public enum ObjectType
    {
        Tile,
        Item,
        Coin,
        Enemy,
        Bullet
    }
    //tile池子
    public Queue<GameObject> tilePool = new Queue<GameObject>();
    public Queue<GameObject> itemPool = new Queue<GameObject>();
    public Queue<GameObject> coinPool = new Queue<GameObject>();
    public Queue<GameObject> enemyPool = new Queue<GameObject>();
    public Queue<GameObject> bulletPool = new Queue<GameObject>();
    //tile样本
    public GameObject tilePrefab;
    //生成父位置
    public Transform parent;
    //
    float totalsum;
    //gamesettings
    public GameSettings advance;
    public Sprite green;
    public Sprite brown;
    public Sprite white;
    public Animation brown_ani;

    float currentY = 0f;
    float cameraBorderLeft;
    float cameraBorderRight;

    int initialSize = 60;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        //currentY为生成bar的起始Y坐标，设为屏幕最下方
        currentY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        //获取屏幕左右边界
        cameraBorderLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x+0.2f;
        cameraBorderRight = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0)).x-0.2f;
        //sum赋值
        GetAllWeight();
        //初始化对象池
        GenerateTilePool();

        //生成物体
        for (int i = 0; i < initialSize; i++)
        {
            GenerateTile();
        }
    }
    private void Update()
    {
        
    }
    /// <summary>
    /// 生成tile池子
    /// </summary>
    void GenerateTilePool()
    {
        for (int i = 0; i < initialSize; i++)
        {
            GameObject go = Instantiate(tilePrefab, parent);
            go.SetActive(false);
            go.name = i.ToString();
            tilePool.Enqueue(go);
        }
    }
    /// <summary>
    /// 随机生成砖块
    /// </summary>
    void GenerateTile()
    {
        //print(currentY);
        GameObject go = GetInactiveObject(ObjectType.Tile);
        float rand = Random.Range(0, totalsum);
        int randNumber = SetTileByRandomNumber(rand);
        Vector2 pos = new Vector2(Random.Range(cameraBorderLeft, cameraBorderRight), currentY);
        switch (randNumber)
        {
            case 0:
                go.transform.position = pos;
                currentY += Random.Range(advance.normalTile.minHeight, advance.normalTile.maxHeight);
                go.name = "p-green";
                go.GetComponent<SpriteRenderer>().sprite = green;
                go.SetActive(true);
                break;
            case 1:
                go.transform.position = pos;
                currentY += Random.Range(advance.brokenTile.minHeight, advance.brokenTile.maxHeight);
                go.name = "p-brown";
                go.GetComponent<SpriteRenderer>().sprite = brown;
                go.GetComponent<SpriteRenderer>().sortingLayerName = "brown_bar";
                //go.GetComponent<Animator>().SetBool("play", false);
                go.GetComponent<Animator>().enabled = true;
                
                go.SetActive(true);
                break;
            case 2:
                go.transform.position = pos;
                currentY += Random.Range(advance.oneTimeOnly.minHeight, advance.oneTimeOnly.maxHeight);
                go.name = "p-white";
                go.GetComponent<SpriteRenderer>().sprite = white;
                go.SetActive(true);
                break;
            case 3:
                go.transform.position = pos;
                currentY += Random.Range(advance.springTile.minHeight, advance.springTile.maxHeight);
                go.name = "3";
                go.SetActive(true);
                break;
            case 4:
                go.transform.position = pos;
                currentY += Random.Range(advance.movingHorizontally.minHeight, advance.movingHorizontally.maxHeight);
                go.name = "4";
                go.SetActive(true);
                break;
            case 5:
                go.transform.position = pos;
                currentY += Random.Range(advance.movingVertically.minHeight, advance.movingVertically.maxHeight);
                go.name = "5";
                go.SetActive(true);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 从池子取物体
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public GameObject GetInactiveObject(ObjectType type)
    {
        switch (type)
        {
            case ObjectType.Tile:
                return tilePool.Dequeue();
            case ObjectType.Item:
                return itemPool.Dequeue();
            case ObjectType.Coin:
                return coinPool.Dequeue();
            case ObjectType.Enemy:
                return enemyPool.Dequeue();
            case ObjectType.Bullet:
                return bulletPool.Dequeue();
            default:
                return null;
        }
    }

    ///获取权值所有
    void GetAllWeight()
    {
        float sum = 0;
        sum += advance.normalTile.weight;
        sum += advance.brokenTile.weight;
        sum += advance.oneTimeOnly.weight;
        sum += advance.springTile.weight;
        sum += advance.movingVertically.weight;
        sum += advance.movingHorizontally.weight;
        totalsum = sum;
    }

    int SetTileByRandomNumber(float number)
    {
        if (number <= advance.normalTile.weight)
            return 0;
        else if (number <= (advance.normalTile.weight + advance.brokenTile.weight))
            return 1;
        else if (number <= (advance.normalTile.weight + advance.brokenTile.weight + advance.oneTimeOnly.weight))
            return 2;
        else if (number <= (advance.normalTile.weight + advance.brokenTile.weight + advance.oneTimeOnly.weight + advance.springTile.weight))
            return 3;
        else if (number <= (advance.normalTile.weight + advance.brokenTile.weight + advance.oneTimeOnly.weight + advance.springTile.weight + advance.movingHorizontally.weight))
            return 4;
        else if (number <= (advance.normalTile.weight + advance.brokenTile.weight + advance.oneTimeOnly.weight + advance.springTile.weight + advance.movingHorizontally.weight + advance.movingVertically.weight))
            return 5;

        return -1;
    }

    public void GenerateTileUpdate()
    {
        GenerateTile();
    }
}
