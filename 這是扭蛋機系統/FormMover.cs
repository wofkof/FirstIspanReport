using System;
using System.Drawing;
using System.Windows.Forms;

namespace 這是扭蛋機系統.Helpers
{
    public class FormMover
    {
        private bool isWndMove = false;
        private int curr_x, curr_y;
        private Form targetForm;

        public FormMover(Form form)
        {
            this.targetForm = form;
        }

        public void Attach(Control control)
        {
            control.MouseDown += Control_MouseDown;
            control.MouseMove += Control_MouseMove;
            control.MouseUp += Control_MouseUp;
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                curr_x = e.X;
                curr_y = e.Y;
                isWndMove = true;
            }
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isWndMove)
            {
                targetForm.Left += e.X - curr_x;
                targetForm.Top += e.Y - curr_y;
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            isWndMove = false;
        }
    }
}
