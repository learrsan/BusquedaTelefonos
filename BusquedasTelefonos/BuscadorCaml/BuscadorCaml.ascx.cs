using System;
using System.ComponentModel;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;

namespace BusquedasTelefonos.BuscadorCaml
{
    [ToolboxItemAttribute(false)]
    public partial class BuscadorCaml : WebPart
    {
        // Uncomment the following SecurityPermission attribute only when doing Performance Profiling on a farm solution
        // using the Instrumentation method, and then remove the SecurityPermission attribute when the code is ready
        // for production. Because the SecurityPermission attribute bypasses the security check for callers of
        // your constructor, it's not recommended for production purposes.
        // [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Assert, UnmanagedCode = true)]
        public BuscadorCaml()
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
            using (var web = SPContext.Current.Web)
            {
                var lista = web.Lists["Lista telefonos"];
                var items = lista.GetItems();
                lstTelefonos.DataSource = items.GetDataTable();
                lstTelefonos.DataBind();
            }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            var consulta = String.Format(@"<Where><Eq><FieldRef Name=""Title"" />
                                            <Value Type=""Text"">{0}</Value>
                                            </Eq></Where>", txtFab.Text);
            var query = new SPQuery();
            query.Query = consulta;

            using (var web = SPContext.Current.Web)
            {
                var lista = web.Lists["Lista telefonos"];
                var items = lista.GetItems(query);
                
                lstTelefonos.DataSource = items.GetDataTable();
                lstTelefonos.DataBind();
                
            }
        }
    }
}
