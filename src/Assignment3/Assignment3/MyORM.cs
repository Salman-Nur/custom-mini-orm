using Assignment3.TestCase1;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Assignment3
{
    public class MyORM<G, T>
    {
        //Complete
        #region Insert(T item)

        List<DbCommand> insertCommands = new List<DbCommand>();

        public void Insert(T item)
        {
            string ParentList = null;
            G parentId = default(G);
            Insert(item, ParentList, parentId);

            using (AdoNetUtility adoNetUtility = new AdoNetUtility())
            {
                DbCommand command = insertCommands.Last();
                string wrongFk = typeof(T).Name + "Id";
                if (command.CommandText.Contains(wrongFk))
                {
                    int parametersIndex = -1;
                    for (int i = 0; i < command.Parameters.Count; i++)
                    {
                        if (command.Parameters[i].ParameterName == "@" + wrongFk)
                        {
                            parametersIndex = i;
                            break;
                        }
                    }
                    if (parametersIndex >= 0)
                    {
                        command.Parameters.RemoveAt(parametersIndex);
                    }

                    command.CommandText = command.CommandText.Replace("," + wrongFk, "");
                    command.CommandText = command.CommandText.Replace(",@" + wrongFk, "");
                }

                HandleRecursiveObject(item);
                List<string> sortedTable = new List<string>();
                IEnumerable<PropertyInfo> properties;
                Type type = item.GetType();
                string tableName = type.Name;
                properties = type.GetProperties();
                sortedTable.Add(tableName);
                SortQuery(insertCommands, item, ref sortedTable);
                sortedTable = sortedTable.Distinct().ToList();
                Dictionary<string, int> tableIndex = new Dictionary<string, int>();
                int currentIndex = 0;
                foreach (var table in sortedTable)
                {
                    if (!tableIndex.ContainsKey(table))
                    {
                        tableIndex.Add(table, currentIndex++);
                    }
                }

                List<DbCommand> sortedCommands = insertCommands.OrderBy(cmd =>
                {
                    string tableName = Regex.Match(cmd.CommandText, @"INSERT\s*INTO\s*(\w+)", 
                        RegexOptions.IgnoreCase).Groups[1].Value;
                    if (tableIndex.ContainsKey(tableName))
                    {
                        return tableIndex[tableName];
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }).ToList();

                List<DbCommand> commandsWithSquareBrackets = new List<DbCommand>();

                foreach (var sortedCommand in sortedCommands)
                {
                    string tableNameWithSquareBrackets = Regex.Match(sortedCommand.CommandText,
                        @"INSERT\s*INTO\s*(\w+)", RegexOptions.IgnoreCase).Groups[1].Value;

                    string modifiedTableName = $"[{tableNameWithSquareBrackets}]";         
                    
                    sortedCommand.CommandText =  sortedCommand.CommandText.Replace(
                        $"INSERT INTO {tableNameWithSquareBrackets}", $"INSERT INTO {modifiedTableName}");

                    
                    commandsWithSquareBrackets.Add(sortedCommand);
                }
                adoNetUtility.WriteFinal(sortedCommands);
            }
            insertCommands.Clear();
        }
        private void Insert(object item, string ParentList, G? parentId)
        {
            if (item != null)
            {
                Type type = item.GetType();
                string tableName = type.Name;
                IEnumerable<PropertyInfo> properties;
                properties = type.GetProperties();

                (string sql, List<DbParameter> parameters) sqlParameters =
                    GenerateInsertQuery(tableName, properties, item, ParentList, parentId);

                using (AdoNetUtility adoNetUtility = new AdoNetUtility())
                {
                    foreach (PropertyInfo property in properties)
                    {
                        if (property.PropertyType.IsGenericType)
                        {
                            continue;
                        }
                        else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            continue;
                        }
                        else
                        {
                            string propertyName = property.Name;
                            var parameter = new SqlParameter($"@{propertyName}", property.GetValue(item)
                                ?? DBNull.Value);
                            sqlParameters.parameters.Add(parameter);
                        }
                    }

                    adoNetUtility.WriteOperation(sqlParameters.sql, sqlParameters.parameters, insertCommands);
                }
            }
        }
        private void HandleRecursiveObject(object item)
        {
            if (item != null)
            {
                var properties = item.GetType().GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType)
                    {
                        var list = property.GetValue(item) as IList;

                        if (list != null)
                        {
                            foreach (var listItem in list)
                            {
                                HandleRecursiveObject(listItem);
                            }
                        }
                        
                    }
                    else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    {
                        var propertyValue = property.GetValue(item);
                        var oneToOne = property.Name + "Id";

                        for (int i = 0; i < insertCommands.Count; i++)
                        {
                            if (insertCommands[i].CommandText.Contains(" " + item.GetType().Name + " ") &&
                                !insertCommands[i].CommandText.Contains(oneToOne))
                            {
                                int firstParentIndex = insertCommands[i].CommandText.IndexOf(')');
                                insertCommands[i].CommandText = insertCommands[i].CommandText.Insert(firstParentIndex, "," + oneToOne);

                                int lastParenIndex = insertCommands[i].CommandText.LastIndexOf(')');
                                insertCommands[i].CommandText = insertCommands[i].CommandText.Insert(lastParenIndex, ",@" + oneToOne);

                                var parameter = insertCommands[i].CreateParameter();
                                parameter.ParameterName = "@" + oneToOne;

                                if (propertyValue == null)
                                {
                                    insertCommands.Remove(insertCommands[i]);
                                    continue;
                                }
                                var idProperty = propertyValue.GetType().GetProperty("Id");
                                if (idProperty != null)
                                {
                                    parameter.Value = idProperty.GetValue(propertyValue);
                                }
                                else
                                {
                                    continue;
                                }

                                insertCommands[i].Parameters.Add(parameter);
                                break;
                            }
                        }

                        HandleRecursiveObject(propertyValue);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        private (string, List<DbParameter>) GenerateInsertQuery(string tableName, IEnumerable<PropertyInfo> properties,
            object item, string parentList, G? parentId, string columns = null, string values = null)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType)
                {
                    var list = property.GetValue(item) as IList;
                    parentList = item.GetType().Name + "Id";
                    parentId = (dynamic)item.GetType().GetProperty("Id").GetValue(item);

                    if (list != null)
                    {
                        foreach (var listItem in list)
                        {
                            Insert(listItem, parentList, parentId);
                        }
                    }
                }
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var propertyValue = property.GetValue(item);
                    parentList = null;
                    parentId = default(G);
                    Insert(propertyValue, parentList, parentId);
                }
                else
                {
                    string propertyName = property.Name;
                    columns += $"{propertyName},";
                    values += $"@{propertyName},";

                    if (parentList != null)
                    {
                        if (!columns.Contains(parentList))
                        {
                            columns += $"{parentList},";
                            values += $"@{parentList},";
                            parameters.Add(new SqlParameter($"@{parentList}", parentId));
                        }
                    }
                }
            }
            columns = columns.TrimEnd(',');
            values = values.TrimEnd(',');
            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
            Console.WriteLine(query);
            return (query, parameters);
        }
        private void SortQuery(List<DbCommand> commands, object item,
            ref List<string> sortedTable, bool flag = true)
        {
            if (item != null)
            {
                IEnumerable<PropertyInfo> properties;
                Type type = item.GetType();
                string tableName = type.Name;
                properties = type.GetProperties();

                foreach (var property in properties)
                {
                    if (property.PropertyType.IsGenericType)
                    {
                        var list = property.GetValue(item) as IList;
                        int index = sortedTable.IndexOf(item.GetType().Name);
                        if (list != null)
                        {
                            foreach (var listItem in list)
                            {
                                sortedTable.Insert(index + 1, listItem.GetType().Name);
                                SortQuery(commands, listItem, ref sortedTable, false);
                            }
                        }
                    }
                    else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                    {
                        int index = sortedTable.IndexOf(item.GetType().Name);
                        sortedTable.Insert(index, property.PropertyType.Name);
                        SortQuery(commands, property.GetValue(item), ref sortedTable, false);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }

        #endregion


        #region Update(T item)

        List<DbCommand> updateCommands = new List<DbCommand>();
        string query = string.Empty;
        public void Update(T item)
        {
            string parentList = null;
            G parentId = default(G);
            Update(item, parentList, parentId);

            using (AdoNetUtility adoNetUtility = new AdoNetUtility())
            {
                DbCommand command = updateCommands.Last();
                string wrongFk = typeof(T).Name + "Id";
                string wrongFKValue = "@" + typeof(T).Name + "Id";
                if (command.CommandText.Contains("," + wrongFk + "=" + wrongFKValue))
                {
                    int parameterIndex = -1;
                    for (int i = 0; i < command.Parameters.Count; i++)
                    {
                        if (command.Parameters[i].ParameterName == "@" + wrongFk)
                        {
                            parameterIndex = i;
                            break;
                        }
                    }
                    if (parameterIndex >= 0)
                    {
                        command.Parameters.RemoveAt(parameterIndex);
                    }
                    command.CommandText = command.CommandText.Replace("," + wrongFk + "=" + wrongFKValue, "");
                }

                HandleRecursiveObject(item);
                List<string> sortedTable = new List<string>();
                IEnumerable<PropertyInfo> properties;
                Type type = item.GetType();
                string tableName = type.Name;
                properties = type.GetProperties();
                sortedTable.Add(tableName);
                SortQuery(updateCommands, item, ref sortedTable);
                sortedTable = sortedTable.Distinct().ToList();
                Dictionary<string, int> tableIndex = new Dictionary<string, int>();
                int currentIndex = 0;

                foreach (var table in sortedTable)
                {
                    if (!tableIndex.ContainsKey(table))
                    {
                        tableIndex.Add(table, currentIndex++);
                    }
                }
                List<DbCommand> sortedCommands = updateCommands.OrderBy(cmd =>
                {
                    string tableName = Regex.Match(cmd.CommandText,
                        @"INSERT\s*INTO\s*(\w+)", RegexOptions.IgnoreCase).Groups[1].Value;
                    if (tableIndex.ContainsKey(tableName))
                    {
                        return tableIndex[tableName];
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }).ToList();

                List<DbCommand> commandsWithSquareBrackets = new List<DbCommand>();

                foreach (var sortedCommand in sortedCommands)
                {
                    string tableNameWithSquareBrackets = Regex.Match(sortedCommand.CommandText,
                        @"UPDATE\s*(\w+)", RegexOptions.IgnoreCase).Groups[1].Value;

                    string modifiedTableName = $"[{tableNameWithSquareBrackets}]";

                    sortedCommand.CommandText = sortedCommand.CommandText.Replace(
                        $"UPDATE {tableNameWithSquareBrackets}", $"UPDATE {modifiedTableName}");

                    commandsWithSquareBrackets.Add(sortedCommand);
                }

                adoNetUtility.WriteFinal(sortedCommands);
            }
            updateCommands.Clear();
        }
        private void Update(object item, string parentList, G? parentId)
        {
            if (item != null)
            {
                Type type = item.GetType();
                string tableName = type.Name;
                IEnumerable<PropertyInfo> properties;
                properties = type.GetProperties();

                (string sql, List<DbParameter> parameters) sqlParameters =
                    GenerateUpdateQuery(tableName, properties, item, parentList, parentId);

                using (AdoNetUtility adoNetUtility = new AdoNetUtility())
                {
                    foreach (var property in properties)
                    {
                        if (property.PropertyType.IsGenericType)
                        {
                            continue;
                        }
                        else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            continue;
                        }
                        else
                        {
                            string propertyName = property.Name;
                            var parameter = new SqlParameter($"@{propertyName}", property.GetValue(item) ?? DBNull.Value);
                            sqlParameters.parameters.Add(parameter);
                        }
                    }
                    adoNetUtility.WriteOperation(sqlParameters.sql, sqlParameters.parameters, updateCommands);
                }
            }
        }
        private (string, List<DbParameter>) GenerateUpdateQuery(string tableName, IEnumerable<PropertyInfo> properties,
            object item, string parentList, G? parentId, string columns = null, string values = null)
        {
            List<DbParameter> parameters = new List<DbParameter>();

            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType)
                {
                    var list = property.GetValue(item) as IList;
                    parentList = item.GetType().Name + "Id";
                    parentId = (dynamic)item.GetType().GetProperty("Id").GetValue(item);

                    if (list != null)
                    {
                        foreach (var listItem in list)
                        {
                            Update(listItem, parentList, parentId);
                        }
                    }
                }
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var propertyValue = property.GetValue(item);
                    parentList = null;
                    parentId = default(G);
                    Update(propertyValue, parentList, parentId);
                }
                else
                {
                    string propertyName = property.Name;
                    var propertyValue = property.GetValue(item);
                    if (propertyValue != null)
                    {
                        columns += $"{propertyName},";
                        values += $"@{propertyName},";

                        if (parentList != null)
                        {
                            if (!columns.Contains(parentList))
                            {
                                columns += $"{parentList},";
                                values += $"@{parentList},";
                                parameters.Add(new SqlParameter($"@{parentList}", parentId));
                            }
                        }
                    }
                }
            }
            columns = columns.TrimEnd(',');
            values = values.TrimEnd(',');
            var sqlColumns = columns.Split(",").Where(c => c != "Id").ToList();
            if (sqlColumns.Count == 0)
            {
                query = $"UPDATE {tableName} SET Id=@Id WHERE Id=@Id";
            }
            else
            {
                var sqlValues = values.Split(",").Where(c => c != "@Id").ToList();
                var updateSql = "";

                for (int i = 0; i < sqlColumns.Count; i++)
                {
                    if (i > 0) updateSql += ",";
                    updateSql += $"{sqlColumns[i]}={sqlValues[i]}";
                }
                query = $"UPDATE {tableName} SET {updateSql} WHERE Id=@Id";
            }
            Console.WriteLine(query);
            return (query, parameters);
        }

        #endregion


        #region Delete(T item)

        List<DbCommand> deleteCommands = new List<DbCommand>();
        public void Delete(T item)
        {
            string parentList = null;
            G parentId = default(G);
            Delete(item, parentList, parentId);

            using (AdoNetUtility adoNetUtility = new AdoNetUtility())
            {
                DbCommand command = deleteCommands.Last();
                string wrongFk = typeof(T).Name + "Id";
                string wrongFKValue = "@" + typeof(T).Name + "Id";
                if (command.CommandText.Contains("," + wrongFk + "=" + wrongFKValue))
                {
                    int parameterIndex = -1;
                    for (int i = 0; i < command.Parameters.Count; i++)
                    {
                        if (command.Parameters[i].ParameterName == "@" + wrongFk)
                        {
                            parameterIndex = i;
                            break;
                        }
                    }
                    if (parameterIndex >= 0)
                    {
                        command.Parameters.RemoveAt(parameterIndex);
                    }

                    command.CommandText = command.CommandText.Replace("," + wrongFk + "=" + wrongFKValue, "");
                }

                HandleRecursiveObject(item);
                List<string> sortedTable = new List<string>();
                IEnumerable<PropertyInfo> properties;
                Type type = item.GetType();
                string tableName = type.Name;
                properties = type.GetProperties();
                sortedTable.Add(tableName);
                SortQuery(deleteCommands, item, ref sortedTable);
                sortedTable.Reverse();
                sortedTable = sortedTable.Distinct().ToList();
                Dictionary<string, int> tableIndex = new Dictionary<string, int>();
                int currentIndex = 0;
                foreach (var table in sortedTable)
                {
                    if (!tableIndex.ContainsKey(table))
                    {
                        tableIndex.Add(table, currentIndex++);
                    }
                }

                List<DbCommand> sortedCommands = deleteCommands.OrderBy(cmd =>
                {
                    string tableName = Regex.Match(cmd.CommandText, @"DELETE\s*FROM\s*(\w+)", RegexOptions.IgnoreCase).Groups[1].Value;

                    if (tableIndex.ContainsKey(tableName))
                    {
                        return tableIndex[tableName];
                    }
                    else
                    {
                        return int.MaxValue;
                    }
                }).ToList();

                List<DbCommand> commandsWithSquareBrackets = new List<DbCommand>();
                foreach (var sortedCommand in sortedCommands)
                {
                    string tableNameWithSquareBrackets = Regex.Match(sortedCommand.CommandText,
                        @"DELETE\s*FROM\s*(\w+)", RegexOptions.IgnoreCase).Groups[1].Value;

                    string modifiedTableName = $"[{tableNameWithSquareBrackets}]";

                    sortedCommand.CommandText = sortedCommand.CommandText.Replace(
                        $"DELETE FROM {tableNameWithSquareBrackets}", $"DELETE FROM {modifiedTableName}");

                    commandsWithSquareBrackets.Add(sortedCommand);
                }

                adoNetUtility.WriteFinal(commandsWithSquareBrackets);
            }
            deleteCommands.Clear();
        }
        private void Delete(object item, string parentList, G? parentId)
        {
            if (item != null)
            {
                Type type = item.GetType();
                string tableName = type.Name;
                IEnumerable<PropertyInfo> properties;
                properties = type.GetProperties();

                (string sql, List<DbParameter> parameters) sqlParameters =
                    GenerateDeleteQuery(tableName, properties, item, parentList, parentId);

                using (var adoNetUtility = new AdoNetUtility())
                {
                    foreach (var property in properties)
                    {
                        if (property.PropertyType.IsGenericType)
                        {
                            continue;
                        }
                        else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            continue;
                        }
                        else
                        {
                            string propertyName = property.Name;
                            var parameter = new SqlParameter($"@{propertyName}", property.GetValue(item) ?? DBNull.Value);
                            sqlParameters.parameters.Add(parameter);
                        }
                    }

                    adoNetUtility.WriteOperation(sqlParameters.sql, sqlParameters.parameters, deleteCommands);
                }
            }
        }
        private (string, List<DbParameter>) GenerateDeleteQuery(string tableName, IEnumerable<PropertyInfo> properties,
            object item, string parentList, G? parentId, string columns = null, string values = null)
        {
            List<DbParameter> parameters = new List<DbParameter>();
            foreach (var property in properties)
            {
                if (property.PropertyType.IsGenericType)
                {
                    var list = property.GetValue(item) as IList;
                    parentList = item.GetType().Name + "Id";
                    parentId = (dynamic)item.GetType().GetProperty("Id").GetValue(item);

                    if (list != null)
                    {
                        foreach (var listItem in list)
                        {
                            Delete(listItem, parentList, parentId);
                        }
                    }
                }
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    var propertyValue = property.GetValue(item);
                    Delete(propertyValue, parentList, parentId);
                }
                else
                {
                    string propertyName = property.Name;
                    columns += $"{propertyName},";
                    values += $"@{propertyName},";

                    if (parentList != null)
                    {
                        if (!columns.Contains(parentList))
                        {
                            columns += $"{parentList},";
                            values += $"@{parentList},";
                            parameters.Add(new SqlParameter($"@{parentList}", parentId));
                        }
                    }
                }
            }

            columns = columns.TrimEnd(',');
            values = values.TrimEnd(',');
            
            string query = $"DELETE FROM {tableName} WHERE Id=@Id";
            Console.WriteLine(query);
            return (query, parameters);
        }

        #endregion



        //Not complete yet
        #region Delete(G id)

        public void Delete(G id)
        {
            Type type = typeof(T);
            string tableName = type.Name;
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            string whereClause = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.Name == "Id")
                {
                    whereClause = $" WHERE {property.Name} = @Id";
                    break;
                }
            }

            using (AdoNetUtility adoNetUtility = new AdoNetUtility())
            {
                using (DbCommand command = new SqlCommand())
                {
                    string query = $"DELETE FROM {tableName}{whereClause}";
                    Console.WriteLine(query);

                    var parameters = new List<DbParameter>
                    {
                        new SqlParameter("@Id", id)
                    };

                    List<DbCommand> commands = adoNetUtility.WriteOperation(query, parameters, new List<DbCommand>());
                    //int affectedRows = adoNetUtility.WriteFinal(commands);
                }
            }
        }

        #endregion

        #region GetById(G id)

        T entity = default(T);
        public T GetById(G id)
        {
            Type type = typeof(T);
            entity = Activator.CreateInstance<T>();
            string tableName = type.Name;
            PropertyInfo[] properties = entity.GetType().GetProperties();

            string whereClause = "";

            foreach (PropertyInfo property in properties)
            {
                if (property.PropertyType.IsGenericType)
                {
                    continue;
                }
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    continue;
                    //object nestedInstance = Activator.CreateInstance(property.PropertyType);
                    //string tabName = property.PropertyType.Name;
                    //GetById(tabName, nestedInstance);
                    //property.SetValue(entity, nestedInstance);
                }
                else if (property.Name == "Id")
                {
                    whereClause = $" WHERE {property.Name} = @Id";
                }
            }

            using (AdoNetUtility adoNetUtility = new AdoNetUtility())
            {
                using (DbCommand command = new SqlCommand())
                {
                    string query = $"SELECT * FROM {tableName}{whereClause}";
                    Console.WriteLine(query);

                    var parameters = new List<DbParameter>
                {
                    new SqlParameter("@Id", id)
                };

                    var rows = adoNetUtility.ReadOperation(query, parameters, false);

                    if (rows.Count > 0)
                    {
                        foreach (var kvp in rows[0])
                        {
                            PropertyInfo property = properties.FirstOrDefault(p => p.Name == kvp.Key);

                            if (property != null)
                            {
                                property.SetValue(entity, kvp.Value);
                            }
                        }
                    }
                }
            }

            return entity;
        }

        private object GetById(string tabName, object item)
        {
            PropertyInfo[] properties = item.GetType().GetProperties();
            string query = $"Select * from {tabName}";
            using (AdoNetUtility adoNetUtility = new AdoNetUtility())
            {
                using (DbCommand command = new SqlCommand())
                {
                    //string query = $"SELECT * FROM {tableName}{whereClause}";
                    Console.WriteLine(query);

                    var parameters = new List<DbParameter>
                    {
                        //new SqlParameter("@Id", id)
                    };

                    var rows = adoNetUtility.ReadOperation(query, parameters, false);

                    if (rows.Count > 0)
                    {
                        foreach (var kvp in rows[0])
                        {
                            PropertyInfo property = properties.FirstOrDefault(p => p.Name == kvp.Key);

                            if (property != null)
                            {
                                property.SetValue(entity, kvp.Value);
                            }
                        }
                    }
                }
            }
            return item;
        }
        #endregion

        #region GetAll()

        List<T> items = new List<T>();
        public IEnumerable<T> GetAll()
        {
            string tableName = typeof(T).Name;
            string query = $"SELECT * FROM {tableName}";

            using (var adoNetUtility = new AdoNetUtility())
            {
                var rows = adoNetUtility.ReadOperation(query, null, false);

                foreach (var row in rows)
                {
                    object item = Activator.CreateInstance<T>();
                    Type type = item.GetType();
                    var properties = type.GetProperties();
                    foreach (var property in properties)
                    {
                        if (property.PropertyType.IsGenericType)
                        {
                            continue;
                        }
                        else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            continue;
                        }
                        else
                        {
                            object value = row[property.Name];
                            if (value != DBNull.Value)
                            {
                                property.SetValue(item, value);
                            }
                        }
                    }

                    //items.Add(item);
                }

                return items;
            }
        }

        private void GetAll(object item)
        {
            Type type = item.GetType();
            string tableName = type.Name;
            string query = $"SELECT * FROM {tableName}";
            var properties = type.GetProperties();

            using (var adoNetUtility = new AdoNetUtility())
            {
                var rows = adoNetUtility.ReadOperation(query, null, false);

                foreach (var row in rows)
                {
                    object item1 = Activator.CreateInstance(type);
                    foreach (var property in properties)
                    {
                        if (property.PropertyType.IsGenericType)
                        {
                            continue;
                        }
                        else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                        {
                            var nestedObject = Activator.CreateInstance(property.PropertyType);
                            GetAll(nestedObject);
                        }
                        else
                        {
                            object value = row[property.Name];
                            if (value != DBNull.Value)
                            {
                                property.SetValue(item1, value);
                            }
                        }
                    }
                    //items.Add(item1);
                    break;
                }
            }
        }

        #endregion
    }
}
