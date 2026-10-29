namespace VentasOnline
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            topMenu = new MenuStrip();
            archivoMenuItem = new ToolStripMenuItem();
            catalogosMenuItem = new ToolStripMenuItem();
            operacionesMenuItem = new ToolStripMenuItem();
            detallePedidoMenuItem = new ToolStripMenuItem();
            ayudaMenuItem = new ToolStripMenuItem();
            sidebar = new Panel();
            navigationPanel = new FlowLayoutPanel();
            productsButton = new Button();
            btnCategoria = new Button();
            btnProducto = new Button();
            btnCliente = new Button();
            btnUsuarios = new Button();
            btnPedidos = new Button();
            btnPagos = new Button();
            btnEnvios = new Button();
            btnDetallesPedido = new Button();
            sessionLabel = new Label();
            subtitleLabel = new Label();
            brandLabel = new Label();
            contentPanel = new Panel();
            instructionLabel = new Label();
            welcomeLabel = new Label();
            topMenu.SuspendLayout();
            sidebar.SuspendLayout();
            navigationPanel.SuspendLayout();
            contentPanel.SuspendLayout();
            SuspendLayout();
            // 
            // topMenu
            // 
            topMenu.BackColor = Color.WhiteSmoke;
            topMenu.Font = new Font("Segoe UI", 9F);
            topMenu.ImageScalingSize = new Size(20, 20);
            topMenu.Items.AddRange(new ToolStripItem[] { archivoMenuItem, catalogosMenuItem, operacionesMenuItem, ayudaMenuItem });
            topMenu.Location = new Point(0, 0);
            topMenu.Name = "topMenu";
            topMenu.Size = new Size(1100, 28);
            topMenu.TabIndex = 0;
            // 
            // archivoMenuItem
            // 
            archivoMenuItem.Name = "archivoMenuItem";
            archivoMenuItem.Size = new Size(73, 24);
            archivoMenuItem.Text = "Archivo";
            // 
            // catalogosMenuItem
            // 
            catalogosMenuItem.Name = "catalogosMenuItem";
            catalogosMenuItem.Size = new Size(90, 24);
            catalogosMenuItem.Text = "Catálogos";
            // 
            // operacionesMenuItem
            // 
            operacionesMenuItem.DropDownItems.AddRange(new ToolStripItem[] { detallePedidoMenuItem });
            operacionesMenuItem.Name = "operacionesMenuItem";
            operacionesMenuItem.Size = new Size(106, 24);
            operacionesMenuItem.Text = "Operaciones";
            // 
            // detallePedidoMenuItem
            // 
            detallePedidoMenuItem.Name = "detallePedidoMenuItem";
            detallePedidoMenuItem.Size = new Size(219, 26);
            detallePedidoMenuItem.Text = "Detalles de pedido";
            // 
            // ayudaMenuItem
            // 
            ayudaMenuItem.Name = "ayudaMenuItem";
            ayudaMenuItem.Size = new Size(65, 24);
            ayudaMenuItem.Text = "Ayuda";
            // 
            // sidebar
            // 
            sidebar.BackColor = Color.FromArgb(18, 51, 105);
            sidebar.Controls.Add(navigationPanel);
            sidebar.Controls.Add(sessionLabel);
            sidebar.Controls.Add(subtitleLabel);
            sidebar.Controls.Add(brandLabel);
            sidebar.Dock = DockStyle.Left;
            sidebar.Location = new Point(0, 28);
            sidebar.Name = "sidebar";
            sidebar.Size = new Size(255, 662);
            sidebar.TabIndex = 1;
            // 
            // navigationPanel
            // 
            navigationPanel.Controls.Add(productsButton);
            navigationPanel.Controls.Add(btnCategoria);
            navigationPanel.Controls.Add(btnProducto);
            navigationPanel.Controls.Add(btnCliente);
            navigationPanel.Controls.Add(btnUsuarios);
            navigationPanel.Controls.Add(btnPedidos);
            navigationPanel.Controls.Add(btnPagos);
            navigationPanel.Controls.Add(btnEnvios);
            navigationPanel.Controls.Add(btnDetallesPedido);
            navigationPanel.FlowDirection = FlowDirection.TopDown;
            navigationPanel.Location = new Point(12, 108);
            navigationPanel.Name = "navigationPanel";
            navigationPanel.Size = new Size(231, 535);
            navigationPanel.TabIndex = 2;
            navigationPanel.WrapContents = false;
            // 
            // productsButton
            // 
            productsButton.Cursor = Cursors.Hand;
            productsButton.ForeColor = SystemColors.ButtonHighlight;
            productsButton.Location = new Point(3, 3);
            productsButton.Name = "productsButton";
            productsButton.Size = new Size(228, 52);
            productsButton.TabIndex = 2;
            productsButton.Text = "Inicio";
            // 
            // btnCategoria
            // 
            btnCategoria.Cursor = Cursors.Hand;
            btnCategoria.ForeColor = SystemColors.ButtonHighlight;
            btnCategoria.Location = new Point(3, 61);
            btnCategoria.Name = "btnCategoria";
            btnCategoria.Size = new Size(228, 52);
            btnCategoria.TabIndex = 3;
            btnCategoria.Text = "Categoria";
            // 
            // btnProducto
            // 
            btnProducto.Cursor = Cursors.Hand;
            btnProducto.ForeColor = SystemColors.ButtonHighlight;
            btnProducto.Location = new Point(3, 119);
            btnProducto.Name = "btnProducto";
            btnProducto.Size = new Size(228, 52);
            btnProducto.TabIndex = 4;
            btnProducto.Text = "Producto";
            // 
            // btnCliente
            // 
            btnCliente.Cursor = Cursors.Hand;
            btnCliente.ForeColor = SystemColors.ButtonHighlight;
            btnCliente.Location = new Point(3, 177);
            btnCliente.Name = "btnCliente";
            btnCliente.Size = new Size(228, 52);
            btnCliente.TabIndex = 5;
            btnCliente.Text = "Cliente";
            // 
            // btnUsuarios
            // 
            btnUsuarios.Cursor = Cursors.Hand;
            btnUsuarios.ForeColor = SystemColors.ButtonHighlight;
            btnUsuarios.Location = new Point(3, 235);
            btnUsuarios.Name = "btnUsuarios";
            btnUsuarios.Size = new Size(228, 52);
            btnUsuarios.TabIndex = 6;
            btnUsuarios.Text = "Usuarios";
            // 
            // btnPedidos
            // 
            btnPedidos.Cursor = Cursors.Hand;
            btnPedidos.ForeColor = SystemColors.ButtonHighlight;
            btnPedidos.Location = new Point(3, 293);
            btnPedidos.Name = "btnPedidos";
            btnPedidos.Size = new Size(228, 49);
            btnPedidos.TabIndex = 7;
            btnPedidos.Text = "Pedidos";
            // 
            // btnPagos
            // 
            btnPagos.Cursor = Cursors.Hand;
            btnPagos.ForeColor = SystemColors.ButtonHighlight;
            btnPagos.Location = new Point(3, 348);
            btnPagos.Name = "btnPagos";
            btnPagos.Size = new Size(228, 54);
            btnPagos.TabIndex = 8;
            btnPagos.Text = "Pagos";
            // 
            // btnEnvios
            // 
            btnEnvios.Cursor = Cursors.Hand;
            btnEnvios.ForeColor = SystemColors.ButtonHighlight;
            btnEnvios.Location = new Point(3, 408);
            btnEnvios.Name = "btnEnvios";
            btnEnvios.Size = new Size(228, 52);
            btnEnvios.TabIndex = 0;
            btnEnvios.Text = "Envios";
            // 
            // btnDetallesPedido
            // 
            btnDetallesPedido.Cursor = Cursors.Hand;
            btnDetallesPedido.ForeColor = SystemColors.ButtonHighlight;
            btnDetallesPedido.Location = new Point(3, 466);
            btnDetallesPedido.Name = "btnDetallesPedido";
            btnDetallesPedido.Size = new Size(228, 54);
            btnDetallesPedido.TabIndex = 1;
            btnDetallesPedido.Text = "Detalles Pedido";
            // 
            // sessionLabel
            // 
            sessionLabel.Font = new Font("Segoe UI", 9F);
            sessionLabel.ForeColor = Color.White;
            sessionLabel.Location = new Point(18, 575);
            sessionLabel.Name = "sessionLabel";
            sessionLabel.Size = new Size(220, 35);
            sessionLabel.TabIndex = 9;
            sessionLabel.Text = "Usuario";
            sessionLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Font = new Font("Segoe UI", 11F);
            subtitleLabel.ForeColor = Color.White;
            subtitleLabel.Location = new Point(93, 63);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(141, 25);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Punto de Venta";
            // 
            // brandLabel
            // 
            brandLabel.AutoSize = true;
            brandLabel.Font = new Font("Segoe UI", 21F, FontStyle.Bold);
            brandLabel.ForeColor = Color.White;
            brandLabel.Location = new Point(33, 16);
            brandLabel.Name = "brandLabel";
            brandLabel.Size = new Size(170, 47);
            brandLabel.TabIndex = 0;
            brandLabel.Text = "SivarPOS";
            // 
            // contentPanel
            // 
            contentPanel.BackColor = Color.White;
            contentPanel.BackgroundImage = (Image)resources.GetObject("contentPanel.BackgroundImage");
            contentPanel.BackgroundImageLayout = ImageLayout.Zoom;
            contentPanel.Controls.Add(instructionLabel);
            contentPanel.Controls.Add(welcomeLabel);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(255, 28);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(845, 662);
            contentPanel.TabIndex = 2;
            // 
            // instructionLabel
            // 
            instructionLabel.AutoSize = true;
            instructionLabel.Font = new Font("Segoe UI", 13F);
            instructionLabel.ForeColor = Color.FromArgb(90, 110, 140);
            instructionLabel.Location = new Point(167, 539);
            instructionLabel.Name = "instructionLabel";
            instructionLabel.Size = new Size(480, 30);
            instructionLabel.TabIndex = 3;
            instructionLabel.Text = "Selecciona una opción del menú para comenzar.";
            // 
            // welcomeLabel
            // 
            welcomeLabel.AutoSize = true;
            welcomeLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            welcomeLabel.ForeColor = Color.FromArgb(18, 51, 105);
            welcomeLabel.Location = new Point(254, 43);
            welcomeLabel.Name = "welcomeLabel";
            welcomeLabel.Size = new Size(278, 50);
            welcomeLabel.TabIndex = 2;
            welcomeLabel.Text = "¡Bienvenido/a!";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1100, 690);
            Controls.Add(contentPanel);
            Controls.Add(sidebar);
            Controls.Add(topMenu);
            Font = new Font("Segoe UI", 9F);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SivarPOS - Sistema de Punto de Venta";
            Load += Form1_Load;
            topMenu.ResumeLayout(false);
            topMenu.PerformLayout();
            sidebar.ResumeLayout(false);
            sidebar.PerformLayout();
            navigationPanel.ResumeLayout(false);
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        private void ConfigureNavigationButton(Button button, string text)
        {
            button.BackColor = Color.Transparent;
            button.FlatAppearance.BorderSize = 0;
            button.FlatAppearance.MouseDownBackColor = Color.FromArgb(43, 111, 194);
            button.FlatAppearance.MouseOverBackColor = Color.FromArgb(43, 111, 194);
            button.FlatStyle = FlatStyle.Flat;
            button.Font = new Font("Segoe UI", 11F);
            button.ForeColor = Color.White;
            button.Margin = new Padding(0, 0, 0, 4);
            button.Name = text;
            button.Size = new Size(225, 46);
            button.TabIndex = navigationPanel.Controls.Count;
            button.Text = text;
            button.TextAlign = ContentAlignment.MiddleLeft;
            button.UseVisualStyleBackColor = false;
        }

        #endregion

        private MenuStrip topMenu;
        private ToolStripMenuItem archivoMenuItem;
        private ToolStripMenuItem catalogosMenuItem;
        private ToolStripMenuItem operacionesMenuItem;
        private ToolStripMenuItem detallePedidoMenuItem;
        private ToolStripMenuItem ayudaMenuItem;
        private Panel sidebar;
        private Label brandLabel;
        private Label subtitleLabel;
        private FlowLayoutPanel navigationPanel;
        private Button btnEnvios;
        private Button btnDetallesPedido;
        private Button productsButton;
        private Button btnCategoria;
        private Button btnProducto;
        private Button btnCliente;
        private Button btnUsuarios;
        private Button btnPedidos;
        private Button btnPagos;
        private Label sessionLabel;
        private Panel contentPanel;
        private Label welcomeLabel;
        private Label instructionLabel;
    }
}
