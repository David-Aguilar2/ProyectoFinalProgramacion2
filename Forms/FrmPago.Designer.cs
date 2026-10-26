namespace VentasOnline.Forms
{
    partial class FrmPago
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
            txtPedidoId = new TextBox();
            txtMetodo = new TextBox();
            txtMonto = new TextBox();
            txtEstado = new TextBox();
            btnGuardar = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            SuspendLayout();
            // 
            // txtPedidoId
            // 
            txtPedidoId.Location = new Point(79, 64);
            txtPedidoId.Name = "txtPedidoId";
            txtPedidoId.Size = new Size(100, 23);
            txtPedidoId.TabIndex = 0;
            // 
            // txtMetodo
            // 
            txtMetodo.Location = new Point(116, 131);
            txtMetodo.Name = "txtMetodo";
            txtMetodo.Size = new Size(100, 23);
            txtMetodo.TabIndex = 1;
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(64, 200);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(100, 23);
            txtMonto.TabIndex = 2;
            // 
            // txtEstado
            // 
            txtEstado.Location = new Point(64, 263);
            txtEstado.Name = "txtEstado";
            txtEstado.Size = new Size(100, 23);
            txtEstado.TabIndex = 3;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(51, 353);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(103, 23);
            btnGuardar.TabIndex = 4;
            btnGuardar.Text = "Guardar Pago";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 67);
            label1.Name = "label1";
            label1.Size = new Size(61, 15);
            label1.TabIndex = 5;
            label1.Text = "ID Pedido:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 134);
            label2.Name = "label2";
            label2.Size = new Size(98, 15);
            label2.TabIndex = 6;
            label2.Text = "Metodo de Pago:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 203);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 7;
            label3.Text = "Monto:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 266);
            label4.Name = "label4";
            label4.Size = new Size(45, 15);
            label4.TabIndex = 8;
            label4.Text = "Estado:";
            // 
            // FrmPago
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnGuardar);
            Controls.Add(txtEstado);
            Controls.Add(txtMonto);
            Controls.Add(txtMetodo);
            Controls.Add(txtPedidoId);
            Name = "FrmPago";
            Text = "FrmPago";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtPedidoId;
        private TextBox txtMetodo;
        private TextBox txtMonto;
        private TextBox txtEstado;
        private Button btnGuardar;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
    }
}