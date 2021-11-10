using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public int x;
    public int y;
    public int selectNum;

    void Start()
    {
        selectNum=0;
        
        if(random.instance.Xnum==x&&random.instance.Ynum==y)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
    void Update()
    {
        Select();
    }

    void Select()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                    if(selectNum==0)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                        selectNum++;
                    }
                        
                    else if(selectNum==1)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                        selectNum=0;
                    }
                       
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                    if(selectNum==0)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                        selectNum++;
                    }
                        
                    else if(selectNum==1)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                        selectNum=0;
                    }
                       
            }
        }
    }
}
