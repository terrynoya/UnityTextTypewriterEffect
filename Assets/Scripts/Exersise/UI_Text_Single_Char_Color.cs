/* ==============================================================================
 * 功能描述：UI_Text_Single_Char_Color  
 * 创 建 者：jianzhou.yao
 * 创建日期：2020/8/21 21:01:14
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Single_Char_Color : BaseMeshEffect
{
    [SerializeField]
    private Color _myColor;
    private UIVertex vertex = new UIVertex();
    public override void ModifyMesh(VertexHelper vh)
    {
        int vertexIndex = 0;
        //一个字符有4个顶点构成
        int vCount = 2;
        for (int j = 0; j < vCount; j++)
        {
            if (vertexIndex < vh.currentVertCount)
            {
                vh.PopulateUIVertex(ref vertex, vertexIndex);
                vertex.color = _myColor;
                vh.SetUIVertex(vertex, vertexIndex);
            }
            vertexIndex++;
        }
    }
}