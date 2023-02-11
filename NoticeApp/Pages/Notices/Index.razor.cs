using DulPager;
using Microsoft.AspNetCore.Components;
using NoticeApp.Models;

namespace NoticeApp.Pages.Notices
{
	public partial class Index
	{
		[Inject]
		public INoticeRepositoryAsync NoticeRepositoryAsyncReference { get; set; }

		[Inject]
		public NavigationManager NavigationManagerReference { get; set; }

		protected List<Notice> models;

        private string searchQuery;

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
			//await Task.Delay(3000);
			var resultSet = await NoticeRepositoryAsyncReference.GetAllAsync(pager.PageIndex, pager.PageSize);
			pager.PageCount = resultSet.TotalRecords;
			models = resultSet.Records.ToList();
		}

        private async Task SearchData()
        {
            //await Task.Delay(3000);
            var resultSet = await NoticeRepositoryAsyncReference.SearchAllAsync(pager.PageIndex, pager.PageSize, searchQuery);
            pager.PageCount = resultSet.TotalRecords;
            models = resultSet.Records.ToList();
        }

        protected void NameClick(int id)
		{
			NavigationManagerReference.NavigateTo($"/Notices/Details/{id}");
		}

		protected async void PageIndexChanged(int pageIndex)
		{
			pager.PageIndex = pageIndex;
			pager.PageNumber = pageIndex + 1;

            if (String.IsNullOrEmpty(this.searchQuery))
                await DisplayData();
            else
                await SearchData();

			StateHasChanged();
        }

		protected async void Search(string query)
		{
			this.searchQuery = query;
			await SearchData();
        }
	}
}
