using AutoMapper;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Profiles
{

    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            //definindo os mapeamentos que serão utilizados
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<Filme, ReadFilmeDto>(); 
            CreateMap<UpdateFilmeDto, Filme >();

        }
    }
}
