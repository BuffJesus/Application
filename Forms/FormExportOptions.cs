using System;
using System.Windows.Forms;
using System.Drawing;

namespace ChocolateBox
{
    public class FormExportOptions : Form
    {
        private ComboBox comboBoxFormat;
        private Label labelFormat;
        private Button buttonOK;
        private Button buttonCancel;

        public ExportFormat SelectedFormat => (ExportFormat)comboBoxFormat.SelectedItem;

        public FormExportOptions()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            
            comboBoxFormat.DataSource = Enum.GetValues(typeof(ExportFormat));
            comboBoxFormat.SelectedIndex = 0;
        }

        private void InitializeComponent()
        {
            this.comboBoxFormat = new ComboBox();
            this.labelFormat = new Label();
            this.buttonOK = new Button();
            this.buttonCancel = new Button();
            this.SuspendLayout();
            // 
            // comboBoxFormat
            // 
            this.comboBoxFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBoxFormat.FormattingEnabled = true;
            this.comboBoxFormat.Location = new Point(12, 32);
            this.comboBoxFormat.Name = "comboBoxFormat";
            this.comboBoxFormat.Size = new Size(260, 21);
            this.comboBoxFormat.TabIndex = 0;
            // 
            // labelFormat
            // 
            this.labelFormat.AutoSize = true;
            this.labelFormat.Location = new Point(12, 13);
            this.labelFormat.Name = "labelFormat";
            this.labelFormat.Size = new Size(79, 13);
            this.labelFormat.TabIndex = 1;
            this.labelFormat.Text = "Export Format:";
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = DialogResult.OK;
            this.buttonOK.Location = new Point(116, 68);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new Size(75, 23);
            this.buttonOK.TabIndex = 2;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = DialogResult.Cancel;
            this.buttonCancel.Location = new Point(197, 68);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new Size(75, 23);
            this.buttonCancel.TabIndex = 3;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // FormExportOptions
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new Size(284, 103);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelFormat);
            this.Controls.Add(this.comboBoxFormat);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExportOptions";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Export Options";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
