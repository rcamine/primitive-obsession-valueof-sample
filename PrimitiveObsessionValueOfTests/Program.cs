using System;
using ValueOf;

namespace PrimitiveObsessionValueOfTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var cpfnew1 = new CpfNormal { Value = "1828128128" };
            var cpfnew2 = new CpfNormal { Value = "3247342432" };
            var cpfnew3 = new CpfNormal { Value = "1828128128" };
            //False em ambos pois mesmo que o Value seja o mesmo, o endereço de alocação na memória é diferente.
            //Para obter true, seria necessário implementar o Equals() e o GetHashCode() na mão
            Console.WriteLine($"CpfNormal 1: Valor: {cpfnew1.Value} | HashCode: {cpfnew1.GetHashCode()}");
            Console.WriteLine($"CpfNormal 2: Valor: {cpfnew2.Value} | HashCode: {cpfnew2.GetHashCode()}");
            Console.WriteLine($"CpfNormal 3: Valor: {cpfnew3.Value} | HashCode: {cpfnew3.GetHashCode()}");
            Console.WriteLine("Cpf1 == Cpf2: " + (cpfnew1 == cpfnew2));
            Console.WriteLine("Cpf1 == Cpf3: " + (cpfnew1 == cpfnew3));

            Console.WriteLine();

            var cpfValueOf1 = CpfComValueOf.From("1828128128");
            var cpfValueOf2 = CpfComValueOf.From("3247342432");
            var cpfValueOf3 = CpfComValueOf.From("1828128128");
            //var cpfInvalido = Cpf.From("");

            Console.WriteLine($"CpfValueOf 1: Valor: {cpfValueOf1.Value} | HashCode: {cpfValueOf1.GetHashCode()}");
            Console.WriteLine($"CpfValueOf 2: Valor: {cpfValueOf2.Value} | HashCode: {cpfValueOf2.GetHashCode()}");
            Console.WriteLine($"CpfValueOf 3: Valor: {cpfValueOf3.Value} | HashCode: {cpfValueOf3.GetHashCode()}");
            //False pois o Value é diferente
            Console.WriteLine("Cpf1 == Cpf2: " + (cpfValueOf1 == cpfValueOf2));
            //True pois mesmo sendo 2 variáveis diferentes, os métodos
            //GetHashCode() e o Equals() foram implementados pela lib e o Value é comparado no lugar
            Console.WriteLine("Cpf1 == Cpf3: " + (cpfValueOf1 == cpfValueOf3)); 
        }
    }


    //No lugar de string, seu Value pode ser uma tupla, por exemplo.
    public class CpfComValueOf : ValueOf<string, CpfComValueOf>
    {
        protected override void Validate()
        {
            //Pode ser usado aqui para validação com DomainEvents/CustomExceptions/FluentValidation
            if (string.IsNullOrEmpty(Value))
                throw new ArgumentException("Cpf inválido.");
        }
    }

    public class CpfNormal
    {
        public string Value { get; set; }
    }

}
