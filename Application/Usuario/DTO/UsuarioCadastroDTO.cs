﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesafioCCAA.Application.Usuario.DTO
{
    public class UsuarioCadastroDTO
    {
        public string Nome { get; set; } 
        public string Email { get; set; } 
        public DateTime DataNascimento { get; set; }
        public string Senha { get; set; }

    }
}
