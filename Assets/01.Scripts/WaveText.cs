using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaveText : MonoBehaviour
{
    private TMP_Text tmpText;

    Mesh mesh;
    Vector3[] vertices;

    void Awake()
    {
        tmpText = GetComponent<TMP_Text>();    
    }
    void Update()
    {
        tmpText.ForceMeshUpdate();

        mesh = tmpText.mesh;

        vertices = mesh.vertices;

        //for(int i =0; i < vertices.Length; i++)
        //{
        //    Vector3 offset = Wobble(Time.time + 1);
        //    vertices[i] += offset;
        //}

        for(int i= 0; i < tmpText.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = tmpText.textInfo.characterInfo[i];
            if(!c.isVisible)
            {
                continue;
            }
            int idx = c.vertexIndex;
            Vector3 offset = Wobble(Time.time + i);
            for(int j = 0; j<4; j++)
            {
                vertices[idx + j] += offset;
            }
        }

        mesh.vertices = vertices;
        tmpText.canvasRenderer.SetMesh(mesh);
    }

    private Vector2 Wobble(float time)
    {
        float x= Mathf.Sin(time*2) * 3f;
        float y= Mathf.Cos(time*2) * 3f;
        return new Vector2(x, y);
    }
}
