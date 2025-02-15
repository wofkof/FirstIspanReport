using System;
using System.Drawing;
using System.Windows.Forms;

namespace 這是扭蛋機系統.Helpers
{
    public static class ButtonStyleHelper
    {
        public static void ApplyHoverEffect(Button button, Color hoverBackColor, Color hoverForeColor, Color normalBackColor, Color normalForeColor)
        {
            // ✅ 滑鼠懸停
            button.MouseMove += (sender, e) =>
            {
                button.BackColor = hoverBackColor;
                button.ForeColor = hoverForeColor;
            };

            // ✅ 滑鼠離開
            button.MouseLeave += (sender, e) =>
            {
                button.BackColor = normalBackColor;
                button.ForeColor = normalForeColor;
            };
        }
    }
}
