using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class RoundedPanel : Panel
{
    private int _cornerRadius = 20;
    private bool _topLeft = true;
    private bool _topRight = true;
    private bool _bottomLeft = true;
    private bool _bottomRight = true;

    public int CornerRadius
    {
        get { return _cornerRadius; }
        set { _cornerRadius = value; this.Invalidate(); }
    }

    public bool TopLeft
    {
        get { return _topLeft; }
        set { _topLeft = value; this.Invalidate(); }
    }

    public bool TopRight
    {
        get { return _topRight; }
        set { _topRight = value; this.Invalidate(); }
    }

    public bool BottomLeft
    {
        get { return _bottomLeft; }
        set { _bottomLeft = value; this.Invalidate(); }
    }

    public bool BottomRight
    {
        get { return _bottomRight; }
        set { _bottomRight = value; this.Invalidate(); }
    }

    public RoundedPanel()
    {
        this.BackColor = Color.White;
        this.Resize += (s, e) => this.Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias; // ✅ 抗鋸齒設定

        GraphicsPath path = new GraphicsPath();
        int arcSize = _cornerRadius * 2;

        if (_topLeft)
            path.AddArc(0, 0, arcSize, arcSize, 180, 90);
        else
            path.AddLine(0, 0, 0, arcSize);

        if (_topRight)
            path.AddArc(Width - arcSize, 0, arcSize, arcSize, 270, 90);
        else
            path.AddLine(Width - arcSize, 0, Width, 0);

        if (_bottomRight)
            path.AddArc(Width - arcSize, Height - arcSize, arcSize, arcSize, 0, 90);
        else
            path.AddLine(Width, Height - arcSize, Width, Height);

        if (_bottomLeft)
            path.AddArc(0, Height - arcSize, arcSize, arcSize, 90, 90);
        else
            path.AddLine(0, Height - arcSize, 0, Height);

        path.CloseFigure();

        // 🔹 設定 `Region` 讓 `Panel` 變成圓角
        this.Region = new Region(path);

        //// 🔹 畫出邊框 (可選)
        //using (Pen pen = new Pen(Color.Gray, 2))
        //{
        //    e.Graphics.DrawPath(pen, path);
        //}
    }
}
