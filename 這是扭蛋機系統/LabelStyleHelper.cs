using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 這是扭蛋機系統.Helpers
{
    public static class LabelStyleHelper
    {
        private static readonly Dictionary<Label, (Color normalForeColor, Color normalBackColor)> _originalColors = new Dictionary<Label, (Color normalForeColor, Color normalBackColor)>();
        private static Label _selectedLabel = null; // ✅ 記錄當前選中的 Label

        public static void ApplyHoverEffect(Label label, Color hoverForeColor, Color hoverBackColor, Color selectedForeColor, Color selectedBackColor)
        {
            // ✅ 存儲原始顏色（只存一次）
            if (!_originalColors.ContainsKey(label))
                _originalColors[label] = (label.ForeColor, label.BackColor);

            label.MouseEnter += (s, e) =>
            {
                if (label != _selectedLabel) // ✅ 只有未選中的 Label 才變色
                {
                    label.ForeColor = hoverForeColor;
                    label.BackColor = hoverBackColor;
                }
            };

            label.MouseLeave += (s, e) =>
            {
                if (label != _selectedLabel) // ✅ 只有未選中的 Label 才恢復原色
                {
                    var (fore, back) = _originalColors[label];
                    label.ForeColor = fore;
                    label.BackColor = back;
                }
            };

            label.Click += (s, e) =>
            {
                // ✅ 恢復所有 Label 為初始狀態
                foreach (var lbl in _originalColors.Keys)
                {
                    lbl.ForeColor = _originalColors[lbl].normalForeColor;
                    lbl.BackColor = _originalColors[lbl].normalBackColor;
                }

                // ✅ 設定選中狀態
                _selectedLabel = label;
                label.ForeColor = selectedForeColor;
                label.BackColor = selectedBackColor;
            };
        }
    }
}
