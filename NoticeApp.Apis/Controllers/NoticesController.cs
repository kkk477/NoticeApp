using Microsoft.AspNetCore.Mvc;
using NoticeApp.Models;

namespace NoticeApp.Apis.Controllers
{
    [Produces("application/json")]
    [Route("api/Notices")]
    public class NoticesController : ControllerBase
    {
        private readonly INoticeRepositoryAsync _repository;
        public ILogger _logger { get; }

        public NoticesController(INoticeRepositoryAsync repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger(nameof(NoticesController));
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] Notice model)
        {
            var tmpModel = new Notice();
            tmpModel.Name = model.Name;
            tmpModel.Title = model.Title;
            tmpModel.Contents = model.Contents;
            tmpModel.ParentId = model.ParentId;
            tmpModel.Created = DateTime.Now;

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var newModel = await _repository.AddAsync(tmpModel);
                if (newModel == null)
                    return BadRequest();

                //return Ok(newModel);
                var uri = Url.Link("GetNoticeById", new {id = newModel.Id});
                return Created(uri, newModel); // 201 Created
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 출력
        // GET api/Notices
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var notices = await _repository.GetAllAsync();
                return Ok(notices);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 상세
        // GET api/Notices/1
        [HttpGet("{id}", Name = "GetNoticeById")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                var model = await _repository.GetByIdAsync(id);
                return Ok(model);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 수정
        // PUT api/Notices/1
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAsync(int id, [FromBody] Notice model)
        {
            model.Id = id;

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                var status = await _repository.EditAsync(model);
                if (!status)
                {
                    return BadRequest();
                }

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 삭제
        // DELETE api/Notices/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var status = await _repository.DeleteAsync(id);
                if(!status)
                    return BadRequest();

                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }

        // 페이징
        // GET api/Notices/Page/0/10
        [HttpGet("Page/{pageIndex}/{pageSize}")]
        public async Task<IActionResult> GetAll(int pageIndex, int pageSize)
        {
            try
            {
                var resultSet = await _repository.GetAllAsync(pageIndex, pageSize);

                // 응답 헤더에 총 레코드 수를 담아서 출력
                Response.Headers.Add("X-TotalRecordCount", resultSet.TotalRecords.ToString());
                Response.Headers.Add("Access-Control-Expose-Headers", "X-TotalRecordCount");

                return Ok(resultSet.Records);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest();
            }
        }
    }
}
