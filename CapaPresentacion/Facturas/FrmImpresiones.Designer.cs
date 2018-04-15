namespace CapaPresentacion
{
    partial class FrmImpresiones
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImpresiones));
            this.ImprimirBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DsPrincipal = new CapaPresentacion.DsPrincipal();
            this.EstadoDeCuentaBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DsEstadoDeCuenta = new CapaPresentacion.DsEstadoDeCuenta();
            this.FacturasMensualBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DsReporteFacturas = new CapaPresentacion.DsReporteFacturas();
            this.ImprimirTableAdapter = new CapaPresentacion.DsPrincipalTableAdapters.ImprimirTableAdapter();
            this.RvFactura = new Microsoft.Reporting.WinForms.ReportViewer();
            this.RvEstadoDeCuenta = new Microsoft.Reporting.WinForms.ReportViewer();
            this.EstadoDeCuentaTableAdapter = new CapaPresentacion.DsEstadoDeCuentaTableAdapters.EstadoDeCuentaTableAdapter();
            this.RvReporteFacturas = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FacturasMensualTableAdapter = new CapaPresentacion.DsReporteFacturasTableAdapters.FacturasMensualTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.ImprimirBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.EstadoDeCuentaBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsEstadoDeCuenta)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacturasMensualBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsReporteFacturas)).BeginInit();
            this.SuspendLayout();
            // 
            // ImprimirBindingSource
            // 
            this.ImprimirBindingSource.DataMember = "Imprimir";
            this.ImprimirBindingSource.DataSource = this.DsPrincipal;
            // 
            // DsPrincipal
            // 
            this.DsPrincipal.DataSetName = "DsPrincipal";
            this.DsPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // EstadoDeCuentaBindingSource
            // 
            this.EstadoDeCuentaBindingSource.DataMember = "EstadoDeCuenta";
            this.EstadoDeCuentaBindingSource.DataSource = this.DsEstadoDeCuenta;
            // 
            // DsEstadoDeCuenta
            // 
            this.DsEstadoDeCuenta.DataSetName = "DsEstadoDeCuenta";
            this.DsEstadoDeCuenta.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FacturasMensualBindingSource
            // 
            this.FacturasMensualBindingSource.DataMember = "FacturasMensual";
            this.FacturasMensualBindingSource.DataSource = this.DsReporteFacturas;
            // 
            // DsReporteFacturas
            // 
            this.DsReporteFacturas.DataSetName = "DsReporteFacturas";
            this.DsReporteFacturas.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ImprimirTableAdapter
            // 
            this.ImprimirTableAdapter.ClearBeforeFill = true;
            // 
            // RvFactura
            // 
            this.RvFactura.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DsFactura";
            reportDataSource1.Value = this.ImprimirBindingSource;
            this.RvFactura.LocalReport.DataSources.Add(reportDataSource1);
            this.RvFactura.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Facturas.Factura.rdlc";
            this.RvFactura.Location = new System.Drawing.Point(0, 0);
            this.RvFactura.Name = "RvFactura";
            this.RvFactura.Size = new System.Drawing.Size(955, 422);
            this.RvFactura.TabIndex = 0;
            // 
            // RvEstadoDeCuenta
            // 
            this.RvEstadoDeCuenta.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DsCuenta";
            reportDataSource2.Value = this.EstadoDeCuentaBindingSource;
            this.RvEstadoDeCuenta.LocalReport.DataSources.Add(reportDataSource2);
            this.RvEstadoDeCuenta.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Pagos.EstadoDeCuenta.rdlc";
            this.RvEstadoDeCuenta.Location = new System.Drawing.Point(0, 0);
            this.RvEstadoDeCuenta.Name = "RvEstadoDeCuenta";
            this.RvEstadoDeCuenta.ShowExportButton = false;
            this.RvEstadoDeCuenta.Size = new System.Drawing.Size(955, 422);
            this.RvEstadoDeCuenta.TabIndex = 1;
            // 
            // EstadoDeCuentaTableAdapter
            // 
            this.EstadoDeCuentaTableAdapter.ClearBeforeFill = true;
            // 
            // RvReporteFacturas
            // 
            this.RvReporteFacturas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RvReporteFacturas.DocumentMapWidth = 17;
            reportDataSource3.Name = "DsReporte";
            reportDataSource3.Value = this.FacturasMensualBindingSource;
            this.RvReporteFacturas.LocalReport.DataSources.Add(reportDataSource3);
            this.RvReporteFacturas.LocalReport.ReportEmbeddedResource = "CapaPresentacion.Reportes.ReporteFacturasMensual.rdlc";
            this.RvReporteFacturas.Location = new System.Drawing.Point(0, 0);
            this.RvReporteFacturas.Name = "RvReporteFacturas";
            this.RvReporteFacturas.Size = new System.Drawing.Size(955, 422);
            this.RvReporteFacturas.TabIndex = 2;
            // 
            // FacturasMensualTableAdapter
            // 
            this.FacturasMensualTableAdapter.ClearBeforeFill = true;
            // 
            // FrmImpresiones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(955, 422);
            this.Controls.Add(this.RvReporteFacturas);
            this.Controls.Add(this.RvEstadoDeCuenta);
            this.Controls.Add(this.RvFactura);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmImpresiones";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = ".:. MAFA\'s Facturación - Imprimir Factura .:.";
            this.Load += new System.EventHandler(this.FrmImpresiones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ImprimirBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.EstadoDeCuentaBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsEstadoDeCuenta)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FacturasMensualBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DsReporteFacturas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.BindingSource ImprimirBindingSource;
        private DsPrincipal DsPrincipal;
        private Microsoft.Reporting.WinForms.ReportViewer RvFactura;
        private Microsoft.Reporting.WinForms.ReportViewer RvEstadoDeCuenta;
        private System.Windows.Forms.BindingSource EstadoDeCuentaBindingSource;
        private DsEstadoDeCuenta DsEstadoDeCuenta;
        private Microsoft.Reporting.WinForms.ReportViewer RvReporteFacturas;
        private System.Windows.Forms.BindingSource FacturasMensualBindingSource;
        private DsReporteFacturas DsReporteFacturas;
        public DsPrincipalTableAdapters.ImprimirTableAdapter ImprimirTableAdapter;
        public DsEstadoDeCuentaTableAdapters.EstadoDeCuentaTableAdapter EstadoDeCuentaTableAdapter;
        public DsReporteFacturasTableAdapters.FacturasMensualTableAdapter FacturasMensualTableAdapter;
    }
}