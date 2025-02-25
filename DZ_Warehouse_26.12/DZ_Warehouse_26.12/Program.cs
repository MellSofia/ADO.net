using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.PortableExecutable;




/*
 CREATE TABLE Type (    Id INT IDENTITY(1,1) PRIMARY KEY,
    TypName NVARCHAR (50) NOT NULL, );

CREATE TABLE Supplier (    Id INT IDENTITY(1,1) PRIMARY KEY,
    SupName NVARCHAR (50) NOT NULL,);
CREATE TABLE Warehouse (
    Id INT IDENTITY(1,1) PRIMARY KEY,    Name NVARCHAR (50) NOT NULL,
    TypeId INT   NOT NULL,    SupId INT NOT NULL,
    Quant INT DEFAULT ((0)) NOT NULL,    Price INT DEFAULT ((100)) NOT NULL,
    Date DATE  DEFAULT (getdate()) NOT NULL,    FOREIGN KEY (TypeId) REFERENCES Type (Id),
    FOREIGN KEY (SupId) REFERENCES Supplier (Id));


INSERT INTO Type (TypName) VALUES ('Electronics');INSERT INTO Type (TypName) VALUES ('Furniture');
INSERT INTO Type (TypName) VALUES ('Clothing'); INSERT INTO Type (TypName) VALUES ('Clothing2');

INSERT INTO Supplier (SupName) VALUES ('SUP_1'),('SUP_2'),('SUP_3'),('SUP_4');

INSERT INTO Warehouse (Name, TypeId, SupId, Quant, Price) VALUES ('Warehouse_1', 1, 1, 50, 200), ('Warehouse_2', 2, 2, 30, 150), ('Warehouse_3', 3, 3, 60, 250);
INSERT INTO Warehouse (Name, TypeId, SupId, Quant, Price, Date) VALUES ('Warehouse_4', 1, 1, 50, 200, DATEADD(day, -2, GETDATE()) );

 
 */









namespace Practic_warehouse_26._12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS;database=Test1;Integrated Security=true;TrustServerCertificate=true";
            SqlConnection conn = null;
            conn = new SqlConnection(connectionString);
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;

            int num = -1;

            String[] str = { "Вставка новых типов товаров", "Вставка новых поставщиков",  "Вставка новых товаров", "Обновление информации о существующих товарах",
                "Обновление информации о существующих поставщиках", "Обновление информации о существующих типах товаров",
                "Удаление товаров", "Удаление поставщиков", "Удаление типов товаров", "Показать информацию о поставщике с наибольшим количеством товаров на складе",
                "Показать информацию о поставщике с наименьшим количеством товаров на складе", "Показать информацию о типе товаров с наибольшим количеством товаров на складе",
                "Показать информацию о типе товаров с наименьшим количеством товаров на складе", "Показать товары с поставки, которых прошло заданное количество дней"
            };


            try
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO Supplier (SupName) VALUES ('SUP_5'); SELECT * FROM Supplier;" + 
                "INSERT INTO Type (TypName) VALUES ('Electronics_5'); SELECT * FROM Type;" +
                "INSERT INTO Warehouse (Name, TypeId, SupId, Quant, Price) VALUES ('Warehouse_test', 5, 5, 55, 255);SELECT * FROM Warehouse;" +
                "Update Warehouse SET Price = 245 WHERE Name = 'Warehouse_test'; Select * FROM Warehouse;" + 
                "Update Supplier SET SupName ='SUP_5.1' WHERE SupName = 'SUP_5'; Select * FROM Supplier;"+
                "Update Type SET TypName ='Electronics_5.1' WHERE TypName = 'Electronics_5'; Select * FROM Type;"+
                "DELETE FROM Warehouse WHERE Name = 'Warehouse_test'; Select * FROM Warehouse;" + 
                "DELETE FROM Supplier WHERE Id = 5; Select * FROM Supplier;" + 
                "DELETE FROM Type WHERE Id = 5;  Select * FROM Type;" +
                "SELECT TOP 1 SupName FROM Supplier ORDER BY (SELECT SUM(Quant) FROM Warehouse WHERE Id = Supplier.Id)DESC;" +
                "SELECT TOP 1 SupName FROM Supplier ORDER BY (SELECT SUM(Quant) FROM Warehouse WHERE Id = Supplier.Id)ASC;" +
                "SELECT TOP 1 TypName FROM Type ORDER BY (SELECT SUM(Quant) FROM Warehouse WHERE Id = Type.Id)DESC;" +
                "SELECT TOP 1 TypName FROM Type ORDER BY (SELECT SUM(Quant) FROM Warehouse WHERE Id = Type.Id)ASC;" + 
                "SELECT * FROM Warehouse WHERE DATEDIFF(day, Date, GETDATE()) >= 2;", conn);
                reader = sqlCommand.ExecuteReader();
                Console.WriteLine(reader.GetName(0) + " " + reader.GetName(1));
                #region запросы для sqlCommand


                //*Supplier 
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
                    Console.WriteLine(reader[0] + "\t" + reader[1]);
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
                reader.NextResult();

                //*Warehouse
                Console.Write("\n" + str[2] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);
                }
                reader.NextResult();

                //Warehouse
                Console.Write("\n" + str[3] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);
                }
                reader.NextResult();

                //*Supplier 
                Console.Write("\n" + str[4] + "\n");
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
                reader.NextResult();
                
                //*Type 
                Console.Write("\n" + str[5] + "\n");
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
                reader.NextResult();

                //*Warehouse
                Console.Write("\n" + str[6] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);
                }
                reader.NextResult();

                //*Supplier 
                Console.Write("\n" + str[7] + "\n");
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
                reader.NextResult();
                
                //*Type 
                Console.Write("\n" + str[8] + "\n");
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
                reader.NextResult();

                //*Supplier 
                Console.Write("\n" + str[9] + "\n");
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
                    Console.WriteLine(reader[0]);
                }
                reader.NextResult();

                //*Supplier 
                Console.Write("\n" + str[10] + "\n");
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
                    Console.WriteLine(reader[0]);
                }
                reader.NextResult();

                //*Type 
                Console.Write("\n" + str[11] + "\n");
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
                    Console.WriteLine(reader[0] );
                }
                reader.NextResult();

                //*Type 
                Console.Write("\n" + str[12] + "\n");
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
                    Console.WriteLine(reader[0]);
                }
                reader.NextResult();

                //*Warehouse
                Console.Write("\n" + str[13] + "\n");
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5] + "\t" + reader[6]);
                }
                reader.NextResult();
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
