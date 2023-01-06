using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeApp.Models
{
	/// <summary>
	/// [6] Repository Class
	/// </summary>
	public class NoticeRepositoryAsync : INoticeRepositoryAsync
	{
		public Task<Notice> AddAsync(Notice model)
		{
			throw new NotImplementedException();
		}

		public Task<Notice> DeleteAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<Notice> EditAsync(Notice model)
		{
			throw new NotImplementedException();
		}

		public Task<Notice> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<PagingResult<Notice>> GetAllAsync(int pageIndex, int pageSize)
		{
			throw new NotImplementedException();
		}

		public Task<PagingResult<Notice>> GetAllByParentIdAsync(int pageIndex, int pageSize, int parentId)
		{
			throw new NotImplementedException();
		}

		public Task<Notice> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}
	}
}
