using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Common;
using Microsoft.AspNetCore.Mvc;

namespace Core31.Web.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class AutoMapperController : Controller
    {
        private IMapper _mapper;

        public AutoMapperController(IMapper mapper) 
        {
            _mapper =mapper;
        }


        public IActionResult Index(string userName)
        {
            Studio source = new Studio() {
                ID = "1",
                Name = "张毅",
                Age = 30,
                CreateDate = DateTime.Now,
                Test1 = "Test1"
            };

            StudioView view = new StudioView();

            //return _mapper.Map(source, view);
            //return _mapper.Map<Studio, StudioView>(source);
            var result = _mapper.Map<StudioView>(source);

            return new OkObjectResult(result);
        }
    }



    

}