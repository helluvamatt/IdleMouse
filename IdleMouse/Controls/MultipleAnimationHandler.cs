using IdleMouse.Interop.IcoCurAni;
using IdleMouse.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace IdleMouse.Controls
{
    internal class MultipleAnimationHandler
    {
        private readonly List<AnimationHandler> _AnimationHandlers;

        public MultipleAnimationHandler()
        {
            _AnimationHandlers = new List<AnimationHandler>();
        }

        public event EventHandler<FrameAnimationEventArgs> FrameTick;

        public Image GetCurrentFrameFor(int index)
        {
            if (index < 0 || index >= _AnimationHandlers.Count) return null;
            return _AnimationHandlers[index].GetCurrentFrame();
        }

        protected void OnFrameTick(int index, bool invalidated)
        {
            FrameTick?.Invoke(this, new FrameAnimationEventArgs(index, invalidated));
        }

        private object _DataSource;
        public object DataSource
        {
            get => _DataSource;
            set
            {
                if (_DataSource != value)
                {
                    SetDataSource(value);
                }
            }
        }

        private void SetDataSource(object value)
        {
            if (value != null && !typeof(IBindingList).IsAssignableFrom(value.GetType())) throw new ArgumentException("DataSource must be an IBindingList");

            var oldBinding = (IBindingList)_DataSource;
            if (oldBinding != null) oldBinding.ListChanged -= Binding_ListChanged;
            _DataSource = value;
            var binding = (IBindingList)_DataSource;
            if (binding != null) binding.ListChanged += Binding_ListChanged;
            ResetHandlers();
        }

        private void ResetHandlers()
        {
            foreach (var handler in _AnimationHandlers)
            {
                handler.Dispose();
            }
            _AnimationHandlers.Clear();
            var binding = (IBindingList)_DataSource;
            if (binding == null) return;
            for (int i = 0; i < binding.Count; i++)
            {
                var item = binding[i] as CursorItem;
                _AnimationHandlers.Add(CreateHandler(i, item.Model));
            }
        }

        private void Binding_ListChanged(object sender, ListChangedEventArgs e)
        {
            var binding = (IBindingList)_DataSource;
            if (binding == null) return;
            if (e.ListChangedType == ListChangedType.ItemAdded)
            {
                // Create new and add to collection at index
                var item = binding[e.NewIndex] as CursorItem;
                _AnimationHandlers.Insert(e.NewIndex, CreateHandler(e.NewIndex, item.Model));
            }
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                // Dispose and remove from collection
                _AnimationHandlers[e.NewIndex].Dispose();
                _AnimationHandlers.RemoveAt(e.NewIndex);
            }
            else if (e.ListChangedType == ListChangedType.ItemMoved)
            {
                // Just keep the collection order
                var oldItem = _AnimationHandlers[e.OldIndex];
                _AnimationHandlers.RemoveAt(e.OldIndex);
                _AnimationHandlers.Insert(e.NewIndex, oldItem);
            }
            else if (e.ListChangedType == ListChangedType.ItemChanged)
            {
                // Update the cursor at the specified index
                _AnimationHandlers[e.NewIndex].Cursor = (binding[e.NewIndex] as CursorItem).Model;
            }
            else
            {
                // Just reset everything
                ResetHandlers();
            }
        }

        private AnimationHandler CreateHandler(int i, CursorModel cursor)
        {
            return new AnimationHandler(invalidated => OnFrameTick(i, invalidated)) { Cursor = cursor };
        }
    }

    internal class FrameAnimationEventArgs : EventArgs
    {
        public FrameAnimationEventArgs(int index, bool invalidated)
        {
            Index = index;
            Invalidated = invalidated;
        }

        public int Index { get; }

        public bool Invalidated { get; }
    }
}
