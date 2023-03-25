using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Historial
    {
        public static ML.Result Add(ML.Historial historial)
        {
            ML.Result resultAdd = new ML.Result();

            try
            {
                using (DL.JrodriguezSuperDigitoContext context = new DL.JrodriguezSuperDigitoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"HistorialAdd {historial.Numero},{historial.Resultado},{historial.Usuario.IdUsuario}");
                    if (query > 0)
                    {
                        resultAdd.Correct = true;
                    }
                    else
                    {
                        resultAdd.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                resultAdd.Ex = ex;
                resultAdd.ErrorMessage = ex.Message;
                resultAdd.Correct = false;
            }
            return resultAdd;
        }

        public static ML.Result Delete(int IdHistorial)
        {
            ML.Result resultDelete= new ML.Result();

            try
            {
                using (DL.JrodriguezSuperDigitoContext context = new DL.JrodriguezSuperDigitoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"[DeleteHistorial] {IdHistorial}");

                    if (query > 0)
                    {
                        resultDelete.Correct= true;
                    }
                    else
                    {
                        resultDelete.Correct = false;
                    }

                }

            }
            catch(Exception ex)
            {
                resultDelete.Ex = ex;
                resultDelete.ErrorMessage = ex.Message;
                resultDelete.Correct = false;
            }
            return resultDelete;

        }
        public static ML.Result HistorialGetByIdUsuario(int? idUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.JrodriguezSuperDigitoContext contex = new DL.JrodriguezSuperDigitoContext())
                {
                    var query = contex.Historials.FromSqlRaw($"[HistorialGetByIdUsuario] {idUsuario}").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var objeto in query)
                        {
                            ML.Historial historial = new ML.Historial();

                            historial.IdHistorial = objeto.IdHistorial;
                            historial.Numero = objeto.Numero.Value;
                            historial.Resultado = objeto.Resultado.Value;
                            historial.FechaIngreso = objeto.FechaHora.ToString();

                            result.Objects.Add(historial);

                        }
                    }
                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.ErrorMessage = ex.Message;
                result.Correct = false;

            }
            return result;

        }

        public static int CalcularSuperDigito(string Numero)
        {
            int Resultado = 0;
            try
            {
                //AGREGAR LOS DIGITOS DEL NUMERO A UN ARREGLO
                String[] ArregloChar = NumeroToArrayString(Numero);

                //CASTEAR DE STRING A INT ARRAY
                int[] NumerosConvertInt = ArregloStringToInt(ArregloChar);
                int Suma = NumerosConvertInt.Sum();

                while (Suma > 9)
                {
                    String ConvertirSuma = Suma.ToString();
                    String[] NewArregloChar = NumeroToArrayString(ConvertirSuma);
                    int[] NewNumerosConvertInt = ArregloStringToInt(NewArregloChar);
                    Suma = NewNumerosConvertInt.Sum();

                }
                Resultado = Suma;
                Console.WriteLine("El super digito del numero " + Numero + " Es: " + Resultado);

            }
            catch (Exception)
            {

            }
            return Resultado;

        }

        public static string[] NumeroToArrayString(string Numero)
        {
            int TamCadena = Numero.Length;
            string[] ArregloChar = new string[TamCadena];

            for (int i = 0; i <= TamCadena - 1; i++)
            {
                string Num = Numero.Substring(i, 1);
                ArregloChar[i] = Num;

            }
            return ArregloChar;
        }

        public static int[] ArregloStringToInt(string[] NumerosString)
        {
            int[] numeros = new int[NumerosString.Length];
            numeros = NumerosString.Select(x => Convert.ToInt32(x)).ToArray();
            return numeros;
        }
    }
}
