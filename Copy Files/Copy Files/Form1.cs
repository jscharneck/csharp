using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Copy_Files
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
          //  var sourceDirectoryPath = Path.Combine(Environment.CurrentDirectory, @"C:\Users\admin\Documents\BOOKS");
            var sourceDirectoryPath = Path.Combine(Environment.CurrentDirectory, @"C:\localVScache\");
            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine(sourceDirectoryPath);
            var sourceDirectoryInfo = new DirectoryInfo(sourceDirectoryPath);

            var targetDirectoryPath = Path.Combine(Environment.CurrentDirectory, @"D:\Visual Studio 2022\");
            var targetDirectoryInfo = new DirectoryInfo(targetDirectoryPath);

            Console.WriteLine(targetDirectoryPath);

            lblCopyStart.Text = "Copying started at: " + DateTime.Now ;
            Console.WriteLine("Copying files...." + DateTime.Now);
            button1.Enabled = false;

            await Task.Run( () => CopyFiles(sourceDirectoryInfo, targetDirectoryInfo));

            listBox1.Items.Add(listBox1.Items.Count + " files copied.");
            lblCopyEnd.Text = "Copying ended at: " + DateTime.Now;
            Console.WriteLine("Copy Completed @ " + DateTime.Now);
            button1.Enabled = true;
        }

        private void CopyFiles(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            foreach (var file in source.GetFiles())
            {
                //Thread.Sleep(50);
                file.CopyTo(Path.Combine(target.FullName, file.Name), true);         
                
                this.Invoke((MethodInvoker)(() => listBox1.Items.Add(file.Name)));
                this.Invoke((MethodInvoker)(() => listBox1.TopIndex = listBox1.Items.Count - 1));
            }

            foreach (var sourceSubDirectory in source.GetDirectories())
            {
                //Thread.Sleep(50);
                var targetSubDirectory = target.CreateSubdirectory(sourceSubDirectory.Name);
                CopyFiles(sourceSubDirectory, targetSubDirectory);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            //txtBxFolderSelect.Text = openFileDialog1.;  
        }
    }
}
