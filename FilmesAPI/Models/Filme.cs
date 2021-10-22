using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmesAPI.Models
{
    public class Filme
    {
        //Modelando um objeto para o banco de dados


        //Titulo é obrigatório ErrorMessage = "Mensagem de erro que aparecerá para o usuário"
        [Required(ErrorMessage ="O campo titulo é obrigatório")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "O campo diretor é obrigatório")]
        public string Diretor { get; set; }
        [StringLength(30, ErrorMessage ="O genêro não pode passar de 30 caracteres")]
        public string Genero { get; set; }

        //Duração válida vai de 1 a 600 minutos
        [Range(1,600, ErrorMessage = "A duração deve ter no mínimo 1 e no máximo 600 minutos")]
        public int Duracao { get; set; }

        [Key]
        [Required]
        public int Id { get; set; }
    }
}
