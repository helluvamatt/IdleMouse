using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace IdleMouse.Controls
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripRadioButton : BindableToolStripButton
    {
        private bool _UpdateButtonGroup = true;

        private int _RadioButtonGroupId;
        [Category("Behavior")]
        public int RadioButtonGroupId
        {
            get
            {
                return _RadioButtonGroupId;
            }
            set
            {
                _RadioButtonGroupId = value;
                UpdateGroup();
            }
        }

        private void SetCheckValue(bool checkValue)
        {
            _UpdateButtonGroup = false;
            Checked = checkValue;
            _UpdateButtonGroup = true;
        }

        private void UpdateGroup()
        {
            if (Parent != null)
            {
                int checkedCount = Parent.Items.OfType<ToolStripRadioButton>().Count(x => x.RadioButtonGroupId == RadioButtonGroupId && x.Checked);
                if (checkedCount > 1)
                {
                    Checked = false;
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Checked = true;
        }

        protected override void OnCheckedChanged(EventArgs e)
        {
            if (Parent != null && _UpdateButtonGroup)
            {
                foreach (ToolStripRadioButton radioButton in Parent.Items.OfType<ToolStripRadioButton>())
                {
                    if (radioButton != this && radioButton.RadioButtonGroupId == RadioButtonGroupId)
                    {
                        radioButton.SetCheckValue(false);
                    }
                }
            }
            base.OnCheckedChanged(e);
        }

        public static void AddRadioCheckedBinding<T>(ToolStripRadioButton radio, object dataSource, string dataMember, T trueValue)
        {
            var binding = new Binding(nameof(Checked), dataSource, dataMember, true, DataSourceUpdateMode.OnPropertyChanged);
            binding.Parse += (s, a) => { if ((bool)a.Value) a.Value = trueValue; };
            binding.Format += (s, a) => a.Value = a.Value != null && ((T)a.Value).Equals(trueValue);
            radio.DataBindings.Add(binding);
        }
    }
}
