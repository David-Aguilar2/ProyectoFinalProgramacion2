namespace VentasOnline.Forms
{
    partial class FrmUsuario
    {
        private System.ComponentModel.IContainer components = null;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label lblMarca;
        private Label lblEncabezado;
        private TextBox txtNombre;
        private TextBox txtCorreo;
        private TextBox txtPassword;
        private TextBox txtTelefono;
        private ComboBox cmbRol;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private Panel panelLista;
        private Panel panelFormulario;
        private Label lblDetalleUsuario;
        private Label lblAyudaPassword;
        private Label lblTituloLista;
        private Label lblCantidadUsuarios;
        private DataGridView dgvUsuarios;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colCorreo;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colRol;

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
            panelLista = new Panel();
            lblTituloLista = new Label();
            lblCantidadUsuarios = new Label();
            dgvUsuarios = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNombre = new DataGridViewTextBoxColumn();
            colCorreo = new DataGridViewTextBoxColumn();
            colTelefono = new DataGridViewTextBoxColumn();
            colRol = new DataGridViewTextBoxColumn();
            panelFormulario = new Panel();
            lblDetalleUsuario = new Label();
            label1 = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            txtCorreo = new TextBox();
            label3 = new Label();
            txtPassword = new TextBox();
            label4 = new Label();
            txtTelefono = new TextBox();
            label5 = new Label();
            cmbRol = new ComboBox();
            lblAyudaPassword = new Label();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            lblMarca = new Label();
            lblEncabezado = new Label();
            panelLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            panelFormulario.SuspendLayout();
            SuspendLayout();
            // 
            // panelLista
            // 
            panelLista.BackColor = Color.FromArgb(222, 222, 222);
            panelLista.BorderStyle = BorderStyle.FixedSingle;
            panelLista.Controls.Add(lblTituloLista);
            panelLista.Controls.Add(lblCantidadUsuarios);
            panelLista.Controls.Add(dgvUsuarios);
            panelLista.Location = new Point(25, 100);
            panelLista.Name = "panelLista";
            panelLista.Size = new Size(645, 538);
            panelLista.TabIndex = 0;
            // 
            // lblTituloLista
            // 
            lblTituloLista.AutoSize = true;
            lblTituloLista.Font = new Font("Segoe UI", 14F);
            lblTituloLista.ForeColor = Color.FromArgb(35, 35, 35);
            lblTituloLista.Location = new Point(17, 12);
            lblTituloLista.Name = "lblTituloLista";
            lblTituloLista.Size = new Size(228, 32);
            lblTituloLista.TabIndex = 0;
            lblTituloLista.Text = "Usuarios registrados";
            // 
            // lblCantidadUsuarios
            // 
            lblCantidadUsuarios.AutoSize = true;
            lblCantidadUsuarios.Font = new Font("Segoe UI", 10F);
            lblCantidadUsuarios.ForeColor = Color.FromArgb(70, 80, 90);
            lblCantidadUsuarios.Location = new Point(17, 480);
            lblCantidadUsuarios.Name = "lblCantidadUsuarios";
            lblCantidadUsuarios.Size = new Size(134, 23);
            lblCantidadUsuarios.TabIndex = 2;
            lblCantidadUsuarios.Text = "Total Usuarios: 0";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.BackgroundColor = Color.FromArgb(222, 222, 222);
            dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsuarios.ColumnHeadersHeight = 38;
            dgvUsuarios.Columns.AddRange(new DataGridViewColumn[] { colId, colNombre, colCorreo, colTelefono, colRol });
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.GridColor = Color.FromArgb(190, 190, 190);
            dgvUsuarios.Location = new Point(17, 53);
            dgvUsuarios.MultiSelect = false;
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowHeadersWidth = 51;
            dgvUsuarios.RowTemplate.Height = 34;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(609, 415);
            dgvUsuarios.TabIndex = 1;
            dgvUsuarios.SelectionChanged += dgvUsuarios_SelectionChanged;
            // 
            // colId
            // 
            colId.FillWeight = 40F;
            colId.HeaderText = "ID Usuario";
            colId.MinimumWidth = 40;
            colId.Name = "colId";
            colId.ReadOnly = true;
            // 
            // colNombre
            // 
            colNombre.FillWeight = 95F;
            colNombre.HeaderText = "Nombre Completo";
            colNombre.MinimumWidth = 95;
            colNombre.Name = "colNombre";
            colNombre.ReadOnly = true;
            // 
            // colCorreo
            // 
            colCorreo.FillWeight = 105F;
            colCorreo.HeaderText = "Correo";
            colCorreo.MinimumWidth = 105;
            colCorreo.Name = "colCorreo";
            colCorreo.ReadOnly = true;
            // 
            // colTelefono
            // 
            colTelefono.FillWeight = 80F;
            colTelefono.HeaderText = "Teléfono";
            colTelefono.MinimumWidth = 80;
            colTelefono.Name = "colTelefono";
            colTelefono.ReadOnly = true;
            // 
            // colRol
            // 
            colRol.FillWeight = 65F;
            colRol.HeaderText = "Rol";
            colRol.MinimumWidth = 65;
            colRol.Name = "colRol";
            colRol.ReadOnly = true;
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = Color.White;
            panelFormulario.BorderStyle = BorderStyle.FixedSingle;
            panelFormulario.Controls.Add(lblDetalleUsuario);
            panelFormulario.Controls.Add(label1);
            panelFormulario.Controls.Add(txtNombre);
            panelFormulario.Controls.Add(label2);
            panelFormulario.Controls.Add(txtCorreo);
            panelFormulario.Controls.Add(label3);
            panelFormulario.Controls.Add(txtPassword);
            panelFormulario.Controls.Add(label4);
            panelFormulario.Controls.Add(txtTelefono);
            panelFormulario.Controls.Add(label5);
            panelFormulario.Controls.Add(cmbRol);
            panelFormulario.Controls.Add(lblAyudaPassword);
            panelFormulario.Controls.Add(btnNuevo);
            panelFormulario.Controls.Add(btnGuardar);
            panelFormulario.Controls.Add(btnEliminar);
            panelFormulario.Controls.Add(btnCancelar);
            panelFormulario.Location = new Point(690, 100);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Size = new Size(405, 538);
            panelFormulario.TabIndex = 1;
            // 
            // lblDetalleUsuario
            // 
            lblDetalleUsuario.AutoSize = true;
            lblDetalleUsuario.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblDetalleUsuario.ForeColor = Color.FromArgb(35, 35, 35);
            lblDetalleUsuario.Location = new Point(18, 12);
            lblDetalleUsuario.Name = "lblDetalleUsuario";
            lblDetalleUsuario.Size = new Size(231, 32);
            lblDetalleUsuario.TabIndex = 0;
            lblDetalleUsuario.Text = "Detalle del Usuario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F);
            label1.ForeColor = Color.FromArgb(35, 35, 35);
            label1.Location = new Point(10, 53);
            label1.Name = "label1";
            label1.Size = new Size(85, 25);
            label1.TabIndex = 0;
            label1.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 10F);
            txtNombre.Location = new Point(105, 48);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "Nombre completo del usuario";
            txtNombre.Size = new Size(278, 30);
            txtNombre.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F);
            label2.ForeColor = Color.FromArgb(35, 35, 35);
            label2.Location = new Point(10, 126);
            label2.Name = "label2";
            label2.Size = new Size(74, 25);
            label2.TabIndex = 2;
            label2.Text = "Correo:";
            // 
            // txtCorreo
            // 
            txtCorreo.Font = new Font("Segoe UI", 10F);
            txtCorreo.Location = new Point(105, 121);
            txtCorreo.MaxLength = 100;
            txtCorreo.Name = "txtCorreo";
            txtCorreo.PlaceholderText = "usuario@sivarpos.com";
            txtCorreo.Size = new Size(278, 30);
            txtCorreo.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F);
            label3.ForeColor = Color.FromArgb(35, 35, 35);
            label3.Location = new Point(10, 204);
            label3.Name = "label3";
            label3.Size = new Size(95, 25);
            label3.TabIndex = 4;
            label3.Text = "Password:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(105, 199);
            txtPassword.MaxLength = 255;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.PlaceholderText = "Contraseña";
            txtPassword.Size = new Size(278, 30);
            txtPassword.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F);
            label4.ForeColor = Color.FromArgb(35, 35, 35);
            label4.Location = new Point(18, 282);
            label4.Name = "label4";
            label4.Size = new Size(88, 25);
            label4.TabIndex = 6;
            label4.Text = "Teléfono:";
            // 
            // txtTelefono
            // 
            txtTelefono.Font = new Font("Segoe UI", 10F);
            txtTelefono.Location = new Point(105, 277);
            txtTelefono.MaxLength = 15;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.PlaceholderText = "(503) 1234-5678";
            txtTelefono.Size = new Size(278, 30);
            txtTelefono.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F);
            label5.ForeColor = Color.FromArgb(35, 35, 35);
            label5.Location = new Point(18, 360);
            label5.Name = "label5";
            label5.Size = new Size(42, 25);
            label5.TabIndex = 8;
            label5.Text = "Rol:";
            // 
            // cmbRol
            // 
            cmbRol.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbRol.Font = new Font("Segoe UI", 10F);
            cmbRol.FormattingEnabled = true;
            cmbRol.Items.AddRange(new object[] { "Seleccione un rol...", "Administrador", "Vendedor" });
            cmbRol.Location = new Point(105, 355);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(278, 31);
            cmbRol.TabIndex = 9;
            // 
            // lblAyudaPassword
            // 
            lblAyudaPassword.AutoSize = true;
            lblAyudaPassword.Font = new Font("Segoe UI", 8F);
            lblAyudaPassword.ForeColor = Color.FromArgb(90, 100, 110);
            lblAyudaPassword.Location = new Point(18, 397);
            lblAyudaPassword.Name = "lblAyudaPassword";
            lblAyudaPassword.Size = new Size(315, 19);
            lblAyudaPassword.TabIndex = 9;
            lblAyudaPassword.Text = "Deja la contraseña vacía al editar para conservarla.";
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(57, 148, 54);
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(18, 418);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(175, 48);
            btnNuevo.TabIndex = 10;
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
            btnGuardar.Location = new Point(208, 418);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(175, 48);
            btnGuardar.TabIndex = 11;
            btnGuardar.Text = "Guardar Usuario";
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
            btnEliminar.Location = new Point(18, 470);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(175, 48);
            btnEliminar.TabIndex = 12;
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
            btnCancelar.Location = new Point(208, 470);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(175, 48);
            btnCancelar.TabIndex = 13;
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
            // FrmUsuario
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
            Name = "FrmUsuario";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Usuarios - SivarPOS";
            Load += FrmUsuario_Load;
            panelLista.ResumeLayout(false);
            panelLista.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}