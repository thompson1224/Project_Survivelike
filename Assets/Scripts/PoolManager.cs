using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // .. 프리팹들을 보관할 변수
    public GameObject[] prefabs;
    // .. 풀 담당 리스트 필요
    private List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // ... 선택한 풀에서 비활성화된 게임오브젝트 접근
            foreach (GameObject item in pools[index])
            {
                //... 발견하면 select에 할당
                if (!item.activeSelf)
                {
                    select = item;
                    select.SetActive(true);
                    break;
                }
            }
            // ... 못 찾을 경우 새롭게 생성 및 select 변수 할당
            if (!select)
            {
                select = Instantiate(prefabs[index], transform);
                pools[index].Add(select);
            }
        
        return select;
    }
}
