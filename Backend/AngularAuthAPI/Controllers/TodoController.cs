using AngularAuthAPI.DTO;
using AngularAuthAPI.Interface;
using AngularAuthAPI.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AngularAuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        public IUnitOfWork uow { get; set; }
       
        public TodoController(IUnitOfWork uow)
        {
            this.uow = uow;
        }
        [Authorize]
        [HttpGet("GetTodo")]
        
        public async Task<List<TodoReqDto>> GetAllTodo()
        {
          
            var data = await uow.TodoRepository.ReadTodo(GetUserId());
            //List<TodoReqDto> res = Mapper.Map<List<TodoReqDto>>(data);
            List<TodoReqDto> res=new List<TodoReqDto>();
            foreach (var todo in data)
            {
                TodoReqDto dto = new TodoReqDto();
                dto.title = todo.Title;
                dto.description = todo.Description;
                res.Add(dto);
            }
            return res;
        }
        [Authorize]
        [HttpPost("AddTodo")]
        public async Task<IActionResult> PostTodo(TodoReqDto todoReqDto)
        {
            int userid = GetUserId();
            if (await uow.TodoRepository.TodoAlreadyExist(todoReqDto.title,userid))
            {
                return BadRequest("same title todo exist");
            }
           
            var todo = uow.TodoRepository.AddTodo(todoReqDto.title, todoReqDto.description, userid);
            await uow.SaveAsync();
            return Ok(new {message="Your todo has been added successfully"});
        }
        [Authorize]
        [HttpDelete("DeleteTodo/{title}")]
        public async Task<IActionResult> DeleteTodo(string title)
        {
            int userid = GetUserId();
            if (!await uow.TodoRepository.TodoAlreadyExist(title, userid))
            {
                return BadRequest("no such todo exist");
            }
            await uow.TodoRepository.DeleteTodo(userid, title);
            await uow.SaveAsync();
            return Ok(new { message = "Your todo has been deleted successfully" });
        }
        [Authorize]
        [HttpPost("EditTodo/{title}/{description}")]
        public async Task<IActionResult> EditTodoDesc(string title,string description)
        {
            int userid = GetUserId();
            if (!await uow.TodoRepository.TodoAlreadyExist(title, userid))
            {
                return BadRequest("no such todo exist");
            }
            await uow.TodoRepository.EditTodoDesc(userid, title, description);
            await uow.SaveAsync();
            return Ok(new {message="Todo list description has been updated"});
        }
        private int GetUserId()
        {
            var authorizationHeader = HttpContext.Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            JwtSecurityTokenHandler jwttoken = new JwtSecurityTokenHandler();
            var ftoken = jwttoken.ReadJwtToken(token);
            var userId = int.Parse(ftoken.Claims.FirstOrDefault(claim => claim.Type == "id").Value);
            return userId;
        }
    }
}
