using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Text;

namespace ConsoleApp5
{
    class Program
    {

        class IpException : Exception
        {
            public IpException()
            {
                Console.WriteLine("Неправильно задан ip адресс");
            }
        }

        class MaskException : Exception
        {
            public MaskException()
            {
                Console.WriteLine("Неправильно задана маска");
            }
        }

        static void Main(string[] args)
        {
            string ipS;
            int[] ip = new int[4];
            string maskS;
            int[] mask = new int[4];
            Console.WriteLine("Введите ip");
            ipS = Console.ReadLine();
            CorrectAddr(ipS);
            Console.WriteLine("Введите маску подсети");
            maskS = Console.ReadLine();
            CorrectAddr(maskS);
            DivisionOnOctets(ref ip, ipS);
            DivisionOnOctets(ref mask, maskS);
            TestIp(ip);
            TestMask(mask);
            Calculate(ip, mask);


        }

        public static void DivisionOnOctets(ref int[] ip, string ipS)
        {
            for (int i = 0; i < 3; i++)
            {
                int x = ipS.IndexOf('.');
                string octet = ipS.Substring(0, x);

                ip[i] = Convert.ToInt32(octet);

                ipS = ipS.Remove(0, x + 1);

            }

            ip[3] = Convert.ToInt32(ipS);
        }

        public static void TestMask(int[] mask)
        {

            int x = 1;

            for (int i = 0; i < 4; i++)
            {
                if ((mask[i] != 0 && x == 0) || (mask[i] == 0 && x == -1))
                {
                    throw new MaskException();
                }

                if (mask[i] > 255 && mask[i] < 0)
                {
                    throw new MaskException();
                }

                x = mask[i];
            }

        }

        public static void TestIp(int[] ip)
        {



            for (int i = 0; i < 4; i++)
            {


                if (ip[i] > 255 || ip[i] < 0)
                {
                    throw new IpException();
                }


            }

        }

        public static void CorrectAddr(string str)
        {
            for (int i = 0; i < 3; i++)
            {
                int y = str.IndexOf('.');
                str = str.Remove(0, y + 1);

                if (y < 0)
                {
                    throw new IpException();
                }
            }

        }

        public static void Calculate(int[] ip, int[] mask)
        {
            int[] IdHost = new int[4] { 0, 0, 0, 0 };
            int[] IdNetwork = new int[4] { 0, 0, 0, 0 };

            for (int i = 0; i < 4; i++)
            {
                if (mask[i] == 255)
                {
                    IdNetwork[i] = ip[i];
                }
                else if (mask[i] == 0)
                {
                    IdHost[i] = ip[i];
                }
                else
                {
                    string s1 = Convert.ToString(ip[i], 2);
                    string s2 = Convert.ToString(mask[i], 2);
                    Console.WriteLine(s1);
                    Console.WriteLine(s2);
                    IdNetwork[i] = Conunction(s1, s2);
                    IdHost[i] = ip[i] - IdNetwork[i];

                }
            }

            Console.Write("\nID хоста: ");

            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    Console.Write(IdHost[i]);
                }
                else
                {
                    Console.Write(IdHost[i] + ".");
                }

            }
            Console.Write("\nID подсети: ");

            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    Console.Write(IdNetwork[i]);
                }
                else
                {
                    Console.Write(IdNetwork[i] + ".");
                }

            }
            Console.WriteLine();
        }

        public static int Conunction(string s1, string s2)
        {

            if (s1.Length != 8)
            {
                for (int i = 0; i < 8 - s1.Length; i++)
                {
                    s1 = s1.Insert(0, "0");
                }
            }
            if (s2.Length != 8)
            {
                for (int i = 0; i < 8 - s2.Length; i++)
                {
                    s2 = s2.Insert(0, "0");
                }
            }

            string v1;
            string v2;
            string result = "";
            for (int i = 0; i < 8; i++)
            {
                v1 = s1.Substring(i, 1);
                v2 = s2.Substring(i, 1);

                //Console.WriteLine(i + ") " +v1 + ' ' + v2);
                if (v1 == "1" && v2 == "1")
                {
                    result += "1";
                }
                else
                {
                    result += "0";
                }
            }
            return ConvertToD(result);
        }

        static public int ConvertToD(string str)
        {
            string s;
            int result = 0;
            for (int i = 1; i <= str.Length; i++)
            {
                s = str.Substring(i - 1, 1);
                if (s == "1")
                {
                    switch (i)
                    {
                        case 1:
                            result += 128;
                            break;
                        case 2:
                            result += 64;
                            break;
                        case 3:
                            result += 32;
                            break;
                        case 4:
                            result += 16;
                            break;
                        case 5:
                            result += 8;
                            break;
                        case 6:
                            result += 4;
                            break;
                        case 7:
                            result += 2;
                            break;
                        case 8:
                            result += 1;
                            break;
                    }
                }
            }
            return result;
        }
    }

}
