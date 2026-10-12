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
            ayudaMenuItem = new ToolStripMenuItem();
            sidebar = new Panel();
            navigationPanel = new FlowLayoutPanel();
            productsButton = new Button();
            clientsButton = new Button();
            usersButton = new Button();
            ordersButton = new Button();
            paymentsButton = new Button();
            shippingButton = new Button();
            reportsButton = new Button();
            homeButton = new Button();
            categoriesButton = new Button();
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
            operacionesMenuItem.Name = "operacionesMenuItem";
            operacionesMenuItem.Size = new Size(106, 24);
            operacionesMenuItem.Text = "Operaciones";
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
            navigationPanel.Controls.Add(clientsButton);
            navigationPanel.Controls.Add(usersButton);
            navigationPanel.Controls.Add(ordersButton);
            navigationPanel.Controls.Add(paymentsButton);
            navigationPanel.Controls.Add(shippingButton);
            navigationPanel.Controls.Add(reportsButton);
            navigationPanel.Controls.Add(homeButton);
            navigationPanel.Controls.Add(categoriesButton);
            navigationPanel.FlowDirection = FlowDirection.TopDown;
            navigationPanel.Location = new Point(12, 108);
            navigationPanel.Name = "navigationPanel";
            navigationPanel.Size = new Size(231, 535);
            navigationPanel.TabIndex = 2;
            navigationPanel.WrapContents = false;
            // 
            // productsButton
            // 
            productsButton.ForeColor = SystemColors.ButtonHighlight;
            productsButton.Location = new Point(3, 3);
            productsButton.Name = "productsButton";
            productsButton.Size = new Size(154, 52);
            productsButton.TabIndex = 2;
            productsButton.Text = "Inicio";
            productsButton.Click += productsButton_Click;
            // 
            // clientsButton
            // 
            clientsButton.ForeColor = SystemColors.ButtonHighlight;
            clientsButton.Location = new Point(3, 61);
            clientsButton.Name = "clientsButton";
            clientsButton.Size = new Size(154, 52);
            clientsButton.TabIndex = 3;
            clientsButton.Text = "Categoria";
            // 
            // usersButton
            // 
            usersButton.ForeColor = SystemColors.ButtonHighlight;
            usersButton.Location = new Point(3, 119);
            usersButton.Name = "usersButton";
            usersButton.Size = new Size(154, 52);
            usersButton.TabIndex = 4;
            usersButton.Text = "Producto";
            // 
            // ordersButton
            // 
            ordersButton.ForeColor = SystemColors.ButtonHighlight;
            ordersButton.Location = new Point(3, 177);
            ordersButton.Name = "ordersButton";
            ordersButton.Size = new Size(154, 52);
            ordersButton.TabIndex = 5;
            ordersButton.Text = "Cliente";
            // 
            // paymentsButton
            // 
            paymentsButton.ForeColor = SystemColors.ButtonHighlight;
            paymentsButton.Location = new Point(3, 235);
            paymentsButton.Name = "paymentsButton";
            paymentsButton.Size = new Size(154, 52);
            paymentsButton.TabIndex = 6;
            paymentsButton.Text = "Usaeio";
            // 
            // shippingButton
            // 
            shippingButton.ForeColor = SystemColors.ButtonHighlight;
            shippingButton.Location = new Point(3, 293);
            shippingButton.Name = "shippingButton";
            shippingButton.Size = new Size(154, 39);
            shippingButton.TabIndex = 7;
            shippingButton.Text = "Pedidos";
            // 
            // reportsButton
            // 
            reportsButton.ForeColor = SystemColors.ButtonHighlight;
            reportsButton.Location = new Point(3, 338);
            reportsButton.Name = "reportsButton";
            reportsButton.Size = new Size(154, 54);
            reportsButton.TabIndex = 8;
            reportsButton.Text = "Pagos";
            // 
            // homeButton
            // 
            homeButton.ForeColor = SystemColors.ButtonHighlight;
            homeButton.Location = new Point(3, 398);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(154, 63);
            homeButton.TabIndex = 0;
            homeButton.Text = "Envios";
            // 
            // categoriesButton
            // 
            categoriesButton.ForeColor = SystemColors.ButtonHighlight;
            categoriesButton.Location = new Point(3, 467);
            categoriesButton.Name = "categoriesButton";
            categoriesButton.Size = new Size(154, 54);
            categoriesButton.TabIndex = 1;
            categoriesButton.Text = "Detalles Pedido";
            categoriesButton.Click += categoriesButton_Click;
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
            contentPanel.Paint += contentPanel_Paint;
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
        private ToolStripMenuItem ayudaMenuItem;
        private Panel sidebar;
        private Label brandLabel;
        private Label subtitleLabel;
        private FlowLayoutPanel navigationPanel;
        private Button homeButton;
        private Button categoriesButton;
        private Button productsButton;
        private Button clientsButton;
        private Button usersButton;
        private Button ordersButton;
        private Button paymentsButton;
        private Button shippingButton;
        private Button reportsButton;
        private Label sessionLabel;
        private Panel contentPanel;
        private Label welcomeLabel;
        private Label instructionLabel;
    }
}
