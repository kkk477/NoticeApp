using DulPager;
using Microsoft.AspNetCore.Components;
using NoticeApp.Models;
using NoticeApp.Pages.Notices.Components;

namespace NoticeApp.Pages.Notices
{
	public partial class Manage
	{
		[Inject]
		public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

		[Inject]
		public NavigationManager NavigationManagerReference { get; set; }

		public EditorForm EditorFormReference { get; set; }
		public DeleteDialog DeleteDialogReference { get; set; }
		protected List<Notice> models;
		private Notice model = new Notice();

		protected DulPagerBase pager = new DulPagerBase()
		{
			PageNumber = 1,
			PageIndex = 0,
			PageSize = 2,
			PagerButtonCount = 5,
		};
		protected override async Task OnInitializedAsync()
		{
			await DisplayData();
		}

		private async Task DisplayData()
		{
			//await Task.Delay(3000);
			var resultSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize);
			pager.PageCount = resultSet.TotalRecords;
			models = resultSet.Records.ToList();

			StateHasChanged();
		}

		protected void NameClick(int id)
		{
			NavigationManagerReference.NavigateTo($"/Notices/Details/{id}");
		}

		protected async void PageIndexChanged(int pageIndex)
		{
			pager.PageIndex = pageIndex;
			pager.PageNumber = pageIndex + 1;

			await DisplayData();
		}

		public string EditorFormTitle { get; set; } = "CREATE";
		protected async void ShowEditorForm()
		{
			EditorFormTitle = "CREATE";
			model = new Notice();
			EditorFormReference.Show();
		}

		protected async void EditBy(Notice model)
		{
			EditorFormTitle = "EDIT";
			this.model = model;
			EditorFormReference.Show();
		}

		protected async void DeleteBy(Notice model)
		{
			this.model = model;
			DeleteDialogReference.Show();
		}

		protected async void DeleteClick()
		{
			await NoticeRepositoryAsyncReference.DeleteAsync(this.model.Id);
			DeleteDialogReference.Hide();
			this.model = new Notice();
			await DisplayData();
		}

		protected async void CreateOrEdit()
		{
			EditorFormReference.Hide();
			await DisplayData();
		}
	}
}
