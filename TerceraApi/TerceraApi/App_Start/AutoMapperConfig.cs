using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TerceraApi.App_Start;
using TerceraApi.Models;
[assembly: PreApplicationStartMethod(typeof(AutoMapperConfig), "Configure")]
namespace TerceraApi.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.Initialize(cfg => cfg.AddProfile(new SuperheroAutoMapperProfile()));
        }
    }
}