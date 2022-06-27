using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT
{
    internal class Gym
    {

        #region Variáveis
        internal string comando = string.Empty;
        internal string args1 = string.Empty;
        internal string dados1 = string.Empty;
        internal string args2 = string.Empty;
        internal string dados2 = string.Empty;
        internal string args3 = string.Empty;
        internal string dados3 = string.Empty;
        internal bool autenticado = false;
        internal int pedidoID = 0;
        internal string estado = "Pendente";
        internal bool pts = false;

        internal Dictionary<string, string> usersString = new Dictionary<string, string>()
            {
                {"Joana", "123" },
                {"Jorge", "321" }
            };

        internal Dictionary<int, string> pt = new Dictionary<int, string>()
            {
                {1,"Carlos"},
                {2,"António"},
                {3,"Ana"},
                {4,"Mafalda"}
            };

        internal List<Gym> pedidos = new List<Gym>();
        #endregion

        #region Proptiedades
        
        internal string NomePt { get; set; }
        internal DateTime Data { get; set; }
        internal DateTime Hora { get; set; }
        internal string Estado { get; set; }
        internal string Mensagem { get; set; }
        internal int PedidoID { get; set; }

        #endregion

        #region Construtores
        internal Gym()
        {
            PedidoID = 0;
            NomePt = string.Empty;
            Data = DateTime.MinValue;
            Hora = DateTime.MinValue;
            Mensagem = string.Empty;
            Estado = string.Empty;
        }

        internal Gym(int pedidoId, string nomePt, DateTime data, DateTime hora, string mensagem, string estado)
        {
            PedidoID = pedidoId;
            NomePt = nomePt;
            Data = data.Date;
            Hora = hora;
            Mensagem = mensagem;
            Estado = estado;
        }


        #endregion

        #region Metodos

        // Menu
        internal void MainMenu()
        {

            Console.WriteLine("Opções:\n");
            Console.WriteLine("GymHelp \t- Ajuda.");
            Console.WriteLine("GymExit \t- Sair da aplicação.");
            Console.WriteLine("Clear \t- Limpar ecrã.");
            Console.WriteLine("Login \t- Autenticar utilizador. ( -u [user] -p [password] )");
            Console.WriteLine("Request \t- Fazer o pedido do PT indicando: nome, data e hora. ( -n [nomePT] -d [dd.mm.yyyy] -h [HH:mm] )"); //request -n {nome} - d {dia} -h {horas}
            Console.WriteLine("Cancel \t- Anular um pedido. ( -r [Pedido] )"); //cancel -r {nº pedido}
            Console.WriteLine("Finish \t- Finalisar Aula. ( -r [Pedido] )"); //finish -r {nº pedido}
            Console.WriteLine("Message \t- Suspensão de aula. ( -r [Pedido] -s [Mensagem] )"); //message -r {nº pedido} -s {assunto}
            Console.WriteLine("Myrequest \t- Listar o pedido efetuado. ( -r [Pedido] )"); //myrequest -r {nº pedido}
            Console.WriteLine("Requests \t- Listar todos os pedidos. ( -a )"); //requests -a 
            Console.WriteLine("Pt \t- Nomes dos Personal Trainers da academia."); //Pt 

            Console.WriteLine();

        }

        // Registar comando

        internal void RegistarComando()
        {
            string [] entrada;
            string[] separador = new string[] { " " };

            // Manipulando a entrada de dados para obeter comando e argumentos
            entrada = Console.ReadLine().Split(separador,7, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < entrada.Length; i++)
            {
               try
               {
                    comando = entrada[0].ToString();

                    args1 = entrada[1].ToString();

                    dados1 = entrada[2].ToString();

                    args2 = entrada[3].ToString();

                    dados2 = entrada[4].ToString();

                    args3 = entrada[5].ToString();

                    dados3 = entrada[6].ToString();

                }
                catch (System.IndexOutOfRangeException)
                {
                        // permite passar á frente sem dar excepção por erro de indice na matriz                    
                }
            }
        }



        // Decisão do menu
        internal bool DecidirMenu()
        {
            switch (comando)
            {
                case "GymHelp":
                    MainMenu();
                    return true;
                case "GymExit":
                    Console.WriteLine("Obrigado.");
                    Console.ReadKey();
                    return false;
                case "Clear":
                    Console.Clear();
                    return true;
                case "Login":
                    EfetuarLogin();
                    return true;
                case "Request":
                    EfetuarRequest();
                    return true;
                case "Cancel":
                    CancelarPedido();
                    return true;
                case "Finish":
                    TerminarAula();
                    return true;
                case "Message":
                    EnviarMensagem();
                    return true;
                case "Myrequest":
                    ListarPedido();
                    return true;
                case "Requests":
                    ListarPedidos();
                    return true;
                case "Pt":
                    ListarPts();
                    return true;
                default:
                    Console.Clear();
                    Console.WriteLine("Escreva uma opção correcta ou GymHelp para ajuda.\n");
                    MainMenu();
                    return true;
            }

        }

        // Login (utilizadores: Joana, Jorge; Passwords: 123, 321)
        internal void EfetuarLogin()
        {
            autenticado = false;
            
            if (args1 != "-u" || args2 != "-p")
            {
                Console.Clear();
                Console.WriteLine("Os argumentos não estão correctos.\n");
                MainMenu();
            }
            else
            {

                foreach (var user in usersString)
                {
                    if (dados1 == user.Key && dados2 == user.Value)
                    {
                        autenticado = true;
                        break; // para o loop após ter encontrado 1 correcto
                    }
                    else
                    {
                        autenticado = false;
                    }
                }

                if (autenticado)
                {
                    Console.WriteLine("Login efetuado com sucesso !!!");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Utilizador ou password errado, tente de novo o login!!!\n");
                    MainMenu();
                }
            }
        }


        // Listar os PT's
        internal void ListarPts()
        {
            if (autenticado)
            {

                foreach (var item in pt)
                {
                    Console.WriteLine(item.Value);
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nessesário efetuar Login primeiro.\n");
                MainMenu();
            }
        }
 
        // Request
        internal void EfetuarRequest()
        {
            bool d1 = false;
            bool h1 = false;
            DateTime d = DateTime.MinValue;
            DateTime h = DateTime.MinValue;

            if (autenticado)
            {
                foreach (var p in pt)
                {
                    //valida o pt
                    if (dados1 == p.Value)
                    {
                        pts = true;
                        break;
                    }
                    else
                    {
                        pts = false;
                    }
                }

                // converte para datetime
                try
                {
                    d = Convert.ToDateTime(dados2);
                    d1 = true;
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Insira a data no formato correto.\n");
                    d1 = false;
                    
                }

                // converte para datetime
                try
                {
                    h = Convert.ToDateTime(dados3);
                    h1 = true;
                }
                catch (System.FormatException)
                {
                    Console.Clear();
                    Console.WriteLine("Insira a hora no formato correcto HH:mm.\n");
                    h1 = false;
                    
                }

                // Valida os argumentos
                if (args1 != "-n" || args2 != "-d" || args3 != "-h" )
                {
                    Console.Clear();
                    Console.WriteLine("Os argumentos não estão correctos.\n");
                    MainMenu();
                }
                else if (h1 == false || d1 == false)
                {
                    MainMenu();
                }
                else if (pts == false)
                {
                    Console.Clear();
                    Console.WriteLine("Não temos nenhum Personal Trainer com o nome {0}!\n", dados1);
                    MainMenu();
                }
                else
                {

                    // validar duplicados 
                    var da = pedidos.Where(item => item.Data.ToShortDateString() == d.ToShortDateString()).Count();
                    var ho = pedidos.Where(item => item.Hora.ToShortTimeString() == h.ToShortTimeString()).Count();

                    // incrementa o pedidoID
                    pedidoID++;

                    // adiciona á lista
                    Gym g1 = new Gym(pedidoID, dados1, d, h, string.Empty, estado);

                    if (da <=1 && ho == 0)
                    {
                        pedidos.Add(g1);

                        Console.WriteLine("\nPedido enviado.\n");
                        Console.WriteLine("Pedido {0} marcado para {1} ás {2} com o estado {3}.\n", g1.PedidoID, g1.Data.ToShortDateString(), g1.Hora.ToShortTimeString(), g1.Estado);


                        // altera o estado
                        if (estado == "Pendente")
                        {
                            estado = "Autorizado";
                        }
                        else
                        {
                            estado = "Pendente";
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Não pode fazer um pedido com a mesma data e hora.\n");
                        MainMenu();
                    }

                }

            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nessesário efetuar Login primeiro.\n");
                MainMenu();
            }

        }

        internal void ListarPedido()
        {
            if (autenticado)
            {
                if (args1 != "-r")
                {
                    Console.Clear();
                    Console.WriteLine("Os argumentos não estão correctos.\n");
                    MainMenu();
                }
                else
                {
                    try
                    {
                        var pe = pedidos.Where(item => item.PedidoID == Convert.ToInt16(dados1));

                        if (pe.Count() > 0)
                        {

                            foreach (var item in pe)
                            {
                                Console.WriteLine("Pedido: {0}\tNome PT: {1}\tData: {2}\tHora: {3}\tEstado: {4}\n", item.PedidoID, item.NomePt, item.Data.Date, item.Hora, item.Estado);
                            }

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("O pedido não existe.\n");
                            MainMenu();
                        }
                    }
                    catch (System.FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("O pedido não existe.\n");
                        MainMenu();
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nessesário efetuar Login primeiro.\n");
                MainMenu();
            }
        }

        internal void ListarPedidos()
        {
            if (autenticado)
            {
                if (args1 != "-a")
                {
                    Console.Clear();
                    Console.WriteLine("Os argumentos não estão correctos.\n");
                    MainMenu();
                }
                else
                {
                    var ped = pedidos.Where(item => item.PedidoID != 0);

                    if (ped.Count() > 0)
                    {

                        foreach (var item in ped)
                        {

                            Console.WriteLine("Pedido: {0}\tNome PT: {1}\tData: {2}\tHora: {3}\tEstado: {4}\n", item.PedidoID, item.NomePt, item.Data.Date, item.Hora, item.Estado);


                        }
                    }
                    else
                    {

                        Console.Clear();
                        Console.WriteLine("Ainda não efetuou nenhum pedido.\n");
                        MainMenu();

                    }
                }
            }
        }
        internal void CancelarPedido()
        {
            if (autenticado)
            {
                
                if (args1 != "-r")
                {
                    Console.Clear();
                    Console.WriteLine("Os argumentos não estão correctos.\n");
                    MainMenu();
                }
                else
                {
                    var rem = pedidos.SingleOrDefault(item => item.PedidoID == Convert.ToInt16(dados1));

                    if (rem.PedidoID == Convert.ToInt16(dados1))
                    {

                        Console.WriteLine("O pedido {0} foi cancelado.\n", rem.PedidoID);
                        pedidos.Remove(rem);
                        MainMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("O pedido {0} não existe.\n", dados1);
                        MainMenu();
                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Ainda não efetuou nenhum pedido.\n");
                MainMenu();
            }
        }

        internal void TerminarAula()
        {
            if (autenticado)
            {
                
                if (args1 != "-r")
                {
                    Console.Clear();
                    Console.WriteLine("Os argumentos não estão correctos.\n");
                    MainMenu();
                }
                else
                {
                    try
                    {
                        var pedi = pedidos.Where(item => item.PedidoID == Convert.ToInt16(dados1));

                        if (pedi.Count() > 0)
                        {

                            foreach (var item in pedi)
                            {
                                item.Estado = "Aula Concluida";
                                item.Data = DateTime.Now;
                                item.Hora = DateTime.Now;
                                Console.WriteLine("{0} a {1} ás {2} horas.\n", item.Estado, item.Data.ToShortDateString(), item.Hora.ToShortTimeString());
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("O pedido não existe.\n");
                            MainMenu();
                        }
                    }
                    catch (System.FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("O pedido não existe.\n");
                        MainMenu();
                    }
                }
                
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nessesário efetuar Login primeiro.\n");
                MainMenu();
            }
        }

        internal void EnviarMensagem()
        {
            if (autenticado)
            {
                if (args1 != "-r" || args2 != "-s")
                {
                    Console.Clear();
                    Console.WriteLine("Os argumentos não estão correctos.\n");
                    MainMenu();
                }
                else
                {
                    try
                    {
                        var pedid = pedidos.Where(item => item.PedidoID == Convert.ToInt16(dados1));

                        if (pedid.Count() > 0)
                        {

                            foreach (var item in pedid)
                            {
                                item.Data = DateTime.Now;
                                item.Hora = DateTime.Now;
                                item.Mensagem = $"{dados2} {args3} {dados3}";
                                item.Estado = "Aula Cancelada";
                                Console.WriteLine("Mensagem enviada a {0} ás {1} horas com a informação : {2}\n", item.Data.ToShortDateString(), item.Hora.ToShortTimeString(), item.Mensagem);

                            }

                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("O pedido não existe.\n");
                            MainMenu();
                        }
                    }
                    catch (System.FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("O pedido não existe.\n");
                        MainMenu();

                    }
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nessesário efetuar Login primeiro.\n");
                MainMenu();
            }
        }
        #endregion

    }
}
