using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NoticeApp.Models;

namespace NoticeApp.Pages.Notices
{
	public partial class Delete
	{
		[Parameter]
		public int Id { get; set; }

		[Inject]
		public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

		[Inject]
		public NavigationManager NavigationManagerReference { get; set; }

		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		protected Notice model = new Notice();

		protected string contents = string.Empty;
		protected override async Task OnInitializedAsync()
		{
			model = await NoticeRepositoryAsyncReference.GetByIdAsync(Id);
			contents = Dul.HtmlUtility.EncodeWithTabAndSpace(model.Contents);
		}

		protected async void DeleteClick()
		{
			bool isDelete = await JSRuntime.InvokeAsync<bool>("confirm", $"{Id}번 글을 정말로 삭제하시겠습니까?");

			if (isDelete)
			{
				await NoticeRepositoryAsyncReference.DeleteAsync(Id);
				NavigationManagerReference.NavigateTo("/Notices");
			}
			else
			{
				await JSRuntime.InvokeAsync<object>("alert", "취소되었습니다.");
			}
		}
	}
}
