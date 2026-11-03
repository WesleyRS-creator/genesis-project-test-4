namespace TestMVCSharpC.Utils;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data;

public class EntityUtils
{
	private readonly TestMVCSharpCContext _context;

	public EntityUtils(TestMVCSharpCContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Detect the best column to display for an entity
	/// </summary>
	public async Task<string> DetectBestDisplayColumnAsync<T>() where T : class
	{
		var entityType = _context.Model.FindEntityType(typeof(T));
		if (entityType == null)
			throw new InvalidOperationException($"Entity type {typeof(T).Name} not found in the model.");

		var nonNullableProperties = entityType.GetProperties()
			.Where(p => !p.IsPrimaryKey() && !p.IsNullable)
			.ToList();

		if (nonNullableProperties.Any())
		{
			var priorities = new List<Func<IProperty, bool>>
			{
				p => p.ClrType == typeof(string) &&
					(p.GetColumnType()?.Contains("json") == true ||
					p.GetColumnType()?.Contains("xml") == true ||
					p.GetColumnType()?.Contains("inet") == true),

				p => p.ClrType == typeof(string),

				p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?),

				p => p.ClrType == typeof(int) || p.ClrType == typeof(int?) ||
					p.ClrType == typeof(long) || p.ClrType == typeof(long?) ||
					p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?) ||
					p.ClrType == typeof(double) || p.ClrType == typeof(double?)
			};

			foreach (var match in priorities)
			{
				var prop = nonNullableProperties.FirstOrDefault(match);
				if (prop != null)
					return prop.Name;
			}

			return nonNullableProperties.First().Name;
		}

		var nullableProperties = entityType.GetProperties()
			.Where(p => !p.IsPrimaryKey())
			.ToList();

		if (!nullableProperties.Any())
			return entityType.GetProperties().First().Name;

		var fallbackPriorities = new List<Func<IProperty, bool>>
		{
			p => p.ClrType == typeof(string),
			p => p.ClrType == typeof(DateTime) || p.ClrType == typeof(DateTime?),
			p => p.ClrType == typeof(int) || p.ClrType == typeof(int?) ||
				p.ClrType == typeof(long) || p.ClrType == typeof(long?) ||
				p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?) ||
				p.ClrType == typeof(double) || p.ClrType == typeof(double?)
		};

		foreach (var match in fallbackPriorities)
		{
			var prop = nullableProperties.FirstOrDefault(match);
			if (prop != null)
				return prop.Name;
		}

		return nullableProperties.First().Name;
	}

	/// <summary>
	/// Detect the primary column for an entity
	/// </summary>
	public string GetPrimaryKeyName<TEntity>() where TEntity : class
	{
		var entityType = _context.Model.FindEntityType(typeof(TEntity));
		var primaryKey = entityType.FindPrimaryKey();

		if (primaryKey == null)
			throw new InvalidOperationException($"Entity {typeof(TEntity).Name} has no primary key defined.");

		// Return the first column of the primary key
		return primaryKey.Properties.First().Name;
	}
}
