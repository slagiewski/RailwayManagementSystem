﻿using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace RailwayManagementSystem
{
    internal static class Reservations
    {
        public static bool AddReservations(SqlConnection sqlConnection, string customerId, string price, string courseId, string stationA, string stationB, string seatNumber)
        {
            try
            {
                sqlConnection.Open();
                string command = $"EXEC ADD_RESERVATIONS " +
                                 $"{customerId}, {price}, {courseId}, '{stationA}', '{stationB}', {seatNumber}";
                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlCommand.ExecuteNonQuery();

            }
            catch
            {
                Debug.WriteLine("Błąd zapytania do bazy danych!");
                return false;
            }
            finally
            {
                sqlConnection.Close();
            }
            return true;
        }

        public static DataTable GetCustomerReservations(SqlConnection sqlConnection, string customerID)
        {
            try
            {
                using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter($"EXEC SHOW_CUSTOMER_RESERVATION {customerID}", sqlConnection))
                {
                    DataTable dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    if (dataTable.Rows.Count != 0)
                        return dataTable;
                    else
                        return null;
                }
            }
            catch
            {
                Debug.WriteLine("Błąd zapytania do bazy danych!");
                return null;
            }

        }
    }
}