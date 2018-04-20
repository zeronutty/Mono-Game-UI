using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UiTesting.Source.Forms
{
    public enum Events
    {
        None,
        OnClick,
        OnMouseDown,
        OnMouseEnter,
        OnMouseLeave,
        OnMouseReleased,
        OnMouseWheelScroll,
        OnStartDrag,
        OnStopDrag,
        OnFocusChange,
        OnValueChange,
        WhileDragging,
        WhileMouseDown,
        WhileMouseHover
    }

    public partial class PropertiesPanel : Form
    {
        public PropertiesPanel()
        {
            InitializeComponent();

            Init();
        }

        public void Init()
        {
            
        }

        public void SetMousePosText(string Xtext, string Ytext)
        {
            tb_MousePosX.Text = Xtext;
            tb_MousePosY.Text = Ytext;
        }

        public void SetEntityTargets(Entity targetEntity, Entity dragTargetEntity)
        {
            lbl_TargetEntityText.Text = targetEntity != null ? targetEntity.Name : "Null";
            lbl_DragTargetEntityText.Text = dragTargetEntity != null ? dragTargetEntity.Name : "Null";
        }

        public void SetEventEntity(Entity entity, Events events)
        {
            switch (events)
            {
                case Forms.Events.None:
                    break;
                case Forms.Events.OnClick:
                    lbl_OnClickValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnMouseDown:
                    lbl_OnMouseDownValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnMouseEnter:
                    lbl_OnMouseEnterValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnMouseLeave:
                    lbl_OnMouseLeaveValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnMouseReleased:
                    lbl_OnMouseReleasedValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnMouseWheelScroll:
                    lbl_OnMouseWheelScrollValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnStartDrag:
                    lbl_OnStartDragValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnStopDrag:
                    lbl_OnStopDragValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnFocusChange:
                    lbl_OnFocusChangeValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.OnValueChange:
                    lbl_OnValueChangeValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.WhileDragging:
                    lbl_WhileDraggingValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.WhileMouseDown:
                    lbl_WhileMouseDownValue.Text = entity != null ? entity.Name : "Null";
                    break;
                case Forms.Events.WhileMouseHover:
                    lbl_WhileMouseHoverValue.Text = entity != null ? entity.Name : "Null";
                    break;
            }
        }


        private void PropertiesPanel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }
    }
}
