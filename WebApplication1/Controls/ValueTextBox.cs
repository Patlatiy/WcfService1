using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebStore.Controls
{
    /// <summary>
    /// Just a regular TextBox with one additional field: value (string)
    /// </summary>
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
