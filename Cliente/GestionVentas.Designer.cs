namespace Cliente
{
    partial class GestionVentas
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
            this.btnConsultarHistorial = new System.Windows.Forms.Button();
            this.btnComprar = new System.Windows.Forms.Button();
            this.dgvVehiculosDisponibles = new System.Windows.Forms.DataGridView();
            this.labelVehiculos = new System.Windows.Forms.Label();
            this.cbxSucursales = new System.Windows.Forms.ComboBox();
            this.labelSucursal = new System.Windows.Forms.Label();
            this.labelCliente = new System.Windows.Forms.Label();
            this.labelTitulo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNombreCliente = new System.Windows.Forms.Label();
            this.idVehiculo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.marca = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.modelo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ano = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoria = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidadDisponible = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculosDisponibles)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConsultarHistorial
            // 
            this.btnConsultarHistorial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConsultarHistorial.Location = new System.Drawing.Point(654, 339);
            this.btnConsultarHistorial.Name = "btnConsultarHistorial";
            this.btnConsultarHistorial.Size = new System.Drawing.Size(181, 40);
            this.btnConsultarHistorial.TabIndex = 18;
            this.btnConsultarHistorial.Text = "CONSULTAR INFORMACIÓN";
            this.btnConsultarHistorial.UseVisualStyleBackColor = true;
            this.btnConsultarHistorial.Click += new System.EventHandler(this.btnConsultarHistorial_Click);
            // 
            // btnComprar
            // 
            this.btnComprar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnComprar.Location = new System.Drawing.Point(12, 339);
            this.btnComprar.Name = "btnComprar";
            this.btnComprar.Size = new System.Drawing.Size(181, 40);
            this.btnComprar.TabIndex = 17;
            this.btnComprar.Text = "COMPRAR VEHÍCULO";
            this.btnComprar.UseVisualStyleBackColor = true;
            this.btnComprar.Click += new System.EventHandler(this.btnComprar_Click);
            // 
            // dgvVehiculosDisponibles
            // 
            this.dgvVehiculosDisponibles.AllowUserToAddRows = false;
            this.dgvVehiculosDisponibles.AllowUserToDeleteRows = false;
            this.dgvVehiculosDisponibles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVehiculosDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVehiculosDisponibles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idVehiculo,
            this.marca,
            this.modelo,
            this.ano,
            this.precio,
            this.categoria,
            this.cantidadDisponible});
            this.dgvVehiculosDisponibles.Location = new System.Drawing.Point(12, 147);
            this.dgvVehiculosDisponibles.MultiSelect = false;
            this.dgvVehiculosDisponibles.Name = "dgvVehiculosDisponibles";
            this.dgvVehiculosDisponibles.ReadOnly = true;
            this.dgvVehiculosDisponibles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVehiculosDisponibles.Size = new System.Drawing.Size(823, 177);
            this.dgvVehiculosDisponibles.TabIndex = 16;
            // 
            // labelVehiculos
            // 
            this.labelVehiculos.AutoSize = true;
            this.labelVehiculos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVehiculos.Location = new System.Drawing.Point(9, 119);
            this.labelVehiculos.Name = "labelVehiculos";
            this.labelVehiculos.Size = new System.Drawing.Size(230, 16);
            this.labelVehiculos.TabIndex = 15;
            this.labelVehiculos.Text = "Vehículos disponibles por sede:";
            // 
            // cbxSucursales
            // 
            this.cbxSucursales.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSucursales.FormattingEnabled = true;
            this.cbxSucursales.Location = new System.Drawing.Point(148, 82);
            this.cbxSucursales.Name = "cbxSucursales";
            this.cbxSucursales.Size = new System.Drawing.Size(292, 21);
            this.cbxSucursales.TabIndex = 14;
            this.cbxSucursales.SelectedIndexChanged += new System.EventHandler(this.cbxSucursales_SelectedIndexChanged);
            // 
            // labelSucursal
            // 
            this.labelSucursal.AutoSize = true;
            this.labelSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelSucursal.Location = new System.Drawing.Point(9, 84);
            this.labelSucursal.Name = "labelSucursal";
            this.labelSucursal.Size = new System.Drawing.Size(133, 16);
            this.labelSucursal.TabIndex = 13;
            this.labelSucursal.Text = "Elija una sucursal:";
            // 
            // labelCliente
            // 
            this.labelCliente.AutoSize = true;
            this.labelCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCliente.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelCliente.Location = new System.Drawing.Point(9, 48);
            this.labelCliente.Name = "labelCliente";
            this.labelCliente.Size = new System.Drawing.Size(0, 16);
            this.labelCliente.TabIndex = 12;
            // 
            // labelTitulo
            // 
            this.labelTitulo.AutoSize = true;
            this.labelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitulo.Location = new System.Drawing.Point(8, 15);
            this.labelTitulo.Name = "labelTitulo";
            this.labelTitulo.Size = new System.Drawing.Size(193, 20);
            this.labelTitulo.TabIndex = 11;
            this.labelTitulo.Text = "GESTIÓN DE VENTAS";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.DarkGreen;
            this.label1.Location = new System.Drawing.Point(12, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 18);
            this.label1.TabIndex = 19;
            this.label1.Text = "¡Hola!";
            // 
            // lblNombreCliente
            // 
            this.lblNombreCliente.AutoSize = true;
            this.lblNombreCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNombreCliente.ForeColor = System.Drawing.Color.DarkGreen;
            this.lblNombreCliente.Location = new System.Drawing.Point(69, 51);
            this.lblNombreCliente.Name = "lblNombreCliente";
            this.lblNombreCliente.Size = new System.Drawing.Size(0, 18);
            this.lblNombreCliente.TabIndex = 20;
            // 
            // idVehiculo
            // 
            this.idVehiculo.HeaderText = "ID Vehículo";
            this.idVehiculo.Name = "idVehiculo";
            this.idVehiculo.ReadOnly = true;
            // 
            // marca
            // 
            this.marca.HeaderText = "Marca";
            this.marca.Name = "marca";
            this.marca.ReadOnly = true;
            // 
            // modelo
            // 
            this.modelo.HeaderText = "Modelo";
            this.modelo.Name = "modelo";
            this.modelo.ReadOnly = true;
            // 
            // ano
            // 
            this.ano.HeaderText = "Año";
            this.ano.Name = "ano";
            this.ano.ReadOnly = true;
            // 
            // precio
            // 
            this.precio.HeaderText = "Precio";
            this.precio.Name = "precio";
            this.precio.ReadOnly = true;
            // 
            // categoria
            // 
            this.categoria.HeaderText = "Categoría";
            this.categoria.Name = "categoria";
            this.categoria.ReadOnly = true;
            // 
            // cantidadDisponible
            // 
            this.cantidadDisponible.HeaderText = "Cantidad";
            this.cantidadDisponible.Name = "cantidadDisponible";
            this.cantidadDisponible.ReadOnly = true;
            // 
            // GestionVentas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 411);
            this.Controls.Add(this.lblNombreCliente);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnConsultarHistorial);
            this.Controls.Add(this.btnComprar);
            this.Controls.Add(this.dgvVehiculosDisponibles);
            this.Controls.Add(this.labelVehiculos);
            this.Controls.Add(this.cbxSucursales);
            this.Controls.Add(this.labelSucursal);
            this.Controls.Add(this.labelCliente);
            this.Controls.Add(this.labelTitulo);
            this.Name = "GestionVentas";
            this.Text = "GestionVentas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvVehiculosDisponibles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConsultarHistorial;
        private System.Windows.Forms.Button btnComprar;
        private System.Windows.Forms.DataGridView dgvVehiculosDisponibles;
        private System.Windows.Forms.Label labelVehiculos;
        private System.Windows.Forms.ComboBox cbxSucursales;
        private System.Windows.Forms.Label labelSucursal;
        private System.Windows.Forms.Label labelCliente;
        private System.Windows.Forms.Label labelTitulo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNombreCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn idVehiculo;
        private System.Windows.Forms.DataGridViewTextBoxColumn marca;
        private System.Windows.Forms.DataGridViewTextBoxColumn modelo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ano;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn categoria;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidadDisponible;
    }
}