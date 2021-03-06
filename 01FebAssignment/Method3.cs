﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace _01FebAssignment
{
    class WithSqlParameters
    {
        SqlConnection cn = null;
        SqlCommand cmd = null;
        SqlDataReader dr = null;
        public int ShowData()
        {
            try
            {
                Console.WriteLine("Data from the Table after the DML command");
                Console.WriteLine("------------\n");
                cn = new SqlConnection("Data Source=LAPTOP-TFADOSTK;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from EmployeeTab", cn);
                cn.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["empid"]}\t{dr["empname"]}\t\t {dr["salary"]}\t\t{dr["deptno"]}");
                }
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 0;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
        }
        public int InsertRow()
        {
            try
            {
                Console.WriteLine("Enter Employee Name");
                var ename = Console.ReadLine();
                Console.WriteLine("Enter Employee Salary");
                var esal = Convert.ToSingle(Console.ReadLine());
                Console.WriteLine("Enter Employee dept id");
                var did = Convert.ToInt32(Console.ReadLine());
                cn = new SqlConnection("Data Source=LAPTOP-TFADOSTK;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("insert into employeeTab values(@ename, @esal,@deptid)", cn);
                cmd.Parameters.Add("@ename", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptid", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }

       
        public int Update()
        {
            try
            {

                Console.WriteLine("Enter an existing Employee id to update the record....");
                var eid = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Enter Employee Name");
                var ename = Console.ReadLine();

                Console.WriteLine("Enter Employee Salary");
                var esal = Convert.ToSingle(Console.ReadLine());

                Console.WriteLine("Enter Employee dept id");
                var did = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection("Data Source=LAPTOP-TFADOSTK;Initial Catalog=WFA3DotNet;Integrated Security=True");

                cmd = new SqlCommand("update employeetab set @ename,@esal,@deptid", cn);
                

                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                cmd.Parameters.Add("@empname", SqlDbType.VarChar, 20).Value = ename;
                cmd.Parameters.Add("@esal", SqlDbType.Float).Value = esal;
                cmd.Parameters.Add("@deptId", SqlDbType.Int).Value = did;
                cn.Open();
                int i = cmd.ExecuteNonQuery();

                ShowData();
                return i;


            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 1;
            }
            finally
            {
                cn.Close();
            }
        }
        public int DeleteRow()
        {
            try
            {
                Console.WriteLine("Enter the existing Employee ID to be deleted");

                var eid = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection("Data Source=LAPTOP-TFADOSTK;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("delete from employeetab where Empid=@eid", cn);
                
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;

                cn.Open();
                int i = cmd.ExecuteNonQuery();
                ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 0;
            }
            finally
            {
                cn.Close();
            }



        }
        public int SearchRow()
        {
            try
            {
                Console.WriteLine("Enter the existing Employee ID to be searched");

                var eid = Convert.ToInt32(Console.ReadLine());

                cn = new SqlConnection("Data Source=LAPTOP-TFADOSTK;Initial Catalog=WFA3DotNet;Integrated Security=True");
                cmd = new SqlCommand("select * from Employeetab where Empid=@eid", cn);
                
                cmd.Parameters.Add("@empid", SqlDbType.Int).Value = eid;
                cn.Open();
                int i = cmd.ExecuteNonQuery();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Console.WriteLine($"{dr["empid"]}\t{dr["empname"]}\t\t {dr["salary"]}\t\t{dr["deptno"]}");
                }

                //ShowData();
                return i;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                return 0;
            }
            finally
            {
                dr.Close();
                cn.Close();
            }
        }
    }
    class Method3
    {
        static void Main()
        {
            Console.WriteLine("-----------------SQL QUERY-----------------");
            Console.WriteLine("Press 1 for insertion");
            Console.WriteLine("Press 2 for updation");
            Console.WriteLine("Press 3 for deletion");
            Console.WriteLine("Press 4 for searching");
            Console.WriteLine("Press 0 to return");


            WithStoredProc m = new WithStoredProc();


            while (true)
            {
                int response = Convert.ToInt32(Console.ReadLine());

                if (response == 1)
                {
                    m.InsertRow();

                }
                else if (response == 2)
                {
                    m.Update();

                }
                else if (response == 3)
                {
                    m.DeleteRow();

                }
                else if (response == 4)
                {
                    m.SearchRow();

                }
                else if (response == 0)
                {
                    Console.WriteLine("We are closing the app");
                    break;
                }
                Console.WriteLine("Press 1 for insertion");
                Console.WriteLine("Press 2 for updation");
                Console.WriteLine("Press 3 for deletion");
                Console.WriteLine("Press 4 for searching");
                Console.WriteLine("Press 0 to return");


            }


            Console.Read();
        }
    }
}
