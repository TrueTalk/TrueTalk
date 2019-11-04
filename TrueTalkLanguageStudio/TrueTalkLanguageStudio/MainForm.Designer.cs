using System;

namespace TrueTalk.IrViewer
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if(disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gViewer1 = new Microsoft.Glee.GraphViewerGdi.GViewer();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.listBoxClauses = new System.Windows.Forms.ListBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gViewer2 = new Microsoft.Glee.GraphViewerGdi.GViewer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.textBoxGrammaticalStructureGraphPennString = new System.Windows.Forms.TextBox();
            this.textBoxPhraseStructureGraphPennString = new System.Windows.Forms.TextBox();
            this.originalClause = new System.Windows.Forms.TextBox();
            this.backward = new System.Windows.Forms.Button();
            this.forward = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gViewer1
            // 
            this.gViewer1.AsyncLayout = false;
            this.gViewer1.AutoScroll = true;
            this.gViewer1.BackwardEnabled = false;
            this.gViewer1.Dock = System.Windows.Forms.DockStyle.Left;
            this.gViewer1.EditObjects = false;
            this.gViewer1.ForwardEnabled = false;
            this.gViewer1.Graph = null;
            this.gViewer1.Location = new System.Drawing.Point(0, 0);
            this.gViewer1.MouseHitDistance = 0.05D;
            this.gViewer1.Name = "gViewer1";
            this.gViewer1.NavigationVisible = true;
            this.gViewer1.PanButtonPressed = false;
            this.gViewer1.SaveButtonVisible = true;
            this.gViewer1.Size = new System.Drawing.Size(648, 650);
            this.gViewer1.TabIndex = 2;
            this.gViewer1.ZoomF = 1D;
            this.gViewer1.ZoomFraction = 0.5D;
            this.gViewer1.ZoomWindowThreshold = 0.05D;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "ttd files (*.ttd)|*.ttd|All files (*.*)|*.*";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // listBoxClauses
            // 
            this.listBoxClauses.FormattingEnabled = true;
            this.listBoxClauses.ItemHeight = 20;
            this.listBoxClauses.Location = new System.Drawing.Point(30, 10);
            this.listBoxClauses.Name = "listBoxClauses";
            this.listBoxClauses.Size = new System.Drawing.Size(1300, 64);
            this.listBoxClauses.TabIndex = 4;
            this.listBoxClauses.Click += new System.EventHandler(this.listBoxClauses_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(30, 125);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gViewer1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gViewer2);
            this.splitContainer1.Size = new System.Drawing.Size(1300, 650);
            this.splitContainer1.SplitterDistance = 650;
            this.splitContainer1.TabIndex = 1;
            // 
            // gViewer2
            // 
            this.gViewer2.AsyncLayout = false;
            this.gViewer2.AutoScroll = true;
            this.gViewer2.BackwardEnabled = false;
            this.gViewer2.Dock = System.Windows.Forms.DockStyle.Right;
            this.gViewer2.EditObjects = false;
            this.gViewer2.ForwardEnabled = false;
            this.gViewer2.Graph = null;
            this.gViewer2.Location = new System.Drawing.Point(-2, 0);
            this.gViewer2.MouseHitDistance = 0.05D;
            this.gViewer2.Name = "gViewer2";
            this.gViewer2.NavigationVisible = true;
            this.gViewer2.PanButtonPressed = false;
            this.gViewer2.SaveButtonVisible = true;
            this.gViewer2.Size = new System.Drawing.Size(648, 650);
            this.gViewer2.TabIndex = 3;
            this.gViewer2.ZoomF = 1D;
            this.gViewer2.ZoomFraction = 0.5D;
            this.gViewer2.ZoomWindowThreshold = 0.05D;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Location = new System.Drawing.Point(30, 800);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.textBoxGrammaticalStructureGraphPennString);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.textBoxPhraseStructureGraphPennString);
            this.splitContainer2.Size = new System.Drawing.Size(1300, 250);
            this.splitContainer2.SplitterDistance = 650;
            this.splitContainer2.TabIndex = 5;
            // 
            // textBoxGrammaticalStructureGraphPennString
            // 
            this.textBoxGrammaticalStructureGraphPennString.Location = new System.Drawing.Point(1, 1);
            this.textBoxGrammaticalStructureGraphPennString.Multiline = true;
            this.textBoxGrammaticalStructureGraphPennString.Name = "textBoxGrammaticalStructureGraphPennString";
            this.textBoxGrammaticalStructureGraphPennString.Size = new System.Drawing.Size(648, 248);
            this.textBoxGrammaticalStructureGraphPennString.TabIndex = 6;
            // 
            // textBoxPhraseStructureGraphPennString
            // 
            this.textBoxPhraseStructureGraphPennString.Location = new System.Drawing.Point(1, 1);
            this.textBoxPhraseStructureGraphPennString.Multiline = true;
            this.textBoxPhraseStructureGraphPennString.Name = "textBoxPhraseStructureGraphPennString";
            this.textBoxPhraseStructureGraphPennString.Size = new System.Drawing.Size(648, 248);
            this.textBoxPhraseStructureGraphPennString.TabIndex = 7;
            // 
            // originalClause
            // 
            this.originalClause.Location = new System.Drawing.Point(30, 36);
            this.originalClause.Multiline = true;
            this.originalClause.Name = "originalClause";
            this.originalClause.Size = new System.Drawing.Size(1300, 42);
            this.originalClause.TabIndex = 8;
            // 
            // backward
            // 
            this.backward.Location = new System.Drawing.Point(31, 85);
            this.backward.Name = "backward";
            this.backward.Size = new System.Drawing.Size(647, 34);
            this.backward.TabIndex = 9;
            this.backward.Text = "<";
            this.backward.UseVisualStyleBackColor = true;
            this.backward.Click += new System.EventHandler( this.backward_Click );
            // 
            // forward
            // 
            this.forward.Location = new System.Drawing.Point(685, 85);
            this.forward.Name = "forward";
            this.forward.Size = new System.Drawing.Size(645, 34);
            this.forward.TabIndex = 10;
            this.forward.Text = ">";
            this.forward.UseVisualStyleBackColor = true;
            this.forward.Click += new System.EventHandler(this.forward_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1356, 1074);
            this.Controls.Add(this.forward);
            this.Controls.Add(this.backward);
            this.Controls.Add(this.originalClause);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.listBoxClauses);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(1350, 1000);
            this.Name = "MainForm";
            this.Text = "IR Viewer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Microsoft.Glee.GraphViewerGdi.GViewer gViewer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ListBox listBoxClauses;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox textBoxPhraseStructureGraphPennString;
        private Microsoft.Glee.GraphViewerGdi.GViewer gViewer2;
        private System.Windows.Forms.TextBox textBoxGrammaticalStructureGraphPennString;
        private System.Windows.Forms.TextBox originalClause;
        private System.Windows.Forms.Button backward;
        private System.Windows.Forms.Button forward;
    }
}

