using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TI_ATP
{                                                      /*Trabalho Interdisiplinar:
                                                        LUCAS AMANCIO MANTINI
                                                        TAIS MARIA RAMOS SANTOS*/
    struct Contato //Struct de dados dos contatos.
    {
        public string nome, email, tel;
        public int idade;
    }
    class Program
    {
        //A
        static int contaContatos(string arqA, string arqB)//Procedimento para contar os contatos.
        {
            StreamReader arqFB = new StreamReader(arqA), arqTT = new StreamReader(arqB);
            int contaFB = 0, contaTT = 0, contador = 0;

            while (!arqFB.EndOfStream)//Facebook.
            {
                arqFB.ReadLine();
                arqFB.ReadLine();
                arqFB.ReadLine();
                arqFB.ReadLine();
                contaFB++;
            }
            while (!arqTT.EndOfStream)//Twitter.
            {
                arqTT.ReadLine();
                arqTT.ReadLine();
                arqTT.ReadLine();
                arqTT.ReadLine();
                contaTT++;
            }
            arqFB.Close();
            arqTT.Close();
            contador = contaFB + contaTT;
            return (contador);
        }

        //B
        static void PreencheVetor(Contato[] dados)//Preenchimento de vetores.
        {
            StreamReader facebook = new StreamReader("Facebook.txt"), twitter = new StreamReader("Twitter.txt");
            int i = 0;

            while (!facebook.EndOfStream)//Preenchendo parte do vetor para Facebook.
            {
                dados[i].nome = facebook.ReadLine();
                dados[i].idade = Convert.ToInt32(facebook.ReadLine());
                dados[i].email = facebook.ReadLine();
                dados[i].tel = facebook.ReadLine();
                i++;
            }
            while (!twitter.EndOfStream)//Preenchendo parte do vetor para Twitter.
            {
                dados[i].nome = twitter.ReadLine();
                dados[i].idade = Convert.ToInt32(twitter.ReadLine());
                dados[i].email = twitter.ReadLine();
                dados[i].tel = twitter.ReadLine();
                i++;
            }
            facebook.Close();
            twitter.Close();
        }
        //C
        static void Ordena(Contato[] dados)//Ordena os contatos em ordem alfabética.
        {
            int compara, posicaomenor;
            Contato menor, aux;

            for (int i = 0; i < dados.Length - 1; i++)
            {
                menor = dados[i];
                posicaomenor = i;

                for (int j = (i + 1); j < dados.Length; j++)
                {
                    compara = string.Compare(dados[j].nome, menor.nome, true);

                    if (compara == -1)
                    {
                        menor = dados[j];
                        posicaomenor = j;
                    }
                }
                aux = dados[i];
                dados[i] = dados[posicaomenor];
                dados[posicaomenor] = aux;
            }
        }
        //D
        static void ExcluirContatos(Contato[] dados)//Percorre o vetor principal e exclui os repetidos.
        {
            for (int i = 0; i < dados.Length; i++)
                for (int j = i + 1; j < dados.Length; j++)
                    if (dados[i].nome == dados[j].nome)//Contatos excluídos recebem o valor -1.
                    {
                        dados[j].tel = "-1";
                    }
        }
        //E
        static void Lista(Contato[] dados)
        {
            ExcluirContatos(dados);
            StreamWriter contatos = new StreamWriter("Contatos.txt", false, Encoding.Unicode);//False se não existe. True se existe.

            for (int i = 0; i < dados.Length; i++)//Imprime dados na tela e salva no arquivo "Contatos.txt".
            {
                if ((dados[i].tel) != "-1")
                {
                    if (dados[i].tel != "-1")
                    {
                        Console.WriteLine("\n\n Nome: " + dados[i].nome);
                        contatos.WriteLine(dados[i].nome);
                        Console.WriteLine(" Idade: " + dados[i].idade);
                        contatos.WriteLine(dados[i].idade);
                        Console.WriteLine(" E-mail: " + dados[i].email);
                        contatos.WriteLine(dados[i].email);
                        Console.WriteLine(" Telefone: " + dados[i].tel);
                        contatos.WriteLine(dados[i].tel);
                        Console.WriteLine();
                    }
                }
            }
            contatos.Close();
        }
        //F
        static void PesquisaContato(Contato[] vet, string pesquisa)//Procedimento que pesquisa o nome contato desejado.
        {
            int verif = 0;
            ExcluirContatos(vet);
            for (int i = 0; i < vet.Length; i++)
            {
                if (vet[i].nome == pesquisa)
                {
                    if (vet[i].tel != "-1")
                    {
                        Console.WriteLine("\n Nome: " + vet[i].nome);
                        Console.WriteLine(" Idade: " + vet[i].idade);
                        Console.WriteLine(" Telefone: " + vet[i].tel);
                        Console.WriteLine(" E-mail: " + vet[i].email);
                        verif = 1;
                    }
                }
            }
            if (verif == 0) Console.WriteLine(" Contato não existente!");
        }
        static void Main(string[] args)
        {
            //Declaração de variáveis, vetores etc.
            char opcao = '0';
            StreamReader facebook = new StreamReader(@"Facebook.txt"), twitter = new StreamReader(@"Twitter.txt");

            int aux = contaContatos("Facebook.txt", "Twitter.txt");//Contagem de contatos.
            Contato[] dados = new Contato[aux];//Vetor recebe contagem de contatos.
            string nome;

            //Chama módulo de preencher os vetores.
            PreencheVetor(dados);

            while (opcao != '4')//ULTIMA opção é pra sair
            {
                //Menu de opções.
                Console.Write("Escolha uma opção: ");
                Console.WriteLine("\n 1 - Total de Contatos.");
                Console.WriteLine(" 2 - Pesquisar Contato.");
                Console.WriteLine(" 3 - Listar Contatos");
                Console.WriteLine(" 4 - Sair do Programa.");
                Console.Write("\n Opção desejada: ");

                opcao = Console.ReadKey().KeyChar;

                switch (opcao)
                {
                    case '1':
                        Console.WriteLine("\n\n O total de contatos é " + aux);
                        break;
                    case '2'://Chama módulo que pesquisa o contato.
                        Console.Write("\n\n Digite o contato desejado: ");
                        nome = Console.ReadLine();
                        PesquisaContato(dados, nome);
                        break;
                    case '3':
                        Ordena(dados);
                        ExcluirContatos(dados);
                        Lista(dados);
                        break;
                    case '4':
                        opcao = '4';
                        Console.WriteLine("\n\n\t Fim =) ");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("\n\n Opção inválida!");
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
