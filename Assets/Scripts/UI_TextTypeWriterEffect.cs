/* ==============================================================================
 * 功能描述：UI_TextTypeWriterEffect  
 * 创 建 者：jianzhou.yao
 * 创建日期：2020/8/20 14:57:50
 * ==============================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Logic
{
    public class UI_TextTypeWriterEffect : BaseMeshEffect
    {

        private Text _lbl;
        private int _textLength;
        private UIVertex vertex = new UIVertex();

        [SerializeField]
        private int _characterIndex;

        private List<TextCharacterInfo> _charInfos = new List<TextCharacterInfo>();

        private bool _isCollectingColorInfo;

        protected override void Awake()
        {
            _lbl = GetComponent<Text>();
            _textLength = _lbl.text.Length;
        }

        private List<UIVertex> stream = new List<UIVertex>();
        private void CollectColorInfo(VertexHelper vh)
        {
            _charInfos.Clear();
            stream.Clear();
            vh.GetUIVertexStream(stream);
            int characterIndex = -1;
            for (int i = 0; i < stream.Count; i++)
            {
                UIVertex v = stream[i];
                if (i % 6 == 0)
                {
                    characterIndex++;
                    _charInfos.Add(new TextCharacterInfo() { Index = characterIndex, Color = v.color });
                }
            }
            Debug.Log("vstream count:" + stream.Count);
        }

        public override void ModifyMesh(VertexHelper vh)
        {
            if (!_isCollectingColorInfo)
            {
                _isCollectingColorInfo = true;
                CollectColorInfo(vh);
            }
            int totalChars = stream.Count / 6;
            int vertexIndex = 0;
            for (int i = 0; i < totalChars; i++)
            {
                if (i < _characterIndex)
                {
                    if (_charInfos != null && _charInfos.Count > 0)
                    {
                        Color c = _charInfos[i].Color;
                        PopulateCharacter(c, vh, ref vertexIndex);
                    }
                }
                else
                {
                    if (_charInfos != null && _charInfos.Count > 0)
                    {
                        Color c = _charInfos[i].Color;
                        c.a = 0;
                        PopulateCharacter(c, vh, ref vertexIndex);
                    }
                }
            }
        }

        private void PopulateCharacter(Color color, VertexHelper vh, ref int vertexIndex)
        {
            for (int j = 0; j < 4; j++)
            {
                if (vertexIndex < vh.currentVertCount)
                {
                    vh.PopulateUIVertex(ref vertex, vertexIndex);
                    vertex.color = color;
                    vh.SetUIVertex(vertex, vertexIndex);
                }
                vertexIndex++;
            }
        }
    }
}
public class TextCharacterInfo
{
    public int Index;
    public Color Color;
}
