using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace HashCheck
{
   

    public partial class frmHashCheck : Form
    {
        public frmHashCheck()
        {
            InitializeComponent();
            this.AllowDrop = true;
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

                ListViewItem listItem = new ListViewItem(fileName);
                listItem.ImageKey = fileName; 
                lvFiles.Items.Add(listItem);
            }

            lvFiles.SmallImageList = imageList1;
        }

        public HashValues CalculateHashes(string filePath)
        {
            HashValues hashes = new HashValues();

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            using (System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create())
            using (System.Security.Cryptography.SHA256 sha256 = System.Security.Cryptography.SHA256.Create())
            using (FileStream stream = File.OpenRead(filePath))
            {
                byte[] md5Hash = md5.ComputeHash(stream);
                byte[] sha1Hash = sha1.ComputeHash(stream);
                byte[] sha256Hash = sha256.ComputeHash(stream);
                //UInt32 crc32HashInt = Crc32Calculator.CalculateCrc32Int(filePath);
                string crc32HashHex = Crc32Calculator.CalculateCrc32Hex(filePath);

                //hashes.MD5 = BitConverter.ToString(md5Hash).Replace("-", "").ToLower();
                hashes.MD5 = BitConverter.ToString(md5Hash).ToLower();
                //hashes.SHA1 = BitConverter.ToString(sha1Hash).Replace("-", "").ToLower();
                hashes.SHA1 = BitConverter.ToString(sha1Hash).ToLower();
                //hashes.SHA256 = BitConverter.ToString(sha256Hash).Replace("-", "").ToLower();
                hashes.SHA256 = BitConverter.ToString(sha256Hash).ToLower();
                //hashes.CRC32Int = crc32HashInt;
                hashes.CRC32Hex = crc32HashHex;
            }

            return hashes;
        }

        private void btnHash_Click(object sender, EventArgs e)
        {
            lvResults.Clear();
            
            //string[] columnNames = { "File Name", "MD5", "SHA1", "SHA256", "CRC32Int", "CRC32Hex" };
            string[] columnNames = { "FILENAME", "MD5", "SHA1", "SHA256", "CRC32" };

            foreach (string columnName in columnNames)
            {
                ColumnHeader header = new ColumnHeader();
                header.Text = columnName;
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
                //string crc32int = hashes.CRC32Int.ToString();
                string crc32hex = hashes.CRC32Hex.ToString();

                ListViewItem resultItem = new ListViewItem(fileName);
                resultItem.SubItems.Add(md5);
                resultItem.SubItems.Add(sha1);
                resultItem.SubItems.Add(sha256);
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
        //public UInt32 CRC32Int { get; set; }
        public string CRC32Hex { get; set; }
    }
}
