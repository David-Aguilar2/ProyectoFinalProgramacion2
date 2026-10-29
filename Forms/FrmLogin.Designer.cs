namespace VentasOnline.Forms
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelLogin = new Panel();
            infoLabel = new Label();
            btnSalir = new Button();
            btnIngresar = new Button();
            txtPassword = new TextBox();
            passwordLabel = new Label();
            txtCorreo = new TextBox();
            correoLabel = new Label();
            subtitleLabel = new Label();
            titleLabel = new Label();
            panelLogin.SuspendLayout();
            SuspendLayout();
            // 
            // panelLogin
            // 
            panelLogin.BackColor = Color.White;
            panelLogin.Controls.Add(infoLabel);
            panelLogin.Controls.Add(btnSalir);
            panelLogin.Controls.Add(btnIngresar);
            panelLogin.Controls.Add(txtPassword);
            panelLogin.Controls.Add(passwordLabel);
            panelLogin.Controls.Add(txtCorreo);
            panelLogin.Controls.Add(correoLabel);
            panelLogin.Controls.Add(subtitleLabel);
            panelLogin.Controls.Add(titleLabel);
            panelLogin.Cursor = Cursors.Arrow;
            panelLogin.Location = new Point(53, 27);
            panelLogin.Margin = new Padding(3, 4, 3, 4);
            panelLogin.Name = "panelLogin";
            panelLogin.Size = new Size(638, 704);
            panelLogin.TabIndex = 0;
            // 
            // infoLabel
            // 
            infoLabel.AutoSize = true;
            infoLabel.ForeColor = Color.FromArgb(100, 100, 100);
            infoLabel.Location = new Point(108, 595);
            infoLabel.Name = "infoLabel";
            infoLabel.Size = new Size(271, 20);
            infoLabel.TabIndex = 4;
            infoLabel.Text = "Accede con tus credenciales de usuario.";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.FromArgb(55, 119, 202);
            btnSalir.Cursor = Cursors.Hand;
            btnSalir.FlatAppearance.BorderSize = 0;
            btnSalir.FlatStyle = FlatStyle.Flat;
            btnSalir.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            btnSalir.ForeColor = Color.White;
            btnSalir.Location = new Point(108, 512);
            btnSalir.Margin = new Padding(3, 4, 3, 4);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(417, 56);
            btnSalir.TabIndex = 3;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // btnIngresar
            // 
            btnIngresar.BackColor = Color.FromArgb(57, 148, 54);
            btnIngresar.Cursor = Cursors.Hand;
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.FlatStyle = FlatStyle.Flat;
            btnIngresar.Font = new Font("Segoe UI", 13F, FontStyle.Bold);
            btnIngresar.ForeColor = Color.White;
            btnIngresar.Location = new Point(108, 428);
            btnIngresar.Margin = new Padding(3, 4, 3, 4);
            btnIngresar.Name = "btnIngresar";
            btnIngresar.Size = new Size(417, 64);
            btnIngresar.TabIndex = 2;
            btnIngresar.Text = "Ingresar";
            btnIngresar.UseVisualStyleBackColor = false;
            btnIngresar.Click += btnIngresar_Click;
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 12F);
            txtPassword.Location = new Point(108, 343);
            txtPassword.Margin = new Padding(3, 4, 3, 4);
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(417, 34);
            txtPassword.TabIndex = 1;
            // 
            // passwordLabel
            // 
            passwordLabel.AutoSize = true;
            passwordLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            passwordLabel.Location = new Point(108, 308);
            passwordLabel.Name = "passwordLabel";
            passwordLabel.Size = new Size(118, 28);
            passwordLabel.TabIndex = 3;
            passwordLabel.Text = "Contraseña";
            // 
            // txtCorreo
            // 
            txtCorreo.Font = new Font("Segoe UI", 12F);
            txtCorreo.Location = new Point(108, 244);
            txtCorreo.Margin = new Padding(3, 4, 3, 4);
            txtCorreo.Name = "txtCorreo";
            txtCorreo.Size = new Size(417, 34);
            txtCorreo.TabIndex = 0;
            // 
            // correoLabel
            // 
            correoLabel.AutoSize = true;
            correoLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            correoLabel.Location = new Point(108, 209);
            correoLabel.Name = "correoLabel";
            correoLabel.Size = new Size(75, 28);
            correoLabel.TabIndex = 2;
            correoLabel.Text = "Correo";
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Font = new Font("Segoe UI", 13F);
            subtitleLabel.ForeColor = Color.FromArgb(87, 97, 107);
            subtitleLabel.Location = new Point(191, 132);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(252, 30);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Iniciar sesión en Desktop";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 30F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(25, 71, 132);
            titleLabel.Location = new Point(191, 65);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(241, 67);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "SivarPOS";
            // 
            // FrmLogin
            // 
            AcceptButton = btnIngresar;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.CornflowerBlue;
            CancelButton = btnSalir;
            ClientSize = new Size(760, 755);
            Controls.Add(panelLogin);
            Cursor = Cursors.IBeam;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SivarPOS - Inicio de sesión";
            panelLogin.ResumeLayout(false);
            panelLogin.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelLogin;
        private Label titleLabel;
        private Label subtitleLabel;
        private Label correoLabel;
        private TextBox txtCorreo;
        private Label passwordLabel;
        private TextBox txtPassword;
        private Button btnIngresar;
        private Button btnSalir;
        private Label infoLabel;
    }
}
