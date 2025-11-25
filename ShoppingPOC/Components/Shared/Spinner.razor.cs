using Microsoft.AspNetCore.Components;

namespace ShoppingPOC.Components.Shared
{
    public partial class Spinner
    {
        [Parameter] public string Label { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            if (string.IsNullOrEmpty(Label))
            {
                Label = "Please wait...";
            }
        }

    }
}