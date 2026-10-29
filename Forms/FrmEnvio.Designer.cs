namespace VentasOnline.Forms
{
    partial class FrmEnvio
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelEnvio;
        private NumericUpDown numIdEnvio;
        private NumericUpDown numIdPedido;
        private ComboBox cmbDireccion;
        private ComboBox cmbEmpresa;
        private TextBox txtNumeroGuia;
        private ComboBox cmbEstado;
        private DateTimePicker dtpFechaEnvio;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private DataGridView dgvEnvios;
        private Label lblIdEnvio;
        private Label lblIdPedido;
        private Label lblDireccion;
        private Label lblEmpresa;
        private Label lblNumeroGuia;
        private Label lblEstado;
        private Label lblFechaEnvio;
        private Label lblTitulo;
        private Label lblMarca;
        private Label lblDesktop;
        private Panel lineaTitulo;
        private DataGridViewTextBoxColumn colIdEnvio;
        private DataGridViewTextBoxColumn colIdPedido;
        private DataGridViewTextBoxColumn colEmpresa;
        private DataGridViewTextBoxColumn colNumeroGuia;
        private DataGridViewTextBoxColumn colEstado;

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
            panelEnvio = new Panel();
            numIdEnvio = new NumericUpDown();
            numIdPedido = new NumericUpDown();
            cmbDireccion = new ComboBox();
            cmbEmpresa = new ComboBox();
            txtNumeroGuia = new TextBox();
            cmbEstado = new ComboBox();
            dtpFechaEnvio = new DateTimePicker();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            lblIdEnvio = new Label();
            lblIdPedido = new Label();
            lblDireccion = new Label();
            lblEmpresa = new Label();
            lblNumeroGuia = new Label();
            lblEstado = new Label();
            lblFechaEnvio = new Label();
            dgvEnvios = new DataGridView();
            colIdEnvio = new DataGridViewTextBoxColumn();
            colIdPedido = new DataGridViewTextBoxColumn();
            colEmpresa = new DataGridViewTextBoxColumn();
            colNumeroGuia = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            lblTitulo = new Label();
            lblMarca = new Label();
            lblDesktop = new Label();
            lineaTitulo = new Panel();
            panelEnvio.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numIdEnvio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numIdPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvEnvios).BeginInit();
            SuspendLayout();
            // 
            // panelEnvio
            // 
            panelEnvio.BackColor = Color.White;
            panelEnvio.BorderStyle = BorderStyle.FixedSingle;
            panelEnvio.Controls.Add(numIdEnvio);
            panelEnvio.Controls.Add(numIdPedido);
            panelEnvio.Controls.Add(cmbDireccion);
            panelEnvio.Controls.Add(cmbEmpresa);
            panelEnvio.Controls.Add(txtNumeroGuia);
            panelEnvio.Controls.Add(cmbEstado);
            panelEnvio.Controls.Add(dtpFechaEnvio);
            panelEnvio.Controls.Add(btnNuevo);
            panelEnvio.Controls.Add(btnGuardar);
            panelEnvio.Controls.Add(btnEliminar);
            panelEnvio.Controls.Add(btnCancelar);
            panelEnvio.Controls.Add(lblIdEnvio);
            panelEnvio.Controls.Add(lblIdPedido);
            panelEnvio.Controls.Add(lblDireccion);
            panelEnvio.Controls.Add(lblEmpresa);
            panelEnvio.Controls.Add(lblNumeroGuia);
            panelEnvio.Controls.Add(lblEstado);
            panelEnvio.Controls.Add(lblFechaEnvio);
            panelEnvio.Location = new Point(835, 94);
            panelEnvio.Name = "panelEnvio";
            panelEnvio.Size = new Size(270, 516);
            panelEnvio.TabIndex = 0;
            // 
            // numIdEnvio
            // 
            numIdEnvio.Font = new Font("Segoe UI", 10F);
            numIdEnvio.Location = new Point(14, 35);
            numIdEnvio.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numIdEnvio.Name = "numIdEnvio";
            numIdEnvio.ReadOnly = true;
            numIdEnvio.Size = new Size(240, 30);
            numIdEnvio.TabIndex = 0;
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
            // cmbDireccion
            // 
            cmbDireccion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbDireccion.Font = new Font("Segoe UI", 9F);
            cmbDireccion.FormattingEnabled = true;
            cmbDireccion.Location = new Point(14, 153);
            cmbDireccion.Name = "cmbDireccion";
            cmbDireccion.Size = new Size(240, 28);
            cmbDireccion.TabIndex = 2;
            // 
            // cmbEmpresa
            // 
            cmbEmpresa.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEmpresa.Font = new Font("Segoe UI", 9F);
            cmbEmpresa.FormattingEnabled = true;
            cmbEmpresa.Location = new Point(14, 212);
            cmbEmpresa.Name = "cmbEmpresa";
            cmbEmpresa.Size = new Size(240, 28);
            cmbEmpresa.TabIndex = 3;
            // 
            // txtNumeroGuia
            // 
            txtNumeroGuia.Font = new Font("Segoe UI", 9F);
            txtNumeroGuia.Location = new Point(14, 271);
            txtNumeroGuia.MaxLength = 100;
            txtNumeroGuia.Name = "txtNumeroGuia";
            txtNumeroGuia.Size = new Size(116, 27);
            txtNumeroGuia.TabIndex = 4;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Font = new Font("Segoe UI", 9F);
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(138, 271);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(116, 28);
            cmbEstado.TabIndex = 5;
            // 
            // dtpFechaEnvio
            // 
            dtpFechaEnvio.CustomFormat = "dd/MM/yyyy";
            dtpFechaEnvio.Font = new Font("Segoe UI", 9F);
            dtpFechaEnvio.Format = DateTimePickerFormat.Custom;
            dtpFechaEnvio.Location = new Point(14, 330);
            dtpFechaEnvio.Name = "dtpFechaEnvio";
            dtpFechaEnvio.Size = new Size(240, 27);
            dtpFechaEnvio.TabIndex = 6;
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
            btnNuevo.TabIndex = 7;
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
            btnGuardar.TabIndex = 8;
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
            btnEliminar.TabIndex = 9;
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
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblIdEnvio
            // 
            lblIdEnvio.AutoSize = true;
            lblIdEnvio.Font = new Font("Segoe UI", 12F);
            lblIdEnvio.ForeColor = Color.FromArgb(35, 35, 35);
            lblIdEnvio.Location = new Point(14, 7);
            lblIdEnvio.Name = "lblIdEnvio";
            lblIdEnvio.Size = new Size(88, 28);
            lblIdEnvio.TabIndex = 11;
            lblIdEnvio.Text = "ID Envío:";
            // 
            // lblIdPedido
            // 
            lblIdPedido.AutoSize = true;
            lblIdPedido.Font = new Font("Segoe UI", 12F);
            lblIdPedido.ForeColor = Color.FromArgb(35, 35, 35);
            lblIdPedido.Location = new Point(14, 66);
            lblIdPedido.Name = "lblIdPedido";
            lblIdPedido.Size = new Size(101, 28);
            lblIdPedido.TabIndex = 12;
            lblIdPedido.Text = "ID Pedido:";
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Font = new Font("Segoe UI", 12F);
            lblDireccion.ForeColor = Color.FromArgb(35, 35, 35);
            lblDireccion.Location = new Point(14, 125);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(98, 28);
            lblDireccion.TabIndex = 13;
            lblDireccion.Text = "Dirección:";
            // 
            // lblEmpresa
            // 
            lblEmpresa.AutoSize = true;
            lblEmpresa.Font = new Font("Segoe UI", 12F);
            lblEmpresa.ForeColor = Color.FromArgb(35, 35, 35);
            lblEmpresa.Location = new Point(14, 184);
            lblEmpresa.Name = "lblEmpresa";
            lblEmpresa.Size = new Size(186, 28);
            lblEmpresa.TabIndex = 14;
            lblEmpresa.Text = "Empresa transporte:";
            // 
            // lblNumeroGuia
            // 
            lblNumeroGuia.AutoSize = true;
            lblNumeroGuia.Font = new Font("Segoe UI", 12F);
            lblNumeroGuia.ForeColor = Color.FromArgb(35, 35, 35);
            lblNumeroGuia.Location = new Point(14, 243);
            lblNumeroGuia.Name = "lblNumeroGuia";
            lblNumeroGuia.Size = new Size(89, 28);
            lblNumeroGuia.TabIndex = 15;
            lblNumeroGuia.Text = "N.º Guía:";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 12F);
            lblEstado.ForeColor = Color.FromArgb(35, 35, 35);
            lblEstado.Location = new Point(138, 243);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(75, 28);
            lblEstado.TabIndex = 16;
            lblEstado.Text = "Estado:";
            // 
            // lblFechaEnvio
            // 
            lblFechaEnvio.AutoSize = true;
            lblFechaEnvio.Font = new Font("Segoe UI", 12F);
            lblFechaEnvio.ForeColor = Color.FromArgb(35, 35, 35);
            lblFechaEnvio.Location = new Point(14, 302);
            lblFechaEnvio.Name = "lblFechaEnvio";
            lblFechaEnvio.Size = new Size(119, 28);
            lblFechaEnvio.TabIndex = 17;
            lblFechaEnvio.Text = "Fecha envío:";
            // 
            // dgvEnvios
            // 
            dgvEnvios.AllowUserToAddRows = false;
            dgvEnvios.AllowUserToDeleteRows = false;
            dgvEnvios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvEnvios.BackgroundColor = Color.FromArgb(222, 222, 222);
            dgvEnvios.ColumnHeadersHeight = 29;
            dgvEnvios.Columns.AddRange(new DataGridViewColumn[] { colIdEnvio, colIdPedido, colEmpresa, colNumeroGuia, colEstado });
            dgvEnvios.EnableHeadersVisualStyles = false;
            dgvEnvios.Location = new Point(21, 180);
            dgvEnvios.MultiSelect = false;
            dgvEnvios.Name = "dgvEnvios";
            dgvEnvios.ReadOnly = true;
            dgvEnvios.RowHeadersVisible = false;
            dgvEnvios.RowHeadersWidth = 51;
            dgvEnvios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEnvios.Size = new Size(790, 430);
            dgvEnvios.TabIndex = 1;
            dgvEnvios.SelectionChanged += dgvEnvios_SelectionChanged;
            // 
            // colIdEnvio
            // 
            colIdEnvio.HeaderText = "ID Envío";
            colIdEnvio.MinimumWidth = 6;
            colIdEnvio.Name = "colIdEnvio";
            colIdEnvio.ReadOnly = true;
            // 
            // colIdPedido
            // 
            colIdPedido.HeaderText = "ID Pedido";
            colIdPedido.MinimumWidth = 6;
            colIdPedido.Name = "colIdPedido";
            colIdPedido.ReadOnly = true;
            // 
            // colEmpresa
            // 
            colEmpresa.HeaderText = "Empresa transporte";
            colEmpresa.MinimumWidth = 6;
            colEmpresa.Name = "colEmpresa";
            colEmpresa.ReadOnly = true;
            // 
            // colNumeroGuia
            // 
            colNumeroGuia.HeaderText = "N.º guía";
            colNumeroGuia.MinimumWidth = 6;
            colNumeroGuia.Name = "colNumeroGuia";
            colNumeroGuia.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 6;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.FromArgb(222, 222, 222);
            lblTitulo.Font = new Font("Segoe UI", 14F);
            lblTitulo.ForeColor = Color.FromArgb(35, 35, 35);
            lblTitulo.Location = new Point(12, 132);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(299, 32);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Lista de Envíos Registrados";
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
            // FrmEnvio
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1121, 627);
            Controls.Add(lineaTitulo);
            Controls.Add(lblTitulo);
            Controls.Add(lblDesktop);
            Controls.Add(lblMarca);
            Controls.Add(panelEnvio);
            Controls.Add(dgvEnvios);
            Font = new Font("Segoe UI", 9F);
            MaximizeBox = false;
            Name = "FrmEnvio";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmEnvios - SivarPOS";
            Load += FrmEnvio_Load;
            panelEnvio.ResumeLayout(false);
            panelEnvio.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numIdEnvio).EndInit();
            ((System.ComponentModel.ISupportInitialize)numIdPedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvEnvios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
