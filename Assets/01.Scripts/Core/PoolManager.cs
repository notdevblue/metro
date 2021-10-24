using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
    public static Dictionary<string, object> pool = new Dictionary<string, object>();
    public static Dictionary<string, GameObject> prefabDictionary = new Dictionary<string, GameObject>();

    //풀을 하나 새로 만드는 함수 AfterImage, Bullet, Projectile
    public static void CreatePool<T>(GameObject prefab, Transform parent, int count = 5)
    {
        // 어떤 프리팹들로 풀을 만들꺼냐? 해당 프리팹들을 누구의 자식으로? 몇개나 만들꺼냐?
        Queue<T> q = new Queue<T>();
        //풀은 큐로 구현할꺼야. 지정한 T타입의 큐를 만드는거
        for(int i = 0; i < count; i++){
            //해당 큐에 prefab을 지정한 갯수만큼 만들어서 넣어준다.
            GameObject g = GameObject.Instantiate(prefab, parent);
            
            T t = g.GetComponent<T>();
            g.SetActive(false);
            //만들어진 게임오브젝트는 지금당장 쓸게 아니기 때문에 active false로 해놓는다.
            q.Enqueue(t);
        }

        //Type type = typeof(T);
        
        string key = typeof(T).ToString(); 
        //이 key에는 "AfterImage"
        pool.Add(key, q);
        //딕셔너리에 "AfterImage"라는 키 값으로 Queue<AfterImage>를 추가한다.
        prefabDictionary.Add(key, prefab);
        //그리고 추가적으로 더 만들 수도 있으니 prefab도 저장해 둔다.
    }

    //그렇게 만들어진 풀에서 원하는 걸 찾아오는 코드다.
    public static T GetItem<T>() where T : MonoBehaviour
    {
        //만약 T를 AfterImage 찾아오는 걸로 가정하고 설명해보자
        string key = typeof(T).ToString();
        //T에서 "AfterImage"라는 문자열을 뽑아내기 위해서 Type을 가져와서 ToString으로 문자열을 뽑는다.
        T item = null;
        //그다음에 돌려줄 item을 만들어주고 null로 초기화 한다.

        if(pool.ContainsKey(key)){
            //풀에 해당하는 key가 존재하면 
            Queue<T> q = (Queue<T>)pool[key];
            //풀 딕셔너리에서 해당 키로 Queue<AfterImage> 를 가져온다. 이때 object 이므로 형변환한다.
            T firstItem = q.Peek();
            // 큐의 첫번째 아이템을 살펴보기위해 Peek을 써서 가져오고(이때 큐에서 뽑아내지는 않는다)
            if(firstItem.gameObject.activeSelf){
                //해당 아이템이 아직 사용중이라면 큐 전체가 사용중인것으로 판단하고 
                //새롭게 만든다.
                GameObject prefab = prefabDictionary[key];
                //프리팹을 가져와서
                GameObject g = GameObject.Instantiate(prefab, firstItem.transform.parent);
                //다시 생성하고
                item = g.GetComponent<T>();
                //거기서 AfterImage 스크립트만 뽑는다.
            }else {
                //그렇지 않다면 사용중이지 않은거니까 큐에서 빼내서
                item = q.Dequeue(); 
                item.gameObject.SetActive(true);
                //활성화만 시켜준다.
            }
            q.Enqueue(item);
            //다시 큐의 맨 마지막으로 넣어준다.
        }

        return item;
    }    
    
}
