using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE_geniert
{
    public class MatriceLayers
    {
        static void Main(string[] args)
        {
            int[,] matrice = new int[,] { { 0, 2254, 347 }, { 4425, 5875, 58876 } };
            //MatriceLayers.Affiche(matrice);
        }

        public static void MatriceInitialize(ushort val1, ushort val2, ushort val3, ushort val4, ushort val5, ushort val6/*, ushort val7, ushort val8, ushort val9, ushort val10, ushort val11, ushort val12*/)
        {
            int[,] matrice = new int[,] { { val1, val2, val3 }, { val4, val5, val6 } };
            MatriceLayers.Affiche(matrice);
        }
        public static void Affiche(int[,] matrice)
        {
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write($"{matrice[i, j]}" + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
