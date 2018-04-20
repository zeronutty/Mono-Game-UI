namespace UiTesting.Source.Forms
{
    partial class PropertiesPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.tb_MousePosX = new System.Windows.Forms.TextBox();
            this.lbl_MousePosX = new System.Windows.Forms.Label();
            this.lbl_MousePosY = new System.Windows.Forms.Label();
            this.tb_MousePosY = new System.Windows.Forms.TextBox();
            this.tc_Main = new System.Windows.Forms.TabControl();
            this.Mouse = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TargetEntity = new System.Windows.Forms.TabPage();
            this.tbl_DragTargetEntity = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_DragTargetEntityText = new System.Windows.Forms.Label();
            this.lbl_DragTargetEntity = new System.Windows.Forms.Label();
            this.tbl_TargetEntity = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_TargetEntityText = new System.Windows.Forms.Label();
            this.lbl_TargetEntity = new System.Windows.Forms.Label();
            this.TP_Events = new System.Windows.Forms.TabPage();
            this.tl_EventsTable = new System.Windows.Forms.TableLayoutPanel();
            this.lbl_OnClick = new System.Windows.Forms.Label();
            this.lbl_OnClickValue = new System.Windows.Forms.Label();
            this.lbl_OnMouseDown = new System.Windows.Forms.Label();
            this.lbl_OnMouseEnter = new System.Windows.Forms.Label();
            this.lbl_OnMouseLeave = new System.Windows.Forms.Label();
            this.lbl_OnMouseReleased = new System.Windows.Forms.Label();
            this.lbl_OnMouseWheelScroll = new System.Windows.Forms.Label();
            this.lbl_OnStartDrag = new System.Windows.Forms.Label();
            this.lbl_OnStopDrag = new System.Windows.Forms.Label();
            this.lbl_OnFocusChange = new System.Windows.Forms.Label();
            this.lbl_OnValueChanged = new System.Windows.Forms.Label();
            this.lbl_WhileDragging = new System.Windows.Forms.Label();
            this.lbl_WhileMouseDown = new System.Windows.Forms.Label();
            this.lbl_WhileMouseHover = new System.Windows.Forms.Label();
            this.lbl_OnMouseDownValue = new System.Windows.Forms.Label();
            this.lbl_OnMouseEnterValue = new System.Windows.Forms.Label();
            this.lbl_OnMouseLeaveValue = new System.Windows.Forms.Label();
            this.lbl_OnMouseReleasedValue = new System.Windows.Forms.Label();
            this.lbl_OnMouseWheelScrollValue = new System.Windows.Forms.Label();
            this.lbl_OnStartDragValue = new System.Windows.Forms.Label();
            this.lbl_OnStopDragValue = new System.Windows.Forms.Label();
            this.lbl_OnFocusChangeValue = new System.Windows.Forms.Label();
            this.lbl_OnValueChangeValue = new System.Windows.Forms.Label();
            this.lbl_WhileDraggingValue = new System.Windows.Forms.Label();
            this.lbl_WhileMouseDownValue = new System.Windows.Forms.Label();
            this.lbl_WhileMouseHoverValue = new System.Windows.Forms.Label();
            this.tc_Main.SuspendLayout();
            this.Mouse.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.TargetEntity.SuspendLayout();
            this.tbl_DragTargetEntity.SuspendLayout();
            this.tbl_TargetEntity.SuspendLayout();
            this.TP_Events.SuspendLayout();
            this.tl_EventsTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_MousePosX
            // 
            this.tb_MousePosX.Location = new System.Drawing.Point(78, 16);
            this.tb_MousePosX.Margin = new System.Windows.Forms.Padding(0);
            this.tb_MousePosX.Name = "tb_MousePosX";
            this.tb_MousePosX.Size = new System.Drawing.Size(40, 20);
            this.tb_MousePosX.TabIndex = 1;
            // 
            // lbl_MousePosX
            // 
            this.lbl_MousePosX.AutoSize = true;
            this.lbl_MousePosX.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_MousePosX.Location = new System.Drawing.Point(3, 19);
            this.lbl_MousePosX.Name = "lbl_MousePosX";
            this.lbl_MousePosX.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbl_MousePosX.Size = new System.Drawing.Size(72, 13);
            this.lbl_MousePosX.TabIndex = 2;
            this.lbl_MousePosX.Text = "Mouse Pos X";
            // 
            // lbl_MousePosY
            // 
            this.lbl_MousePosY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_MousePosY.AutoSize = true;
            this.lbl_MousePosY.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lbl_MousePosY.Location = new System.Drawing.Point(206, 19);
            this.lbl_MousePosY.Name = "lbl_MousePosY";
            this.lbl_MousePosY.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.lbl_MousePosY.Size = new System.Drawing.Size(72, 13);
            this.lbl_MousePosY.TabIndex = 4;
            this.lbl_MousePosY.Text = "Mouse Pos Y";
            // 
            // tb_MousePosY
            // 
            this.tb_MousePosY.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_MousePosY.Location = new System.Drawing.Point(284, 16);
            this.tb_MousePosY.Name = "tb_MousePosY";
            this.tb_MousePosY.Size = new System.Drawing.Size(40, 20);
            this.tb_MousePosY.TabIndex = 3;
            // 
            // tc_Main
            // 
            this.tc_Main.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tc_Main.Controls.Add(this.Mouse);
            this.tc_Main.Controls.Add(this.TargetEntity);
            this.tc_Main.Controls.Add(this.TP_Events);
            this.tc_Main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tc_Main.Location = new System.Drawing.Point(0, 0);
            this.tc_Main.Multiline = true;
            this.tc_Main.Name = "tc_Main";
            this.tc_Main.SelectedIndex = 0;
            this.tc_Main.Size = new System.Drawing.Size(359, 438);
            this.tc_Main.TabIndex = 5;
            // 
            // Mouse
            // 
            this.Mouse.Controls.Add(this.groupBox1);
            this.Mouse.Location = new System.Drawing.Point(4, 25);
            this.Mouse.Name = "Mouse";
            this.Mouse.Padding = new System.Windows.Forms.Padding(10);
            this.Mouse.Size = new System.Drawing.Size(351, 409);
            this.Mouse.TabIndex = 0;
            this.Mouse.Text = "Mouse";
            this.Mouse.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.tb_MousePosX);
            this.groupBox1.Controls.Add(this.tb_MousePosY);
            this.groupBox1.Controls.Add(this.lbl_MousePosY);
            this.groupBox1.Controls.Add(this.lbl_MousePosX);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.groupBox1.Size = new System.Drawing.Size(331, 55);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mouse Position";
            // 
            // TargetEntity
            // 
            this.TargetEntity.Controls.Add(this.tbl_DragTargetEntity);
            this.TargetEntity.Controls.Add(this.tbl_TargetEntity);
            this.TargetEntity.Location = new System.Drawing.Point(4, 25);
            this.TargetEntity.Name = "TargetEntity";
            this.TargetEntity.Padding = new System.Windows.Forms.Padding(6);
            this.TargetEntity.Size = new System.Drawing.Size(351, 409);
            this.TargetEntity.TabIndex = 1;
            this.TargetEntity.Text = "Target Entity";
            this.TargetEntity.ToolTipText = "The Entity that has been targeted";
            this.TargetEntity.UseVisualStyleBackColor = true;
            // 
            // tbl_DragTargetEntity
            // 
            this.tbl_DragTargetEntity.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbl_DragTargetEntity.ColumnCount = 2;
            this.tbl_DragTargetEntity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_DragTargetEntity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_DragTargetEntity.Controls.Add(this.lbl_DragTargetEntityText, 1, 0);
            this.tbl_DragTargetEntity.Controls.Add(this.lbl_DragTargetEntity, 0, 0);
            this.tbl_DragTargetEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbl_DragTargetEntity.Location = new System.Drawing.Point(6, 36);
            this.tbl_DragTargetEntity.Name = "tbl_DragTargetEntity";
            this.tbl_DragTargetEntity.RowCount = 1;
            this.tbl_DragTargetEntity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_DragTargetEntity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_DragTargetEntity.Size = new System.Drawing.Size(339, 30);
            this.tbl_DragTargetEntity.TabIndex = 1;
            // 
            // lbl_DragTargetEntityText
            // 
            this.lbl_DragTargetEntityText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_DragTargetEntityText.AutoSize = true;
            this.lbl_DragTargetEntityText.Location = new System.Drawing.Point(310, 8);
            this.lbl_DragTargetEntityText.Name = "lbl_DragTargetEntityText";
            this.lbl_DragTargetEntityText.Size = new System.Drawing.Size(25, 13);
            this.lbl_DragTargetEntityText.TabIndex = 1;
            this.lbl_DragTargetEntityText.Text = "Null";
            // 
            // lbl_DragTargetEntity
            // 
            this.lbl_DragTargetEntity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_DragTargetEntity.AutoSize = true;
            this.lbl_DragTargetEntity.Location = new System.Drawing.Point(4, 8);
            this.lbl_DragTargetEntity.Name = "lbl_DragTargetEntity";
            this.lbl_DragTargetEntity.Size = new System.Drawing.Size(96, 13);
            this.lbl_DragTargetEntity.TabIndex = 0;
            this.lbl_DragTargetEntity.Text = "Drag Target Entity:";
            // 
            // tbl_TargetEntity
            // 
            this.tbl_TargetEntity.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tbl_TargetEntity.ColumnCount = 2;
            this.tbl_TargetEntity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_TargetEntity.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tbl_TargetEntity.Controls.Add(this.lbl_TargetEntityText, 1, 0);
            this.tbl_TargetEntity.Controls.Add(this.lbl_TargetEntity, 0, 0);
            this.tbl_TargetEntity.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbl_TargetEntity.Location = new System.Drawing.Point(6, 6);
            this.tbl_TargetEntity.Name = "tbl_TargetEntity";
            this.tbl_TargetEntity.RowCount = 1;
            this.tbl_TargetEntity.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tbl_TargetEntity.Size = new System.Drawing.Size(339, 30);
            this.tbl_TargetEntity.TabIndex = 0;
            // 
            // lbl_TargetEntityText
            // 
            this.lbl_TargetEntityText.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.lbl_TargetEntityText.AutoSize = true;
            this.lbl_TargetEntityText.Location = new System.Drawing.Point(310, 10);
            this.lbl_TargetEntityText.Name = "lbl_TargetEntityText";
            this.lbl_TargetEntityText.Size = new System.Drawing.Size(25, 13);
            this.lbl_TargetEntityText.TabIndex = 1;
            this.lbl_TargetEntityText.Text = "Null";
            // 
            // lbl_TargetEntity
            // 
            this.lbl_TargetEntity.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_TargetEntity.AutoSize = true;
            this.lbl_TargetEntity.Location = new System.Drawing.Point(4, 10);
            this.lbl_TargetEntity.Name = "lbl_TargetEntity";
            this.lbl_TargetEntity.Size = new System.Drawing.Size(70, 13);
            this.lbl_TargetEntity.TabIndex = 0;
            this.lbl_TargetEntity.Text = "Target Entity:";
            // 
            // TP_Events
            // 
            this.TP_Events.Controls.Add(this.tl_EventsTable);
            this.TP_Events.Location = new System.Drawing.Point(4, 25);
            this.TP_Events.Name = "TP_Events";
            this.TP_Events.Padding = new System.Windows.Forms.Padding(3);
            this.TP_Events.Size = new System.Drawing.Size(351, 409);
            this.TP_Events.TabIndex = 2;
            this.TP_Events.Text = "Events";
            this.TP_Events.UseVisualStyleBackColor = true;
            // 
            // tl_EventsTable
            // 
            this.tl_EventsTable.AutoSize = true;
            this.tl_EventsTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tl_EventsTable.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tl_EventsTable.ColumnCount = 2;
            this.tl_EventsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 74.7093F));
            this.tl_EventsTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.2907F));
            this.tl_EventsTable.Controls.Add(this.lbl_OnClick, 0, 0);
            this.tl_EventsTable.Controls.Add(this.lbl_OnClickValue, 1, 0);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseDown, 0, 1);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseEnter, 0, 2);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseLeave, 0, 3);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseReleased, 0, 4);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseWheelScroll, 0, 5);
            this.tl_EventsTable.Controls.Add(this.lbl_OnStartDrag, 0, 6);
            this.tl_EventsTable.Controls.Add(this.lbl_OnStopDrag, 0, 7);
            this.tl_EventsTable.Controls.Add(this.lbl_OnFocusChange, 0, 8);
            this.tl_EventsTable.Controls.Add(this.lbl_OnValueChanged, 0, 9);
            this.tl_EventsTable.Controls.Add(this.lbl_WhileDragging, 0, 10);
            this.tl_EventsTable.Controls.Add(this.lbl_WhileMouseDown, 0, 11);
            this.tl_EventsTable.Controls.Add(this.lbl_WhileMouseHover, 0, 12);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseDownValue, 1, 1);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseEnterValue, 1, 2);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseLeaveValue, 1, 3);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseReleasedValue, 1, 4);
            this.tl_EventsTable.Controls.Add(this.lbl_OnMouseWheelScrollValue, 1, 5);
            this.tl_EventsTable.Controls.Add(this.lbl_OnStartDragValue, 1, 6);
            this.tl_EventsTable.Controls.Add(this.lbl_OnStopDragValue, 1, 7);
            this.tl_EventsTable.Controls.Add(this.lbl_OnFocusChangeValue, 1, 8);
            this.tl_EventsTable.Controls.Add(this.lbl_OnValueChangeValue, 1, 9);
            this.tl_EventsTable.Controls.Add(this.lbl_WhileDraggingValue, 1, 10);
            this.tl_EventsTable.Controls.Add(this.lbl_WhileMouseDownValue, 1, 11);
            this.tl_EventsTable.Controls.Add(this.lbl_WhileMouseHoverValue, 1, 12);
            this.tl_EventsTable.Dock = System.Windows.Forms.DockStyle.Top;
            this.tl_EventsTable.Location = new System.Drawing.Point(3, 3);
            this.tl_EventsTable.Name = "tl_EventsTable";
            this.tl_EventsTable.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tl_EventsTable.RowCount = 12;
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tl_EventsTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tl_EventsTable.Size = new System.Drawing.Size(345, 404);
            this.tl_EventsTable.TabIndex = 0;
            // 
            // lbl_OnClick
            // 
            this.lbl_OnClick.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnClick.AutoSize = true;
            this.lbl_OnClick.Location = new System.Drawing.Point(4, 9);
            this.lbl_OnClick.Name = "lbl_OnClick";
            this.lbl_OnClick.Size = new System.Drawing.Size(72, 13);
            this.lbl_OnClick.TabIndex = 0;
            this.lbl_OnClick.Text = "OnClickEvent";
            // 
            // lbl_OnClickValue
            // 
            this.lbl_OnClickValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnClickValue.Location = new System.Drawing.Point(260, 1);
            this.lbl_OnClickValue.Name = "lbl_OnClickValue";
            this.lbl_OnClickValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnClickValue.TabIndex = 1;
            this.lbl_OnClickValue.Text = "Null";
            this.lbl_OnClickValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnMouseDown
            // 
            this.lbl_OnMouseDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnMouseDown.AutoSize = true;
            this.lbl_OnMouseDown.Location = new System.Drawing.Point(4, 40);
            this.lbl_OnMouseDown.Name = "lbl_OnMouseDown";
            this.lbl_OnMouseDown.Size = new System.Drawing.Size(109, 13);
            this.lbl_OnMouseDown.TabIndex = 2;
            this.lbl_OnMouseDown.Text = "OnMouseDownEvent";
            // 
            // lbl_OnMouseEnter
            // 
            this.lbl_OnMouseEnter.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnMouseEnter.AutoSize = true;
            this.lbl_OnMouseEnter.Location = new System.Drawing.Point(4, 71);
            this.lbl_OnMouseEnter.Name = "lbl_OnMouseEnter";
            this.lbl_OnMouseEnter.Size = new System.Drawing.Size(106, 13);
            this.lbl_OnMouseEnter.TabIndex = 3;
            this.lbl_OnMouseEnter.Text = "OnMouseEnterEvent";
            // 
            // lbl_OnMouseLeave
            // 
            this.lbl_OnMouseLeave.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnMouseLeave.AutoSize = true;
            this.lbl_OnMouseLeave.Location = new System.Drawing.Point(4, 102);
            this.lbl_OnMouseLeave.Name = "lbl_OnMouseLeave";
            this.lbl_OnMouseLeave.Size = new System.Drawing.Size(111, 13);
            this.lbl_OnMouseLeave.TabIndex = 4;
            this.lbl_OnMouseLeave.Text = "OnMouseLeaveEvent";
            // 
            // lbl_OnMouseReleased
            // 
            this.lbl_OnMouseReleased.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnMouseReleased.AutoSize = true;
            this.lbl_OnMouseReleased.Location = new System.Drawing.Point(4, 133);
            this.lbl_OnMouseReleased.Name = "lbl_OnMouseReleased";
            this.lbl_OnMouseReleased.Size = new System.Drawing.Size(126, 13);
            this.lbl_OnMouseReleased.TabIndex = 5;
            this.lbl_OnMouseReleased.Text = "OnMouseReleasedEvent";
            // 
            // lbl_OnMouseWheelScroll
            // 
            this.lbl_OnMouseWheelScroll.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnMouseWheelScroll.AutoSize = true;
            this.lbl_OnMouseWheelScroll.Location = new System.Drawing.Point(4, 164);
            this.lbl_OnMouseWheelScroll.Name = "lbl_OnMouseWheelScroll";
            this.lbl_OnMouseWheelScroll.Size = new System.Drawing.Size(138, 13);
            this.lbl_OnMouseWheelScroll.TabIndex = 6;
            this.lbl_OnMouseWheelScroll.Text = "OnMouseWheelScrollEvent";
            // 
            // lbl_OnStartDrag
            // 
            this.lbl_OnStartDrag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnStartDrag.AutoSize = true;
            this.lbl_OnStartDrag.Location = new System.Drawing.Point(4, 195);
            this.lbl_OnStartDrag.Name = "lbl_OnStartDrag";
            this.lbl_OnStartDrag.Size = new System.Drawing.Size(94, 13);
            this.lbl_OnStartDrag.TabIndex = 7;
            this.lbl_OnStartDrag.Text = "OnStartDragEvent";
            // 
            // lbl_OnStopDrag
            // 
            this.lbl_OnStopDrag.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnStopDrag.AutoSize = true;
            this.lbl_OnStopDrag.Location = new System.Drawing.Point(4, 226);
            this.lbl_OnStopDrag.Name = "lbl_OnStopDrag";
            this.lbl_OnStopDrag.Size = new System.Drawing.Size(94, 13);
            this.lbl_OnStopDrag.TabIndex = 8;
            this.lbl_OnStopDrag.Text = "OnStopDragEvent";
            // 
            // lbl_OnFocusChange
            // 
            this.lbl_OnFocusChange.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnFocusChange.AutoSize = true;
            this.lbl_OnFocusChange.Location = new System.Drawing.Point(4, 257);
            this.lbl_OnFocusChange.Name = "lbl_OnFocusChange";
            this.lbl_OnFocusChange.Size = new System.Drawing.Size(115, 13);
            this.lbl_OnFocusChange.TabIndex = 9;
            this.lbl_OnFocusChange.Text = "OnFocusChangeEvent";
            // 
            // lbl_OnValueChanged
            // 
            this.lbl_OnValueChanged.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_OnValueChanged.AutoSize = true;
            this.lbl_OnValueChanged.Location = new System.Drawing.Point(4, 288);
            this.lbl_OnValueChanged.Name = "lbl_OnValueChanged";
            this.lbl_OnValueChanged.Size = new System.Drawing.Size(119, 13);
            this.lbl_OnValueChanged.TabIndex = 10;
            this.lbl_OnValueChanged.Text = "OnValueChangedEvent";
            // 
            // lbl_WhileDragging
            // 
            this.lbl_WhileDragging.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_WhileDragging.AutoSize = true;
            this.lbl_WhileDragging.Location = new System.Drawing.Point(4, 319);
            this.lbl_WhileDragging.Name = "lbl_WhileDragging";
            this.lbl_WhileDragging.Size = new System.Drawing.Size(105, 13);
            this.lbl_WhileDragging.TabIndex = 11;
            this.lbl_WhileDragging.Text = "WhileDraggingEvent";
            // 
            // lbl_WhileMouseDown
            // 
            this.lbl_WhileMouseDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_WhileMouseDown.AutoSize = true;
            this.lbl_WhileMouseDown.Location = new System.Drawing.Point(4, 350);
            this.lbl_WhileMouseDown.Name = "lbl_WhileMouseDown";
            this.lbl_WhileMouseDown.Size = new System.Drawing.Size(122, 13);
            this.lbl_WhileMouseDown.TabIndex = 12;
            this.lbl_WhileMouseDown.Text = "WhileMouseDownEvent";
            // 
            // lbl_WhileMouseHover
            // 
            this.lbl_WhileMouseHover.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lbl_WhileMouseHover.AutoSize = true;
            this.lbl_WhileMouseHover.Location = new System.Drawing.Point(4, 381);
            this.lbl_WhileMouseHover.Name = "lbl_WhileMouseHover";
            this.lbl_WhileMouseHover.Size = new System.Drawing.Size(123, 13);
            this.lbl_WhileMouseHover.TabIndex = 13;
            this.lbl_WhileMouseHover.Text = "WhileMouseHoverEvent";
            // 
            // lbl_OnMouseDownValue
            // 
            this.lbl_OnMouseDownValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnMouseDownValue.Location = new System.Drawing.Point(260, 32);
            this.lbl_OnMouseDownValue.Name = "lbl_OnMouseDownValue";
            this.lbl_OnMouseDownValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnMouseDownValue.TabIndex = 15;
            this.lbl_OnMouseDownValue.Text = "Null";
            this.lbl_OnMouseDownValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnMouseEnterValue
            // 
            this.lbl_OnMouseEnterValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnMouseEnterValue.Location = new System.Drawing.Point(260, 63);
            this.lbl_OnMouseEnterValue.Name = "lbl_OnMouseEnterValue";
            this.lbl_OnMouseEnterValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnMouseEnterValue.TabIndex = 16;
            this.lbl_OnMouseEnterValue.Text = "Null";
            this.lbl_OnMouseEnterValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnMouseLeaveValue
            // 
            this.lbl_OnMouseLeaveValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnMouseLeaveValue.Location = new System.Drawing.Point(260, 94);
            this.lbl_OnMouseLeaveValue.Name = "lbl_OnMouseLeaveValue";
            this.lbl_OnMouseLeaveValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnMouseLeaveValue.TabIndex = 17;
            this.lbl_OnMouseLeaveValue.Text = "Null";
            this.lbl_OnMouseLeaveValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnMouseReleasedValue
            // 
            this.lbl_OnMouseReleasedValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnMouseReleasedValue.Location = new System.Drawing.Point(260, 125);
            this.lbl_OnMouseReleasedValue.Name = "lbl_OnMouseReleasedValue";
            this.lbl_OnMouseReleasedValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnMouseReleasedValue.TabIndex = 18;
            this.lbl_OnMouseReleasedValue.Text = "Null";
            this.lbl_OnMouseReleasedValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnMouseWheelScrollValue
            // 
            this.lbl_OnMouseWheelScrollValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnMouseWheelScrollValue.Location = new System.Drawing.Point(260, 156);
            this.lbl_OnMouseWheelScrollValue.Name = "lbl_OnMouseWheelScrollValue";
            this.lbl_OnMouseWheelScrollValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnMouseWheelScrollValue.TabIndex = 19;
            this.lbl_OnMouseWheelScrollValue.Text = "Null";
            this.lbl_OnMouseWheelScrollValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnStartDragValue
            // 
            this.lbl_OnStartDragValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnStartDragValue.Location = new System.Drawing.Point(260, 187);
            this.lbl_OnStartDragValue.Name = "lbl_OnStartDragValue";
            this.lbl_OnStartDragValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnStartDragValue.TabIndex = 20;
            this.lbl_OnStartDragValue.Text = "Null";
            this.lbl_OnStartDragValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnStopDragValue
            // 
            this.lbl_OnStopDragValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnStopDragValue.Location = new System.Drawing.Point(260, 218);
            this.lbl_OnStopDragValue.Name = "lbl_OnStopDragValue";
            this.lbl_OnStopDragValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnStopDragValue.TabIndex = 21;
            this.lbl_OnStopDragValue.Text = "Null";
            this.lbl_OnStopDragValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnFocusChangeValue
            // 
            this.lbl_OnFocusChangeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnFocusChangeValue.Location = new System.Drawing.Point(260, 249);
            this.lbl_OnFocusChangeValue.Name = "lbl_OnFocusChangeValue";
            this.lbl_OnFocusChangeValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnFocusChangeValue.TabIndex = 22;
            this.lbl_OnFocusChangeValue.Text = "Null";
            this.lbl_OnFocusChangeValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_OnValueChangeValue
            // 
            this.lbl_OnValueChangeValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_OnValueChangeValue.Location = new System.Drawing.Point(260, 280);
            this.lbl_OnValueChangeValue.Name = "lbl_OnValueChangeValue";
            this.lbl_OnValueChangeValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_OnValueChangeValue.TabIndex = 23;
            this.lbl_OnValueChangeValue.Text = "Null";
            this.lbl_OnValueChangeValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WhileDraggingValue
            // 
            this.lbl_WhileDraggingValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_WhileDraggingValue.Location = new System.Drawing.Point(260, 311);
            this.lbl_WhileDraggingValue.Name = "lbl_WhileDraggingValue";
            this.lbl_WhileDraggingValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_WhileDraggingValue.TabIndex = 24;
            this.lbl_WhileDraggingValue.Text = "Null";
            this.lbl_WhileDraggingValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WhileMouseDownValue
            // 
            this.lbl_WhileMouseDownValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_WhileMouseDownValue.Location = new System.Drawing.Point(260, 342);
            this.lbl_WhileMouseDownValue.Name = "lbl_WhileMouseDownValue";
            this.lbl_WhileMouseDownValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_WhileMouseDownValue.TabIndex = 25;
            this.lbl_WhileMouseDownValue.Text = "Null";
            this.lbl_WhileMouseDownValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_WhileMouseHoverValue
            // 
            this.lbl_WhileMouseHoverValue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_WhileMouseHoverValue.Location = new System.Drawing.Point(260, 373);
            this.lbl_WhileMouseHoverValue.Name = "lbl_WhileMouseHoverValue";
            this.lbl_WhileMouseHoverValue.Size = new System.Drawing.Size(81, 30);
            this.lbl_WhileMouseHoverValue.TabIndex = 26;
            this.lbl_WhileMouseHoverValue.Text = "Null";
            this.lbl_WhileMouseHoverValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PropertiesPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(359, 438);
            this.Controls.Add(this.tc_Main);
            this.Name = "PropertiesPanel";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PropertiesPanel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PropertiesPanel_FormClosing);
            this.tc_Main.ResumeLayout(false);
            this.Mouse.ResumeLayout(false);
            this.Mouse.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.TargetEntity.ResumeLayout(false);
            this.tbl_DragTargetEntity.ResumeLayout(false);
            this.tbl_DragTargetEntity.PerformLayout();
            this.tbl_TargetEntity.ResumeLayout(false);
            this.tbl_TargetEntity.PerformLayout();
            this.TP_Events.ResumeLayout(false);
            this.TP_Events.PerformLayout();
            this.tl_EventsTable.ResumeLayout(false);
            this.tl_EventsTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox tb_MousePosX;
        private System.Windows.Forms.Label lbl_MousePosX;
        private System.Windows.Forms.Label lbl_MousePosY;
        private System.Windows.Forms.TextBox tb_MousePosY;
        private System.Windows.Forms.TabControl tc_Main;
        private System.Windows.Forms.TabPage Mouse;
        private System.Windows.Forms.TabPage TargetEntity;
        private System.Windows.Forms.TableLayoutPanel tbl_DragTargetEntity;
        private System.Windows.Forms.Label lbl_DragTargetEntityText;
        private System.Windows.Forms.Label lbl_DragTargetEntity;
        private System.Windows.Forms.TableLayoutPanel tbl_TargetEntity;
        private System.Windows.Forms.Label lbl_TargetEntityText;
        private System.Windows.Forms.Label lbl_TargetEntity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage TP_Events;
        private System.Windows.Forms.TableLayoutPanel tl_EventsTable;
        private System.Windows.Forms.Label lbl_OnClick;
        private System.Windows.Forms.Label lbl_OnClickValue;
        private System.Windows.Forms.Label lbl_OnMouseDown;
        private System.Windows.Forms.Label lbl_OnMouseEnter;
        private System.Windows.Forms.Label lbl_OnMouseLeave;
        private System.Windows.Forms.Label lbl_OnMouseReleased;
        private System.Windows.Forms.Label lbl_OnMouseWheelScroll;
        private System.Windows.Forms.Label lbl_OnStartDrag;
        private System.Windows.Forms.Label lbl_OnStopDrag;
        private System.Windows.Forms.Label lbl_OnFocusChange;
        private System.Windows.Forms.Label lbl_OnValueChanged;
        private System.Windows.Forms.Label lbl_WhileDragging;
        private System.Windows.Forms.Label lbl_WhileMouseDown;
        private System.Windows.Forms.Label lbl_WhileMouseHover;
        private System.Windows.Forms.Label lbl_OnMouseDownValue;
        private System.Windows.Forms.Label lbl_OnMouseEnterValue;
        private System.Windows.Forms.Label lbl_OnMouseLeaveValue;
        private System.Windows.Forms.Label lbl_OnMouseReleasedValue;
        private System.Windows.Forms.Label lbl_OnMouseWheelScrollValue;
        private System.Windows.Forms.Label lbl_OnStartDragValue;
        private System.Windows.Forms.Label lbl_OnStopDragValue;
        private System.Windows.Forms.Label lbl_OnFocusChangeValue;
        private System.Windows.Forms.Label lbl_OnValueChangeValue;
        private System.Windows.Forms.Label lbl_WhileDraggingValue;
        private System.Windows.Forms.Label lbl_WhileMouseDownValue;
        private System.Windows.Forms.Label lbl_WhileMouseHoverValue;
    }
}