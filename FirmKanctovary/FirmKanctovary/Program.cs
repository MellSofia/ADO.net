using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;








/*

CREATE TABLE TypeKanctovara (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE 
);

CREATE TABLE Kanctovary (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL, 
    TypeId INT NOT NULL, 
    Quantity INT NOT NULL, 
    CostPrice DECIMAL(10, 2) NOT NULL, 
    FOREIGN KEY (TypeId) REFERENCES TypeKanctovara(Id) 
);

CREATE TABLE Managers (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL 
);

CREATE TABLE Sales (
    Id INT PRIMARY KEY IDENTITY(1,1),
    KanctovarId INT NOT NULL, 
    ManagerId INT NOT NULL, 
    BuyerFirm NVARCHAR(100) NOT NULL, 
    QuantitySold INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    SaleDate DATE NOT NULL, 
    FOREIGN KEY (KanctovarId) REFERENCES Kanctovary(Id), 
    FOREIGN KEY (ManagerId) REFERENCES Managers(Id) 
);

CREATE TABLE Archive_Kanctovary (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100),
    Type NVARCHAR(50),
    Quantity INT,
    CostPrice DECIMAL(10, 2),
    DeletedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Archive_Managers (
    Id INT PRIMARY KEY,
    Name NVARCHAR(100),
    DeletedDate DATETIME DEFAULT GETDATE()
);
 */





