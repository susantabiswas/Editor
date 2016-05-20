using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TabbedEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        //For Open option in File->Open 
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Stream stream;

            OpenFileDialog opendialog = new OpenFileDialog();
            if (opendialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((stream = opendialog.OpenFile()) != null)
                {
                    string strfilename = opendialog.FileName;
                    string filetext = File.ReadAllText(strfilename);

                    //For making a temporary page tab so that  a file can be opened in it if no currently opened tabs are there
                    TabPage TempPage = new TabPage("New Document");
                    RichTextBox RichText = new RichTextBox();
                    RichText.Dock = DockStyle.Fill;
                    TempPage.Controls.Add(RichText);
                    tabControl1.Controls.Add(TempPage);

                    GetRichTextBox().Text = filetext;

                    //for showing the filename of the opened file  in the tab name
                    tabControl1.SelectedTab.Text = opendialog.SafeFileName;
                }

            }
        }

        //For Cut,Copy,Paste Operations
        private RichTextBox GetRichTextBox()
        {
            RichTextBox RichText = new RichTextBox();
            RichText = null;
            TabPage Tpage = tabControl1.SelectedTab;
            if (Tpage != null)
            {
                RichText = Tpage.Controls[0] as RichTextBox;
            }

            return RichText;

        }

        //For the Tool strip menu
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TabPage Page = new TabPage("New Document");
            RichTextBox RichText = new RichTextBox();
            RichText.Dock = DockStyle.Fill;
            Page.Controls.Add(RichText);
            tabControl1.Controls.Add(Page);
        }

        //for Cut option in Edit Menu
        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Cut();
        }
        //for Copy option in Edit Menu
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Copy();
        }

        //for Paste option in Edit Menu
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Paste();
        }


        //Close Tab button,closes the currently selected Tab
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                TabPage CurrentTab = tabControl1.SelectedTab;
                tabControl1.TabPages.Remove(CurrentTab);
            }
            catch (Exception ex)
            {

                MessageBox.Show("No Open Tabs to Close");
            }


        }


        //For search button:    searches the text and highlights the searched text
        private void searchb_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                while (index < GetRichTextBox().Text.LastIndexOf(search_txt.Text))
                {
                    GetRichTextBox().Find(search_txt.Text, index, GetRichTextBox().TextLength, RichTextBoxFinds.MatchCase);
                    GetRichTextBox().SelectionBackColor = Color.Yellow;
                    index = GetRichTextBox().Text.IndexOf(search_txt.Text, index) + 1;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("No Currently Open Tabs");
            }
        }


        //For the Save option in File->Save  : saves the file
        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.OK)
            {
                using (Stream s = File.Open(save.FileName, FileMode.CreateNew))
                using (StreamWriter sw = new StreamWriter(s))
                {
                    sw.Write(tabControl1.Text);
                    tabControl1.SelectedTab.Text = Path.GetFileName(save.FileName);
                }
            }
        }


        //For the Select All option
        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().SelectAll();
        }

        //For the Undo option
        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Undo();
        }

        //For the Redo option
        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Redo();
        }

        //For the Clear option
        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().Clear();
        }

        //For the Undo Clear option
        private void undoClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GetRichTextBox().ClearUndo();
        }
    }
}
