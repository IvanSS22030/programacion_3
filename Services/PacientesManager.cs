using System;
using System.Collections.Generic;
using Npgsql;
using ProyectoFinal.Models;

namespace ProyectoFinal.Services
{
    public class PacientesManager
    {
        private string connString = "Host=localhost;Username=postgres;Password=Use your password;Database=pacientesdb";

        public void AddPaciente(Paciente p)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("CALL sp_insert_paciente(@nombre, @apellido, @direccion, @telefono, @estatus)", conn))
                {
                    cmd.Parameters.AddWithValue("nombre", p.NombrePac);
                    cmd.Parameters.AddWithValue("apellido", p.ApellidoPac);
                    cmd.Parameters.AddWithValue("direccion", p.DireccionPac ?? "");
                    cmd.Parameters.AddWithValue("telefono", p.TelefonoPac ?? "");
                    cmd.Parameters.AddWithValue("estatus", p.EstatusPac ?? "Activo");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdatePaciente(Paciente p)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("CALL sp_update_paciente(@codigo, @nombre, @apellido, @direccion, @telefono, @estatus)", conn))
                {
                    cmd.Parameters.AddWithValue("codigo", p.CodigoPac);
                    cmd.Parameters.AddWithValue("nombre", p.NombrePac);
                    cmd.Parameters.AddWithValue("apellido", p.ApellidoPac);
                    cmd.Parameters.AddWithValue("direccion", p.DireccionPac ?? "");
                    cmd.Parameters.AddWithValue("telefono", p.TelefonoPac ?? "");
                    cmd.Parameters.AddWithValue("estatus", p.EstatusPac ?? "Activo");
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeletePaciente(int codigo)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("CALL sp_delete_paciente(@codigo)", conn))
                {
                    cmd.Parameters.AddWithValue("codigo", codigo);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Paciente> GetAllPacientes()
        {
            var list = new List<Paciente>();
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand("SELECT * FROM fn_get_all_pacientes()", conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Paciente
                        {
                            CodigoPac = reader.GetInt32(0),
                            NombrePac = reader.GetString(1),
                            ApellidoPac = reader.GetString(2),
                            DireccionPac = reader.IsDBNull(3) ? "" : reader.GetString(3),
                            TelefonoPac = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            EstatusPac = reader.IsDBNull(5) ? "Activo" : reader.GetString(5)
                        });
                    }
                }
            }
            return list;
        }
    }
}

// Actualización de repositorio - 2026-04-08
