using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;


namespace HashCheck
{


    public partial class frmHashCheck : Form
    {
        public frmHashCheck()
        {
            InitializeComponent();
            UpdateListViewLayout();
            this.AllowDrop = true;
        }

        private void frmHashCheck_Load(object sender, EventArgs e)
        {
            UpdateListViewLayout();
        }

        private void frmHashCheck_Resize(object sender, EventArgs e)
        {
            UpdateListViewLayout();
        }

        private void UpdateListViewLayout()
        {
            int formHeight = this.ClientSize.Height;

            // Set lvFiles size
            lvFiles.Size = new Size(this.ClientSize.Width - 5, formHeight / 2);

            // Set lvFiles location to the top of the form
            lvFiles.Location = new Point(0, 0);

            // Calculate the available height for lvResults after accounting for btnHash
            int availableHeightForLvResults = (formHeight - btnHash.Height) / 2;

            // Set lvResults size
            lvResults.Size = new Size(this.ClientSize.Width - 5, availableHeightForLvResults - 10);

            // Set lvResults location below lvFiles
            lvResults.Location = new Point(0, formHeight / 2);

        }

        private void lvResults_DoubleClick(object sender, EventArgs e)
        {
            StringBuilder csvContent = new StringBuilder();

            int maxNameLength = 0;

            foreach (ListViewItem item in lvResults.Items)
            {
                int nameLength = item.SubItems[0].Text.Length;
                if (nameLength > maxNameLength)
                {
                    maxNameLength = nameLength;
                }
            }

            foreach (ListViewItem item in lvResults.Items)
            {
                csvContent.Append(item.SubItems[0].Text.PadRight(maxNameLength, ' ') + "\t ,"); // Add a tabulation after each comma
                for (int i = 1; i < item.SubItems.Count; i++)
                {
                    csvContent.Append(item.SubItems[i].Text + "\t,\t"); // Add a comma and tabulation after each item
                }
                csvContent.Length -= 3;  // Remove the last comma and two tabulations

                csvContent.Append("\t;" + Environment.NewLine); // Add new line
            }

            string tempFilePath = Path.GetTempFileName().Replace("tmp", "csv");

            try
            {
                File.WriteAllText(tempFilePath, csvContent.ToString());

                Process.Start(tempFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void lvFiles_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void lvFiles_DragDrop(object sender, DragEventArgs e)
        {
            lvFiles.Clear();

            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            imageList1.ImageSize = new Size(16, 16);

            foreach (string filePath in files)
            {
                string fileName = Path.GetFullPath(filePath);

                Icon fileIcon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
                imageList1.Images.Add(fileName, fileIcon);

                ListViewItem listItem = new ListViewItem(fileName)
                {
                    ImageKey = fileName
                };
                lvFiles.Items.Add(listItem);
            }

            lvFiles.SmallImageList = imageList1;
        }

        public HashValues CalculateHashes(string filePath)
        {
            HashValues hashes = new HashValues();

            using (MD5 md5 = MD5.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                hashes.MD5 = BitConverter.ToString(md5.ComputeHash(stream)).ToLower();
            }

            using (SHA1 sha1 = SHA1.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                hashes.SHA1 = BitConverter.ToString(sha1.ComputeHash(stream)).ToLower();
            }

            using (SHA256 sha256 = SHA256.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                hashes.SHA256 = BitConverter.ToString(sha256.ComputeHash(stream)).ToLower();
            }

            using (SHA384 sha384 = SHA384.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                hashes.SHA384 = BitConverter.ToString(sha384.ComputeHash(stream)).ToLower();
            }

            using (SHA512 sha512 = SHA512.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                hashes.SHA512 = BitConverter.ToString(sha512.ComputeHash(stream)).ToLower();
            }

            string crc32HashHex = Crc32Calculator.CalculateCrc32Hex(filePath);
            hashes.CRC32Hex = crc32HashHex;

            return hashes;
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            lvResults.Clear();
            lvResults.FullRowSelect = true;

            //string[] columnNames = { "File Name", "MD5", "SHA1", "SHA256", "CRC32Int", "CRC32Hex" };
            string[] columnNames = { "FILENAME", "MD5", "SHA1", "SHA256", "SHA384", "SHA512", "CRC32" };

            foreach (string columnName in columnNames)
            {
                ColumnHeader header = new ColumnHeader
                {
                    Text = columnName
                };
                lvResults.Columns.Add(header);
            }

            foreach (ListViewItem item in lvFiles.Items)
            {
                string filePath = item.Text;
                string fileName = Path.GetFileName(filePath);

                HashValues hashes = CalculateHashes(filePath);

                string md5 = hashes.MD5;
                string sha1 = hashes.SHA1;
                string sha256 = hashes.SHA256;
                string sha384 = hashes.SHA384;
                string sha512 = hashes.SHA512;
                //string crc32int = hashes.CRC32Int.ToString();
                string crc32hex = hashes.CRC32Hex.ToString();

                ListViewItem resultItem = new ListViewItem(fileName);
                resultItem.SubItems.Add(md5);
                resultItem.SubItems.Add(sha1);
                resultItem.SubItems.Add(sha256);
                resultItem.SubItems.Add(sha384);
                resultItem.SubItems.Add(sha512);
                //resultItem.SubItems.Add(crc32int);
                resultItem.SubItems.Add(crc32hex);

                lvResults.Items.Add(resultItem);
            }
            lvResults.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
        }
    }

    public class HashValues
    {
        public string MD5 { get; set; }
        public string SHA1 { get; set; }
        public string SHA256 { get; set; }
        public string SHA384 { get; set; }
        public string SHA512 { get; set; }
        public string CRC32Hex { get; set; }
    }
}
