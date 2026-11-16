
using GestaoJogosEFCore.Models;

namespace GestaoJogosEFCore.Repositories.Services
{
    public class JogoService
    {

        private readonly JogoRepository _repository;
        
        public JogoService (JogoRepository  repository)
        {
            _repository = repository;
        }


    public List<Jogo> Listar ()
        {
            return _repository.Listar();
        }
        public Jogo? BuscarPorId(int id)=> _repository.BuscarPorId(id);

        public void Cadastrar (Jogo jogo) => _repository.Adicionar(jogo);
    
        public bool Atualizar (Jogo jogoAtualizado) => _repository.Atualizar(jogoAtualizado);
        public bool Excluir (int Id) => _repository.Excluir(Id);



        public List<Desenvolvedora> ListarDesenvolvedoras() => _repository.ListarDesenvolvedora();
        public void CadastrarDesenvolvedoras(Desenvolvedora novaDesenvolvedora) => _repository.AdicionarDesenvolvedora(novaDesenvolvedora);

        

    }
}