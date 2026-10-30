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
            btnNuevaVenta = new Button();
            comboBox1 = new ComboBox();
            label2 = new Label();
            cmbClienteEncabezado = new ComboBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            label4 = new Label();
            btnQuitarLinea = new Button();
            dgvDetallePedido = new DataGridView();
            btnAgregar = new Button();
            numericUpDown1 = new NumericUpDown();
            label3 = new Label();
            comboBox3 = new ComboBox();
            cmbProducto = new Label();
            groupBox3 = new GroupBox();
            dgvHistorialPedidos = new DataGridView();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHistorialPedidos).BeginInit();
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
            groupBox1.Controls.Add(btnNuevaVenta);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cmbClienteEncabezado);
            groupBox1.Controls.Add(label1);
            groupBox1.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox1.Location = new Point(21, 66);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new Size(705, 140);
            groupBox1.TabIndex = 7;
            groupBox1.TabStop = false;
            groupBox1.Text = "Encabezado de la Venta";
            // 
            // btnNuevaVenta
            // 
            btnNuevaVenta.Location = new Point(7, 96);
            btnNuevaVenta.Margin = new Padding(3, 4, 3, 4);
            btnNuevaVenta.Name = "btnNuevaVenta";
            btnNuevaVenta.Size = new Size(114, 31);
            btnNuevaVenta.TabIndex = 5;
            btnNuevaVenta.Text = "Nueva Venta";
            btnNuevaVenta.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(431, 43);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(267, 29);
            comboBox1.TabIndex = 4;
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
            // cmbClienteEncabezado
            // 
            cmbClienteEncabezado.FormattingEnabled = true;
            cmbClienteEncabezado.Location = new Point(87, 43);
            cmbClienteEncabezado.Margin = new Padding(3, 4, 3, 4);
            cmbClienteEncabezado.Name = "cmbClienteEncabezado";
            cmbClienteEncabezado.Size = new Size(267, 29);
            cmbClienteEncabezado.TabIndex = 2;
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
            groupBox2.Controls.Add(label4);
            groupBox2.Controls.Add(btnQuitarLinea);
            groupBox2.Controls.Add(dgvDetallePedido);
            groupBox2.Controls.Add(btnAgregar);
            groupBox2.Controls.Add(numericUpDown1);
            groupBox2.Controls.Add(label3);
            groupBox2.Controls.Add(comboBox3);
            groupBox2.Controls.Add(cmbProducto);
            groupBox2.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox2.Location = new Point(21, 279);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new Size(705, 332);
            groupBox2.TabIndex = 8;
            groupBox2.TabStop = false;
            groupBox2.Text = "Detalle de Productos";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(367, 296);
            label4.Name = "label4";
            label4.Size = new Size(49, 23);
            label4.TabIndex = 11;
            label4.Text = "Total";
            // 
            // btnQuitarLinea
            // 
            btnQuitarLinea.Location = new Point(7, 291);
            btnQuitarLinea.Margin = new Padding(3, 4, 3, 4);
            btnQuitarLinea.Name = "btnQuitarLinea";
            btnQuitarLinea.Size = new Size(135, 33);
            btnQuitarLinea.TabIndex = 10;
            btnQuitarLinea.Text = "Quitar Linea";
            btnQuitarLinea.UseVisualStyleBackColor = true;
            // 
            // dgvDetallePedido
            // 
            dgvDetallePedido.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDetallePedido.Location = new Point(7, 97);
            dgvDetallePedido.Margin = new Padding(3, 4, 3, 4);
            dgvDetallePedido.Name = "dgvDetallePedido";
            dgvDetallePedido.RowHeadersWidth = 51;
            dgvDetallePedido.Size = new Size(651, 173);
            dgvDetallePedido.TabIndex = 9;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(563, 43);
            btnAgregar.Margin = new Padding(3, 4, 3, 4);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.Size = new Size(95, 33);
            btnAgregar.TabIndex = 9;
            btnAgregar.Text = "Agregar";
            btnAgregar.UseVisualStyleBackColor = true;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(431, 43);
            numericUpDown1.Margin = new Padding(3, 4, 3, 4);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(80, 29);
            numericUpDown1.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(367, 47);
            label3.Name = "label3";
            label3.Size = new Size(50, 23);
            label3.TabIndex = 3;
            label3.Text = "Cant:";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new Point(87, 43);
            comboBox3.Margin = new Padding(3, 4, 3, 4);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(267, 29);
            comboBox3.TabIndex = 2;
            // 
            // cmbProducto
            // 
            cmbProducto.AutoSize = true;
            cmbProducto.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProducto.Location = new Point(7, 47);
            cmbProducto.Name = "cmbProducto";
            cmbProducto.Size = new Size(84, 23);
            cmbProducto.TabIndex = 0;
            cmbProducto.Text = "Producto:";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(dgvHistorialPedidos);
            groupBox3.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            groupBox3.Location = new Point(789, 66);
            groupBox3.Margin = new Padding(3, 4, 3, 4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(3, 4, 3, 4);
            groupBox3.Size = new Size(319, 504);
            groupBox3.TabIndex = 9;
            groupBox3.TabStop = false;
            groupBox3.Text = "Historial de Pedidos";
            // 
            // dgvHistorialPedidos
            // 
            dgvHistorialPedidos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHistorialPedidos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHistorialPedidos.Location = new Point(7, 43);
            dgvHistorialPedidos.Margin = new Padding(3, 4, 3, 4);
            dgvHistorialPedidos.Name = "dgvHistorialPedidos";
            dgvHistorialPedidos.RowHeadersWidth = 51;
            dgvHistorialPedidos.Size = new Size(305, 436);
            dgvHistorialPedidos.TabIndex = 10;
            // 
            // FrmPedido
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1121, 627);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(desktopLabel);
            Controls.Add(logoLabel);
            MaximizeBox = false;
            Name = "FrmPedido";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmPedido";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDetallePedido).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHistorialPedidos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private GroupBox groupBox1;
        private Label label1;
        private ComboBox cmbClienteEncabezado;
        private ComboBox comboBox1;
        private Label label2;
        private Button btnNuevaVenta;
        private GroupBox groupBox2;
        private Label label3;
        private ComboBox comboBox3;
        private Label cmbProducto;
        private NumericUpDown numericUpDown1;
        private Button btnAgregar;
        private DataGridView dgvDetallePedido;
        private Label label4;
        private Button btnQuitarLinea;
        private GroupBox groupBox3;
        private DataGridView dgvHistorialPedidos;
    }
}
