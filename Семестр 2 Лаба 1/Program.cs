using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Семестр_2_Лаба_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("ЗАВДАННЯ №1\nКiлькiсть людей:\t");
            {
                int N = int.Parse(Console.ReadLine()); //кількість людей
                Console.WriteLine();
                List<int> People = new List<int>(N);
                for (int i = 0; i < N; i++)
                {
                    People.Add(i + 1);
                }
                foreach (int item in People)
                {
                    Console.Write($"{item}\t");
                }
                Console.WriteLine();
                DeletingEverySecondPerson(People);
                Console.WriteLine();
            }
            Console.WriteLine("\nЗАВДАННЯ №2\n");
            {
                Dictionary<string, int> inputDictionary1 = new Dictionary<string, int>(5);
                InitializationDictionariesWithSameKeys(inputDictionary1,5);
                Console.WriteLine("\nСловник №1\n");
                GetDictionary(inputDictionary1);
                Dictionary<string, int> inputDictionary2 = new Dictionary<string, int>(10);
                InitializationDictionariesWithSameKeys(inputDictionary2,10);
                Console.WriteLine("\nСловник №2\n");
                GetDictionary(inputDictionary2);
                Dictionary<string, int> inputDictionary3 = new Dictionary<string, int>(3);
                InitializationDictionariesWithSameKeys(inputDictionary3,3);
                Console.WriteLine("\nСловник №3\n");
                GetDictionary(inputDictionary3);
                Dictionary<string, int> inputDictionary4 = new Dictionary<string, int>(7);
                InitializationDictionariesWithSameKeys(inputDictionary4,7);
                Console.WriteLine("\nСловник №4\n");
                GetDictionary(inputDictionary4);
                Dictionary<string, int> inputDictionary5 = new Dictionary<string, int>(4);
                InitializationDictionariesWithSameKeys(inputDictionary5,4);
                Console.WriteLine("\nСловник №5\n");
                GetDictionary(inputDictionary5);
                Dictionary<string, int> outputDictionary = new Dictionary<string, int>(10);
                for (int i = 0; i < 10; i++)
                {
                    int value1=0;
                    int value2=0;
                    int value3=0;
                    int value4=0;
                    int value5=0;
                    // якщо є ключ , бере значення ,якщо ні то бере 0
                    if (inputDictionary1.ContainsKey($"item{i}"))
                    {
                        value1 = inputDictionary1[$"item{i}"];
                    }
                    if (inputDictionary2.ContainsKey($"item{i}"))
                    {
                        value2 = inputDictionary2[$"item{i}"];
                    }
                    if (inputDictionary3.ContainsKey($"item{i}"))
                    {
                        value3 = inputDictionary3[$"item{i}"];
                    }
                    if (inputDictionary4.ContainsKey($"item{i}"))
                    {
                        value4 = inputDictionary4[$"item{i}"];
                    }
                    if (inputDictionary5.ContainsKey($"item{i}"))
                    {
                        value5 = inputDictionary5[$"item{i}"];
                    }
                    outputDictionary.Add($"item{i}",value1+value2+value3+value4+value5);//в словник добавляє ключ і суму значень;
                }
                Console.WriteLine("\nРезультат\n");
                //GetDictionary(outputDictionary);
                string json1 = JsonConvert.SerializeObject(outputDictionary);
                File.WriteAllText(@"C:\Users\user\OneDrive\Lab1Task2.txt",json1);
                string json2 = File.ReadAllText(@"C:\Users\user\OneDrive\Lab1Task2.txt");
                Dictionary<string, int> outputDictionary2 = JsonConvert.DeserializeObject<Dictionary<string, int>>(json2);
                GetDictionary(outputDictionary2);
                
            }
            Console.WriteLine("\nЗАВДАННЯ №3\n");
            {
                List<int> list = new List<int> { 1, 2, 3, 1, 4, 5, 2, 2, 1 };
                ReplacingRepeatingElementsOfListUsingLinq(list);//заміна повторюваних значень на нулі
            }
            Console.ReadKey();
        }
        static void DeletingEverySecondPerson(List<int> list)
        {
            //для парної і не парної кількості людей будуть різні варіанти розв'язку
            int count = 1;
            byte count2 = 0;
            bool pairing;
            if (list.Count % 2 == 0)
            {
                pairing = true;
            }
            else
            {
                pairing = false;
            }
            while (list.Count > 1) //поки в колі не залишиться одна людина 
            {
                for (int i = count; i < list.Count; i++)       //пропускаючи нульовий елемент видаляє кожен наступний,
                {                                              //при цьому елементи після кожного видалення зсуваються
                    Console.WriteLine($"{list[i]}-вибув");     //індексами вліво і виходить якраз кожен другий
                    list.RemoveAt(i);                           
                }                                              //від того чи парна чи не парна кількість людей залежить
                                                               //з якого елемента почнеться видалення людей в наступному колі

                if (pairing==true)                             
                {
                    if (count2>1) //для парної - з третього кола видалення почнеться з нульвого елемента ,
                                  //і з кожним наступним колом нульовий буде чергуватись з першим
                    {
                        if (count2%2==0)
                        {
                            count=0;    
                        }
                        if (count2!=0)
                        {
                            count = 1;
                        }
                        if (list.Count == 2)
                        {
                            count = 0;
                        }
                    }
                }
                if (pairing==false) //для непарної - все те саме , тільки з другого кола 
                {
                    if (count2 % 2 == 0)
                    {
                        count = 0;
                    }
                    if (count2 != 0)
                    {
                        count = 1;
                    }
                    if (list.Count == 2)
                    {
                        count = 0;
                    }
                }
                count2++;
            }
            Console.WriteLine($"{list[0]}-залишився");
        }
        static void ReplacingRepeatingElementsOfListUsingLinq(List<int> list)
        {
            var dupls = list.GroupBy(i => i).Where(i => i.Count() > 1).Select(i => i.Key); //Дістає з списка елементи що повторюються 
            var result = list.Select(x =>
            {
                if (dupls.Contains(x)) x = 0; //Змінює їх на нулі
                return x;
            }
            );
            foreach (var item in result) //Вивід 
            {
                Console.Write(item+"\t");
            } 
        }
        static Dictionary<string,int> InitializationDictionariesWithSameKeys(Dictionary<string,int> keyValuePairs,int capacityOfDictionary)
        {//Заповнення словника рандомними значеннями 
            Random random = new Random(); 
            for (int i = 0; i < capacityOfDictionary; i++)
            {
                int value = random.Next(10, 100);
                keyValuePairs.Add($"item{i}", value);
            }
            return keyValuePairs;
        }
        static void GetDictionary(Dictionary<string, int> keyValuePairs)
        {//Вивід словника
            foreach (var kv in keyValuePairs)
            {
                Console.WriteLine(kv.Key+"\t"+kv.Value);
            }
        }
    }
}