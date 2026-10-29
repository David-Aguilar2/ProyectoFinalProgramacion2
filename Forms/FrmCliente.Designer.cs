namespace VentasOnline.Forms
{
    partial class FrmCliente
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblMarca;
        private Label lblEncabezado;
        private Label lblTituloLista;
        private Label lblCantidad;
        private Label lblNombre;
        private Label lblTelefono;
        private Label lblCorreo;
        private TextBox txtNombre;
        private TextBox txtTelefono;
        private TextBox txtCorreo;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private DataGridView dgvClientes;
        private DataGridViewTextBoxColumn colIdCliente;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colCorreo;
        private Panel panelLista;
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
            panelLista = new Panel();
            panelFormulario = new Panel();
            lblMarca = new Label();
            lblEncabezado = new Label();
            lblTituloLista = new Label();
            lblCantidad = new Label();
            lblNombre = new Label();
            lblTelefono = new Label();
            lblCorreo = new Label();
            txtNombre = new TextBox();
            txtTelefono = new TextBox();
            txtCorreo = new TextBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            dgvClientes = new DataGridView();
            colIdCliente = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colCorreo = new DataGridViewTextBoxColumn();
            panelLista.SuspendLayout();
            panelFormulario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // panelLista
            // 
            panelLista.BackColor = Color.FromArgb(222, 222, 222);
            panelLista.BorderStyle = BorderStyle.FixedSingle;
            panelLista.Controls.Add(lblTituloLista);
            panelLista.Controls.Add(lblCantidad);
            panelLista.Controls.Add(dgvClientes);
            panelLista.Location = new Point(25, 100);
            panelLista.Name = "panelLista";
            panelLista.Size = new Size(645, 520);
            panelLista.TabIndex = 0;
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.White;
            panelFormulario.BorderStyle = BorderStyle.FixedSingle;
            panelFormulario.Controls.Add(lblNombre);
            panelFormulario.Controls.Add(lblTelefono);
            panelFormulario.Controls.Add(lblCorreo);
            panelFormulario.Controls.Add(txtNombre);
            panelFormulario.Controls.Add(txtTelefono);
            panelFormulario.Controls.Add(txtCorreo);
            panelFormulario.Controls.Add(btnNuevo);
            panelFormulario.Controls.Add(btnGuardar);
            panelFormulario.Controls.Add(btnEliminar);
            panelFormulario.Controls.Add(btnCancelar);
            panelFormulario.Location = new Point(690, 100);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(405, 520);
            panelFormulario.TabIndex = 1;
            // 
            // lblMarca
            // 
            lblMarca.AutoSize = true;
            lblMarca.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            lblMarca.ForeColor = Color.FromArgb(25, 71, 132);
            lblMarca.Location = new Point(35, 22);
            lblMarca.Name = "lblMarca";
            lblMarca.Size = new Size(229, 62);
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
            lblEncabezado.Size = new Size(178, 62);
            lblEncabezado.TabIndex = 3;
            lblEncabezado.Text = "Desktop";
            // 
            // lblTituloLista
            // 
            lblTituloLista.AutoSize = true;
            lblTituloLista.Font = new Font("Segoe UI", 14F, FontStyle.Regular);
            lblTituloLista.ForeColor = Color.FromArgb(35, 35, 35);
            lblTituloLista.Location = new Point(17, 12);
            lblTituloLista.Name = "lblTituloLista";
            lblTituloLista.Size = new Size(188, 32);
            lblTituloLista.TabIndex = 0;
            lblTituloLista.Text = "Clientes registrados";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 10F);
            lblCantidad.ForeColor = Color.FromArgb(70, 80, 90);
            lblCantidad.Location = new Point(17, 480);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(165, 23);
            lblCantidad.TabIndex = 2;
            lblCantidad.Text = "Clientes registrados: 0";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 11F);
            lblNombre.ForeColor = Color.FromArgb(35, 35, 35);
            lblNombre.Location = new Point(18, 30);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(79, 25);
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
            lblTelefono.Size = new Size(82, 25);
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
            lblCorreo.Size = new Size(71, 25);
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
            // txtTelefono
            // 
            txtTelefono.Font = new Font("Segoe UI", 10F);
            txtTelefono.Location = new Point(18, 149);
            txtTelefono.MaxLength = 15;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "Ej. (503) 1234-5678";
            txtTelefono.Size = new Size(365, 30);
            txtTelefono.TabIndex = 3;
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
            // dgvClientes
            // 
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.AllowUserToDeleteRows = false;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.BackgroundColor = Color.FromArgb(222, 222, 222);
            dgvClientes.ColumnHeadersHeight = 38;
            dgvClientes.Columns.AddRange(new DataGridViewColumn[] { colIdCliente, colNombre, colTelefono, colCorreo });
            dgvClientes.Location = new Point(17, 53);
            dgvClientes.MultiSelect = false;
            dgvClientes.Name = "dgvClientes";
            dgvClientes.ReadOnly = true;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.Size = new Size(609, 415);
            dgvClientes.TabIndex = 1;
            // 
            // colIdCliente
            // 
            colIdCliente.HeaderText = "ID";
            colIdCliente.Name = "colIdCliente";
            colIdCliente.ReadOnly = true;
            colIdCliente.FillWeight = 35F;
            // 
            // colNombre
            // 
            colNombre.HeaderText = "Nombre";
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            colNombre.FillWeight = 95F;
            // 
            // colTelefono
            // 
            colTelefono.HeaderText = "Teléfono";
            colTelefono.Name = "colTelefono";
            colTelefono.ReadOnly = true;
            colTelefono.FillWeight = 75F;
            // 
            // colCorreo
            // 
            colCorreo.HeaderText = "Correo";
            colCorreo.Name = "colCorreo";
            colCorreo.ReadOnly = true;
            colCorreo.FillWeight = 110F;
            // 
            // FrmCliente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1120, 650);
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
            panelLista.ResumeLayout(false);
            panelLista.PerformLayout();
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
