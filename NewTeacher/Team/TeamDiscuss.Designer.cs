namespace NewTeacher
{
    partial class TeamDiscuss
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TeamDiscuss));
            this.onLineListView = new System.Windows.Forms.ListView();
            this.MenuImageList = new System.Windows.Forms.ImageList(this.components);
            this.teamMemList = new System.Windows.Forms.ListView();
            this.cboxTeam = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnCreate = new System.Windows.Forms.Button();
            this.txtCreate = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.memberMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.memDel = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.myGroupBox3 = new SharedForms.MyGroupBox();
            this.myGroupBox1 = new SharedForms.MyGroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddStudent = new System.Windows.Forms.Button();
            this.cboxTeam2 = new System.Windows.Forms.ComboBox();
            this.myGroupBox2 = new SharedForms.MyGroupBox();
            this.panelContent.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.memberMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.myGroupBox3.SuspendLayout();
            this.myGroupBox1.SuspendLayout();
            this.myGroupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.tabControl1);
            this.panelContent.Size = new System.Drawing.Size(843, 493);
            // 
            // onLineListView
            // 
            this.onLineListView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.onLineListView.CheckBoxes = true;
            this.onLineListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.onLineListView.LargeImageList = this.MenuImageList;
            this.onLineListView.Location = new System.Drawing.Point(10, 28);
            this.onLineListView.Margin = new System.Windows.Forms.Padding(2);
            this.onLineListView.Name = "onLineListView";
            this.onLineListView.Size = new System.Drawing.Size(293, 423);
            this.onLineListView.SmallImageList = this.MenuImageList;
            this.onLineListView.TabIndex = 0;
            this.onLineListView.UseCompatibleStateImageBehavior = false;
            this.onLineListView.View = System.Windows.Forms.View.List;
            // 
            // MenuImageList
            // 
            this.MenuImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("MenuImageList.ImageStream")));
            this.MenuImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.MenuImageList.Images.SetKeyName(0, "客户端.png");
            this.MenuImageList.Images.SetKeyName(1, "主机端.png");
            // 
            // teamMemList
            // 
            this.teamMemList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.teamMemList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.teamMemList.LargeImageList = this.MenuImageList;
            this.teamMemList.Location = new System.Drawing.Point(10, 28);
            this.teamMemList.Margin = new System.Windows.Forms.Padding(2);
            this.teamMemList.Name = "teamMemList";
            this.teamMemList.Size = new System.Drawing.Size(298, 423);
            this.teamMemList.SmallImageList = this.MenuImageList;
            this.teamMemList.TabIndex = 0;
            this.teamMemList.UseCompatibleStateImageBehavior = false;
            this.teamMemList.View = System.Windows.Forms.View.List;
            this.teamMemList.MouseDown += new System.Windows.Forms.MouseEventHandler(this.teamMemList_MouseDown);
            // 
            // cboxTeam
            // 
            this.cboxTeam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTeam.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboxTeam.FormattingEnabled = true;
            this.cboxTeam.Location = new System.Drawing.Point(13, 19);
            this.cboxTeam.Margin = new System.Windows.Forms.Padding(2);
            this.cboxTeam.Name = "cboxTeam";
            this.cboxTeam.Size = new System.Drawing.Size(170, 24);
            this.cboxTeam.TabIndex = 0;
            this.cboxTeam.SelectedIndexChanged += new System.EventHandler(this.cboxTeam_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.btnDelete);
            this.groupBox3.Controls.Add(this.btnEdit);
            this.groupBox3.Controls.Add(this.cboxTeam);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 77);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(829, 387);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "编辑删除群组";
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(200, 45);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(130, 22);
            this.btnDelete.TabIndex = 4;
            this.btnDelete.Text = "删除选择的分组";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(200, 19);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(2);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(130, 22);
            this.btnEdit.TabIndex = 3;
            this.btnEdit.Text = "修改选择的分组名";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnCreate
            // 
            this.btnCreate.Location = new System.Drawing.Point(133, 26);
            this.btnCreate.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreate.Name = "btnCreate";
            this.btnCreate.Size = new System.Drawing.Size(61, 22);
            this.btnCreate.TabIndex = 2;
            this.btnCreate.Text = "创建";
            this.btnCreate.UseVisualStyleBackColor = true;
            this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
            // 
            // txtCreate
            // 
            this.txtCreate.Location = new System.Drawing.Point(8, 26);
            this.txtCreate.Margin = new System.Windows.Forms.Padding(2);
            this.txtCreate.Name = "txtCreate";
            this.txtCreate.Size = new System.Drawing.Size(107, 21);
            this.txtCreate.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtCreate);
            this.groupBox4.Controls.Add(this.btnCreate);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 3);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(829, 74);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "创建群组";
            // 
            // memberMenu
            // 
            this.memberMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.memberMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.memDel});
            this.memberMenu.Name = "memberMenu";
            this.memberMenu.Size = new System.Drawing.Size(125, 26);
            // 
            // memDel
            // 
            this.memDel.Name = "memDel";
            this.memDel.Size = new System.Drawing.Size(124, 22);
            this.memDel.Text = "删除成员";
            this.memDel.Click += new System.EventHandler(this.memDel_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(843, 493);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox4);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(835, 467);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "群组";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.tabPage2.Controls.Add(this.myGroupBox3);
            this.tabPage2.Controls.Add(this.myGroupBox1);
            this.tabPage2.Controls.Add(this.myGroupBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(835, 467);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "群组成员";
            // 
            // myGroupBox3
            // 
            this.myGroupBox3.Controls.Add(this.teamMemList);
            this.myGroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myGroupBox3.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myGroupBox3.ForeColor = System.Drawing.Color.White;
            this.myGroupBox3.Location = new System.Drawing.Point(514, 3);
            this.myGroupBox3.Name = "myGroupBox3";
            this.myGroupBox3.Padding = new System.Windows.Forms.Padding(10);
            this.myGroupBox3.Size = new System.Drawing.Size(318, 461);
            this.myGroupBox3.TabIndex = 9;
            this.myGroupBox3.TabStop = false;
            this.myGroupBox3.Text = "群组成员";
            // 
            // myGroupBox1
            // 
            this.myGroupBox1.Controls.Add(this.btnSave);
            this.myGroupBox1.Controls.Add(this.label1);
            this.myGroupBox1.Controls.Add(this.btnAddStudent);
            this.myGroupBox1.Controls.Add(this.cboxTeam2);
            this.myGroupBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.myGroupBox1.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myGroupBox1.ForeColor = System.Drawing.Color.White;
            this.myGroupBox1.Location = new System.Drawing.Point(316, 3);
            this.myGroupBox1.Name = "myGroupBox1";
            this.myGroupBox1.Size = new System.Drawing.Size(198, 461);
            this.myGroupBox1.TabIndex = 8;
            this.myGroupBox1.TabStop = false;
            this.myGroupBox1.Text = "选择群组成员";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.Location = new System.Drawing.Point(6, 304);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(186, 32);
            this.btnSave.TabIndex = 11;
            this.btnSave.Text = "保存群组信息";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 19);
            this.label1.TabIndex = 8;
            this.label1.Text = "选择群组";
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.btnAddStudent.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddStudent.ForeColor = System.Drawing.Color.White;
            this.btnAddStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnAddStudent.Image")));
            this.btnAddStudent.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddStudent.Location = new System.Drawing.Point(6, 184);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(186, 32);
            this.btnAddStudent.TabIndex = 10;
            this.btnAddStudent.Text = "选择学生";
            this.btnAddStudent.UseVisualStyleBackColor = false;
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click_1);
            // 
            // cboxTeam2
            // 
            this.cboxTeam2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxTeam2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboxTeam2.FormattingEnabled = true;
            this.cboxTeam2.Location = new System.Drawing.Point(6, 43);
            this.cboxTeam2.Margin = new System.Windows.Forms.Padding(2);
            this.cboxTeam2.Name = "cboxTeam2";
            this.cboxTeam2.Size = new System.Drawing.Size(189, 24);
            this.cboxTeam2.TabIndex = 6;
            this.cboxTeam2.SelectedIndexChanged += new System.EventHandler(this.cboxTeam2_SelectedIndexChanged);
            // 
            // myGroupBox2
            // 
            this.myGroupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(152)))), ((int)(((byte)(249)))));
            this.myGroupBox2.Controls.Add(this.onLineListView);
            this.myGroupBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.myGroupBox2.Font = new System.Drawing.Font("微软雅黑", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.myGroupBox2.ForeColor = System.Drawing.Color.White;
            this.myGroupBox2.Location = new System.Drawing.Point(3, 3);
            this.myGroupBox2.Name = "myGroupBox2";
            this.myGroupBox2.Padding = new System.Windows.Forms.Padding(10);
            this.myGroupBox2.Size = new System.Drawing.Size(313, 461);
            this.myGroupBox2.TabIndex = 7;
            this.myGroupBox2.TabStop = false;
            this.myGroupBox2.Text = "在线学生";
            // 
            // TeamDiscuss
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(845, 525);
            this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "TeamDiscuss";
            this.ShowInTaskbar = false;
            this.Text = "建立分组";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TeamDiscuss_FormClosing);
            this.Load += new System.EventHandler(this.TeamDiscuss_Load);
            this.Controls.SetChildIndex(this.panelContent, 0);
            this.panelContent.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.memberMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.myGroupBox3.ResumeLayout(false);
            this.myGroupBox1.ResumeLayout(false);
            this.myGroupBox1.PerformLayout();
            this.myGroupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListView onLineListView;
        private System.Windows.Forms.ImageList MenuImageList;
        private System.Windows.Forms.ListView teamMemList;
        private System.Windows.Forms.ComboBox cboxTeam;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnCreate;
        private System.Windows.Forms.TextBox txtCreate;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ContextMenuStrip memberMenu;
        private System.Windows.Forms.ToolStripMenuItem memDel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox cboxTeam2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddStudent;
        private SharedForms.MyGroupBox myGroupBox2;
        private SharedForms.MyGroupBox myGroupBox1;
        private System.Windows.Forms.Button btnSave;
        private SharedForms.MyGroupBox myGroupBox3;
    }
}