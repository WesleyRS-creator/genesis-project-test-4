namespace TestMVCSharpC.Utils;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

using Npgsql;


public class ConstraintDisabler
{
  public enum DbType { PostgreSQL, MySQL, SQLServer, Oracle }

  private readonly DbConnection _connection;
  private readonly DbType _dbType;
  private readonly Stack<string> _restoreDdls = new Stack<string>();
  private readonly HashSet<string> _processedTables = new HashSet<string>();

  public ConstraintDisabler(DbConnection connection)
  {
    _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    _dbType = DetectDatabaseType();
  }

  private DbType DetectDatabaseType()
  {
    switch (_connection)
    {
      
      case NpgsqlConnection:
        return DbType.PostgreSQL;
      
        default:
          throw new NotSupportedException($"SGBD non pris en charge: {_connection.GetType().Name}");
    }
  }

  public void StartTransaction()
  {
      if (_dbType == DbType.PostgreSQL)
      {
          using var cmd = _connection.CreateCommand();
          cmd.CommandText = "SET CONSTRAINTS ALL DEFERRED";
          cmd.ExecuteNonQuery();
    }
  }

  public void DisableConstraintsForTable(string tableName)
  {
    if (_processedTables.Contains(tableName)) return;
    _processedTables.Add(tableName);

    switch (_dbType)
    {
      
      case DbType.PostgreSQL:
        DisablePostgres(tableName);
        break;
      
    }
  }

  public void RestoreConstraints()
  {
    Console.WriteLine($"Restoring {_restoreDdls.Count} constraints");

    // Separate NOT NULL and other constraints to restore NOT NULL first
    var notNullDdls = new List<string>();
    var otherDdls = new List<string>();
    foreach (var ddl in _restoreDdls)
    {
      if (ddl.Contains("NOT NULL"))
      notNullDdls.Add(ddl);
      else
      otherDdls.Add(ddl);
    }

    // Restore NOT NULL constraints first
    foreach (var ddl in notNullDdls)
    {
      try
      {
        Console.WriteLine($"Restoring: {ddl}");
            using (var cmd = CreateDbCommand(ddl))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Successfully restored: {ddl}");
            }
        }
        catch (DbException ex)
        {
            Console.Error.WriteLine($"Error restoring constraint: {ddl}");
            Console.Error.WriteLine($"Reason: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine($"Connection issue: {ex.Message}");
        }
    }

    // Restore other constraints (CHECK, FOREIGN KEY, UNIQUE)
    foreach (var ddl in otherDdls)
    {
        try
        {
            Console.WriteLine($"Restoring: {ddl}");
            using (var cmd = CreateDbCommand(ddl))
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Successfully restored: {ddl}");
            }
        }
        catch (DbException ex)
        {
            Console.Error.WriteLine($"Error restoring constraint: {ddl}");
            Console.Error.WriteLine($"Reason: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine($"Connection issue: {ex.Message}");
        }
    }

    _restoreDdls.Clear();
    _processedTables.Clear();
}

  private DbCommand CreateDbCommand(string commandText)
  {
      switch (_dbType)
      {
          
          case DbType.PostgreSQL:
              return new NpgsqlCommand(commandText, (NpgsqlConnection)_connection);
          
          default:
              throw new NotSupportedException($"Unsupported database type: {_dbType}");
      }
  }
  
  private void DisablePostgres(string tableName)
  {
      // 1. Désactiver les contraintes référencées
      var foreignConstraints = new List<(string Name, string SourceTable, string Definition)>();

      using (var cmd = new NpgsqlCommand(
          "SELECT conname, conrelid::regclass::text AS source_table, " +
          "pg_get_constraintdef(oid) AS definition " +
      "FROM pg_constraint " +
      "WHERE confrelid = @tableName::regclass",
      (NpgsqlConnection)_connection))
      {
        cmd.Parameters.AddWithValue("tableName", tableName);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          foreignConstraints.Add((
          reader.GetString(0),
          reader.GetString(1),
          reader.GetString(2)
          ));
        }
      }

      foreach (var (name, sourceTable, definition) in foreignConstraints)
      {
        _restoreDdls.Push($"ALTER TABLE {sourceTable} ADD CONSTRAINT {name} {definition}");

          using var dropCmd = new NpgsqlCommand(
              $"ALTER TABLE {sourceTable} DROP CONSTRAINT {name} CASCADE",
        (NpgsqlConnection)_connection);
        dropCmd.ExecuteNonQuery();
      }

      // 2. Désactiver les contraintes locales
      var localConstraints = new List<(string Name, string Definition)>();

      using (var cmd = new NpgsqlCommand(
      "SELECT conname, pg_get_constraintdef(oid) AS def " +
      "FROM pg_constraint " +
      "WHERE conrelid = @tableName::regclass",
      (NpgsqlConnection)_connection))
      {
        cmd.Parameters.AddWithValue("tableName", tableName);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          localConstraints.Add((
          reader.GetString(0),
          reader.GetString(1)
          ));
        }
      }

      foreach (var (name, def) in localConstraints)
      {
        _restoreDdls.Push($"ALTER TABLE {tableName} ADD CONSTRAINT {name} {def}");

          using var dropCmd = new NpgsqlCommand(
              $"ALTER TABLE {tableName} DROP CONSTRAINT {name} CASCADE",
        (NpgsqlConnection)_connection);
        dropCmd.ExecuteNonQuery();
      }

      // 3. Gérer les contraintes NOT NULL
      var notNullColumns = new List<string>();

      using (var cmd = new NpgsqlCommand(
      "SELECT a.attname AS column_name " +
      "FROM pg_attribute a " +
      "JOIN pg_class c ON c.oid = a.attrelid " +
      "WHERE c.relname = @tableName " +
      "  AND a.attnotnull " +
      "  AND a.attidentity = '' " +
      "  AND a.attnum > 0 " +
      "  AND NOT a.attisdropped",
      (NpgsqlConnection)_connection))
      {
        cmd.Parameters.AddWithValue("tableName", tableName);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          notNullColumns.Add(reader.GetString(0));
        }
      }

      foreach (var colName in notNullColumns)
      {
        _restoreDdls.Push($"ALTER TABLE {tableName} ALTER COLUMN {colName} SET NOT NULL");

        using var alterCmd = new NpgsqlCommand(
        $"ALTER TABLE {tableName} ALTER COLUMN {colName} DROP NOT NULL",
        (NpgsqlConnection)_connection);
        alterCmd.ExecuteNonQuery();
      }
  }
  
}
