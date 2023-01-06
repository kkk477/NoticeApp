namespace NoticeApp.Models
{
	/// <summary>
	/// [3] Generic Repository Interface
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ICrudRepositoryAsync<T>
	{
		Task<T> AddAsync(T model);	// 입력
		Task<T> GetAllAsync();	// 출력
		Task<T> GetByIdAsync(int id);	// 상세
		Task<T> EditAsync(T model);	// 수정
		Task<T> DeleteAsync(int id);	// 삭제
		Task<PagingResult<T>> GetAllAsync(int pageIndex, int pageSize);	// 페이징
		Task<PagingResult<T>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId);	// 부모
	}
}
