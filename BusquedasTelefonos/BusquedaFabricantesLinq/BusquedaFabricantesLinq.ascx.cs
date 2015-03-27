using System;
using System.ComponentModel;
using System.Linq;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace BusquedasTelefonos.BusquedaFabricantesLinq
{
    [ToolboxItemAttribute(false)]
    public partial class BusquedaFabricantesLinq : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public BusquedaFabricantesLinq()
        {
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitializeControl();
        }

         protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Page.IsPostBack) { 
            using (var ctx = new ModeloDataContext("http://localhost"))
            {
                
                lstTelefonos.DataSource = ctx.ListaTelefonos;
                lstTelefonos.DataBind();
            }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            

            using (var ctx=new ModeloDataContext ("http://localhost"))
            {
                var data = ctx.ListaTelefonos.Where(o => o.Title == txtFab.Text);
                
                
                lstTelefonos.DataSource = data;
                lstTelefonos.DataBind();
                
            }
        }
        }
}

        