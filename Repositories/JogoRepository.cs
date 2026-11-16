
using GestaoJogosEFCore.Data;
using GestaoJogosEFCore.Models;
using Microsoft.EntityFrameworkCore;


namespace GestaoJogosEFCore.Repositories
{
    public class JogoRepository
    {
        
        private readonly DbContextOptions<GestaoJogosContext> _options;  //*Guarda a configuação do EF Core para criar contextos nas operações do repositorio



    public JogoRepository(DbContextOptions<GestaoJogosContext> options)
        {
           
            _options=options;   //*recebe as opções configuradas na inicialização da aplicação
        }

        public List<Jogo> Listar()
        {
            
            using var context = new GestaoJogosContext(_options);  //*Cada operação usa seu proprio contexto; using descarta o contexto ao finalizar o metodo
            return context.Jogo     
            .Include(j => j.IdDesenvolvedoraNavigation)  //* Include carega a nevegação da desenvolvedora junto com os jogos (parecido com join)
            .OrderBy(j => j.IdJogo)                      //* Ordenar os dados por Id em ordem crescente 
            .ToList();                                   //* Executa a consulta no banco de dados e devolve-os em uma lista 


        }

    public Jogo? BuscarPorId(int id)
        {
            using var context = new GestaoJogosContext(_options); 

            return context.Jogo
            .Include(j => j.IdDesenvolvedoraNavigation)
            .FirstOrDefault(j => j.IdJogo == id);
        }

        public void Adicionar(Jogo jogo)
        {
            using var context =new GestaoJogosContext(_options);
             context.Jogo.Add(jogo);
             context.SaveChanges(); //* como se fosse o commit no banco de dados

        }


        public bool Atualizar (Jogo JogoAtualizado)
        {
            using var context =new GestaoJogosContext(_options);
            var jogo = context.Jogo.Find(JogoAtualizado.IdJogo); //* procura pela Pk e rastreia a entidade encontrada

            if (jogo is null)
            {
                return false;
            }

                //*Copiando para a entidade encontrada os valores recebidos pelo objeto JogoAtualizado
             jogo.IdDesenvolvedora=JogoAtualizado.IdDesenvolvedora;
             jogo.Nome=JogoAtualizado.Nome;
             jogo.Genero = JogoAtualizado.Genero;
             jogo.AnoLancamento=JogoAtualizado.AnoLancamento;
             jogo.Preco=JogoAtualizado.Preco;

             context.SaveChanges();
             return true;

        
        }
       
        public bool Excluir(int id)
        {
            using var context =new GestaoJogosContext(_options);
            var jogo = context.Jogo.Find(id);

             if (jogo is null)
            {
                return false;
            }

            context.Jogo.Remove(jogo);  //*Exclui  o jogo como o DELETE faria pelo SQL
            context.SaveChanges();
            return true;
        }

        public List<Desenvolvedora> ListarDesenvolvedora()
        {
             using var context = new GestaoJogosContext(_options);

             return context.Desenvolvedora
             .OrderBy(d=> d.Nome)
             .ToList();

        }

        public void AdicionarDesenvolvedora(Desenvolvedora desenvolvedora)
        {
            using var context = new GestaoJogosContext(_options);
            context.Desenvolvedora.Add(desenvolvedora);
            context.SaveChanges();
        }
    }
}