namespace VentasOnline.Forms
{
    partial class FrmProductos
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
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            panel1 = new Panel();
            label1 = new Label();
            cmbProveedor = new ComboBox();
            btnBuscarImagen = new Button();
            txtImagen = new TextBox();
            label7 = new Label();
            numericStock = new NumericUpDown();
            label6 = new Label();
            numericPrecio = new NumericUpDown();
            label5 = new Label();
            label4 = new Label();
            cmbCategoria = new ComboBox();
            btnCancelar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            btnNuevo = new Button();
            txtDescripcion = new TextBox();
            label3 = new Label();
            txtNombre = new TextBox();
            label2 = new Label();
            logoLabel = new Label();
            desktopLabel = new Label();
            dgvProductos = new DataGridView();
            txtBusqueda = new TextBox();
            lblBusqueda = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cmbProveedor);
            panel1.Controls.Add(btnBuscarImagen);
            panel1.Controls.Add(txtImagen);
            panel1.Controls.Add(label7);
            panel1.Controls.Add(numericStock);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(numericPrecio);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(cmbCategoria);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnEliminar);
            panel1.Controls.Add(btnGuardar);
            panel1.Controls.Add(btnNuevo);
            panel1.Controls.Add(txtDescripcion);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtNombre);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(779, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(330, 651);
            panel1.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F);
            label1.Location = new Point(15, 66);
            label1.Name = "label1";
            label1.Size = new Size(120, 30);
            label1.TabIndex = 20;
            label1.Text = "Proveedor:";
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbProveedor.Font = new Font("Segoe UI", 11F);
            cmbProveedor.FormattingEnabled = true;
            cmbProveedor.Location = new Point(15, 99);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(294, 33);
            cmbProveedor.TabIndex = 19;
            // 
            // btnBuscarImagen
            // 
            btnBuscarImagen.BackColor = Color.White;
            btnBuscarImagen.FlatStyle = FlatStyle.Flat;
            btnBuscarImagen.Font = new Font("Segoe UI", 11F);
            btnBuscarImagen.Location = new Point(238, 478);
            btnBuscarImagen.Name = "btnBuscarImagen";
            btnBuscarImagen.Size = new Size(87, 38);
            btnBuscarImagen.TabIndex = 18;
            btnBuscarImagen.Text = "Buscar";
            btnBuscarImagen.UseVisualStyleBackColor = true;
            btnBuscarImagen.Click += btnBuscarImagen_Click;
            // 
            // txtImagen
            // 
            txtImagen.Font = new Font("Segoe UI", 11F);
            txtImagen.Location = new Point(15, 488);
            txtImagen.MaxLength = 100;
            txtImagen.Name = "txtImagen";
            txtImagen.Size = new Size(217, 32);
            txtImagen.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 13F);
            label7.Location = new Point(15, 455);
            label7.Name = "label7";
            label7.Size = new Size(91, 30);
            label7.TabIndex = 16;
            label7.Text = "Imagen:";
            // 
            // numericStock
            // 
            numericStock.Font = new Font("Segoe UI", 11F);
            numericStock.Location = new Point(25, 420);
            numericStock.Name = "numericStock";
            numericStock.Size = new Size(150, 32);
            numericStock.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 13F);
            label6.Location = new Point(15, 394);
            label6.Name = "label6";
            label6.Size = new Size(70, 30);
            label6.TabIndex = 14;
            label6.Text = "Stock:";
            // 
            // numericPrecio
            // 
            numericPrecio.DecimalPlaces = 2;
            numericPrecio.Font = new Font("Segoe UI", 11F);
            numericPrecio.Location = new Point(25, 349);
            numericPrecio.Name = "numericPrecio";
            numericPrecio.Size = new Size(150, 32);
            numericPrecio.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13F);
            label5.Location = new Point(17, 320);
            label5.Name = "label5";
            label5.Size = new Size(78, 30);
            label5.TabIndex = 12;
            label5.Text = "Precio:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 13F);
            label4.Location = new Point(15, -1);
            label4.Name = "label4";
            label4.Size = new Size(112, 30);
            label4.TabIndex = 11;
            label4.Text = "Categoría:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.Font = new Font("Segoe UI", 11F);
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(15, 32);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(294, 33);
            cmbCategoria.TabIndex = 10;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(55, 119, 202);
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(176, 603);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(112, 36);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(55, 119, 202);
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(25, 603);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(110, 36);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(57, 148, 54);
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(176, 544);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(112, 39);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.FromArgb(57, 148, 54);
            btnNuevo.FlatAppearance.BorderSize = 0;
            btnNuevo.FlatStyle = FlatStyle.Flat;
            btnNuevo.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(25, 544);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(110, 39);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Font = new Font("Segoe UI", 11F);
            txtDescripcion.Location = new Point(15, 242);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(294, 75);
            txtDescripcion.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13F);
            label3.Location = new Point(15, 209);
            label3.Name = "label3";
            label3.Size = new Size(130, 30);
            label3.TabIndex = 4;
            label3.Text = "Descripción:";
            // 
            // txtNombre
            // 
            txtNombre.Font = new Font("Segoe UI", 11F);
            txtNombre.Location = new Point(15, 168);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(294, 32);
            txtNombre.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13F);
            label2.Location = new Point(15, 135);
            label2.Name = "label2";
            label2.Size = new Size(99, 30);
            label2.TabIndex = 2;
            label2.Text = "Nombre:";
            // 
            // logoLabel
            // 
            logoLabel.AutoSize = true;
            logoLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            logoLabel.ForeColor = Color.FromArgb(25, 71, 132);
            logoLabel.Location = new Point(35, 24);
            logoLabel.Name = "logoLabel";
            logoLabel.Size = new Size(225, 62);
            logoLabel.TabIndex = 4;
            logoLabel.Text = "SivarPOS";
            // 
            // desktopLabel
            // 
            desktopLabel.AutoSize = true;
            desktopLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            desktopLabel.ForeColor = Color.FromArgb(87, 97, 107);
            desktopLabel.Location = new Point(266, 24);
            desktopLabel.Name = "desktopLabel";
            desktopLabel.Size = new Size(210, 62);
            desktopLabel.TabIndex = 5;
            desktopLabel.Text = "Desktop";
            desktopLabel.Click += desktopLabel_Click;
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.BackgroundColor = Color.FromArgb(222, 222, 222);
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(205, 205, 205);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.FromArgb(35, 35, 35);
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvProductos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(222, 222, 222);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.FromArgb(35, 35, 35);
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(55, 111, 161);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvProductos.DefaultCellStyle = dataGridViewCellStyle6;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.Location = new Point(21, 157);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(752, 515);
            dgvProductos.TabIndex = 2;
            dgvProductos.CellClick += dgvProductos_CellClick;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(181, 109);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(373, 27);
            txtBusqueda.TabIndex = 11;
            // 
            // lblBusqueda
            // 
            lblBusqueda.AutoSize = true;
            lblBusqueda.Location = new Point(33, 112);
            lblBusqueda.Name = "lblBusqueda";
            lblBusqueda.Size = new Size(138, 20);
            lblBusqueda.TabIndex = 10;
            lblBusqueda.Text = "Buscar por nombre:";
            lblBusqueda.Click += lblBusqueda_Click;
            // 
            // FrmProductos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1121, 684);
            Controls.Add(txtBusqueda);
            Controls.Add(lblBusqueda);
            Controls.Add(desktopLabel);
            Controls.Add(logoLabel);
            Controls.Add(panel1);
            Controls.Add(dgvProductos);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmProductos";
            Load += FrmProductos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericStock).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPrecio).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Button btnCancelar;
        private Button btnEliminar;
        private Button btnGuardar;
        private Button btnNuevo;
        private TextBox txtDescripcion;
        private Label label3;
        private TextBox txtNombre;
        private Label label2;
        private DataGridView dgvProductos;
        private Label label4;
        private NumericUpDown numericPrecio;
        private Label label5;
        private NumericUpDown numericStock;
        private Label label6;
        private Button btnBuscarImagen;
        private TextBox txtImagen;
        private Label label7;
        private Label logoLabel;
        private Label desktopLabel;
        private ComboBox cmbCategoria;
        private Label label1;
        private ComboBox cmbProveedor;
        private TextBox txtBusqueda;
        private Label lblBusqueda;
    }
}