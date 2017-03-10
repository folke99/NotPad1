using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NotePad_Forms
{
    public partial class Form1 : Form
    {
        private string fileName = null;
        private bool isUnsaved = false;
        private bool ignoreTextChangeEvent = false;
        public Form1()
        {
            InitializeComponent();
            UpdateTitle();
        }
        private void UpdateTitle()
        {
            string file;
            if (string.IsNullOrEmpty(fileName))
                file = "Unnamed";
            else
                file = Path.GetFileName(fileName);
            Text = file + " -Notepad";
            if (isUnsaved)
                Text = file + "* -Notepad";
            else
                Text = file + " -Notepad";
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            textBox.Text = string.Empty;
            fileName = null;
            UpdateTitle();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (ignoreTextChangeEvent)
            {
                ignoreTextChangeEvent = false;
                return;
            }

            isUnsaved = true;
            UpdateTitle();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ignoreTextChangeEvent = true;
                textBox.Text = File.ReadAllText(openFileDialog.FileName);
                fileName = openFileDialog.FileName;
                UpdateTitle();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(fileName))
                return;
            File.WriteAllText(fileName, textBox.Text);
        }
    }
}
