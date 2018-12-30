using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace IdleMouse.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class BindableToolStripButton : ToolStripButton, IBindableComponent
    {
        #region IBindableComponent Members

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

        private ControlBindingsCollection _DataBindings;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Category("Data")]
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

        #endregion
    }
}
