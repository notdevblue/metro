using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;

    public static Transform Player
    {
        get
        {
            if(instance != null)
            {
                return instance.player;
            }
            else
            {
                return null;
            }
           
        }
    }
    public Transform player;
    public GameObject bloodParticlePrefab;

    public DialogPanel dialogPanel; //다이얼로그 패널 제어용 스크립트
    private Dictionary<int, List<TextVO>> dialogTextDictionary = new Dictionary<int, List<TextVO>>();
    
    private float timeScale = 1f;
    
    public static float TimeScale
    {
        get
        {
            if(instance != null)
            {
                return instance.timeScale;
            }
            else
            {
                return 0f;
            }
                      
        }
        set
        {
            instance.timeScale = Mathf.Clamp( value, 0, 1);
        }
    }

    private int coinCount = 0;

    public static void AddCoin(int value)
    {
        //여기서는 단순히 코인을 증가시키기만 하지만 나중에 여기에 UI를 갱신하는 로직이 들어가야 한다.
        instance.coinCount += value;
        UIManager.SetCoinText(instance.coinCount);
    }

    public static void RemoveCoin(int value)
    {
        //당연히 여기서도 UI갱신
        instance.coinCount = Mathf.Clamp(instance.coinCount - value, 0, instance.coinCount);
        UIManager.SetCoinText(instance.coinCount);
    }


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("<color=ffcccc>WARN</color>: There are more than one GameManager running in same scene");
        }

        instance = this;

        TextAsset dJson = Resources.Load("dialogText") as TextAsset;
        GameTextDataVO textData = JsonUtility.FromJson<GameTextDataVO>(dJson.ToString());

        foreach(DialogVO vo in textData.list)
        {
            dialogTextDictionary.Add(vo.code, vo.text);
        }
    }

    private void Start()
    {
        PoolManager.CreatePool<BloodParticle>(bloodParticlePrefab, transform, 10);

        UIManager.SetCoinText(coinCount);
    }

    public static void ShowDialog(int index, Action callback = null)
    {
        if(index >= instance.dialogTextDictionary.Count)
        {
            return;
        }

        //해당 인덱스 의 대화를 재생하도록 함.
        instance.dialogPanel.StartDialog(instance.dialogTextDictionary[index], callback);
    }
}