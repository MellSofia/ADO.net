using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.PortableExecutable;




/*CREATE TABLE Type (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TypName NVARCHAR (50) NOT NULL
);

CREATE TABLE Supplier (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SupName NVARCHAR (50) NOT NULL
);

CREATE TABLE Warehouse (
    Id INT IDENTITY(1,1) PRIMARY KEY, Name NVARCHAR (50) NOT NULL,
    TypeId INT   NOT NULL, SupId INT NOT NULL,
    Quant INT DEFAULT ((0)) NOT NULL, Price INT DEFAULT ((100)) NOT NULL,
    Date DATE  DEFAULT (getdate()) NOT NULL, FOREIGN KEY (TypeId) REFERENCES Type (Id),
    FOREIGN KEY (SupId) REFERENCES Supplier (Id)
);


INSERT INTO Type (TypName) VALUES ('Electronics'),('Furniture'), ('Clothing'), ('Clothing2');
INSERT INTO Supplier (SupName) VALUES ('SUP_1'),('SUP_2'),('SUP_3'),('SUP_4');
INSERT INTO Warehouse (Name, TypeId, SupId, Quant, Price) VALUES ('Warehouse_1', 1, 1, 50, 200), ('Warehouse_2', 2, 2, 30, 150), ('Warehouse_3', 3, 3, 60, 250), ('Warehouse_4', 4, 4, 40, 300);
*/






namespace Practic_warehouse_26._12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS;database=Warehouse;Integrated Security=true;TrustServerCertificate=true";
            SqlConnection conn = null;
            conn = new SqlConnection(connectionString);
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;

            int num = -1;

            String[] str = { " Отображение всей информации о товаре", "Отображение всех типов товаров", 
                "Отображение всех поставщиков", "Показать товар с максимальным количеством",
                "Показать товар с минимальным количеством", "Показать товар с минимальной себестоимостью",
                "Показать товар с максимальной себестоимостью", "Показать товары, заданной категории", 
                "Показать товары, заданного поставщика", "Показать самый старый товар на складе", "Показать среднее количество товаров по каждому типу товара"};
            

            try
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Warehouse;" + "SELECT * FROM Type;" +
                    "SELECT * FROM Supplier;" + "SELECT * FROM Warehouse WHERE Quant = (SELECT MAX(Quant) FROM Warehouse);" +
                    "SELECT * FROM Warehouse WHERE Quant = (SELECT MIN(Quant) FROM Warehouse);" + "SELECT * FROM Warehouse WHERE Price = (SELECT MIN(Price) FROM Warehouse);" +
                    "SELECT * FROM Warehouse WHERE Price = (SELECT MAX(Price) FROM Warehouse);" + "SELECT * FROM Warehouse WHERE TypeId = (SELECT Id FROM Type WHERE TypName = 'Electronics');" +
                    "SELECT * FROM Warehouse WHERE SupId = (SELECT Id FROM Supplier WHERE SupName = 'SUP_1');" + "SELECT * FROM Warehouse WHERE Date = (SELECT MIN(Date) FROM Warehouse);" +
                    "SELECT TypeId, AVG(Quant) AS AverageQuantity FROM Warehouse GROUP BY TypeId;", conn);
                reader = sqlCommand.ExecuteReader();
                Console.WriteLine(reader.GetName(0) + " " + reader.GetName(1) + " " + reader.GetName(2) + " " + reader.GetName(3) + " " + reader.GetName(4) + " " + reader.GetName(5) + " " + reader.GetName(6));
                #region запросы для sqlCommand задание 2

                //*Warehouse
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);
                }
                reader.NextResult();

                //*Type 
                Console.Write("\n" + str[1] + "\n");
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
                    Console.WriteLine(" ");
                    Console.WriteLine(reader[0] + "\t" + reader[1]);
                }

                //*Supplier 
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
                    Console.WriteLine(" ");
                    Console.WriteLine(reader[0] + "\t" + reader[1]);
                }

                //MAX(Quant)
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);
                }

                //MIN(Quant)
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);

                }

                //MIN(Price)
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);

                }

                //MAX(Price)
                Console.Write("\n" + str[6] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);

                }

                //Electronics SUP_1
                Console.Write("\n" + str[7] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);

                }

                // SUP_1 MIN(Date)
                Console.Write("\n" + str[8] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);

                }

                // MIN(Date)
                Console.Write("\n" + str[8] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);

                }

                // TypeId, AVG(Quant) AS AverageQuantity FROM Warehouse
                Console.Write("\n" + str[8] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1]);
                }
                #endregion
            }


            catch (Exception ex)
            {
                Console.WriteLine("  Ошибка: " + ex.Message);
            }
            finally
            {
            }

        }
    }
}
