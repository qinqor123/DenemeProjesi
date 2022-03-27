using Microsoft.Win32;
using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO;
using System.Diagnostics;

namespace DenemeProjesi
{
    public partial class AnyOtp : Form
    {
        public AnyOtp()
        {
            InitializeComponent();
        }

        String ID;
        RegistryKey key1;
        string fileName;        
        string seciliDosya;

        //private void ExportRegistryKey(string RegistryKeyPath, string ValueName, string Value, string ExportFileName = "ExportedRegValue.reg")
        //{
        //    string regTemplate = @"Windows Registry Editor Version 5.00\r\n[{0}]\r\n""{1}""=""{2}""";
        //    string regFileContent = string.Format(regTemplate, RegistryKeyPath, ValueName, Value);
        //    File.WriteAllText(ExportFileName, regFileContent);
        //    return true;
        //}
        static void RunRegFile(string s, string dosyaadi)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "reg.exe";
            startInfo.Arguments ="IMPORT " + s;
            Process.Start(startInfo);
        }
        private void altdizinarama(String yol)
        {
            string[] dosya;
            dosya = Directory.GetFiles(yol, "*.reg");
            for (int i = 0; i < 1; i++)
            {
                listBox1.Items.AddRange(dosya);
                if (checkBox1.Checked == true)
                {
                    string[] dizin;
                    dizin = Directory.GetDirectories(yol);
                    for (int s = 0; s < dizin.Length; s++)
                    {
                        altdizinarama(dizin[s]);
                    }
                }
            }
        }
        void griddoldur()
        {
            
                listBox1.Items.Clear();
                string[] dosya;
                String yol = Application.StartupPath;// *1
                dosya = Directory.GetFiles(yol, "*.reg");// *2
                for (int i = 0; i < 1; i++)
                {
                    listBox1.Items.AddRange(dosya);
                    if (checkBox1.Checked == true)// *3
                    {
                        string[] dizin;
                        dizin = Directory.GetDirectories(yol);
                        for (int s = 0; s < dizin.Length; s++)
                        {
                            altdizinarama(dizin[s]);
                        }
                    }
                    if (listBox1.Items.Count == 0)
                    {
                        listBox1.Items.Add("ARANILAN DOSYA TÜRÜ BULUNAMADI!");
                    }
                }
           
        }
        void veriYukle()
        {

            if (listBox1.SelectedItems.Count != 0) { 
                string path = listBox1.Items[listBox1.SelectedIndex].ToString();
                byte[] fileBytes = System.IO.File.ReadAllBytes(@path);
                fileName = Path.GetFileName(path);
                ID = Path.GetFileNameWithoutExtension(path);
                seciliDosya = path;
                key1 = Registry.CurrentUser.OpenSubKey("Software\\AnyOTP");
                if (key1 != null) Registry.CurrentUser.DeleteSubKey("Software\\AnyOTP");
                RunRegFile(path,fileName);
                this.Text = ID;
            } 
            else
            {
                MessageBox.Show("Lütfen bir Dosya seçip sonra yükleme yapınız.");
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            griddoldur();
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            
            if (listBox1.SelectedItems.Count != 0) 
            {
                DialogResult dialogResult = MessageBox.Show("Silmek istedğinizden eminmisiniz.", "Silme İşlemi", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) File.Delete(seciliDosya); 
            }
            else { MessageBox.Show("Lütfen silmek istediğiniz dosyayı seçiniz."); }
            griddoldur();
        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void BtnYukle_Click(object sender, EventArgs e)
        {
            veriYukle();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItems.Count != 0)
            {
                string path = listBox1.Items[listBox1.SelectedIndex].ToString();
                byte[] fileBytes = System.IO.File.ReadAllBytes(@path);
                fileName = Path.GetFileName(path);
                ID = Path.GetFileNameWithoutExtension(path);
                seciliDosya = path;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
