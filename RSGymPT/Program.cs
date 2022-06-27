using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSGymPT
{
    class Program
    {
        static void Main(string[] args)
        {
            // Menu
            Gym gym = new Gym();
            
            bool showMenu = true;

            Console.WriteLine("Bem vinso ao RSGymPT");
            Console.WriteLine("Escreva uma opção ou GymHelp para ajuda.");

            while (showMenu)
            {
                gym.RegistarComando();
                showMenu = gym.DecidirMenu();
            }

            


 

        }
    }
}
