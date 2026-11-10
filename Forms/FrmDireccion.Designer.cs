namespace VentasOnline.Forms
{
    partial class FrmDireccion
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            desktopLabel = new Label();
            logoLabel = new Label();
            panel1 = new Panel();
            label1 = new Label();
            cboDepartamento = new ComboBox();
            btnCancelar = new Button();
            btnEliminar = new Button();
            btnGuardar = new Button();
            txtUbicacion = new TextBox();
            label3 = new Label();
            txtCliente = new TextBox();
            label2 = new Label();
            dgvDirecciones = new DataGridView();
            txtBusqueda = new TextBox();
            lblBusqueda = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDirecciones).BeginInit();
            SuspendLayout();
            // 
            // desktopLabel
            // 
            desktopLabel.AutoSize = true;
            desktopLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            desktopLabel.ForeColor = Color.FromArgb(87, 97, 107);
            desktopLabel.Location = new Point(250, 2);
            desktopLabel.Name = "desktopLabel";
            desktopLabel.Size = new Size(210, 62);
            desktopLabel.TabIndex = 7;
            desktopLabel.Text = "Desktop";
            // 
            // logoLabel
            // 
            logoLabel.AutoSize = true;
            logoLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            logoLabel.ForeColor = Color.FromArgb(25, 71, 132);
            logoLabel.Location = new Point(7, 2);
            logoLabel.Name = "logoLabel";
            logoLabel.Size = new Size(225, 62);
            logoLabel.TabIndex = 6;
            logoLabel.Text = "SivarPOS";
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(label1);
            panel1.Controls.Add(cboDepartamento);
            panel1.Controls.Add(btnCancelar);
            panel1.Controls.Add(btnEliminar);
            panel1.Controls.Add(btnGuardar);
            panel1.Controls.Add(txtUbicacion);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(txtCliente);
            panel1.Controls.Add(label2);
            panel1.Location = new Point(594, 122);
            panel1.Name = "panel1";
            panel1.Size = new Size(395, 457);
            panel1.TabIndex = 5;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13F);
            label1.Location = new Point(13, 172);
            label1.Name = "label1";
            label1.Size = new Size(112, 30);
            label1.TabIndex = 11;
            label1.Text = "Ubicación:";
            // 
            // cboDepartamento
            // 
            cboDepartamento.FormattingEnabled = true;
            cboDepartamento.Location = new Point(170, 123);
            cboDepartamento.Name = "cboDepartamento";
            cboDepartamento.Size = new Size(217, 28);
            cboDepartamento.TabIndex = 10;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.FromArgb(55, 119, 202);
            btnCancelar.Cursor = Cursors.Hand;
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.FlatStyle = FlatStyle.Flat;
            btnCancelar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnCancelar.ForeColor = Color.White;
            btnCancelar.Location = new Point(212, 403);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(157, 48);
            btnCancelar.TabIndex = 9;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // btnEliminar
            // 
            btnEliminar.BackColor = Color.FromArgb(55, 119, 202);
            btnEliminar.Cursor = Cursors.Hand;
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatStyle = FlatStyle.Flat;
            btnEliminar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnEliminar.ForeColor = Color.White;
            btnEliminar.Location = new Point(22, 403);
            btnEliminar.Name = "btnEliminar";
            btnEliminar.Size = new Size(157, 48);
            btnEliminar.TabIndex = 8;
            btnEliminar.Text = "Eliminar";
            btnEliminar.UseVisualStyleBackColor = true;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.FromArgb(57, 148, 54);
            btnGuardar.Cursor = Cursors.Hand;
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.FlatStyle = FlatStyle.Flat;
            btnGuardar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnGuardar.ForeColor = Color.White;
            btnGuardar.Location = new Point(125, 328);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(157, 48);
            btnGuardar.TabIndex = 7;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = true;
            // 
            // txtUbicacion
            // 
            txtUbicacion.Font = new Font("Segoe UI", 11F);
            txtUbicacion.Location = new Point(13, 215);
            txtUbicacion.Multiline = true;
            txtUbicacion.Name = "txtUbicacion";
            txtUbicacion.Size = new Size(374, 107);
            txtUbicacion.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 13F);
            label3.Location = new Point(12, 118);
            label3.Name = "label3";
            label3.Size = new Size(158, 30);
            label3.TabIndex = 4;
            label3.Text = "Departamento:";
            // 
            // txtCliente
            // 
            txtCliente.Font = new Font("Segoe UI", 11F);
            txtCliente.Location = new Point(13, 66);
            txtCliente.MaxLength = 100;
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(374, 32);
            txtCliente.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 13F);
            label2.Location = new Point(12, 36);
            label2.Name = "label2";
            label2.Size = new Size(85, 30);
            label2.TabIndex = 2;
            label2.Text = "Cliente:";
            // 
            // dgvDirecciones
            // 
            dgvDirecciones.AllowUserToAddRows = false;
            dgvDirecciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDirecciones.BackgroundColor = Color.FromArgb(222, 222, 222);
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(55, 55, 55);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgvDirecciones.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgvDirecciones.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(38, 38, 38);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(55, 111, 161);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgvDirecciones.DefaultCellStyle = dataGridViewCellStyle2;
            dgvDirecciones.EnableHeadersVisualStyles = false;
            dgvDirecciones.GridColor = Color.FromArgb(55, 55, 55);
            dgvDirecciones.Location = new Point(7, 122);
            dgvDirecciones.MultiSelect = false;
            dgvDirecciones.Name = "dgvDirecciones";
            dgvDirecciones.ReadOnly = true;
            dgvDirecciones.RowHeadersWidth = 51;
            dgvDirecciones.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDirecciones.Size = new Size(578, 457);
            dgvDirecciones.TabIndex = 4;
            dgvDirecciones.CellClick += DgvDirecciones_CellClick;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(208, 77);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(373, 27);
            txtBusqueda.TabIndex = 9;
            // 
            // lblBusqueda
            // 
            lblBusqueda.AutoSize = true;
            lblBusqueda.Location = new Point(16, 80);
            lblBusqueda.Name = "lblBusqueda";
            lblBusqueda.Size = new Size(186, 20);
            lblBusqueda.TabIndex = 8;
            lblBusqueda.Text = "Buscar por nombre cliente:";
            // 
            // FrmDireccion
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(994, 583);
            Controls.Add(txtBusqueda);
            Controls.Add(lblBusqueda);
            Controls.Add(desktopLabel);
            Controls.Add(logoLabel);
            Controls.Add(panel1);
            Controls.Add(dgvDirecciones);
            Name = "FrmDireccion";
            Text = "FrmDireccion";
            Load += FrmDireccion_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDirecciones).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label desktopLabel;
        private Label logoLabel;
        private Panel panel1;
        private Button btnCancelar;
        private Button btnEliminar;
        private Button btnGuardar;
        private TextBox txtUbicacion;
        private Label label3;
        private TextBox txtCliente;
        private Label label2;
        private DataGridView dgvDirecciones;
        private ComboBox cboDepartamento;
        private Label label1;
        private TextBox txtBusqueda;
        private Label lblBusqueda;
    }
}