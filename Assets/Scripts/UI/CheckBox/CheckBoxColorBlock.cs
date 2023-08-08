using System;
using UnityEngine;

namespace UI.CheckBox
{
    [Serializable]
    public struct CheckBoxColorBlock
    {
        public static CheckBoxColorBlock DefaultColorBlock;
        
        public Color normalColor;
        public Color selectedColor;
        public Color highlightedNormalColor;
        public Color highlightedSelectedColor;
        public Color disabledColor;

        static CheckBoxColorBlock()
        {
            DefaultColorBlock = new CheckBoxColorBlock
            {
                normalColor = new Color(255, 255, 255, 255),
                selectedColor = new Color(245, 245, 245, 255),
                highlightedNormalColor = new Color(200, 200, 200, 255),
                highlightedSelectedColor = new Color(245, 245, 245, 255),
                disabledColor = new Color(200, 200, 200, 128)
            };
        }
    }
}