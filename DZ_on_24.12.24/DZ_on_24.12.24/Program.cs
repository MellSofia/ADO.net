using Microsoft.Data.SqlClient;
using System.Drawing;

using System.Reflection.PortableExecutable;

/*
    CREATE TABLE VegetablesANDFruits (
    Id int IDENTITY(1, 1) NOT NULL,

    Name nvarchar (50) NOT NULL,

    Type nvarchar (50) NOT NULL,

    Color nvarchar (50) NOT NULL,

    Calories int NOT NULL
)
INSERT INTO [dbo].[VegetablesANDFruits] (Name, Type, Color, Calories) VALUES
('Томат', 'Овощ', 'Красный', 25),
('Банан', 'Фрукт', 'Желтый', 89),
('Лимон', 'Фрукт', 'Желтый', 29),
('Огурец', 'Овощ', 'Зеленый', 14),
('Слива', 'Фрукт', 'Синий', 46),
('Яблоко', 'Фрукт', 'Красный', 52);
 
 */




namespace DZ_on_24._12._24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS;database=VegetablesANDFruits;Integrated Security=true;TrustServerCertificate=true";
            SqlConnection conn = null;
            conn = new SqlConnection(connectionString);
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;

            int num = -1;

            String[] str = { " Отображение всей информации из таблицы с овощами и фруктами", 
                "Отображение всех названий овощей и фруктов",
                "Отображение всех цветов", "Показать максимальную калорийность",
                "Показать минимальную калорийность", "Показать среднюю калорийность"};
            String[] str2 = { "Показать количество овощей", "Показать количество фруктов","Показать количество овощей и фруктов заданного(красного) цвета",
                "Показать количество овощей фруктов каждого цвета", "Показать овощи и фрукты с калорийностью ниже указанной(40)",
                "Показать овощи и фрукты с калорийностью выше указанной(40)", "Показать овощи и фрукты с калорийностью в указанном диапазоне(20-60)",
                "Показать все овощи и фрукты, у которых цвет желтый или красный" };

            try {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM VegetablesANDFruits;" + "SELECT Name FROM VegetablesANDFruits;" + 
                    "SELECT Color FROM VegetablesANDFruits;" +"SELECT MAX(Calories) FROM VegetablesANDFruits;" + 
                    "SELECT MIN(Calories) FROM VegetablesANDFruits;" + "SELECT AVG(Calories) FROM VegetablesANDFruits;"+ 
                    "SELECT COUNT(*) FROM VegetablesANDFruits WHERE Type = N'Овощ';"+ "SELECT COUNT(*) FROM VegetablesANDFruits WHERE Type = N'Фрукт';"+
                    "SELECT COUNT(*) FROM VegetablesANDFruits WHERE Color = N'Красный'" + "SELECT Color, COUNT(*) FROM VegetablesANDFruits GROUP BY Color;" +
                    "SELECT * FROM VegetablesANDFruits WHERE Calories < 40; " + "SELECT * FROM VegetablesANDFruits WHERE Calories > 40; " +
                    "SELECT * FROM VegetablesANDFruits WHERE Calories < 60 AND Calories > 20;" +
                    "SELECT * FROM VegetablesANDFruits WHERE Color  = N'Красный' OR COLOR = N'Желтый'; ", conn);
                reader = sqlCommand.ExecuteReader();
                Console.WriteLine(reader.GetName(0) + " " + reader.GetName(1) + " " + reader.GetName(2) + " " + reader.GetName(3) + " " + reader.GetName(4));
                #region запросы для sqlCommand задание 2
                //*FROM
                Console.Write("\n" + str[0] + "\n");
                int line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.WriteLine(" ");
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }

                //Name
                Console.Write("\n" + str[1] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0]);
                }

                //Color
                Console.Write("\n" + str[2] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0]);
                }

                //MAX(Calories)
                Console.Write("\n" + str[3] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0]);
                }

                //MIN(Calories)
                Console.Write("\n" + str[4] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0]);
                }

                //AVG(Calories)
                Console.Write("\n" + str[5] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0]);
                }
                #endregion

                #region запросы для sqlCommand задание 3
                

                reader.NextResult();
                line = 0;
                do
                {
                    while (reader.Read())
                    {
                        if (line == 0)
                        {
                            Console.Write("  ");
                            for (int i = 0; i < reader.FieldCount; i++)
                                Console.Write(reader.GetName(i).ToString() + "\t");
                            Console.WriteLine();
                        }
                        Console.Write("\n" + str2[line] + "\n");
                        line++;
                        Console.Write(" ");
                        Console.WriteLine(reader[0]);
                        Console.Write("\n");
                    }
                } while (reader.NextResult() && line<3);

                //количество овощей фруктов каждого цвета
                Console.Write("\n" + str2[3] + "\n");
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0] + " " + reader[1]);
                }

                //Показать овощи и фрукты с калорийностью ниже указанной(40)
                Console.Write("\n" + str2[4] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }

                //Показать овощи и фрукты с калорийностью выше указанной(40)
                Console.Write("\n" + str2[5] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }
                //Показать овощи и фрукты с калорийностью в указанном диапазоне(20-60)
                Console.Write("\n" + str2[6] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }
                //Показать все овощи и фрукты, у которых цвет желтый или красный
                Console.Write("\n" + str2[7] + "\n");
                reader.NextResult();
                line = 0;
                while (reader.Read())
                {
                    if (line == 0)
                    {
                        Console.Write("  ");
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i).ToString() + "\t");
                        Console.WriteLine();
                    }
                    line++;
                    Console.Write(" ");
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }

                #endregion

            }


            catch (Exception ex) {
                Console.WriteLine("  Ошибка: " + ex.Message);
            }
            finally {
            }

        }
    }
}
