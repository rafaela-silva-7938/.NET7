using Biblioteca.Application.Commands;
using Biblioteca.Application.Responses;
using Biblioteca.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Application.Interfaces
{
    public interface IUsuarioApplicationService : IBaseApplicationService<Usuario, Int32>
    {
        void CriarConta(UsuarioCreateCommand command);
        void UpdatePreSenha(UsuarioPreUpdateSenhaCommand command);
        void UpdateSenha(UsuarioUpdateSenhaCommand command);
        Task<UsuarioValidarCredenciaisResponse> ValidarCredenciais(string email, string senha);
        Task<UsuarioObterUsuarioPorEmailResponse> ObterUsuarioPorEmail(string email);
        Task<UsuarioObterUsuarioPorTokenResponse> ObterUsuarioPorToken(string token);
        Task<List<GetAllUsuariosComLivrosAsyncResponse>> GetAllUsuariosComLivrosAsync();
    }
}
