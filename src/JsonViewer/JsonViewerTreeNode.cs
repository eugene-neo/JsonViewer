using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Opensource.Json.Viewer
{
    public class JsonViewerTreeNode : TreeNode
    {
        JsonObject _jsonObject;
        List<ICustomTextProvider> _textVisualizers = new List<ICustomTextProvider>();
        List<IJsonVisualizer> _visualizers = new List<IJsonVisualizer>();
        private bool _init;
        private IJsonVisualizer _lastVisualizer;

        public JsonViewerTreeNode(JsonObject jsonObject)
        {
            this._jsonObject = jsonObject;
        }

        public List<ICustomTextProvider> TextVisualizers
        {
            get
            {
                return this._textVisualizers;
            }
        }

        public List<IJsonVisualizer> Visualizers
        {
            get
            {
                return this._visualizers;
            }
        }

        public JsonObject JsonObject
        {
            get
            {
                return this._jsonObject;
            }
        }

        internal bool Initialized
        {
            get
            {
                return this._init;
            }
            set
            {
                this._init = value;
            }
        }

        internal void RefreshText()
        {
            StringBuilder sb = new StringBuilder(this._jsonObject.Text);
            foreach (ICustomTextProvider textVisualizer in this._textVisualizers)
            {
                try
                {
                    string customText = textVisualizer.GetText(this._jsonObject);
                    sb.Append(" (" + customText + ")");
                }
                catch
                {
                    //silently ignore
                }
            }
            string text = sb.ToString();
            if (text != this.Text)
                this.Text = text;
        }

        public IJsonVisualizer LastVisualizer
        {
            get
            {
                return this._lastVisualizer;
            }
            set
            {
                this._lastVisualizer = value;
            }
        }
    }
}