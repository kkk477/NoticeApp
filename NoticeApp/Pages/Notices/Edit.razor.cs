using Microsoft.AspNetCore.Components;
using NoticeApp.Models;

namespace NoticeApp.Pages.Notices
{
	public partial class Edit
	{
		[Parameter]
		public int Id { get; set; }

		[Inject]
		public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

		[Inject]
		public NavigationManager NavigationManagerReference { get; set; }

		protected Notice model = new Notice();

		public string ParentId { get; set; }

		protected int[] parentIds = { 1, 2, 3 };

		protected string contents = string.Empty;
		
		protected override async Task OnInitializedAsync()
		{
			model = await NoticeRepositoryAsyncReference.GetByIdAsync(Id);
			contents = Dul.HtmlUtility.EncodeWithTabAndSpace(model.Contents);
			ParentId = model.ParentId.ToString();
		}

		protected async void FormSubmit()
		{
			int.TryParse(ParentId, out int parentId);
			model.ParentId = parentId;
			await NoticeRepositoryAsyncReference.EditAsync(model);
			NavigationManagerReference.NavigateTo("/Notices");
		}
	}
}
