using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoticeApp.Models
{
	/// <summary>
	/// [2] Model Class
	/// </summary>
	[Table("Notices")]
	public class Notice
	{
		/// <summary>
		/// Serial Number
		/// </summary>
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		/// <summary>
		/// 외래키
		/// </summary>
		public int? ParentId { get; set; }

		/// <summary>
		/// 이름
		/// </summary>
		[Required(ErrorMessage = "이름을 입력하세요.")]
		[MaxLength(255)]
		public string Name { get; set; }

		/// <summary>
		/// 제목
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 내용
		/// </summary>
		public string? Contents { get; set; }

		/// <summary>
		/// 상단 고정
		/// </summary>
		public bool IsPinned { get; set; }

		/// <summary>
		/// 등록자
		/// </summary>
		public string? CreatedBy { get; set; }

		/// <summary>
		/// 등록일
		/// </summary>
		public DateTime Created { get; set; }

		/// <summary>
		/// 수정자
		/// </summary>
		public string? ModifiedBy { get; set; }

		/// <summary>
		/// 수정일
		/// </summary>
		public DateTime? Modified { get; set; }
	}
}
