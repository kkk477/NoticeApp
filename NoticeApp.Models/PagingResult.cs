namespace NoticeApp.Models
{
	public struct PagingResult<T>
	{
		public IEnumerable<T> Records { get; set; }
		public int TotalRecords { get; set; }

		public PagingResult(IEnumerable<T> items, int totalRecords)
		{
			Records = items;
			TotalRecords = totalRecords;
		}
	}
}
