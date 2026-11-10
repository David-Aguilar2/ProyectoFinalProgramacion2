namespace VentasOnline.Forms
{
    partial class FrmPedido
    {
        private System.ComponentModel.IContainer components = null;
        private Label logoLabel;
        private Label desktopLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            logoLabel = new Label();
            desktopLabel = new Label();
            groupBox1 = new GroupBox();
            lblVenta = new Label();
            cboUsuario = new ComboBox();
            label2 = new Label();
            cboCliente = new ComboBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            lblStock = new Label();
            lblTotal = new Label();
            btnQuitarLinea = new Button();
            dgvDetalle = new DataGridView();
            btnAgregar = new Button();
            nudCantidad = new NumericUpDown();
            label3 = new Label();
            cboProducto = new ComboBox();
            cmbProducto = new Label();
            groupBox3 = new GroupBox();
            dgvPedidos = new DataGridView();
            btnGuardar = new Button();
            btnNueva = new Button();
            pictureBoxProducto = new PictureBox();
            btnCerrar = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProducto).BeginInit();
            SuspendLayout();
            // 
            // logoLabel
            // 
            logoLabel.AutoSize = true;
            logoLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            logoLabel.ForeColor = Color.FromArgb(25, 71, 132);
            logoLabel.Location = new Point(21, 3);
            logoLabel.Name = "logoLabel";
            logoLabel.Size = new Size(225, 62);
            logoLabel.TabIndex = 3;
            logoLabel.Text = "SivarPOS";
            // 
            // desktopLabel
            // 
            desktopLabel.AutoSize = true;
            desktopLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            desktopLabel.ForeColor = Color.FromArgb(87, 97, 107);
            desktopLabel.Location = new Point(261, 3);
            desktopLabel.Name = "desktopLabel";
            desktopLabel.Size = new Size(210, 62);
            desktopLabel.TabIndex = 4;
            desktopLabel.Text = "Desktop";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lblVenta);
            groupBox1.Controls.Add(cboUsuario);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cboCliente);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(21, 66);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(705, 127);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Encabezado de la Venta";
            // 
            // lblVenta
            // 
            lblVenta.AutoSize = true;
            lblVenta.Location = new Point(7, 100);
            lblVenta.Name = "lblVenta";
            lblVenta.Size = new Size(121, 23);
            lblVenta.TabIndex = 5;
            lblVenta.Text = "Nuevo Pedido";
            // 
            // cboUsuario
            // 
            cboUsuario.FormattingEnabled = true;
            cboUsuario.Location = new Point(431, 43);
            cboUsuario.Margin = new Padding(3, 4, 3, 4);
            cboUsuario.Name = "cboUsuario";
            cboUsuario.Size = new Size(267, 29);
            cboUsuario.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.Location = new Point(367, 47);
            label2.Name = "label2";
            label2.Size = new Size(72, 23);
            label2.TabIndex = 3;
            label2.Text = "Usuario:";
            // 
            // cboCliente
            // 
            cboCliente.FormattingEnabled = true;
            cboCliente.Location = new Point(87, 43);
            cboCliente.Margin = new Padding(3, 4, 3, 4);
            cboCliente.Name = "cboCliente";
            cboCliente.Size = new Size(267, 29);
            cboCliente.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(7, 47);
            label1.Name = "label1";
            label1.Size = new Size(67, 23);
            label1.TabIndex = 0;
            label1.Text = "Cliente:";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lblStock);
            groupBox2.Controls.Add(lblTotal);
            groupBox2.Controls.Add(btnQuitarLinea);
            groupBox2.Controls.Add(dgvDetalle);
            groupBox2.Controls.Add(btnAgregar);
            groupBox2.Controls.Add(nudCantidad);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(cboProducto);
            groupBox2.Controls.Add(cmbProducto);
            groupBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(21, 290);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(705, 332);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalle de Productos";
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.Location = new Point(17, 64);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(61, 23);
            lblStock.TabIndex = 12;
            lblStock.Text = "Stock:";
            // 
            // lblTotal
            // 
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTotal.Location = new Point(367, 296);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(49, 23);
            lblTotal.TabIndex = 11;
            lblTotal.Text = "Total";
            // 
            // btnQuitarLinea
            // 
            btnQuitarLinea.Cursor = Cursors.Hand;
            btnQuitarLinea.Location = new Point(7, 291);
            btnQuitarLinea.Margin = new Padding(3, 4, 3, 4);
            btnQuitarLinea.Name = "btnQuitarLinea";
            btnQuitarLinea.Size = new Size(135, 33);
            btnQuitarLinea.TabIndex = 10;
            btnQuitarLinea.Text = "Quitar Linea";
            btnQuitarLinea.UseVisualStyleBackColor = true;
            btnQuitarLinea.Click += btnQuitar_Click;
            // 
            // dgvDetalle
            // 
            dgvDetalle.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetalle.Location = new Point(7, 97);
            dgvDetalle.Margin = new Padding(3, 4, 3, 4);
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.RowHeadersWidth = 51;
            dgvDetalle.Size = new Size(651, 173);
            dgvDetalle.TabIndex = 9;
            // 
            // btnAgregar
            // 
            btnAgregar.Cursor = Cursors.Hand;
            btnAgregar.Location = new Point(563, 43);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 33);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            btnAgregar.Click += btnAgregar_Click;
            // 
            // nudCantidad
            // 
            nudCantidad.Location = new Point(431, 28);
            nudCantidad.Margin = new Padding(3, 4, 3, 4);
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(80, 29);
            nudCantidad.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(367, 32);
            label3.Name = "label3";
            label3.Size = new Size(50, 23);
            label3.TabIndex = 3;
            label3.Text = "Cant:";
            // 
            // cboProducto
            // 
            cboProducto.FormattingEnabled = true;
            cboProducto.Location = new Point(87, 28);
            cboProducto.Margin = new Padding(3, 4, 3, 4);
            cboProducto.Name = "cboProducto";
            cboProducto.Size = new Size(267, 29);
            cboProducto.TabIndex = 2;
            cboProducto.SelectedIndexChanged += cboProducto_SelectedIndexChanged;
            // 
            // cmbProducto
            // 
            cmbProducto.AutoSize = true;
            cmbProducto.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProducto.Location = new Point(7, 32);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(84, 23);
            cmbProducto.TabIndex = 0;
            cmbProducto.Text = "Producto:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvPedidos);
            groupBox3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(746, 66);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(472, 556);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Historial de Pedidos";
            // 
            // dgvPedidos
            // 
            dgvPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPedidos.Location = new Point(7, 30);
            dgvPedidos.Margin = new Padding(3, 4, 3, 4);
            dgvPedidos.Name = "dgvPedidos";
            dgvPedidos.RowHeadersWidth = 51;
            dgvPedidos.Size = new Size(459, 526);
            dgvPedidos.TabIndex = 10;
            dgvPedidos.CellClick += dgvPedidos_CellClick;
            // 
            // btnGuardar
            // 
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.Location = new Point(228, 634);
            btnGuardar.Margin = new Padding(3, 4, 3, 4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(136, 31);
            btnGuardar.TabIndex = 17;
            btnGuardar.Text = "Guardar Pedido";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNueva
            // 
            btnNueva.Cursor = Cursors.Hand;
            btnNueva.Location = new Point(26, 634);
            btnNueva.Margin = new Padding(3, 4, 3, 4);
            btnNueva.Name = "btnNueva";
            btnNueva.Size = new Size(117, 31);
            btnNueva.TabIndex = 16;
            btnNueva.Text = "Nuevo Pedido";
            btnNueva.UseVisualStyleBackColor = true;
            btnNueva.Click += btnNueva_Click;
            // 
            // pictureBoxProducto
            // 
            pictureBoxProducto.Location = new Point(483, 191);
            pictureBoxProducto.Name = "pictureBoxProducto";
            pictureBoxProducto.Size = new Size(125, 107);
            pictureBoxProducto.TabIndex = 19;
            pictureBoxProducto.TabStop = false;
            // 
            // btnCerrar
            // 
            btnCerrar.Cursor = Cursors.Hand;
            btnCerrar.Location = new Point(474, 634);
            btnCerrar.Margin = new Padding(3, 4, 3, 4);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.Size = new Size(134, 31);
            btnCerrar.TabIndex = 20;
            btnCerrar.Text = "Cerrar";
            btnCerrar.UseVisualStyleBackColor = true;
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FrmPedido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1230, 673);
            Controls.Add(btnCerrar);
            Controls.Add(pictureBoxProducto);
            Controls.Add(btnGuardar);
            Controls.Add(btnNueva);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(desktopLabel);
            Controls.Add(logoLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmPedido";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPedido";
            Load += FrmPedido_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPedidos).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxProducto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private GroupBox groupBox1;
        private Label label1;
        private ComboBox cboCliente;
        private ComboBox cboUsuario;
        private Label label2;
        private GroupBox groupBox2;
        private Label label3;
        private ComboBox cboProducto;
        private Label cmbProducto;
        private NumericUpDown nudCantidad;
        private Button btnAgregar;
        private DataGridView dgvDetalle;
        private Label lblTotal;
        private Button btnQuitarLinea;
        private GroupBox groupBox3;
        private DataGridView dgvPedidos;
        private Button btnGuardar;
        private Button btnNueva;
        private PictureBox pictureBoxProducto;
        private Label lblVenta;
        private Label lblStock;
        private Button btnCerrar;
    }
}
