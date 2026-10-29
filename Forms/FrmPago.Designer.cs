namespace VentasOnline.Forms
{
    partial class FrmPago
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelPago;
        private NumericUpDown numIdPago;
        private NumericUpDown numIdPedido;
        private ComboBox cmbMetodo;
        private NumericUpDown numMonto;
        private DateTimePicker dtpFechaPago;
        private ComboBox cmbEstado;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private DataGridView dgvPagos;
        private Label lblIdPago;
        private Label lblIdPedido;
        private Label lblMetodo;
        private Label lblMonto;
        private Label lblFechaPago;
        private Label lblEstado;
        private Label lblTitulo;
        private Label lblMarca;
        private Label lblDesktop;
        private Panel lineaTitulo;
        private DataGridViewTextBoxColumn colIdPago;
        private DataGridViewTextBoxColumn colIdPedido;
        private DataGridViewTextBoxColumn colMetodo;
        private DataGridViewTextBoxColumn colMonto;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colFechaPago;

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
            panelPago = new Panel();
            numIdPago = new NumericUpDown();
            numIdPedido = new NumericUpDown();
            cmbMetodo = new ComboBox();
            numMonto = new NumericUpDown();
            dtpFechaPago = new DateTimePicker();
            cmbEstado = new ComboBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            lblIdPago = new Label();
            lblIdPedido = new Label();
            lblMetodo = new Label();
            lblMonto = new Label();
            lblFechaPago = new Label();
            lblEstado = new Label();
            dgvPagos = new DataGridView();
            colIdPago = new DataGridViewTextBoxColumn();
            colIdPedido = new DataGridViewTextBoxColumn();
            colMetodo = new DataGridViewTextBoxColumn();
            colMonto = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colFechaPago = new DataGridViewTextBoxColumn();
            lblTitulo = new Label();
            lblMarca = new Label();
            lblDesktop = new Label();
            lineaTitulo = new Panel();
            panelPago.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numIdPago).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numIdPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numMonto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPagos).BeginInit();
            SuspendLayout();
            // 
            // panelPago
            // 
            panelPago.BackColor = Color.White;
            panelPago.BorderStyle = BorderStyle.FixedSingle;
            panelPago.Controls.Add(numIdPago);
            panelPago.Controls.Add(numIdPedido);
            panelPago.Controls.Add(cmbMetodo);
            panelPago.Controls.Add(numMonto);
            panelPago.Controls.Add(dtpFechaPago);
            panelPago.Controls.Add(cmbEstado);
            panelPago.Controls.Add(btnNuevo);
            panelPago.Controls.Add(btnGuardar);
            panelPago.Controls.Add(btnEliminar);
            panelPago.Controls.Add(btnCancelar);
            panelPago.Controls.Add(lblIdPago);
            panelPago.Controls.Add(lblIdPedido);
            panelPago.Controls.Add(lblMetodo);
            panelPago.Controls.Add(lblMonto);
            panelPago.Controls.Add(lblFechaPago);
            panelPago.Controls.Add(lblEstado);
            panelPago.Location = new Point(835, 94);
            panelPago.Name = "panelPago";
            panelPago.Size = new Size(270, 516);
            panelPago.TabIndex = 0;
            // 
            // numIdPago
            // 
            numIdPago.Font = new Font("Segoe UI", 10F);
            numIdPago.Location = new Point(14, 35);
            numIdPago.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numIdPago.Name = "numIdPago";
            numIdPago.ReadOnly = true;
            numIdPago.Size = new Size(240, 30);
            numIdPago.TabIndex = 0;
            // 
            // numIdPedido
            // 
            numIdPedido.Font = new Font("Segoe UI", 10F);
            numIdPedido.Location = new Point(14, 94);
            numIdPedido.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numIdPedido.Name = "numIdPedido";
            numIdPedido.Size = new Size(240, 30);
            numIdPedido.TabIndex = 1;
            numIdPedido.ValueChanged += numIdPedido_ValueChanged;
            numIdPedido.KeyDown += numIdPedido_KeyDown;
            numIdPedido.Leave += numIdPedido_Leave;
            // 
            // cmbMetodo
            // 
            cmbMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodo.Font = new Font("Segoe UI", 9F);
            cmbMetodo.FormattingEnabled = true;
            cmbMetodo.Location = new Point(14, 153);
            cmbMetodo.Name = "cmbMetodo";
            cmbMetodo.Size = new Size(240, 28);
            cmbMetodo.TabIndex = 2;
            // 
            // numMonto
            // 
            numMonto.DecimalPlaces = 2;
            numMonto.Font = new Font("Segoe UI", 10F);
            numMonto.Increment = 0.01m;
            numMonto.Location = new Point(14, 212);
            numMonto.Maximum = 99999999.99m;
            numMonto.Name = "numMonto";
            numMonto.Size = new Size(240, 30);
            numMonto.TabIndex = 3;
            numMonto.ThousandsSeparator = true;
            // 
            // dtpFechaPago
            // 
            dtpFechaPago.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpFechaPago.Font = new Font("Segoe UI", 9F);
            dtpFechaPago.Format = DateTimePickerFormat.Custom;
            dtpFechaPago.Location = new Point(14, 271);
            dtpFechaPago.Name = "dtpFechaPago";
            dtpFechaPago.Size = new Size(240, 27);
            dtpFechaPago.TabIndex = 4;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Font = new Font("Segoe UI", 9F);
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(14, 330);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(240, 28);
            cmbEstado.TabIndex = 5;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(57, 148, 54);
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(14, 376);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(116, 40);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(57, 148, 54);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(138, 376);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(116, 40);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(55, 119, 202);
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(14, 427);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(116, 40);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(55, 119, 202);
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(138, 427);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(116, 40);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblIdPago
            // 
            lblIdPago.AutoSize = true;
            lblIdPago.Font = new Font("Segoe UI", 12F);
            lblIdPago.ForeColor = Color.FromArgb(35, 35, 35);
            lblIdPago.Location = new Point(14, 7);
            lblIdPago.Name = "lblIdPago";
            lblIdPago.Size = new Size(81, 28);
            lblIdPago.TabIndex = 10;
            lblIdPago.Text = "ID Pago:";
            // 
            // lblIdPedido
            // 
            lblIdPedido.AutoSize = true;
            lblIdPedido.Font = new Font("Segoe UI", 12F);
            lblIdPedido.ForeColor = Color.FromArgb(35, 35, 35);
            lblIdPedido.Location = new Point(14, 66);
            lblIdPedido.Name = "lblIdPedido";
            lblIdPedido.Size = new Size(101, 28);
            lblIdPedido.TabIndex = 11;
            lblIdPedido.Text = "ID Pedido:";
            // 
            // lblMetodo
            // 
            lblMetodo.AutoSize = true;
            lblMetodo.Font = new Font("Segoe UI", 12F);
            lblMetodo.ForeColor = Color.FromArgb(35, 35, 35);
            lblMetodo.Location = new Point(14, 125);
            lblMetodo.Name = "lblMetodo";
            lblMetodo.Size = new Size(83, 28);
            lblMetodo.TabIndex = 12;
            lblMetodo.Text = "Método:";
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Font = new Font("Segoe UI", 12F);
            lblMonto.ForeColor = Color.FromArgb(35, 35, 35);
            lblMonto.Location = new Point(14, 184);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(74, 28);
            lblMonto.TabIndex = 13;
            lblMonto.Text = "Monto:";
            // 
            // lblFechaPago
            // 
            lblFechaPago.AutoSize = true;
            lblFechaPago.Font = new Font("Segoe UI", 12F);
            lblFechaPago.ForeColor = Color.FromArgb(35, 35, 35);
            lblFechaPago.Location = new Point(14, 243);
            lblFechaPago.Name = "lblFechaPago";
            lblFechaPago.Size = new Size(116, 28);
            lblFechaPago.TabIndex = 14;
            lblFechaPago.Text = "Fecha pago:";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 12F);
            lblEstado.ForeColor = Color.FromArgb(35, 35, 35);
            lblEstado.Location = new Point(14, 302);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(75, 28);
            lblEstado.TabIndex = 15;
            lblEstado.Text = "Estado:";
            // 
            // dgvPagos
            // 
            dgvPagos.AllowUserToAddRows = false;
            dgvPagos.AllowUserToDeleteRows = false;
            dgvPagos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPagos.BackgroundColor = Color.FromArgb(222, 222, 222);
            dgvPagos.ColumnHeadersHeight = 29;
            dgvPagos.Columns.AddRange(new DataGridViewColumn[] { colIdPago, colIdPedido, colMetodo, colMonto, colEstado, colFechaPago });
            dgvPagos.EnableHeadersVisualStyles = false;
            dgvPagos.Location = new Point(21, 180);
            dgvPagos.MultiSelect = false;
            dgvPagos.Name = "dgvPagos";
            dgvPagos.ReadOnly = true;
            dgvPagos.RowHeadersVisible = false;
            dgvPagos.RowHeadersWidth = 51;
            dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPagos.Size = new Size(790, 430);
            dgvPagos.TabIndex = 1;
            dgvPagos.SelectionChanged += dgvPagos_SelectionChanged;
            // 
            // colIdPago
            // 
            colIdPago.HeaderText = "ID Pago";
            colIdPago.MinimumWidth = 6;
            colIdPago.Name = "colIdPago";
            colIdPago.ReadOnly = true;
            // 
            // colIdPedido
            // 
            colIdPedido.HeaderText = "ID Pedido";
            colIdPedido.MinimumWidth = 6;
            colIdPedido.Name = "colIdPedido";
            colIdPedido.ReadOnly = true;
            // 
            // colMetodo
            // 
            colMetodo.HeaderText = "Método";
            colMetodo.MinimumWidth = 6;
            colMetodo.Name = "colMetodo";
            colMetodo.ReadOnly = true;
            // 
            // colMonto
            // 
            colMonto.HeaderText = "Monto";
            colMonto.MinimumWidth = 6;
            colMonto.Name = "colMonto";
            colMonto.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 6;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colFechaPago
            // 
            colFechaPago.HeaderText = "Fecha de pago";
            colFechaPago.MinimumWidth = 6;
            colFechaPago.Name = "colFechaPago";
            colFechaPago.ReadOnly = true;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.FromArgb(222, 222, 222);
            lblTitulo.Font = new Font("Segoe UI", 14F);
            lblTitulo.ForeColor = Color.FromArgb(35, 35, 35);
            lblTitulo.Location = new Point(12, 132);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(305, 32);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Lista de Pagos Registrados";
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblMarca.ForeColor = Color.FromArgb(25, 71, 132);
            lblMarca.Location = new Point(35, 22);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(225, 62);
            lblMarca.TabIndex = 3;
            lblMarca.Text = "SivarPOS";
            // 
            // lblDesktop
            // 
            lblDesktop.AutoSize = true;
            lblDesktop.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblDesktop.ForeColor = Color.FromArgb(87, 97, 107);
            lblDesktop.Location = new Point(266, 22);
            lblDesktop.Name = "lblDesktop";
            lblDesktop.Size = new Size(210, 62);
            lblDesktop.TabIndex = 4;
            lblDesktop.Text = "Desktop";
            // 
            // lineaTitulo
            // 
            lineaTitulo.BackColor = Color.FromArgb(190, 190, 190);
            lineaTitulo.Location = new Point(310, 162);
            lineaTitulo.Name = "lineaTitulo";
            lineaTitulo.Size = new Size(500, 2);
            lineaTitulo.TabIndex = 5;
            // 
            // FrmPago
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1121, 627);
            Controls.Add(lineaTitulo);
            Controls.Add(lblTitulo);
            Controls.Add(lblDesktop);
            Controls.Add(lblMarca);
            Controls.Add(panelPago);
            Controls.Add(dgvPagos);
            Font = new Font("Segoe UI", 9F);
            MaximizeBox = false;
            Name = "FrmPago";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPagos - SivarPOS";
            Load += FrmPago_Load;
            panelPago.ResumeLayout(false);
            panelPago.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numIdPago).EndInit();
            ((System.ComponentModel.ISupportInitialize)numIdPedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)numMonto).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPagos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
