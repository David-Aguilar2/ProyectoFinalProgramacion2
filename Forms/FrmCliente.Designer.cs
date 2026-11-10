namespace VentasOnline.Forms
{
    partial class FrmCliente
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblMarca;
        private Label lblEncabezado;
        private Label lblNombre;
        private Label lblTelefono;
        private Label lblCorreo;
        private TextBox txtNombre;
        private TextBox txtCorreo;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private Panel panelFormulario;

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
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panelFormulario = new Panel();
            btnDireccion = new Button();
            txtTelefono = new MaskedTextBox();
            lblNombre = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            txtNombre = new TextBox();
            txtCorreo = new TextBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            lblMarca = new Label();
            lblEncabezado = new Label();
            lblCantidad = new Label();
            panelLista = new Panel();
            lblTituloLista = new Label();
            dgvClientes = new DataGridView();
            colIdCliente = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colCorreo = new DataGridViewTextBoxColumn();
            txtBusqueda = new TextBox();
            lblBusqueda = new Label();
            panelFormulario.SuspendLayout();
            panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.White;
            panelFormulario.BorderStyle = BorderStyle.FixedSingle;
            panelFormulario.Controls.Add(btnDireccion);
            panelFormulario.Controls.Add(txtTelefono);
            panelFormulario.Controls.Add(lblNombre);
            panelFormulario.Controls.Add(lblTelefono);
            panelFormulario.Controls.Add(lblCorreo);
            panelFormulario.Controls.Add(txtNombre);
            panelFormulario.Controls.Add(txtCorreo);
            panelFormulario.Controls.Add(btnNuevo);
            panelFormulario.Controls.Add(btnGuardar);
            panelFormulario.Controls.Add(btnEliminar);
            panelFormulario.Controls.Add(btnCancelar);
            panelFormulario.Location = new Point(690, 139);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(405, 520);
            panelFormulario.TabIndex = 1;
            // 
            // btnDireccion
            // 
            btnDireccion.BackColor = Color.FromArgb(57, 148, 54);
            btnDireccion.FlatAppearance.BorderSize = 0;
            btnDireccion.FlatStyle = FlatStyle.Flat;
            btnDireccion.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDireccion.ForeColor = Color.White;
            btnDireccion.Location = new Point(91, 444);
            btnDireccion.Name = "btnDireccion";
            btnDireccion.Size = new Size(245, 48);
            btnDireccion.TabIndex = 11;
            btnDireccion.Text = "Añadir o editar dirección";
            btnDireccion.UseVisualStyleBackColor = false;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(18, 165);
            txtTelefono.Mask = "0000-0000";
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(88, 27);
            txtTelefono.TabIndex = 10;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 11F);
            lblNombre.ForeColor = Color.FromArgb(35, 35, 35);
            lblNombre.Location = new Point(18, 30);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(85, 25);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre:";
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Font = new Font("Segoe UI", 11F);
            lblTelefono.ForeColor = Color.FromArgb(35, 35, 35);
            lblTelefono.Location = new Point(18, 120);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(88, 25);
            lblTelefono.TabIndex = 2;
            lblTelefono.Text = "Teléfono:";
            // 
            // lblCorreo
            // 
            lblCorreo.AutoSize = true;
            lblCorreo.Font = new Font("Segoe UI", 11F);
            lblCorreo.ForeColor = Color.FromArgb(35, 35, 35);
            lblCorreo.Location = new Point(18, 210);
            lblCorreo.Name = "lblCorreo";
            lblCorreo.Size = new Size(74, 25);
            lblCorreo.TabIndex = 4;
            lblCorreo.Text = "Correo:";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(18, 59);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Nombre completo del cliente";
            txtNombre.Size = new Size(365, 30);
            txtNombre.TabIndex = 1;
            // 
            // txtCorreo
            // 
            txtCorreo.Font = new Font("Segoe UI", 10F);
            txtCorreo.Location = new Point(18, 239);
            txtCorreo.MaxLength = 100;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.PlaceholderText = "ejemplo@correo.com";
            txtCorreo.Size = new Size(365, 30);
            txtCorreo.TabIndex = 5;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(57, 148, 54);
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(18, 310);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(175, 48);
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
            btnGuardar.Location = new Point(208, 310);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(175, 48);
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
            btnEliminar.Location = new Point(18, 374);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(175, 48);
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
            btnCancelar.Location = new Point(208, 374);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(175, 48);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblMarca.ForeColor = Color.FromArgb(25, 71, 132);
            lblMarca.Location = new Point(35, 22);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(225, 62);
            lblMarca.TabIndex = 2;
            lblMarca.Text = "SivarPOS";
            // 
            // lblEncabezado
            // 
            lblEncabezado.AutoSize = true;
            lblEncabezado.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblEncabezado.ForeColor = Color.FromArgb(87, 97, 107);
            lblEncabezado.Location = new Point(278, 22);
            lblEncabezado.Name = "lblEncabezado";
            lblEncabezado.Size = new Size(210, 62);
            lblEncabezado.TabIndex = 3;
            lblEncabezado.Text = "Desktop";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 10F);
            lblCantidad.ForeColor = Color.FromArgb(70, 80, 90);
            lblCantidad.Location = new Point(17, 480);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(177, 23);
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Clientes registrados: 0";
            // 
            // panelLista
            // 
            panelLista.BackColor = Color.FromArgb(222, 222, 222);
            panelLista.BorderStyle = BorderStyle.FixedSingle;
            panelLista.Controls.Add(lblTituloLista);
            panelLista.Controls.Add(lblCantidad);
            panelLista.Controls.Add(dgvClientes);
            panelLista.Location = new Point(25, 139);
            panelLista.Name = "panelLista";
            panelLista.Size = new Size(645, 520);
            panelLista.TabIndex = 0;
            // 
            // lblTituloLista
            // 
            lblTituloLista.AutoSize = true;
            lblTituloLista.Font = new Font("Segoe UI", 14F);
            lblTituloLista.ForeColor = Color.FromArgb(35, 35, 35);
            lblTituloLista.Location = new Point(17, 12);
            lblTituloLista.Name = "lblTituloLista";
            lblTituloLista.Size = new Size(223, 32);
            lblTituloLista.TabIndex = 0;
            lblTituloLista.Text = "Clientes registrados";
            // 
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.BackgroundColor = Color.FromArgb(222, 222, 222);
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle4.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvClientes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvClientes.ColumnHeadersHeight = 38;
            dgvClientes.Columns.AddRange(new DataGridViewColumn[] { colIdCliente, colNombre, colTelefono, colCorreo });
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgvClientes.DefaultCellStyle = dataGridViewCellStyle5;
            dgvClientes.Location = new Point(17, 53);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Control;
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            dgvClientes.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(609, 415);
            dgvClientes.TabIndex = 1;
            dgvClientes.CellClick += dgvClientes_CellClick;
            // 
            // colIdCliente
            // 
            colIdCliente.FillWeight = 35F;
            colIdCliente.HeaderText = "ID";
            colIdCliente.MinimumWidth = 6;
            colIdCliente.Name = "colIdCliente";
            colIdCliente.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.FillWeight = 95F;
            colNombre.HeaderText = "Nombre";
            colNombre.MinimumWidth = 6;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colTelefono
            // 
            colTelefono.FillWeight = 75F;
            colTelefono.HeaderText = "Teléfono";
            colTelefono.MinimumWidth = 6;
            colTelefono.Name = "colTelefono";
            colTelefono.ReadOnly = true;
            // 
            // colCorreo
            // 
            colCorreo.FillWeight = 110F;
            colCorreo.HeaderText = "Correo";
            colCorreo.MinimumWidth = 6;
            colCorreo.Name = "colCorreo";
            colCorreo.ReadOnly = true;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(192, 92);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(373, 27);
            txtBusqueda.TabIndex = 7;
            // 
            // lblBusqueda
            // 
            lblBusqueda.AutoSize = true;
            lblBusqueda.Location = new Point(48, 95);
            lblBusqueda.Name = "lblBusqueda";
            lblBusqueda.Size = new Size(129, 20);
            lblBusqueda.TabIndex = 6;
            lblBusqueda.Text = "Buscar por correo:";
            // 
            // FrmCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1120, 672);
            Controls.Add(txtBusqueda);
            Controls.Add(lblBusqueda);
            Controls.Add(panelFormulario);
            Controls.Add(panelLista);
            Controls.Add(lblEncabezado);
            Controls.Add(lblMarca);
            Font = new Font("Segoe UI", 9F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmCliente";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Clientes - SivarPOS";
            Load += FrmCliente_Load;
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            panelLista.ResumeLayout(false);
            panelLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private MaskedTextBox txtTelefono;
        private Button btnDireccion;
        private Label lblCantidad;
        private Panel panelLista;
        private Label lblTituloLista;
        private DataGridView dgvClientes;
        private DataGridViewTextBoxColumn colIdCliente;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colCorreo;
        private TextBox txtBusqueda;
        private Label lblBusqueda;
    }
}
