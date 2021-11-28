using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEditor;
using UnityEngine.Tilemaps;
using System.IO;

public class TestMenu : MonoBehaviour
{
    // % ctrl, # shift & alt
    [MenuItem("Tools/GGM %&g")]
    private static void TestGGM()
    {
        GameObject obj = GameObject.Find("ground");
        Tilemap tm = obj.GetComponent<Tilemap>();

        using (StreamWriter writer = File.CreateText("Assets/Resources/data.txt"))
        {
            writer.WriteLine(tm.cellBounds.xMin);
            writer.WriteLine(tm.cellBounds.xMax); 
            writer.WriteLine(tm.cellBounds.yMin);
            writer.WriteLine(tm.cellBounds.yMax);

            for(int y = tm.cellBounds.yMax; y >= tm.cellBounds.yMin; y--)
            {
                for(int x = tm.cellBounds.xMin; x <= tm.cellBounds.xMax; x++)
                {
                    TileBase tb = tm.GetTile(new Vector3Int(x, y, 0));
                    if(tb != null)
                    {
                        writer.Write("1");
                    }
                    else
                    {
                        writer.Write("0");
                    }
                }
                writer.WriteLine();
            }
        }

    }

    // TNC => 사전수요 
    // 서버 쿡앱스 스파인
}
