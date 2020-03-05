using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core31.WebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Core31.WebApi.Controllers
{
    /// <summary>
    /// Api/Test 测试
    /// </summary>
    //[Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 这是一个带参数的get请求Test3
        /// </summary>
        /// <remarks>
        /// 例子:Get api/Test/Test3/zhangyi
        /// </remarks>
        /// <param name="name">名字</param>
        /// <param name="age">年龄</param>
        /// <returns>是否成功</returns> 
        /// <response code="200">成功啦 哈哈</response>
        /// <response code="201">201错误</response>
        /// <response code="400">400错误</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public ActionResult<string> Test3(string name,int age)
        {
            return new OkObjectResult(new {  name });
        }

        [HttpGet]
        public ActionResult<string> GetId1(int id)
        {
            return $"你请求的 id 是 {id}";
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetId2(int id)
        {
            return $"你请求的 id 是 {id}";
        }

        /// <summary>
        /// 这是一个带参数的get请求
        /// </summary>
        /// <remarks>
        /// 例子:Get api/Test/Save
        /// </remarks>
        /// <param name="model">请求实体</param>
        /// <returns>是否成功</returns> 
        /// <response code="201">201错误</response>
        /// <response code="200">成功</response>
        [HttpPost]
        public ActionResult<string> Save([FromBody] TestModel model)
        {
            return new OkObjectResult(new { msg = $"保存成功 {model.Name}" });
        }


    }




}