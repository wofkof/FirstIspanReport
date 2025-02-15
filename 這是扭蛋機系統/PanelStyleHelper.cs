using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace 這是扭蛋機系統.Helpers
{
    public static class PanelStyleHelper
    {
        private static readonly Dictionary<Panel, Color> _originalColors = new Dictionary<Panel, Color>();
        private static Panel _selectedPanel = null; // ✅ 記錄當前選中的 Panel

        public static void ApplyHoverEffect(Panel panel, Color hoverBackColor, Color normalBackColor, Color selectedBackColor)
        {
            // ✅ 確保每個 Panel 只被初始化一次
            if (!_originalColors.ContainsKey(panel))
                _originalColors[panel] = normalBackColor;

            panel.MouseEnter += (s, e) =>
            {
                if (panel != _selectedPanel) // ✅ 只有當前沒被選中的 Panel 才變色
                    panel.BackColor = hoverBackColor;
            };

            panel.MouseLeave += (s, e) =>
            {
                if (panel != _selectedPanel) // ✅ 只有當前沒被選中的 Panel 才還原顏色
                    panel.BackColor = _originalColors[panel];
            };

            panel.Click += (s, e) =>
            {
                // ✅ 還原其他 Panel 顏色
                foreach (var p in _originalColors.Keys)
                {
                    p.BackColor = _originalColors[p];
                }

                // ✅ 設定當前選中 Panel
                _selectedPanel = panel;
                panel.BackColor = selectedBackColor;
            };
        }
    }
}
