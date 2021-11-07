using System;
using System.Collections.Generic;

namespace Projeto.Lib.Repositorios
{
    public interface IRepositorio<T>
    {
        T Criar(T t);
        T ObterPorIdentificador(Guid guid);
        List<T> ObterTodos();
        void Atualizar(T tOld, T tNew);        void Apagar(T t);
    }
}
