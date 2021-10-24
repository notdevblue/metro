using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject coinPrefab;

    // Start is called before the first frame update
    void Start()
    {
        PoolManager.CreatePool<Coin>(coinPrefab, transform, 30);
    }

    static public void PopCoin(Vector3 pos, int count)
    {
        for (int i = 0; i < count; ++i)
        {
            Coin coin = PoolManager.GetItem<Coin>();
            coin.PopUp(pos);
        }
    }
}
