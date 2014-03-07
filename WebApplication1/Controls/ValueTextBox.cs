using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ValueTextBox runat=server></{0}:ValueTextBox>")]
    public class ValueTextBox : TextBox
    {
        [Bindable(true)]
        [Category("Custom")]
        [DefaultValue("")]
        [Localizable(true)]
        public string Value
        {
            get
            {
                var s = (String)ViewState["Value"];
                return (s ?? String.Empty);
            }

            set
            {
                ViewState["Value"] = value;
            }
        }
    }
}
