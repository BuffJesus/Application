using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ChocolateBox
{
    public static class ThemeManager
    {
        public static bool IsDarkMode { get; set; } = true;

        public static Color BackgroundColor => IsDarkMode ? Color.FromArgb(25, 25, 25) : Color.FromArgb(240, 240, 240);
        public static Color ForegroundColor => IsDarkMode ? Color.FromArgb(225, 225, 225) : Color.FromArgb(30, 30, 30);
        public static Color ControlColor => IsDarkMode ? Color.FromArgb(40, 40, 40) : Color.White;
        public static Color BorderColor => IsDarkMode ? Color.FromArgb(55, 55, 55) : Color.FromArgb(200, 200, 200);
        public static Color SelectionColor => IsDarkMode ? Color.FromArgb(0, 120, 215) : Color.FromArgb(200, 230, 255);
        public static Color AccentColor => Color.FromArgb(0, 120, 215);

        public static Font MainFont = new Font("Segoe UI", 9f);
        public static Font HeaderFont = new Font("Segoe UI", 10f, FontStyle.Bold);

        public static void ApplyTheme(Control control)
        {
            control.Font = MainFont;

            if (control is Form form)
            {
                form.BackColor = BackgroundColor;
                form.ForeColor = ForegroundColor;
            }
            else if (control is TreeView tv)
            {
                tv.BackColor = ControlColor;
                tv.ForeColor = ForegroundColor;
                tv.LineColor = IsDarkMode ? Color.FromArgb(80, 80, 80) : Color.FromArgb(200, 200, 200);
                tv.BorderStyle = BorderStyle.None;
                tv.HotTracking = true;
            }
            else if (control is ListBox lb)
            {
                lb.BackColor = ControlColor;
                lb.ForeColor = ForegroundColor;
                lb.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ListView lv)
            {
                lv.BackColor = ControlColor;
                lv.ForeColor = ForegroundColor;
                lv.BorderStyle = BorderStyle.None;
                
                // Only use owner draw for Details view to theme the header
                if (lv.View == View.Details)
                {
                    lv.OwnerDraw = true;
                    lv.DrawColumnHeader += (s, e) =>
                    {
                        e.Graphics.FillRectangle(new SolidBrush(ControlColor), e.Bounds);
                        TextRenderer.DrawText(e.Graphics, e.Header.Text, MainFont, e.Bounds, ForegroundColor, TextFormatFlags.VerticalCenter | TextFormatFlags.Left);
                    };
                    lv.DrawItem += (s, e) => { e.DrawDefault = true; };
                    lv.DrawSubItem += (s, e) => { e.DrawDefault = true; };
                }
            }
            else if (control is TextBox tb)
            {
                tb.BackColor = ControlColor;
                tb.ForeColor = ForegroundColor;
                tb.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ComboBox cb)
            {
                cb.BackColor = ControlColor;
                cb.ForeColor = ForegroundColor;
                cb.FlatStyle = FlatStyle.Flat;
            }
            else if (control is Button btn)
            {
                btn.BackColor = ControlColor;
                btn.ForeColor = ForegroundColor;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderColor = BorderColor;
                btn.FlatAppearance.MouseOverBackColor = IsDarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(230, 230, 230);
                btn.FlatAppearance.MouseDownBackColor = AccentColor;
            }
            else if (control is CheckBox cbx)
            {
                cbx.BackColor = Color.Transparent;
                cbx.ForeColor = ForegroundColor;
                cbx.FlatStyle = FlatStyle.Flat;
            }
            else if (control is RadioButton rb)
            {
                rb.BackColor = Color.Transparent;
                rb.ForeColor = ForegroundColor;
                rb.FlatStyle = FlatStyle.Flat;
            }
            else if (control is MenuStrip ms)
            {
                ms.BackColor = BackgroundColor;
                ms.ForeColor = ForegroundColor;
                ms.RenderMode = ToolStripRenderMode.Professional;
                ms.Renderer = new ModernToolStripRenderer();
            }
            else if (control is ToolStrip ts)
            {
                ts.BackColor = BackgroundColor;
                ts.ForeColor = ForegroundColor;
                ts.GripStyle = ToolStripGripStyle.Hidden;
                ts.RenderMode = ToolStripRenderMode.Professional;
                ts.Renderer = new ModernToolStripRenderer();
            }
            else if (control is StatusStrip ss)
            {
                ss.BackColor = AccentColor;
                ss.ForeColor = Color.White;
                ss.SizingGrip = false;
            }
            else if (control is Label l)
            {
                l.BackColor = Color.Transparent;
                l.ForeColor = ForegroundColor;
            }
            else if (control is RichTextBox rtb)
            {
                rtb.BackColor = ControlColor;
                rtb.ForeColor = ForegroundColor;
                rtb.BorderStyle = BorderStyle.None;
            }
            else if (control is Panel p)
            {
                p.BackColor = BackgroundColor;
                p.ForeColor = ForegroundColor;
            }
            else if (control is Splitter s)
            {
                s.BackColor = BorderColor;
            }
            else if (control is TabControl tc)
            {
                tc.Appearance = TabAppearance.Normal;
                tc.BackColor = BackgroundColor;
            }
            else if (control is TabPage tp)
            {
                tp.BackColor = BackgroundColor;
                tp.ForeColor = ForegroundColor;
            }
            else if (control is GroupBox gb)
            {
                gb.ForeColor = AccentColor;
                gb.BackColor = BackgroundColor;
            }
            else if (control is NumericUpDown nud)
            {
                nud.BackColor = ControlColor;
                nud.ForeColor = ForegroundColor;
                nud.BorderStyle = BorderStyle.FixedSingle;
            }
            else if (control is ProgressBar pb)
            {
                pb.BackColor = ControlColor;
                pb.ForeColor = AccentColor;
            }
            else
            {
                control.BackColor = BackgroundColor;
                control.ForeColor = ForegroundColor;
            }

            foreach (Control child in control.Controls)
            {
                ApplyTheme(child);
            }

            if (control is ToolStrip strip)
            {
                foreach (ToolStripItem item in strip.Items)
                {
                    ApplyThemeToItem(item);
                }
            }
        }

        private static void ApplyThemeToItem(ToolStripItem item)
        {
            item.ForeColor = (item.Owner is StatusStrip) ? Color.White : ForegroundColor;
            item.Font = MainFont;

            if (item is ToolStripDropDownItem dropDown)
            {
                foreach (ToolStripItem subItem in dropDown.DropDownItems)
                {
                    ApplyThemeToItem(subItem);
                }
            }
        }

        private class ModernToolStripRenderer : ToolStripProfessionalRenderer
        {
            public ModernToolStripRenderer() : base(new ModernColorTable()) { }

            protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
            {
                e.ArrowColor = ForegroundColor;
                base.OnRenderArrow(e);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                e.TextColor = ForegroundColor;
                base.OnRenderItemText(e);
            }
        }

        private class ModernColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected => IsDarkMode ? Color.FromArgb(60, 60, 60) : Color.FromArgb(230, 230, 230);
            public override Color MenuItemSelectedGradientBegin => MenuItemSelected;
            public override Color MenuItemSelectedGradientEnd => MenuItemSelected;
            public override Color MenuItemBorder => Color.Transparent;
            public override Color MenuBorder => BorderColor;
            public override Color ToolStripDropDownBackground => ControlColor;
            public override Color ImageMarginGradientBegin => ControlColor;
            public override Color ImageMarginGradientEnd => ControlColor;
            public override Color ImageMarginGradientMiddle => ControlColor;
            public override Color SeparatorDark => BorderColor;
            public override Color SeparatorLight => Color.Transparent;
        }
    }
}
