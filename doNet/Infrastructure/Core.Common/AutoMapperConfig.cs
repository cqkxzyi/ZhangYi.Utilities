using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace Core.Common
{

    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Studio, StudioView>().ReverseMap(); //表示双向映射

            var temp = CreateMap<Studio, StudioView>();
            temp = temp.ForMember(d => d.Name, opt => opt.MapFrom(src => src.Name));//字段映射
            temp = temp.ForMember(d => d.Test2, opt => opt.MapFrom(src => src.Test1));//字段映射
            temp = temp.ForMember(d => d.Age, opt => opt.Ignore());//不映射字段
        }
    }



    public class Studio
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateDate { get; set; }
        public string Test1 { get; set; }
    }

    public class StudioView
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreateDate { get; set; }
        public string Test2 { get; set; }
        public int Num { get; set; } = 100;
    }
}
