using System;
using System.Windows.Forms;

namespace 這是扭蛋機系統.Helpers
{
    public static class ClockHelper
    {
        private static Timer _timer;

        public static void StartClock(Label lblTime)
        {
            if (_timer == null)
            {
                _timer = new Timer();
                _timer.Interval = 1000; // 每秒更新
                _timer.Tick += (s, e) => UpdateClock(lblTime);
                _timer.Start();
            }
            UpdateClock(lblTime); // 立即更新一次
        }

        private static void UpdateClock(Label lblTime)
        {
            // ✅ **顯示 "日期 + 上午/下午 + 12小時制"**
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd tt hh:mm:ss");

            // ✅ **如果你想要 24 小時制**
            // lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd tt HH:mm:ss");
        }
    }
}
