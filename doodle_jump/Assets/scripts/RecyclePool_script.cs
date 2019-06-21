using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecyclePool_script : MonoBehaviour
{
    public bar_manage bars;
    private Vector3 pos_offset;
    // Start is called before the first frame update
    void Start()
    {
        pos_offset = Camera.main.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.transform.position - pos_offset;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "doodleR")
        {
            //gameover

        }
        else if (collision.name.Contains("p-"))
        {
            collision.gameObject.SetActive(false);
            bars.tilePool.Enqueue(collision.gameObject);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            
            //Destroy(collision.GetComponent<GameObject>());
            //print(tilePool);
            bars.GenerateTileUpdate();
        }
        
    }
}
