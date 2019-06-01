using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HuffmunCore;

namespace HuffmunApp
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Run();
        }

        public void Run()
        {
            var compressor = new Compressor();

            var fileStream = File.Open("C:\\Users\\varku\\Desktop\\test.txt", FileMode.Open);
            compressor.Compress(fileStream, "compressed");
        }
    }
}
