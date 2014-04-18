using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:RoleList runat=server></{0}:RoleList>")]
    public class ListWithValue : DropDownList
    {
        [Bindable(true)]
        [Category("Custom")]
        [DefaultValue(0)]
        [Localizable(true)]
        public string Value
        {
            get
            {
                var s = (string)ViewState["Value"];
                return (s ?? string.Empty);
            }

            set
            {
                ViewState["Value"] = value;
            }
        }
    }
}
