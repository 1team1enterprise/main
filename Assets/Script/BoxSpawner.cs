using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject boxPrefab;

    void Start()
    {
        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < 11; x++)
            {
                Vector3 position = new Vector3(-6.25f + x * 1.25f, 6.25f - y * 1.25f, 0);

                GameObject clone = Instantiate(boxPrefab, position, Quaternion.identity) as GameObject;
                clone.transform.SetParent(gameObject.transform, false);
                clone.GetComponent<Box>().x = x + 1;
                clone.GetComponent<Box>().y = y + 1;
            }
        }
    }

}
