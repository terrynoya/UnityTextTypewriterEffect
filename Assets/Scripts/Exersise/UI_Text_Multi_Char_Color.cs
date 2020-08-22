/* ==============================================================================
 * 功能描述：UI_Text_Multi_Char_Color  
 * 创 建 者：jianzhou.yao
 * 创建日期：2020/8/21 21:29:16
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UI_Text_Multi_Char_Color : BaseMeshEffect
{
    [SerializeField]
    private Color _myColor;
    [SerializeField]
    private int _index;
    private UIVertex vertex = new UIVertex();
    public override void ModifyMesh(VertexHelper vh)
    {
        //文本长度
        int charLen = this.GetComponent<Text>().text.Length;
        int vertexIndex = 0;
        for (int i = 0; i < charLen; i++)
        {
            //一个字符有4个顶点构成
            int vCount = 4;
            for (int j = 0; j < vCount; j++)
            {
                if (vertexIndex < vh.currentVertCount)
                {
                    vh.PopulateUIVertex(ref vertex, vertexIndex);
                    Color c = _myColor;
                    c.a = i <= _index ? 1 : 0;
                    vertex.color = c;
                    vh.SetUIVertex(vertex, vertexIndex);
                }
                vertexIndex++;
            }
        }
    }
}