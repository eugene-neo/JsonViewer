namespace Opensource.Json.JsonView
{
    using System;
    using System.Windows.Forms;
    using Viewer;
    using System.IO;

    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadFromFile(string fileName)
        {
            var json = File.ReadAllText(fileName);
            this.jsonViewer.ShowTab(Tabs.Viewer);
            this.jsonViewer.Json = json;
        }

        private void LoadFromClipboard()
        {
            var json = Clipboard.GetText();
            if (!string.IsNullOrEmpty(json))
            {
                this.jsonViewer.ShowTab(Tabs.Viewer);
                this.jsonViewer.Json = json;
            }
        }

        private void OpenFileDialog()
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = @"Json files (*.json)|*.json|Text with json files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.InitialDirectory = Application.StartupPath;
            dialog.Title = @"Select a JSON file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.LoadFromFile(dialog.FileName);
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            if (args == null || args.Length == 0)
            {
                return;
            }

            for (int i = 1; i < args.Length; i++)
            {
                string arg = args[i];
                if (arg.Equals("/c", StringComparison.OrdinalIgnoreCase))
                {
                    LoadFromClipboard();
                }
                else if (File.Exists(arg))
                {
                    LoadFromFile(arg);
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.jsonViewer.ShowTab(Tabs.Text);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jsonViewer.SelectsAllTextInTheTextbox();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jsonViewer.DeleteSelectedTextInTheTextbox();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jsonViewer.PasteTextIntoTheTextbox();

        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jsonViewer.CopyTextIntoTheClipboard();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var c = this.jsonViewer.Controls.Find("txtJson", true)[0];
            ((RichTextBox)c).Cut();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var c = this.jsonViewer.Controls.Find("txtJson", true)[0];
            ((RichTextBox)c).Undo();
        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jsonViewer.DisplaysTheFindPrompt();
        }

        /// <summary>
        /// Expands all the nodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Menu item Viewer > Expand All</remarks>
        private void expandAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.jsonViewer.TreeViewJson.BeginUpdate();
            try
            {
                if (this.jsonViewer.TreeViewJson.SelectedNode != null)
                {
                    var topNode = this.jsonViewer.TreeViewJson.TopNode;
                    this.jsonViewer.TreeViewJson.SelectedNode.ExpandAll();
                    this.jsonViewer.TreeViewJson.TopNode = topNode;
                }
            }
            finally
            {
                this.jsonViewer.TreeViewJson.EndUpdate();
            }
        }

        /// <summary>
        /// Copies a node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Menu item Viewer > Copy</remarks>
        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var node = this.jsonViewer.TreeViewJson.SelectedNode;
            if (node != null)
            {
                Clipboard.SetText(node.Text);
            }
        }

        /// <summary>
        /// Copies just the node's value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>Menu item Viewer > Copy Value</remarks>
        /// <!-- JsonViewerTreeNode had to be made public to be accessible here -->
        private void copyValueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var node = this.jsonViewer.TreeViewJson.SelectedNode as JsonViewerTreeNode;
            if (node != null && node.JsonObject.Value != null)
            {
                Clipboard.SetText(node.JsonObject.Value.ToString());
            }
        }
    }
}