using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RSoft.Componenets.WinForm
{
    [ToolboxItem(true)]
    public class CircularProgressBar : Label
    {
        private System.Windows.Forms.Timer _Tmr;
        private int _MaxValue = 100;
        private int _Value = 0;
        public int ProgressBorder { get; set; } = 3;
        private Color _ProgressColor = Color.Red;
        private Color _ProgressBackColor = Color.LightGray;
        public override ContentAlignment TextAlign => ContentAlignment.MiddleCenter;
        public override Color ForeColor => _ProgressColor;

        private int _LoadingPosition = 0;
        private int LoadingPosition
        {
            get { return _LoadingPosition >= 360 ? _LoadingPosition = 0 : ++_LoadingPosition; }
        }

        public int MaxValue
        {
            get
            {
                return _MaxValue;
            }

            set
            {
                _MaxValue = value;
                Refresh();
            }
        }

        public int Value
        {
            get
            {
                return _Value;
            }

            set
            {
                _Value = value;
                Refresh();
                if (_Value == _MaxValue || _Value == 0)
                    _Tmr.Stop();
                else
                    _Tmr.Start();
            }
        }

        public Color ProgressColor
        {
            get
            {
                return _ProgressColor;
            }

            set
            {
                _ProgressColor = value;
                Refresh();
            }
        }

        public Color ProgressBackColor
        {
            get
            {
                return _ProgressBackColor;
            }

            set
            {
                _ProgressBackColor = value;
                Refresh();
            }
        }

        public CircularProgressBar()
        {
            _Tmr = new System.Windows.Forms.Timer();
            _Tmr.Interval = 50;
            _Tmr.Tick += Tmr_Tick;
        }

        private void Tmr_Tick(object sender, EventArgs e)
        {
            Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rec = new Rectangle(5, 5, Width - 10, Height - 10);

            e.Graphics.DrawArc(new Pen(_ProgressBackColor, ProgressBorder), rec, 90, 360);
            e.Graphics.DrawArc(new Pen(_ProgressColor, ProgressBorder), rec,
                0 + LoadingPosition, 360 * ((float)Value / (float)MaxValue));

            Text = string.Concat("%", 100 * ((float)_Value / (float)_MaxValue));

            base.OnPaint(e);
        }
    }
}
