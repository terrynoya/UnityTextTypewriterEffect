using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Test_First_Step : BaseMeshEffect
{
    private List<UIVertex> stream = new List<UIVertex>();
    private UIVertex vertex = new UIVertex();

    [SerializeField]
    private Color _color;

    public override void ModifyMesh(VertexHelper vh)
    {
        stream.Clear();
        vh.GetUIVertexStream(stream);
        int totalChars = stream.Count / 6;
        Debug.Log("totalChars:" + totalChars);
        int vertexIndex = 0;
        for (int i = 0; i < totalChars; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                if (vertexIndex < vh.currentVertCount)
                {
                    vh.PopulateUIVertex(ref vertex, vertexIndex);
                    vertex.color = _color;
                    vh.SetUIVertex(vertex, vertexIndex);
                }
                vertexIndex++;
            }
        }
    }
}