namespace FirmKanctovary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=.\SQLEXPRESS;database=FirmKanstovary1;Integrated Security=true;TrustServerCertificate=true";
            SqlConnection conn = null;
            conn = new SqlConnection(connectionString);
            SqlDataReader reader = null;
            SqlDataReader reader2 = null;

            int num = -1;

            String[] str = { "Вставка новых канцтоваров", "Вставка новых типов канцтоваров", "Вставка новых менеджеров по продажам", "Вставка новых фирм покупателей",
                "Обновление информации о существующих канцтоварах", "Обновление информации о существующих фирмах покупателях", "Обновление информации о существующих менеджерах по продажам", 
                "Обновление информации о существующих типах канцтоваров", "Удаление канцтоваров", "Удаление менеджеров по продажам", "Удаление типов канцтоваров","Удаление фирм покупателей",
                "Показать информацию о менеджере с наибольшим количеством продаж по количеству единиц", "Показать информацию о менеджере по продажам с наибольшей общей суммой прибыли", 
                "Показать информацию о менеджере по продажам с наибольшей общей суммой прибыли за указанный промежуток времени", "Показать информацию о фирме покупателе, которая купила на самую большую сумму", 
                "Показать информацию о типе канцтоваров с наибольшим количеством продаж по единицам", "Показать информацию о типе самых прибыльных канцтоваров", 
                "Показать название самых популярных канцтоваров", "Показать название канцтоваров, которые не продавались заданное количество дней"
            };

            //Удаление конфликт
            try
            {
                conn.Open();

                SqlCommand sqlCommand = new SqlCommand("INSERT INTO TypeKanctovara (Name) VALUES ('Художественные принадлежности');  Select * From TypeKanctovara;" +
                "INSERT INTO Kanctovary (Name, TypeId, Quantity, CostPrice) VALUES ('Макркер',4, 100, 15.50); Select * From Kanctovary;" +
                "INSERT INTO Managers (Name) VALUES ('Васильев Василий'); Select * From Managers;"+
                "INSERT INTO Sales  (KanctovarId, ManagerId, BuyerFirm, QuantitySold, UnitPrice, SaleDate) VALUES (4, 4, 'Фирма Г', 6,250.00, '2024-12-23'); Select * From Sales;" + 
                "UPDATE Sales SET BuyerFirm = 'Фирма Г2' WHERE BuyerFirm = 'Фирма Г'; Select* From Sales;"+
                "UPDATE Kanctovary SET Quantity = 200, CostPrice = 2.00 WHERE Id = 4; Select* From Kanctovary;"+
                "UPDATE Managers SET Name = 'Васильев Виталий' WHERE Id = 4; Select* From Managers;"+
                "UPDATE TypeKanctovara SET Name = 'Худ. пренад' WHERE Id = 4; Select* From TypeKanctovara;"+
                "DELETE FROM Kanctovary WHERE TypeId = 4; Select* From Kanctovary;" +
                "DELETE FROM Managers WHERE Id = 4; Select* From Managers;"+
                "DELETE FROM TypeKanctovara WHERE Id = 4; Select* From TypeKanctovara;"+
                "SELECT Top 1 Managers.Name, SUM(Sales.QuantitySold) AS TotalUnitsSold FROM Sales JOIN Managers ON Sales.ManagerId = Managers.Id GROUP BY Managers.Name ORDER BY TotalUnitsSold DESC;"+
                "SELECT Top 1 Managers.Name AS ManagerName, SUM(Sales.QuantitySold * Sales.UnitPrice) AS TotalProfit FROM Sales JOIN Managers ON Sales.ManagerId = Managers.Id GROUP BY Managers.Name ORDER BY TotalProfit DESC;"+
                "SELECT Top 1 Managers.Name AS ManagerName, SUM(Sales.QuantitySold * Sales.UnitPrice) AS TotalProfit FROM Sales JOIN Managers ON Sales.ManagerId = Managers.Id WHERE Sales.SaleDate BETWEEN '2024-12-01' AND '2024-12-03' GROUP BY Managers.Name ORDER BY TotalProfit DESC;"+
                "SELECT Top 1 Sales.BuyerFirm, SUM(Sales.QuantitySold * Sales.UnitPrice) AS TotalPurchaseAmount FROM Sales GROUP BY Sales.BuyerFirm ORDER BY TotalPurchaseAmount DESC;"+
                "SELECT Top 1 TypeKanctovara.Name AS TypeName, SUM(Sales.QuantitySold) AS TotalQuantitySold FROM Sales JOIN Kanctovary ON Sales.KanctovarId = Kanctovary.Id JOIN TypeKanctovara  ON Kanctovary.TypeId = TypeKanctovara.Id GROUP BY TypeKanctovara.Name ORDER BY TotalQuantitySold DESC;"+
                "SELECT Top 1 TypeKanctovara.Name AS TypeName, SUM(Sales.QuantitySold * Sales.UnitPrice) AS TotalProfit FROM Sales JOIN Kanctovary ON Sales.KanctovarId = Kanctovary.Id JOIN TypeKanctovara ON Kanctovary.TypeId = TypeKanctovara.Id GROUP BY TypeKanctovara.Name ORDER BY TotalProfit DESC;"+
                "SELECT TOP 1 Kanctovary.Name AS KanctovarName, SUM(Sales.QuantitySold) AS TotalQuantitySold FROM Sales JOIN Kanctovary  ON Sales.KanctovarId = Kanctovary.Id GROUP BY Kanctovary.Name ORDER BY TotalQuantitySold DESC;"+
                "SELECT Kanctovary.Name AS KanctovarName FROM Kanctovary LEFT JOIN Sales ON Kanctovary.Id = Sales.KanctovarId AND Sales.SaleDate >= DATEADD(DAY, -23, GETDATE()) WHERE Sales.KanctovarId IS NULL; ", conn);
                //В последнем запросе для того, чтобы вывелось 1 уменьшать -23 до нынешняя дата-1(или 2 если надо 2 и тд) 
                reader = sqlCommand.ExecuteReader();
                Console.WriteLine(reader.GetName(0) + " " + reader.GetName(1));
                #region запросы для sqlCommand


                //*1 
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

                //*2 
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }
                reader.NextResult();

                //*3
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
                    Console.WriteLine(reader[0] + "\t" + reader[1]);
                }
                reader.NextResult();

                //4
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5]);
                }
                reader.NextResult();

                //5
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4] + "\t" + reader[5]);
                }
                reader.NextResult();

                //*6 
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }
                reader.NextResult();

                //*7
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


                //*8
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

                //9 
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

                //*10
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
                    Console.WriteLine(reader[0] + "\t" + reader[1] + "\t" + reader[2] + "\t" + reader[3] + "\t" + reader[4]);
                }
                reader.NextResult();

                //*11
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

                //*12
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

                //*13
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

                //*14
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

                //*15
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

                //*16
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

                //*17
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

                //*18
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

                //*19
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

                //*20
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
                    Console.WriteLine(reader[0]);
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
