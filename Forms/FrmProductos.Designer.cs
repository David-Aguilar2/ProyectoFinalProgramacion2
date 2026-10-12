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
            panel1 = new Panel();
            button1 = new Button();
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
            txtId = new TextBox();
            label1 = new Label();
            dgvProductos = new DataGridView();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericStock).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPrecio).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button1);
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
            panel1.Controls.Add(txtId);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(779, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(330, 594);
            panel1.TabIndex = 3;
            // 
            // button1
            // 
            button1.Location = new Point(238, 450);
            button1.Name = "button1";
            button1.Size = new Size(71, 27);
            button1.TabIndex = 18;
            button1.Text = "Buscar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnBuscarImagen_Click;
            // 
            // txtImagen
            // 
            txtImagen.Location = new Point(15, 450);
            txtImagen.MaxLength = 100;
            txtImagen.Name = "txtImagen";
            txtImagen.Size = new Size(217, 27);
            txtImagen.TabIndex = 17;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 427);
            label7.Name = "label7";
            label7.Size = new Size(62, 20);
            label7.TabIndex = 16;
            label7.Text = "Imagen:";
            // 
            // numericStock
            // 
            numericStock.Location = new Point(15, 389);
            numericStock.Name = "numericStock";
            numericStock.Size = new Size(150, 27);
            numericStock.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 366);
            label6.Name = "label6";
            label6.Size = new Size(48, 20);
            label6.TabIndex = 14;
            label6.Text = "Stock:";
            // 
            // numericPrecio
            // 
            numericPrecio.DecimalPlaces = 2;
            numericPrecio.Location = new Point(15, 328);
            numericPrecio.Name = "numericPrecio";
            numericPrecio.Size = new Size(150, 27);
            numericPrecio.TabIndex = 13;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 305);
            label5.Name = "label5";
            label5.Size = new Size(53, 20);
            label5.TabIndex = 12;
            label5.Text = "Precio:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 70);
            label4.Name = "label4";
            label4.Size = new Size(77, 20);
            label4.TabIndex = 11;
            label4.Text = "Categoría:";
            // 
            // cmbCategoria
            // 
            cmbCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCategoria.FormattingEnabled = true;
            cmbCategoria.Location = new Point(15, 93);
            cmbCategoria.Name = "cmbCategoria";
            cmbCategoria.Size = new Size(294, 28);
            cmbCategoria.TabIndex = 10;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(191, 553);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(94, 29);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnEliminar
            // 
            btnEliminar.Location = new Point(41, 553);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(94, 29);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            btnEliminar.Click += btnEliminar_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(191, 499);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(94, 29);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // btnNuevo
            // 
            btnNuevo.Location = new Point(41, 499);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(94, 29);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = true;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(15, 217);
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.Size = new Size(294, 80);
            txtDescripcion.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(15, 194);
            label3.Name = "label3";
            label3.Size = new Size(90, 20);
            label3.TabIndex = 4;
            label3.Text = "Descripción:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(15, 158);
            txtNombre.MaxLength = 100;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(294, 27);
            txtNombre.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 135);
            label2.Name = "label2";
            label2.Size = new Size(67, 20);
            label2.TabIndex = 2;
            label2.Text = "Nombre:";
            // 
            // txtId
            // 
            txtId.Location = new Point(15, 32);
            txtId.Name = "txtId";
            txtId.ReadOnly = true;
            txtId.Size = new Size(265, 27);
            txtId.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 9);
            label1.Name = "label1";
            label1.Size = new Size(27, 20);
            label1.TabIndex = 0;
            label1.Text = "ID:";
            // 
            // dgvProductos
            // 
            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(21, 21);
            dgvProductos.MultiSelect = false;
            dgvProductos.Name = "dgvProductos";
            dgvProductos.ReadOnly = true;
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.Size = new Size(752, 594);
            dgvProductos.TabIndex = 2;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;
            // 
            // FrmProductos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1121, 627);
            Controls.Add(panel1);
            Controls.Add(dgvProductos);
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
        private TextBox txtId;
        private Label label1;
        private DataGridView dgvProductos;
        private Label label4;
        private ComboBox cmbCategoria;
        private NumericUpDown numericPrecio;
        private Label label5;
        private NumericUpDown numericStock;
        private Label label6;
        private Button button1;
        private TextBox txtImagen;
        private Label label7;
    }
}