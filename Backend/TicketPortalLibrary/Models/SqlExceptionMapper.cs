using System;
using Microsoft.Data.SqlClient;

namespace TicketPortalLibrary.Models;

public static class SqlExceptionMapper
{
    public static TicketException Map(SqlException ex){
        return ex.Number switch
        {
            2627 => new TicketException("Duplicate primary key. Record already exists.",499),
            2601 => new TicketException("Duplicate value. Unique constraint violated.",499),
            547  => new TicketException("Operation violates foreign key constraint.",499),
            1205 => new TicketException("Deadlock occurred. Please retry.",499),
            4060 => new TicketException("Database unavailable.",499),
            18456 => new TicketException("Database authentication failed.",499),
            _ => new TicketException($"Database error (SQL Error No: {ex.Number}).",499)
        };
    }
}