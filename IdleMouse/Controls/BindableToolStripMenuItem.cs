using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace IdleMouse.Controls
{
    internal class BindableToolStripMenuItem : ToolStripMenuItem, IBindableComponent
    {
        public BindableToolStripMenuItem() : base() { }

        public BindableToolStripMenuItem(string text) : base(text) { }

        public BindableToolStripMenuItem(Image icon) : base(icon) { }

        public BindableToolStripMenuItem(string text, Image icon) : base(text, icon) { }

        public BindableToolStripMenuItem(string text, Image image, EventHandler onClick) : base(text, image, onClick) { }

        public BindableToolStripMenuItem(string text, Image image, params ToolStripItem[] dropDownItems) : base(text, image, dropDownItems) { }

        public BindableToolStripMenuItem(string text, Image image, EventHandler onClick, Keys shortcutKeys) : base(text, image, onClick, shortcutKeys) { }

        public BindableToolStripMenuItem(string text, Image image, EventHandler onClick, string name) : base(text, image, onClick, name) { }

        private ControlBindingsCollection _DataBindings;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ControlBindingsCollection DataBindings
        {
            get
            {
                if (_DataBindings == null)
                {
                    _DataBindings = new ControlBindingsCollection(this);
                }
                return _DataBindings;
            }
        }

        private BindingContext _BindingContext;
        [Browsable(false)]
        public BindingContext BindingContext
        {
            get
            {
                if (_BindingContext == null)
                {
                    _BindingContext = new BindingContext();
                }
                return _BindingContext;
            }
            set
            {
                _BindingContext = value;
            }
        }
    }
}
