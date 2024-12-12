using Soap.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Services;
using System.Xml.Serialization;

namespace Soap.Services
{
    /// <summary>
    /// Web Service for CRUD operations on the Transportes database table.
    /// </summary>
    [WebService(Namespace = "http://example.com/soap/transportes/", Description = "CRUD operations on the Transportes database table")]
    public class TransportesWS : WebService, IService
    {
        #region GET

        [WebMethod(Description = "Retrieves all transportes from the Transportes table.")]
        /// <summary>
        /// Retrieves all transportes from the Transportes table.
        /// </summary>
        /// <returns>A List containing all transportes.</returns>
        public List<Transporte> GetAllTransportes()
        {
            List<Transporte> transportes = new List<Transporte>();

            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TransportesConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    string query = "SELECT * FROM Transporte";
                    SqlCommand cmd = new SqlCommand(query, con);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            transportes.Add(new Transporte
                            {
                                Id = reader.GetInt32(0),
                                Tipo = reader.GetString(1),
                                Matricula = reader.GetString(2),
                                ArCondicionado = reader.IsDBNull(3) ? false : reader.GetBoolean(3),
                                TemperaturaAtual = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4),
                                IdUtilizador = reader.GetInt32(5)
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving transportes: " + ex.Message);
            }

            return transportes;
        }

        [WebMethod(Description = "Retrieves a transporte from the Transportes table based on its ID.")]
        /// <summary>
        /// Retrieves a transporte from the Transportes table based on its ID.
        /// </summary>
        /// <param name="Id">The ID of the transporte to retrieve.</param>
        /// <returns>The transporte details.</returns>
        public Transporte GetTransporteById(int Id)
        {
            Transporte transporte = null;

            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TransportesConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    string query = "SELECT * FROM Transporte WHERE Id = @Id";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Id);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            transporte = new Transporte
                            {
                                Id = reader.GetInt32(0),
                                Tipo = reader.GetString(1),
                                Matricula = reader.GetString(2),
                                ArCondicionado = reader.IsDBNull(3) ? false : reader.GetBoolean(3),
                                TemperaturaAtual = reader.IsDBNull(4) ? 0.0m : reader.GetDecimal(4),
                                IdUtilizador = reader.GetInt32(5)
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving transporte: " + ex.Message);
            }

            return transporte;
        }

        #endregion

        #region POST

        [WebMethod(Description = "Inserts a new transporte into the Transportes table.")]
        /// <summary>
        /// Inserts a new transporte into the Transportes table.
        /// </summary>
        /// <param name="transporte">The onject Transporte</param>
        /// <returns>The number of rows affected by the insertion operation.</returns>
        /// 
        public int InsertTransporte(Transporte transporte)
        {
            int rowsAffected = 0;

            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TransportesConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    string query = "INSERT INTO Transporte (Tipo, Matricula, ArCondicionado, TemperaturaAtual, IdUtilizador) " +
                                   "VALUES (@Tipo, @Matricula, @ArCondicionado, @TemperaturaAtual, @IdUtilizador)";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Tipo", transporte.Tipo);
                    cmd.Parameters.AddWithValue("@Matricula", transporte.Matricula);
                    cmd.Parameters.AddWithValue("@ArCondicionado", transporte.ArCondicionado);
                    cmd.Parameters.AddWithValue("@TemperaturaAtual", transporte.TemperaturaAtual);
                    cmd.Parameters.AddWithValue("@IdUtilizador", transporte.IdUtilizador);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error inserting transporte: " + ex.Message);
            }

            return rowsAffected;
        }


        #endregion

        #region PUT

        [WebMethod(Description = "Updates a transporte in the Transportes table.")]
        /// <summary>
        /// Updates a transporte in the Transportes table.
        /// </summary>
        /// <param name="Id">The ID of the transporte to update.</param>
        /// <param name="temperaturaAtual">The new temperaturaAtual of the transporte.</param>
        /// <returns>The number of rows affected by the update operation.</returns>
        public int UpdateTransporte(int Id, decimal temperaturaAtual)
        {
            int rowsAffected = 0;

            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TransportesConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    string query = "UPDATE Transporte SET TemperaturaAtual = @TemperaturaAtual WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.Parameters.AddWithValue("@TemperaturaAtual", temperaturaAtual);
                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating transporte: " + ex.Message);
            }

            return rowsAffected;
        }

        #endregion

        #region DELETE

        [WebMethod(Description = "Deletes a transporte from the Transportes table.")]
        /// <summary>
        /// Deletes a transporte from the Transportes table based on its ID.
        /// </summary>
        /// <param name="id">The ID of the transporte to delete.</param>
        /// <returns>The number of rows affected by the delete operation.</returns>
        public int DeleteTransporte(int id)
        {
            int rowsAffected = 0;

            try
            {
                string cs = ConfigurationManager.ConnectionStrings["TransportesConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(cs))
                {
                    con.Open();

                    string query = "DELETE FROM Transporte WHERE Id = @Id";

                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Id", id);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting transporte: " + ex.Message);
            }

            return rowsAffected;
        }

        #endregion
    }
}
