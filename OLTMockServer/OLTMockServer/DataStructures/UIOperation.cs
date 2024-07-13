using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    public class UIOperation
    {
        public UIOperation(string text)
        {
            enabled = true;
            Id = Guid.NewGuid();
            Text = text;
        }

        private bool enabled;
        public string Text { get; set; }
        public Guid Id { get; set; }
        public EventHandler<UIOperation> OnSelected { get; set; }
        public EventHandler<UIOperation> OnEnableChanged;

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                enabled = value;
                OnEnableChanged?.Invoke(this, this);
            }
        }

        public void RaisOnSelect()
        {
            OnSelected?.Invoke(this, this);
        }
    }
}
