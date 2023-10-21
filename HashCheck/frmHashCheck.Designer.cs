namespace HashCheck
{
    partial class frmHashCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmHashCheck));
            this.lvFiles = new System.Windows.Forms.ListView();
            this.lvResults = new System.Windows.Forms.ListView();
            this.btnHash = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // lvFiles
            // 
            this.lvFiles.AllowDrop = true;
            this.lvFiles.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvFiles.HideSelection = false;
            this.lvFiles.LargeImageList = this.imageList1;
            this.lvFiles.Location = new System.Drawing.Point(0, 0);
            this.lvFiles.Name = "lvFiles";
            this.lvFiles.Size = new System.Drawing.Size(1458, 249);
            this.lvFiles.SmallImageList = this.imageList1;
            this.lvFiles.TabIndex = 0;
            this.lvFiles.UseCompatibleStateImageBehavior = false;
            this.lvFiles.View = System.Windows.Forms.View.List;
            this.lvFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.lvFiles_DragDrop);
            this.lvFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.lvFiles_DragEnter);
            // 
            // lvResults
            // 
            this.lvResults.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(0, 276);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(1458, 450);
            this.lvResults.TabIndex = 1;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // btnHash
            // 
            this.btnHash.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnHash.Location = new System.Drawing.Point(0, 249);
            this.btnHash.MaximumSize = new System.Drawing.Size(0, 27);
            this.btnHash.Name = "btnHash";
            this.btnHash.Size = new System.Drawing.Size(1458, 27);
            this.btnHash.TabIndex = 2;
            this.btnHash.Text = "Calculate Hashes";
            this.btnHash.UseVisualStyleBackColor = true;
            this.btnHash.Click += new System.EventHandler(this.btnHash_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // frmHashCheck
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1458, 726);
            this.Controls.Add(this.btnHash);
            this.Controls.Add(this.lvResults);
            this.Controls.Add(this.lvFiles);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmHashCheck";
            this.Text = "HASH CHECKSUM";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvFiles;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.Button btnHash;
        private System.Windows.Forms.ImageList imageList1;
    }
}