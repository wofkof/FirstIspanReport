using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace 這是扭蛋機系統.Helpers
{
    public static class PanelLabelButtonHelper
    {
        private static readonly Dictionary<Panel, (Color normalPanelColor, Color normalLabelForeColor)> _originalColors = new Dictionary<Panel, (Color normalPanelColor, Color normalLabelForeColor)>();
        private static Panel _selectedPanel = null;

        public static void ApplyEffect(Panel panel, Label label, Color normalPanelColor, Color normalLabelForeColor, Color hoverPanelColor, Color hoverLabelForeColor)
        {
            if (!_originalColors.ContainsKey(panel))
                _originalColors[panel] = (normalPanelColor, normalLabelForeColor);

            panel.BackColor = normalPanelColor;
            label.ForeColor = normalLabelForeColor;
            label.BackColor = normalPanelColor; // 讓 Label 背景與 Panel 相同

            void OnMouseEnter(object s, EventArgs e)
            {
                if (panel != _selectedPanel)
                {
                    panel.BackColor = hoverPanelColor;
                    label.ForeColor = hoverLabelForeColor;
                    label.BackColor = hoverPanelColor;
                }
            }

            void OnMouseLeave(object s, EventArgs e)
            {
                if (panel != _selectedPanel)
                {
                    panel.BackColor = normalPanelColor;
                    label.ForeColor = normalLabelForeColor;
                    label.BackColor = normalPanelColor;
                }
            }

            void OnClick(object s, EventArgs e)
            {
                // **重置所有 Panel 為初始狀態**
                foreach (var p in _originalColors.Keys)
                {
                    p.BackColor = _originalColors[p].normalPanelColor;
                    var lbl = p.Controls.OfType<Label>().FirstOrDefault();
                    if (lbl != null)
                    {
                        lbl.ForeColor = _originalColors[p].normalLabelForeColor;
                        lbl.BackColor = _originalColors[p].normalPanelColor;
                    }
                }

                // **設定當前 Panel 為選中狀態**
                _selectedPanel = panel;
                panel.BackColor = hoverPanelColor;
                label.ForeColor = hoverLabelForeColor;
                label.BackColor = hoverPanelColor;
            }

            panel.MouseEnter += OnMouseEnter;
            panel.MouseLeave += OnMouseLeave;
            panel.Click += OnClick;

            label.MouseEnter += OnMouseEnter;
            label.MouseLeave += OnMouseLeave;
            label.Click += OnClick;
        }
    }
}
