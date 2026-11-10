namespace VentasOnline.Forms
{
    partial class FrmEnvio
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            lblContadorPendientes = new Label();
            cboFiltroEstado = new ComboBox();
            lbltxtFiltro = new Label();
            lblTitulo = new Label();
            desktopLabel = new Label();
            logoLabel = new Label();
            panel1 = new Panel();
            btnGenerarReporte = new Button();
            empresaTransporte = new TextBox();
            txtGuiaTracking = new TextBox();
            lblTracking = new Label();
            cboEstadoEntrega = new ComboBox();
            lblEstadoEntrega = new Label();
            lblEmpresaTransporte = new Label();
            lblTotalValor = new Label();
            lblTotalLabel = new Label();
            btnCancelar = new Button();
            btnGuardar = new Button();
            dgvHistorial = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).BeginInit();
            SuspendLayout();
            // 
            // lblContadorPendientes
            // 
            lblContadorPendientes.AutoSize = true;
            lblContadorPendientes.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContadorPendientes.ForeColor = Color.FromArgb(217, 83, 79);
            lblContadorPendientes.Location = new Point(372, 136);
            lblContadorPendientes.Name = "lblContadorPendientes";
            lblContadorPendientes.Size = new Size(171, 23);
            lblContadorPendientes.TabIndex = 19;
            lblContadorPendientes.Text = "Envios Pendientes: 0";
            // 
            // cboFiltroEstado
            // 
            cboFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboFiltroEstado.FormattingEnabled = true;
            cboFiltroEstado.Location = new Point(176, 132);
            cboFiltroEstado.Name = "cboFiltroEstado";
            cboFiltroEstado.Size = new Size(151, 28);
            cboFiltroEstado.TabIndex = 18;
            // 
            // lbltxtFiltro
            // 
            lbltxtFiltro.AutoSize = true;
            lbltxtFiltro.Location = new Point(39, 135);
            lbltxtFiltro.Name = "lbltxtFiltro";
            lbltxtFiltro.Size = new Size(116, 20);
            lbltxtFiltro.TabIndex = 17;
            lbltxtFiltro.Text = "Filtro de Estado:";
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(424, 71);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(203, 31);
            lblTitulo.TabIndex = 16;
            lblTitulo.Text = "Gestión de Envios";
            // 
            // desktopLabel
            // 
            desktopLabel.AutoSize = true;
            desktopLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            desktopLabel.ForeColor = Color.FromArgb(87, 97, 107);
            desktopLabel.Location = new Point(550, 9);
            desktopLabel.Name = "desktopLabel";
            desktopLabel.Size = new Size(210, 62);
            desktopLabel.TabIndex = 15;
            desktopLabel.Text = "Desktop";
            // 
            // logoLabel
            // 
            logoLabel.AutoSize = true;
            logoLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            logoLabel.ForeColor = Color.FromArgb(25, 71, 132);
            logoLabel.Location = new Point(307, 9);
            logoLabel.Name = "logoLabel";
            logoLabel.Size = new Size(225, 62);
            logoLabel.TabIndex = 14;
            logoLabel.Text = "SivarPOS";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(btnGenerarReporte);
            panel1.Controls.Add(empresaTransporte);
            panel1.Controls.Add(txtGuiaTracking);
            panel1.Controls.Add(lblTracking);
            panel1.Controls.Add(cboEstadoEntrega);
            panel1.Controls.Add(lblEstadoEntrega);
            panel1.Controls.Add(lblEmpresaTransporte);
            panel1.Controls.Add(lblTotalValor);
            panel1.Controls.Add(lblTotalLabel);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnGuardar);
            panel1.Location = new Point(689, 180);
            panel1.Name = "panel1";
            panel1.Size = new Size(342, 387);
            panel1.TabIndex = 13;
            // 
            // btnGenerarReporte
            // 
            btnGenerarReporte.BackColor = Color.FromArgb(57, 148, 54);
            btnGenerarReporte.Cursor = Cursors.Hand;
            btnGenerarReporte.FlatAppearance.BorderSize = 0;
            btnGenerarReporte.FlatStyle = FlatStyle.Flat;
            btnGenerarReporte.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnGenerarReporte.ForeColor = Color.White;
            btnGenerarReporte.Location = new Point(69, 342);
            btnGenerarReporte.Name = "btnGenerarReporte";
            btnGenerarReporte.Size = new Size(202, 35);
            btnGenerarReporte.TabIndex = 19;
            btnGenerarReporte.Text = "Generar reporte";
            btnGenerarReporte.UseVisualStyleBackColor = true;
            // 
            // empresaTransporte
            // 
            empresaTransporte.Location = new Point(169, 87);
            empresaTransporte.Name = "empresaTransporte";
            empresaTransporte.ReadOnly = true;
            empresaTransporte.Size = new Size(151, 27);
            empresaTransporte.TabIndex = 18;
            // 
            // txtGuiaTracking
            // 
            txtGuiaTracking.Location = new Point(129, 210);
            txtGuiaTracking.Name = "txtGuiaTracking";
            txtGuiaTracking.ReadOnly = true;
            txtGuiaTracking.Size = new Size(191, 27);
            txtGuiaTracking.TabIndex = 17;
            // 
            // lblTracking
            // 
            lblTracking.AutoSize = true;
            lblTracking.Location = new Point(7, 212);
            lblTracking.Name = "lblTracking";
            lblTracking.Size = new Size(101, 20);
            lblTracking.TabIndex = 16;
            lblTracking.Text = "Código envío:";
            // 
            // cboEstadoEntrega
            // 
            cboEstadoEntrega.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstadoEntrega.FormattingEnabled = true;
            cboEstadoEntrega.Location = new Point(169, 145);
            cboEstadoEntrega.Name = "cboEstadoEntrega";
            cboEstadoEntrega.Size = new Size(151, 28);
            cboEstadoEntrega.TabIndex = 13;
            // 
            // lblEstadoEntrega
            // 
            lblEstadoEntrega.AutoSize = true;
            lblEstadoEntrega.Location = new Point(7, 148);
            lblEstadoEntrega.Name = "lblEstadoEntrega";
            lblEstadoEntrega.Size = new Size(112, 20);
            lblEstadoEntrega.TabIndex = 14;
            lblEstadoEntrega.Text = "Estado entrega:";
            // 
            // lblEmpresaTransporte
            // 
            lblEmpresaTransporte.AutoSize = true;
            lblEmpresaTransporte.Location = new Point(7, 87);
            lblEmpresaTransporte.Name = "lblEmpresaTransporte";
            lblEmpresaTransporte.Size = new Size(156, 20);
            lblEmpresaTransporte.TabIndex = 12;
            lblEmpresaTransporte.Text = "Empresa transportista:";
            // 
            // lblTotalValor
            // 
            lblTotalValor.AutoSize = true;
            lblTotalValor.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotalValor.ForeColor = Color.FromArgb(0, 122, 204);
            lblTotalValor.Location = new Point(169, 31);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(55, 23);
            lblTotalValor.TabIndex = 11;
            lblTotalValor.Text = "$0.00";
            // 
            // lblTotalLabel
            // 
            lblTotalLabel.AutoSize = true;
            lblTotalLabel.Location = new Point(7, 31);
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
            btnCancelar.Location = new Point(202, 293);
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
            btnGuardar.Location = new Point(21, 293);
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
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(55, 55, 55);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvHistorial.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(38, 38, 38);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(55, 111, 161);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvHistorial.DefaultCellStyle = dataGridViewCellStyle2;
            dgvHistorial.EnableHeadersVisualStyles = false;
            dgvHistorial.GridColor = Color.FromArgb(55, 55, 55);
            dgvHistorial.Location = new Point(4, 180);
            dgvHistorial.MultiSelect = false;
            dgvHistorial.Name = "dgvHistorial";
            dgvHistorial.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgvHistorial.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgvHistorial.RowHeadersWidth = 51;
            dgvHistorial.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHistorial.Size = new Size(679, 387);
            dgvHistorial.TabIndex = 12;
            // 
            // FrmEnvio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1046, 576);
            Controls.Add(lblContadorPendientes);
            Controls.Add(cboFiltroEstado);
            Controls.Add(lbltxtFiltro);
            Controls.Add(lblTitulo);
            Controls.Add(desktopLabel);
            Controls.Add(logoLabel);
            Controls.Add(panel1);
            Controls.Add(dgvHistorial);
            Font = new Font("Segoe UI", 9F);
            MaximizeBox = false;
            Name = "FrmEnvio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEnvios - SivarPOS";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorial).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private Label lblContadorPendientes;
        private ComboBox cboFiltroEstado;
        private Label lbltxtFiltro;
        private Label lblTitulo;
        private Label desktopLabel;
        private Label logoLabel;
        private Panel panel1;
        private ComboBox cboEstadoEntrega;
        private Label lblEstadoEntrega;
        private Label lblEmpresaTransporte;
        private Label lblTotalValor;
        private Label lblTotalLabel;
        private Button btnCancelar;
        private Button btnGuardar;
        private DataGridView dgvHistorial;
        private Label lblTracking;
        private TextBox txtGuiaTracking;
        private TextBox empresaTransporte;
        private Button btnGenerarReporte;
    }
}
