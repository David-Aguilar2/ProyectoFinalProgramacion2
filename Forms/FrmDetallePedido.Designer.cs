namespace VentasOnline.Forms
{
    partial class FrmDetallePedido
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelDetalle;
        private NumericUpDown numIdDetalle;
        private NumericUpDown numIdPedido;
        private ComboBox cmbProducto;
        private NumericUpDown numCantidad;
        private NumericUpDown numPrecioUnitario;
        private TextBox txtSubtotal;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
        private DataGridView dgvDetalles;
        private Label lblIdDetalle;
        private Label lblIdPedido;
        private Label lblProducto;
        private Label lblCantidad;
        private Label lblPrecioUnitario;
        private Label lblSubtotal;
        private Label lblTitulo;
        private Label lblMarca;
        private Label lblDesktop;
        private Panel lineaTitulo;
        private DataGridViewTextBoxColumn colIdDetalle;
        private DataGridViewTextBoxColumn colProducto;
        private DataGridViewTextBoxColumn colCantidad;
        private DataGridViewTextBoxColumn colPrecioUnitario;
        private DataGridViewTextBoxColumn colSubtotal;

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
            panelDetalle = new Panel();
            numIdDetalle = new NumericUpDown();
            numIdPedido = new NumericUpDown();
            cmbProducto = new ComboBox();
            numCantidad = new NumericUpDown();
            numPrecioUnitario = new NumericUpDown();
            txtSubtotal = new TextBox();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnEliminar = new Button();
            btnCancelar = new Button();
            lblIdDetalle = new Label();
            lblIdPedido = new Label();
            lblProducto = new Label();
            lblCantidad = new Label();
            lblPrecioUnitario = new Label();
            lblSubtotal = new Label();
            dgvDetalles = new DataGridView();
            colIdDetalle = new DataGridViewTextBoxColumn();
            colProducto = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colPrecioUnitario = new DataGridViewTextBoxColumn();
            colSubtotal = new DataGridViewTextBoxColumn();
            lblTitulo = new Label();
            lblMarca = new Label();
            lblDesktop = new Label();
            lineaTitulo = new Panel();
            panelDetalle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numIdDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numIdPedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioUnitario).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).BeginInit();
            SuspendLayout();
            // 
            // panelDetalle
            // 
            panelDetalle.BackColor = Color.White;
            panelDetalle.BorderStyle = BorderStyle.FixedSingle;
            panelDetalle.Controls.Add(numIdDetalle);
            panelDetalle.Controls.Add(numIdPedido);
            panelDetalle.Controls.Add(cmbProducto);
            panelDetalle.Controls.Add(numCantidad);
            panelDetalle.Controls.Add(numPrecioUnitario);
            panelDetalle.Controls.Add(txtSubtotal);
            panelDetalle.Controls.Add(btnNuevo);
            panelDetalle.Controls.Add(btnGuardar);
            panelDetalle.Controls.Add(btnEliminar);
            panelDetalle.Controls.Add(btnCancelar);
            panelDetalle.Controls.Add(lblIdDetalle);
            panelDetalle.Controls.Add(lblIdPedido);
            panelDetalle.Controls.Add(lblProducto);
            panelDetalle.Controls.Add(lblCantidad);
            panelDetalle.Controls.Add(lblPrecioUnitario);
            panelDetalle.Controls.Add(lblSubtotal);
            panelDetalle.Location = new Point(740, 94);
            panelDetalle.Name = "panelDetalle";
            panelDetalle.Size = new Size(365, 516);
            panelDetalle.TabIndex = 0;
            // 
            // numIdDetalle
            // 
            numIdDetalle.Font = new Font("Segoe UI", 11F);
            numIdDetalle.Location = new Point(18, 35);
            numIdDetalle.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numIdDetalle.Name = "numIdDetalle";
            numIdDetalle.ReadOnly = true;
            numIdDetalle.Size = new Size(327, 32);
            numIdDetalle.TabIndex = 0;
            // 
            // numIdPedido
            // 
            numIdPedido.Font = new Font("Segoe UI", 11F);
            numIdPedido.Location = new Point(18, 96);
            numIdPedido.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            numIdPedido.Name = "numIdPedido";
            numIdPedido.Size = new Size(327, 32);
            numIdPedido.TabIndex = 1;
            numIdPedido.ValueChanged += numIdPedido_ValueChanged;
            numIdPedido.KeyDown += numIdPedido_KeyDown;
            numIdPedido.Leave += numIdPedido_Leave;
            // 
            // cmbProducto
            // 
            cmbProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProducto.Font = new Font("Segoe UI", 11F);
            cmbProducto.FormattingEnabled = true;
            cmbProducto.Location = new Point(18, 157);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(327, 33);
            cmbProducto.TabIndex = 2;
            cmbProducto.SelectedIndexChanged += cmbProducto_SelectedIndexChanged;
            // 
            // numCantidad
            // 
            numCantidad.Font = new Font("Segoe UI", 11F);
            numCantidad.Location = new Point(18, 218);
            numCantidad.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(327, 32);
            numCantidad.TabIndex = 3;
            numCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.ValueChanged += numCantidad_ValueChanged;
            // 
            // numPrecioUnitario
            // 
            numPrecioUnitario.DecimalPlaces = 2;
            numPrecioUnitario.Font = new Font("Segoe UI", 11F);
            numPrecioUnitario.Location = new Point(18, 279);
            numPrecioUnitario.Maximum = new decimal(new int[] { 99999999, 0, 0, 0 });
            numPrecioUnitario.Name = "numPrecioUnitario";
            numPrecioUnitario.Size = new Size(327, 32);
            numPrecioUnitario.TabIndex = 4;
            numPrecioUnitario.ThousandsSeparator = true;
            numPrecioUnitario.ValueChanged += numPrecioUnitario_ValueChanged;
            // 
            // txtSubtotal
            // 
            txtSubtotal.Font = new Font("Segoe UI", 11F);
            txtSubtotal.Location = new Point(18, 340);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.ReadOnly = true;
            txtSubtotal.Size = new Size(327, 32);
            txtSubtotal.TabIndex = 5;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(57, 148, 54);
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(18, 397);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(145, 42);
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
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(200, 397);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(145, 42);
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
            btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(18, 451);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(145, 42);
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
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(200, 451);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(145, 42);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // lblIdDetalle
            // 
            lblIdDetalle.AutoSize = true;
            lblIdDetalle.Font = new Font("Segoe UI", 13F);
            lblIdDetalle.ForeColor = Color.FromArgb(35, 35, 35);
            lblIdDetalle.Location = new Point(18, 8);
            lblIdDetalle.Name = "lblIdDetalle";
            lblIdDetalle.Size = new Size(112, 30);
            lblIdDetalle.TabIndex = 10;
            lblIdDetalle.Text = "ID Detalle:";
            // 
            // lblIdPedido
            // 
            lblIdPedido.AutoSize = true;
            lblIdPedido.Font = new Font("Segoe UI", 13F);
            lblIdPedido.ForeColor = Color.FromArgb(35, 35, 35);
            lblIdPedido.Location = new Point(18, 69);
            lblIdPedido.Name = "lblIdPedido";
            lblIdPedido.Size = new Size(112, 30);
            lblIdPedido.TabIndex = 11;
            lblIdPedido.Text = "ID Pedido:";
            // 
            // lblProducto
            // 
            lblProducto.AutoSize = true;
            lblProducto.Font = new Font("Segoe UI", 13F);
            lblProducto.ForeColor = Color.FromArgb(35, 35, 35);
            lblProducto.Location = new Point(18, 130);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(106, 30);
            lblProducto.TabIndex = 12;
            lblProducto.Text = "Producto:";
            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Font = new Font("Segoe UI", 13F);
            lblCantidad.ForeColor = Color.FromArgb(35, 35, 35);
            lblCantidad.Location = new Point(18, 191);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(104, 30);
            lblCantidad.TabIndex = 13;
            lblCantidad.Text = "Cantidad:";
            // 
            // lblPrecioUnitario
            // 
            lblPrecioUnitario.AutoSize = true;
            lblPrecioUnitario.Font = new Font("Segoe UI", 13F);
            lblPrecioUnitario.ForeColor = Color.FromArgb(35, 35, 35);
            lblPrecioUnitario.Location = new Point(18, 252);
            lblPrecioUnitario.Name = "lblPrecioUnitario";
            lblPrecioUnitario.Size = new Size(160, 30);
            lblPrecioUnitario.TabIndex = 14;
            lblPrecioUnitario.Text = "Precio Unitario:";
            // 
            // lblSubtotal
            // 
            lblSubtotal.AutoSize = true;
            lblSubtotal.Font = new Font("Segoe UI", 13F);
            lblSubtotal.ForeColor = Color.FromArgb(35, 35, 35);
            lblSubtotal.Location = new Point(18, 313);
            lblSubtotal.Name = "lblSubtotal";
            lblSubtotal.Size = new Size(98, 30);
            lblSubtotal.TabIndex = 15;
            lblSubtotal.Text = "Subtotal:";
            // 
            // dgvDetalles
            // 
            dgvDetalles.AllowUserToAddRows = false;
            dgvDetalles.AllowUserToDeleteRows = false;
            dgvDetalles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalles.BackgroundColor = Color.FromArgb(222, 222, 222);
            dgvDetalles.ColumnHeadersHeight = 29;
            dgvDetalles.Columns.AddRange(new DataGridViewColumn[] { colIdDetalle, colProducto, colCantidad, colPrecioUnitario, colSubtotal });
            dgvDetalles.EnableHeadersVisualStyles = false;
            dgvDetalles.Location = new Point(21, 180);
            dgvDetalles.MultiSelect = false;
            dgvDetalles.Name = "dgvDetalles";
            dgvDetalles.ReadOnly = true;
            dgvDetalles.RowHeadersVisible = false;
            dgvDetalles.RowHeadersWidth = 51;
            dgvDetalles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalles.Size = new Size(700, 400);
            dgvDetalles.TabIndex = 1;
            dgvDetalles.SelectionChanged += dgvDetalles_SelectionChanged;
            // 
            // colIdDetalle
            // 
            colIdDetalle.HeaderText = "ID Detalle";
            colIdDetalle.MinimumWidth = 6;
            colIdDetalle.Name = "colIdDetalle";
            colIdDetalle.ReadOnly = true;
            // 
            // colProducto
            // 
            colProducto.HeaderText = "Producto";
            colProducto.MinimumWidth = 6;
            colProducto.Name = "colProducto";
            colProducto.ReadOnly = true;
            // 
            // colCantidad
            // 
            colCantidad.HeaderText = "Cantidad";
            colCantidad.MinimumWidth = 6;
            colCantidad.Name = "colCantidad";
            colCantidad.ReadOnly = true;
            // 
            // colPrecioUnitario
            // 
            colPrecioUnitario.HeaderText = "Precio unitario";
            colPrecioUnitario.MinimumWidth = 6;
            colPrecioUnitario.Name = "colPrecioUnitario";
            colPrecioUnitario.ReadOnly = true;
            // 
            // colSubtotal
            // 
            colSubtotal.HeaderText = "Subtotal";
            colSubtotal.MinimumWidth = 6;
            colSubtotal.Name = "colSubtotal";
            colSubtotal.ReadOnly = true;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.BackColor = Color.FromArgb(222, 222, 222);
            lblTitulo.Font = new Font("Segoe UI", 14F);
            lblTitulo.ForeColor = Color.FromArgb(35, 35, 35);
            lblTitulo.Location = new Point(35, 132);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(292, 32);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Detalles del Pedido Actual";
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
            lineaTitulo.Size = new Size(400, 2);
            lineaTitulo.TabIndex = 5;
            // 
            // FrmDetallePedido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1121, 627);
            Controls.Add(lineaTitulo);
            Controls.Add(lblTitulo);
            Controls.Add(lblDesktop);
            Controls.Add(lblMarca);
            Controls.Add(panelDetalle);
            Controls.Add(dgvDetalles);
            Font = new Font("Segoe UI", 9F);
            MaximizeBox = false;
            Name = "FrmDetallePedido";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmDetallePedido - SivarPOS";
            Load += FrmDetallePedido_Load;
            panelDetalle.ResumeLayout(false);
            panelDetalle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numIdDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)numIdPedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)numCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)numPrecioUnitario).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalles).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
