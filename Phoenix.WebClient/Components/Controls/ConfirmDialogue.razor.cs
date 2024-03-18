namespace Phoenix.WebClient.Components.Controls
{
    public partial class ConfirmDialogue
    {
        [Parameter]
        public bool Show { get; set; }

        [Parameter]
        public EventCallback OnCancel { get; set; }

        [Parameter]
        public EventCallback OnOk { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}