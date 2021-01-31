
namespace Paint
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.shapesMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ellipseMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.lineMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.circleMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.penMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.colorMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.freeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.clearMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.panel = new System.Windows.Forms.Panel();
            this.logMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.shapesMenu,
            this.penMenu,
            this.freeMenu,
            this.clearMenu,
            this.logMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(689, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // shapesMenu
            // 
            this.shapesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ellipseMenu,
            this.lineMenu,
            this.rectangleMenu,
            this.circleMenu});
            this.shapesMenu.Name = "shapesMenu";
            this.shapesMenu.Size = new System.Drawing.Size(56, 20);
            this.shapesMenu.Text = "Shapes";
            // 
            // ellipseMenu
            // 
            this.ellipseMenu.Name = "ellipseMenu";
            this.ellipseMenu.Size = new System.Drawing.Size(126, 22);
            this.ellipseMenu.Text = "Ellipse";
            // 
            // lineMenu
            // 
            this.lineMenu.Name = "lineMenu";
            this.lineMenu.Size = new System.Drawing.Size(126, 22);
            this.lineMenu.Text = "Line";
            // 
            // rectangleMenu
            // 
            this.rectangleMenu.Name = "rectangleMenu";
            this.rectangleMenu.Size = new System.Drawing.Size(126, 22);
            this.rectangleMenu.Text = "Rectangle";
            // 
            // circleMenu
            // 
            this.circleMenu.Name = "circleMenu";
            this.circleMenu.Size = new System.Drawing.Size(126, 22);
            this.circleMenu.Text = "Circle";
            // 
            // penMenu
            // 
            this.penMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeMenu,
            this.colorMenu});
            this.penMenu.Name = "penMenu";
            this.penMenu.Size = new System.Drawing.Size(39, 20);
            this.penMenu.Text = "Pen";
            // 
            // sizeMenu
            // 
            this.sizeMenu.Name = "sizeMenu";
            this.sizeMenu.Size = new System.Drawing.Size(117, 22);
            this.sizeMenu.Text = "Pen Size";
            // 
            // colorMenu
            // 
            this.colorMenu.Name = "colorMenu";
            this.colorMenu.Size = new System.Drawing.Size(117, 22);
            this.colorMenu.Text = "Color";
            // 
            // freeMenu
            // 
            this.freeMenu.Name = "freeMenu";
            this.freeMenu.Size = new System.Drawing.Size(71, 20);
            this.freeMenu.Text = "Free Draw";
            // 
            // clearMenu
            // 
            this.clearMenu.Name = "clearMenu";
            this.clearMenu.Size = new System.Drawing.Size(46, 20);
            this.clearMenu.Text = "Clear";
            // 
            // panel
            // 
            this.panel.BackColor = System.Drawing.Color.White;
            this.panel.Location = new System.Drawing.Point(0, 27);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(689, 475);
            this.panel.TabIndex = 1;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            this.panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel_MouseDown);
            this.panel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel_MouseMove);
            this.panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_MouseUp);
            // 
            // logMenu
            // 
            this.logMenu.Name = "logMenu";
            this.logMenu.Size = new System.Drawing.Size(39, 20);
            this.logMenu.Text = "Log";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(689, 500);
            this.Controls.Add(this.panel);
            this.Controls.Add(this.menuStrip);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Paint";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem shapesMenu;
        private System.Windows.Forms.ToolStripMenuItem ellipseMenu;
        private System.Windows.Forms.ToolStripMenuItem lineMenu;
        private System.Windows.Forms.ToolStripMenuItem rectangleMenu;
        private System.Windows.Forms.ToolStripMenuItem penMenu;
        private System.Windows.Forms.ToolStripMenuItem sizeMenu;
        private System.Windows.Forms.ToolStripMenuItem colorMenu;
        private System.Windows.Forms.ToolStripMenuItem freeMenu;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.ToolStripMenuItem clearMenu;
        private System.Windows.Forms.ToolStripMenuItem circleMenu;
        private System.Windows.Forms.ToolStripMenuItem logMenu;
    }
}

