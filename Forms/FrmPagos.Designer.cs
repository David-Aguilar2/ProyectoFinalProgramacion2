namespace VentasOnline.Forms
{
    partial class FrmPagos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            desktopLabel = new Label();
            logoLabel = new Label();
            panel1 = new Panel();
            lblFechaLabel = new Label();
            cboMetodo = new ComboBox();
            lblMetodoLabel = new Label();
            cboEstado = new ComboBox();
            lblEstadoLabel = new Label();
            lblTotalValor = new Label();
            lblTotalLabel = new Label();
            btnCancelar = new Button();
            btnGuardar = new Button();
            dgvHistorial = new DataGridView();
            lblTitulo = new Label();
            lbltxtFiltro = new Label();
            cboFiltroEstado = new ComboBox();
            lblContadorPendientes = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // desktopLabel
            // 
            desktopLabel.AutoSize = true;
            desktopLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            desktopLabel.ForeColor = Color.FromArgb(87, 97, 107);
            desktopLabel.Location = new Point(553, 9);
            desktopLabel.Name = "desktopLabel";
            desktopLabel.Size = new Size(210, 62);
            desktopLabel.TabIndex = 7;
            desktopLabel.Text = "Desktop";
            // 
            // logoLabel
            // 
            logoLabel.AutoSize = true;
            logoLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            logoLabel.ForeColor = Color.FromArgb(25, 71, 132);
            logoLabel.Location = new Point(310, 9);
            logoLabel.Name = "logoLabel";
            logoLabel.Size = new Size(225, 62);
            logoLabel.TabIndex = 6;
            logoLabel.Text = "SivarPOS";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(lblFechaLabel);
            panel1.Controls.Add(cboMetodo);
            panel1.Controls.Add(lblMetodoLabel);
            panel1.Controls.Add(cboEstado);
            panel1.Controls.Add(lblEstadoLabel);
            panel1.Controls.Add(lblTotalValor);
            panel1.Controls.Add(lblTotalLabel);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnGuardar);
            panel1.Location = new Point(872, 177);
            panel1.Name = "panel1";
            panel1.Size = new Size(326, 387);
            panel1.TabIndex = 5;
            // 
            // lblFechaLabel
            // 
            lblFechaLabel.AutoSize = true;
            lblFechaLabel.ForeColor = Color.DimGray;
            lblFechaLabel.Location = new Point(20, 228);
            lblFechaLabel.Name = "lblFechaLabel";
            lblFechaLabel.Size = new Size(245, 20);
            lblFechaLabel.TabIndex = 15;
            lblFechaLabel.Text = "Fecha de pago: Sin fecha registrada";
            // 
            // cboMetodo
            // 
            cboMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMetodo.FormattingEnabled = true;
            cboMetodo.Location = new Point(130, 166);
            cboMetodo.Name = "cboMetodo";
            cboMetodo.Size = new Size(151, 28);
            cboMetodo.TabIndex = 13;
            // 
            // lblMetodoLabel
            // 
            lblMetodoLabel.AutoSize = true;
            lblMetodoLabel.Location = new Point(20, 169);
            lblMetodoLabel.Name = "lblMetodoLabel";
            lblMetodoLabel.Size = new Size(104, 20);
            lblMetodoLabel.TabIndex = 14;
            lblMetodoLabel.Text = "Método pago:";
            // 
            // cboEstado
            // 
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.FormattingEnabled = true;
            cboEstado.Location = new Point(130, 109);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(151, 28);
            cboEstado.TabIndex = 12;
            // 
            // lblEstadoLabel
            // 
            lblEstadoLabel.AutoSize = true;
            lblEstadoLabel.Location = new Point(20, 112);
            lblEstadoLabel.Name = "lblEstadoLabel";
            lblEstadoLabel.Size = new Size(104, 20);
            lblEstadoLabel.TabIndex = 12;
            lblEstadoLabel.Text = "Nuevo Estado:";
            // 
            // lblTotalValor
            // 
            lblTotalValor.AutoSize = true;
            lblTotalValor.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalValor.ForeColor = Color.FromArgb(0, 122, 204);
            lblTotalValor.Location = new Point(153, 50);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(55, 23);
            lblTotalValor.TabIndex = 11;
            lblTotalValor.Text = "$0.00";
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.AutoSize = true;
            lblTotalLabel.Location = new Point(20, 50);
            lblTotalLabel.Name = "lblTotalLabel";
            lblTotalLabel.Size = new Size(115, 20);
            lblTotalLabel.TabIndex = 10;
            lblTotalLabel.Text = "Monto a cobrar:";
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(55, 119, 202);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(193, 322);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(118, 35);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cerrar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(57, 148, 54);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(20, 322);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(118, 35);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // dgvHistorial
            // 
            dgvHistorial.AllowUserToAddRows = false;
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorial.BackgroundColor = Color.FromArgb(222, 222, 222);
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(55, 55, 55);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvHistorial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(38, 38, 38);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(55, 111, 161);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dgvHistorial.DefaultCellStyle = dataGridViewCellStyle4;
            dgvHistorial.EnableHeadersVisualStyles = false;
            dgvHistorial.GridColor = Color.FromArgb(55, 55, 55);
            dgvHistorial.Location = new Point(22, 177);
            dgvHistorial.MultiSelect = false;
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dgvHistorial.RowHeadersWidth = 51;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.Size = new Size(844, 387);
            dgvHistorial.TabIndex = 4;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(321, 76);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(438, 31);
            lblTitulo.TabIndex = 8;
            lblTitulo.Text = "Gestión de Pagos (Pago Contra Entrega)";
            // 
            // lbltxtFiltro
            // 
            lbltxtFiltro.AutoSize = true;
            lbltxtFiltro.Location = new Point(25, 132);
            lbltxtFiltro.Name = "lbltxtFiltro";
            lbltxtFiltro.Size = new Size(116, 20);
            lbltxtFiltro.TabIndex = 9;
            lbltxtFiltro.Text = "Filtro de Estado:";
            // 
            // cboFiltroEstado
            // 
            cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFiltroEstado.FormattingEnabled = true;
            cboFiltroEstado.Location = new Point(162, 129);
            cboFiltroEstado.Name = "cboFiltroEstado";
            cboFiltroEstado.Size = new Size(151, 28);
            cboFiltroEstado.TabIndex = 10;
            // 
            // lblContadorPendientes
            // 
            lblContadorPendientes.AutoSize = true;
            lblContadorPendientes.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContadorPendientes.ForeColor = Color.FromArgb(217, 83, 79);
            lblContadorPendientes.Location = new Point(358, 133);
            lblContadorPendientes.Name = "lblContadorPendientes";
            lblContadorPendientes.Size = new Size(168, 23);
            lblContadorPendientes.TabIndex = 11;
            lblContadorPendientes.Text = "Pagos Pendientes: 0";
            // 
            // FrmPagos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1210, 576);
            Controls.Add(lblContadorPendientes);
            Controls.Add(cboFiltroEstado);
            Controls.Add(lbltxtFiltro);
            Controls.Add(lblTitulo);
            Controls.Add(desktopLabel);
            Controls.Add(logoLabel);
            Controls.Add(panel1);
            Controls.Add(dgvHistorial);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FrmPagos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestión de Pagos - SivarPOS Desktop";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label desktopLabel;
        private Label logoLabel;
        private Panel panel1;
        private Button btnCancelar;
        private Button btnGuardar;
        private DataGridView dgvHistorial;
        private Label lblTitulo;
        private Label lbltxtFiltro;
        private ComboBox cboFiltroEstado;
        private Label lblContadorPendientes;
        private Label lblTotalValor;
        private Label lblTotalLabel;
        private ComboBox cboEstado;
        private Label lblEstadoLabel;
        private ComboBox cboMetodo;
        private Label lblMetodoLabel;
        private Label lblFechaLabel;
    }
}