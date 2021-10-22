using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Data.Dtos;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        //Injeção de dependencia
        //adicionando o contexto do banco de dados
        private FilmeContext _context;
        private IMapper _mapper;

        //construtor para atribuir o contexto do banco de dados
        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //Método que será utilizado é o Post
        //adicionando um filme 
        [HttpPost]
        //[FromBody] indica que o parametro será passado do corpo da requisição
        public IActionResult AdicionaFilme([FromBody]CreateFilmeDto filmeDto)
        {
            //--------------------------------------------------------------------------//
            //Sem automapper
            //Filme filme = new Filme
            //{
            //    Titulo = filmeDto.Titulo,
            //    Genero = filmeDto.Genero,
            //    Duracao = filmeDto.Duracao,
            //    Diretor = filmeDto.Diretor
            //};

            //Com automapper, nós conseguimo converter um FilmeDto para um filme
            Filme filme = _mapper.Map<Filme>(filmeDto);
            //--------------------------------------------------------------------------//


            //adicionando o filme no banco
            _context.Filmes.Add(filme);
            //salvando no banco de dados
            _context.SaveChanges();
            //retornando o location o header
            return CreatedAtAction(nameof(RecuperarFilmesPorId), new { Id = filme.Id},filme);
        }

        //Recuperando a lista de filme
        [HttpGet]
        public IActionResult RecuperarFilmes()
        {
            //_context.Filmes = todos os filmes
            return Ok(_context.Filmes);
        }

        //Retornando um filme pelo Id, passando o id pela rota
        [HttpGet("{id}")]
        public IActionResult RecuperarFilmesPorId(int id)
        {
            //--------------------------------------------------------------------------//
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            //Faz a mesma coisa que o código acima
            //foreach (Filme filme in filmes)
            //{
            //    if (filme.Id == id)
            //    {
            //        return filme;
            //    }
            //}
            //return null;
            //--------------------------------------------------------------------------//

            if (filme != null)
            {
                //--------------------------------------------------------------------------//
                //Sem automapper
                //ReadFilmeDto filmeDto = new ReadFilmeDto
                //{
                //    Titulo = filme.Titulo,
                //    Diretor = filme.Diretor,
                //    Duracao = filme.Duracao,
                //    Id = filme.Id,
                //    Genero = filme.Genero,
                //    HoaDaConsulta = DateTime.Now

                //};

                //Com automapper, conseguimos converter o nosso filme para ReadFilmeDto
                ReadFilmeDto filmeDto = _mapper.Map<ReadFilmeDto>(filme);
                //--------------------------------------------------------------------------//
                return Ok(filmeDto);
            }
            return NotFound();

            
        }
        

        //atualizando um elemento passando o id pela rota
        [HttpPut("{id}")]
        public IActionResult AtualizaFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            //pega no banco de dados o primeiro elemento que tiver o Id igual o id passado na rota
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

            if (filme == null)
            {
                return NotFound();
            }

            //--------------------------------------------------------------------------//
            //Sobreescrevendo as informações do filmes com as informações do filmeDto
            _mapper.Map(filmeDto, filme);

            //Faz a mesma coisas que o código acima
            //filme.Titulo = filmeDto.Titulo;
            //filme.Genero = filmeDto.Genero;
            //filme.Duracao = filmeDto.Duracao;
            //filme.Diretor = filmeDto.Diretor;
            //--------------------------------------------------------------------------//

            //salvando as alterações no banco
            _context.SaveChanges();
            //retornando nada
            return NoContent();

        }

        //Deletando um filme com base no Id
        [HttpDelete("{id}")]
        public IActionResult DeletarFilme(int id)
        {
            Filme filme = _context.Filmes.FirstOrDefault(filme => filme.Id == id);

             if (filme == null)
            {
                return NotFound();
            }

            //Removendo do banco de dados
            _context.Remove(filme);

            //salvando as mudança no banco de daos
            _context.SaveChanges();

            //retornando nenhuma informação
            return NoContent();

        }


    }
}
