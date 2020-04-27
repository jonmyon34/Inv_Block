using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockGenerater : MonoBehaviour
{
    private GameObject blockPrefab;

    const float BLOCK_WIDTH = 2.0f;
    const float BLOCK_DEPTH = 1.0f;

    void Awake()
    {
        blockPrefab = (GameObject)Resources.Load("Prefabs/Block");
    }

    void Start()
    {
        Generate();
    }

    private void Generate()
    {
        for (int i = 1; i <= 5; i++)
        {
            for (int j = 0; j <= 4; j++)
            {
                float margin = 0.25f;
                Vector3 pos = Vector3.zero;
                pos.x = (float)(j - 2) * (BLOCK_WIDTH + margin);
                pos.y = 1.0f;
                pos.z = i * (BLOCK_DEPTH + margin) + 1.0f;

                var obj = Instantiate(blockPrefab, pos, Quaternion.identity);
                obj.transform.parent = this.gameObject.transform;
            }
        }
    }
}