﻿using Microsoft.Data.SqlClient;
using System.Data;
using Dapper;

namespace BibliotecaAPI
{
    public class DapperContext
    {

        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            // Obtener de appsettings.json
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() =>
            new SqlConnection(_connectionString);


    }
}
