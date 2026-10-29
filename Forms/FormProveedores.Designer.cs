namespace VentasOnline.Forms
{
    partial class FormProveedores
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
            label1 = new Label();
            label2 = new Label();
            txtNombre = new TextBox();
            txtCorreo = new TextBox();
            txtTelefono = new TextBox();
            desktopLabel = new Label();
            VistaPreviaDeProveedores = new Label();
            dgvProveedores = new DataGridView();
            panel2 = new Panel();
            label5 = new Label();
            label6 = new Label();
            btnNuevo = new Button();
            btnGuardar = new Button();
            btnCancelar = new Button();
            btnEliminar = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).BeginInit();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(245, 247, 250);
            label1.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(25, 71, 132);
            label1.Location = new Point(37, 28);
            label1.Name = "label1";
            label1.Size = new Size(182, 51);
            label1.TabIndex = 4;
            label1.Text = "SivarPOS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13F);
            label2.ForeColor = Color.Black;
            label2.Location = new Point(20, 25);
            label2.Name = "label2";
            label2.Size = new Size(82, 25);
            label2.TabIndex = 1;
            label2.Text = "Nombre:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(20, 53);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(246, 23);
            txtNombre.TabIndex = 4;
            // 
            // txtCorreo
            // 
            txtCorreo.Location = new Point(20, 205);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(246, 23);
            txtCorreo.TabIndex = 5;
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(20, 128);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(246, 23);
            txtTelefono.TabIndex = 6;
            // 
            // desktopLabel
            // 
            desktopLabel.AutoSize = true;
            desktopLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            desktopLabel.ForeColor = Color.FromArgb(87, 97, 107);
            desktopLabel.Location = new Point(242, 28);
            desktopLabel.Name = "desktopLabel";
            desktopLabel.Size = new Size(171, 51);
            desktopLabel.TabIndex = 7;
            desktopLabel.Text = "Desktop";
            // 
            // VistaPreviaDeProveedores
            // 
            VistaPreviaDeProveedores.AutoSize = true;
            VistaPreviaDeProveedores.BackColor = Color.FromArgb(222, 222, 222);
            VistaPreviaDeProveedores.Font = new Font("Segoe UI", 14F);
            VistaPreviaDeProveedores.ForeColor = Color.FromArgb(35, 35, 35);
            VistaPreviaDeProveedores.Location = new Point(37, 114);
            VistaPreviaDeProveedores.Name = "VistaPreviaDeProveedores";
            VistaPreviaDeProveedores.Size = new Size(250, 25);
            VistaPreviaDeProveedores.TabIndex = 8;
            VistaPreviaDeProveedores.Text = "Vista Previa de Proveedores:";
            // 
            // dgvProveedores
            // 
            dgvProveedores.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedores.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedores.Location = new Point(37, 157);
            dgvProveedores.Name = "dgvProveedores";
            dgvProveedores.Size = new Size(453, 282);
            dgvProveedores.TabIndex = 9;
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(btnEliminar);
            panel2.Controls.Add(btnCancelar);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(btnNuevo);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(txtCorreo);
            panel2.Controls.Add(txtTelefono);
            panel2.Controls.Add(txtNombre);
            panel2.Location = new Point(517, 28);
            panel2.Name = "panel2";
            panel2.Size = new Size(271, 411);
            panel2.TabIndex = 11;
            panel2.Paint += panel2_Paint;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 13F);
            label5.ForeColor = Color.Black;
            label5.Location = new Point(20, 177);
            label5.Name = "label5";
            label5.Size = new Size(70, 25);
            label5.TabIndex = 7;
            label5.Text = "Correo:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 13F);
            label6.ForeColor = Color.Black;
            label6.Location = new Point(20, 100);
            label6.Name = "label6";
            label6.Size = new Size(83, 25);
            label6.TabIndex = 8;
            label6.Text = "Teléfono:";
            // 
            // btnNuevo
            // 
            btnNuevo.BackColor = Color.ForestGreen;
            btnNuevo.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnNuevo.ForeColor = Color.White;
            btnNuevo.Location = new Point(20, 289);
            btnNuevo.Margin = new Padding(3, 2, 3, 2);
            btnNuevo.Name = "btnNuevo";
            btnNuevo.Size = new Size(96, 29);
            btnNuevo.TabIndex = 6;
            btnNuevo.Text = "Nuevo";
            btnNuevo.UseVisualStyleBackColor = false;
            btnNuevo.Click += btnNuevo_Click;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.ForestGreen;
            btnGuardar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(150, 289);
            btnGuardar.Margin = new Padding(3, 2, 3, 2);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(96, 29);
            btnGuardar.TabIndex = 9;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(55, 119, 202);
            btnCancelar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(150, 357);
            btnCancelar.Margin = new Padding(3, 2, 3, 2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(96, 29);
            btnCancelar.TabIndex = 10;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(55, 119, 202);
            btnEliminar.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(20, 357);
            btnEliminar.Margin = new Padding(3, 2, 3, 2);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(96, 29);
            btnEliminar.TabIndex = 11;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = false;
            // 
            // FormProveedores
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(dgvProveedores);
            Controls.Add(VistaPreviaDeProveedores);
            Controls.Add(desktopLabel);
            Controls.Add(label1);
            ForeColor = Color.CornflowerBlue;
            Name = "FormProveedores";
            Text = "FormProveedores";
            Load += FormProveedores_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProveedores).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox txtNombre;
        private TextBox txtCorreo;
        private TextBox txtTelefono;
        private Label desktopLabel;
        private Label VistaPreviaDeProveedores;
        private DataGridView dgvProveedores;
        private Panel panel2;
        private Label label6;
        private Label label5;
        private Button btnNuevo;
        private Button btnGuardar;
        private Button btnEliminar;
        private Button btnCancelar;
    }
}