﻿using MinhasFinancasAPI.Entities;
using System.Collections.Generic;

namespace MinhasFinancasAPI.Service.Interface
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();

        void SaveUser(User user);

        void SaveRegister(Registro user);

        User RetornaUsuarioLogado(User user);

        string VerificaToken(string token);

    }
}
