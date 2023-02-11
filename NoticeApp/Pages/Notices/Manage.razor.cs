using DulPager;
using Microsoft.AspNetCore.Components;
using NoticeApp.Models;
using NoticeApp.Pages.Notices.Components;

namespace NoticeApp.Pages.Notices
{
	public partial class Manage
	{
		[Parameter]
		public int ParentId { get; set; }

		[Inject]
		public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

		[Inject]
		public NavigationManager NavigationManagerReference { get; set; }

		public EditorForm EditorFormReference { get; set; }
		public DeleteDialog DeleteDialogReference { get; set; }
		protected List<Notice> models;
		private Notice model = new Notice();

		private string searchQuery;

		/// <summary>
		/// 공지사항으로 올리기 폼을 띄울건지 여부
		/// </summary>
		public bool IsInlineDialogShow { get; set; } = false;

		protected DulPagerBase pager = new DulPagerBase()
		{
			PageNumber = 1,
			PageIndex = 0,
			PageSize = 2,
			PagerButtonCount = 5,
		};
		protected override async Task OnInitializedAsync()
		{
			if (String.IsNullOrEmpty(this.searchQuery))
				await DisplayData();
			else
				await SearchData();
		}

		private async Task DisplayData()
		{
			if (ParentId == 0)
			{
				//await Task.Delay(3000);
				var resultSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize);
				pager.PageCount = resultSet.TotalRecords;
				models = resultSet.Records.ToList();
			}
			else
			{
				var resultSet = await NoticeRepositoryAsyncReference.GetAllByParentIdAsync(pager.PageIndex, pager.PageSize, ParentId);
				pager.PageCount = resultSet.TotalRecords;
				models = resultSet.Records.ToList();
			}

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

		protected async void ToggleBy(Notice model)
		{
			this.model = model;
			IsInlineDialogShow = !IsInlineDialogShow;
		}

		protected async void DeleteClick()
		{
			await NoticeRepositoryAsyncReference.DeleteAsync(this.model.Id);
			DeleteDialogReference.Hide();
			this.model = new Notice();
			await DisplayData();
		}

		protected async void ToggleClose()
		{
			IsInlineDialogShow = false;
			this.model = new Notice();
		}

		protected async void ToggleClick()
		{
			this.model.IsPinned = !this.model.IsPinned;

			await NoticeRepositoryAsyncReference.EditAsync(this.model);
			IsInlineDialogShow = false;
			this.model = new Notice();
			await DisplayData();
		}

		protected async void CreateOrEdit()
		{
			EditorFormReference.Hide();
			await DisplayData();
		}

		private async Task SearchData()
		{
			if (ParentId == 0)
			{
				var resultSet = await NoticeRepositoryAsyncReference.SearchAllAsync(pager.PageIndex, pager.PageSize, searchQuery);
				pager.PageCount = resultSet.TotalRecords;
				models = resultSet.Records.ToList();
			}
			else
			{
				var resultSet = await NoticeRepositoryAsyncReference.SearchAllByParentIdAsync(pager.PageIndex, pager.PageSize, searchQuery, ParentId);
				pager.PageCount = resultSet.TotalRecords;
				models = resultSet.Records.ToList();
			}
		}

		protected async void Search(string query)
		{
			this.searchQuery = query;
			await SearchData();
		}
	}
}
