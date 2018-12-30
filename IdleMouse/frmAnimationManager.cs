using IdleMouse.Controls;
using IdleMouse.Models;
using IdleMouse.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Settings = IdleMouse.Properties.Settings;

namespace IdleMouse
{
    internal partial class frmAnimationManager : Form
    {
        private readonly IdleMovementManager _Manager;

        public frmAnimationManager(IdleMovementManager manager)
        {
            _Manager = manager;
            InitializeComponent();
            toolStripButtonAddPoint.DataBindings.Add(new Binding("Enabled", animationEditorControl, "IsAllowPointEditing"));
            toolStripButtonRemovePoint.DataBindings.Add(new Binding("Enabled", animationEditorControl, "IsAllowPointEditing"));
            toolStripButtonInterpolationModeNormal.DataBindings.Add(new Binding("Enabled", animationEditorControl, "IsAllowPointEditing"));
            toolStripButtonInterpolationModeAlternate.DataBindings.Add(new Binding("Enabled", animationEditorControl, "IsAllowPointEditing"));
            toolStripButtonInterpolationModeRepeat.DataBindings.Add(new Binding("Enabled", animationEditorControl, "IsAllowPointEditing"));

            ToolStripRadioButton.AddRadioCheckedBinding(toolStripButtonInterpolationModeNormal, animationBindingSource, "PathInterpolationMode", InterpolationMode.Normal);
            ToolStripRadioButton.AddRadioCheckedBinding(toolStripButtonInterpolationModeAlternate, animationBindingSource, "PathInterpolationMode", InterpolationMode.Alternate);
            ToolStripRadioButton.AddRadioCheckedBinding(toolStripButtonInterpolationModeRepeat, animationBindingSource, "PathInterpolationMode", InterpolationMode.Repeat);

            trkPreviewSpeed.Value = Settings.Default.IdleAnimationSpeed;

            cmbAnimFunction.DataSource = EnumUtils.GetEnumValueList(typeof(Easings.Functions)).ToList();
            cmbAnimType.DataSource = new Dictionary<Type, string> { { typeof(AnimationPath), "Path" }, { typeof(AnimationEllipse), "Ellipse" } }.ToList();

            animationBindingSource.DataSource = _Manager.Animations;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            animationBindingSource.RemoveCurrent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            animationBindingSource.AddNew();
        }

        private void txtName_Validating(object sender, CancelEventArgs e)
        {
            if (animationBindingSource.List.Cast<Animation>().Where(a => a != animationBindingSource.Current && a.Name == txtName.Text).Any())
            {
                e.Cancel = true;
                txtName.SelectAll();
                errorProvider.SetError(txtName, "Name is already in use");
            }
            else
            {
                errorProvider.SetError(txtName, string.Empty);
            }
        }

        private void animationBindingSource_CurrentItemChanged(object sender, EventArgs e)
        {
            var animation = animationBindingSource.Current as Animation;
            animationEditorControl.Animation = animation;
            grpEditAnimation.Enabled = animation != null;
        }

        private void animationBindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {
            btnRemove.Enabled = animationBindingSource.Count > 0;
        }

        private void toolStripRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var tsrb = sender as ToolStripRadioButton;
            if (tsrb != null && tsrb.Checked) animationEditorControl.Mode = (AnimationEditorMode)tsrb.Tag;
        }

        private void trkPreviewSpeed_ValueChanged(object sender, EventArgs e)
        {
            animationEditorControl.PreviewSpeed = Convert.ToUInt16(trkPreviewSpeed.Value);
        }
    }
}
