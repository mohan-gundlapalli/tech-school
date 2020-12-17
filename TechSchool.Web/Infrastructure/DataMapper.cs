using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechSchool.Web.DTO;
using TechSchool.Web.Models;

namespace TechSchool.Web.Infrastructure
{
    public static class DataMapper
    {
        public static IMapper Mapper;


        public static D Map<D>(object source){
            string sourceType = source.GetType().FullName;
            string destinationType = typeof(D).FullName;
            return Mapper.Map<D>(source);
        }
        public static D Map<S, D>(S source, D destination) => Mapper.Map<S, D>(source, destination);

        public static void InitMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Student, StudentDTO>();
            });

            Mapper =  config.CreateMapper();
        }

        private static void CreateMap<S, T>(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<S, T>();
            cfg.CreateMap<T, S>();
        }
    }
}




    


