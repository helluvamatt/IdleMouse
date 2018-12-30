using IdleMouse.Interop.IcoCurAni;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace IdleMouse.Models
{
    internal class CursorItem
    {
        public CursorItem(CursorModel model, bool isDefault)
        {
            Model = model;
            IsDefault = isDefault;
        }

        public CursorModel Model { get; }

        public bool IsDefault { get; }
    }

    internal class CursorManager : INotifyPropertyChanged
    {
        // TODO For system cursor loading:
        //private static readonly string[] REG_KEYS = new string[]
        //{
        //    "AppStarting",
        //    "Arrow",
        //    "Crosshair",
        //    "Hand",
        //    "Help",
        //    "IBeam",
        //    "No",
        //    "NWPen",
        //    "SizeAll",
        //    "SizeNESW",
        //    "SizeNS",
        //    "SizeNWSE",
        //    "SizeWE",
        //    "UpArrow",
        //    "Wait",
        //};

        //private readonly List<CursorModel> _Cursors;

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        #endregion

        public CursorManager()
        {
            Cursors.Add(new CursorItem(null, true));

            // TODO Load system cursors
            // TODO Load custom cursors from persistent storage
        }

        private int _CurrentIndex;
        public int CurrentIndex
        {
            get => _CurrentIndex;
            set
            {
                if (_CurrentIndex != value)
                {
                    _CurrentIndex = value;
                    OnPropertyChanged(nameof(CurrentIndex));
                }
            }
        }

        public BindingList<CursorItem> Cursors { get; } = new BindingList<CursorItem>() { AllowNew = false };

        public int Count => Cursors.Count;

        public CursorModel this[int index] => (index > -1 && index < Cursors.Count) ? Cursors[index].Model : null;

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Cursors.Count) return;
            var removedItem = Cursors[index];
            if (removedItem.IsDefault) return;
            Cursors.RemoveAt(index);
            if (CurrentIndex == index) ClearCurrentCursor();
            OnPropertyChanged(nameof(Count));
        }

        public void LoadAndSetCurrent(string filename)
        {
            var loaded = CursorReader.Load(null, filename);
            Cursors.Add(new CursorItem(loaded, false));
            OnPropertyChanged(nameof(Count));
            CurrentIndex = Count - 1;
        }

        public void SetCurrent(int index)
        {
            if (index > -1 && index < Cursors.Count) CurrentIndex = index;
            else ClearCurrentCursor();
        }

        public void ClearCurrentCursor()
        {
            CurrentIndex = -1;
        }
    }
}
