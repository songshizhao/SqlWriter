using System;
using System.Collections.Generic;

namespace SqlWriter
{

	public enum Order
	{
		DESC,
		ASC,

	}


	public class OrderBy {

		public string itemname;
		public Order order;

		public OrderBy(string name, Order od) {

			itemname = name;
			order = od;
		}

	}


	public class SqlString
	{
		public string Insert;
		public string Delete;
		public string Update;
		public string Select;
		
		
		public List<string> OrderBy = new List<string>();
		public string Where;
		public List<string> WhereAnd=new List<string>();

		public string result;

		/// <summary>
		/// alternative method of getting combined sql(not recommended)
		/// </summary>
		public void GetResult() {


		}

	}


	/// <summary>
	/// an interesting way to combine sql strings
	/// </summary>
	public static class CombineClass
	{

		/// <summary>
		/// Where item=value (appear only once)
		/// </summary>
		/// <typeparam name="T">string/int/double/...</typeparam>
		/// <returns></returns>
		public static SqlString Where<T>(this SqlString Sql,string item,T value)
		{

			if (typeof(T) == typeof(string))
			{
				Sql.Where = $" Where [{item}]='{value}'";
				
			}
			else
			{
				Sql.Where = $" Where [{item}]={value.ToString()}";
			}
			Sql.result += Sql.Where;
			return Sql;

			
		}
		/// <summary>
		/// Where item=value (appear only after Where, can repeat)
		/// </summary>
		/// <returns></returns>
		public static SqlString WhereAnd<T>(this SqlString Sql, string item, T value)
		{
			string where_and_item = "";
			if (typeof(T) == typeof(string))
			{
				
				where_and_item = $" and [{item}]='{value}'";
			}
			else
			{
				where_and_item = $" and [{item}]={value.ToString()}";
			}
			Sql.WhereAnd.Add(where_and_item);
			Sql.result += where_and_item;
			return Sql;
		}


		public static SqlString Select(this SqlString Sql, string[] item,string tablename)
		{
			var selectedItem = "";
			if (item==null)
			{
				selectedItem = "*";
			}
			else
			{
				for (int i = 0; i < item.Length; i++)
				{
					if (i==0)
					{
						selectedItem += $"[{item[i]}]";
					}
					else
					{
						selectedItem += $",[{item[i]}]";
					}
					
				}
				
			}

			Sql.Select= $"Select {selectedItem} from [{tablename}]";
			Sql.result += Sql.Select;

			return Sql;
		}


		public static SqlString SelectTop(this SqlString Sql,int limit, string[] item, string tablename)
		{
			var selectedItem = "";
			if (item == null)
			{
				selectedItem = "*";
			}
			else
			{
				for (int i = 0; i < item.Length; i++)
				{
					if (i == 0)
					{
						selectedItem += $"[{item[i]}]";
					}
					else
					{
						selectedItem += $",[{item[i]}]";
					}

				}

			}

			Sql.Select = $"Select top {limit.ToString()} {selectedItem} from [{tablename}]";
			Sql.result += Sql.Select;

			return Sql;
		}



		public static SqlString Insert(this SqlString Sql, string tablename, string[] items,object[] values)
		{
			string insertitems = ""; string insertvalues = "";
			for (int i = 0; i < items.Length; i++)
			{
				if (i==0)
				{
					insertitems += $"[{items[i]}]";
				}
				else
				{
					insertitems += $",[{items[i]}]";
				}
				
			}
			for (int i = 0; i < values.Length; i++)
			{
				if (values[i].GetType() == typeof(string))
				{
					values[i] = "'"+ values[i].ToString()+"'";
				}

				if (i == 0)
				{
					insertvalues += $"{values[i]}";
				}
				else
				{
					insertvalues += $",{values[i]}";
				}
			}
			Sql.Insert = $"INSERT INTO {tablename}({insertitems}) VALUES({insertvalues})";
			Sql.result += Sql.Insert;
			return Sql;
		}
		/// <summary>
		/// delete from 'tablename' without 'where' and should have 'where' followed
		/// </summary>
		/// <returns></returns>
		public static SqlString Delete(this SqlString Sql, string tablename)
		{
			Sql.Delete=$"DELETE FROM {tablename}";
			Sql.result += Sql.Delete;

			return Sql;
		}


		/// <summary>
		/// update 'tablename' without 'where' and should have 'where' followed
		/// </summary>
		/// <returns></returns>
		public static SqlString Update(this SqlString Sql, string tablename, string[] items, object[] values)
		{

			string setContent = "";
			for (int i = 0; i < items.Length; i++)
			{

				if (values[i].GetType()==typeof(string))
				{
					values[i] = "'"+ values[i] + "'";
				}

				if (i == 0)
				{
					setContent += $"[{items[i]}]={values[i].ToString()}";
				}
				else
				{
					setContent += $",[{items[i]}]={values[i].ToString()}";
				}
				
			}

	

			Sql.Update=$"UPDATE {tablename} set {setContent}";
			Sql.result += Sql.Update;
			return Sql;
		}

		/// <summary>
		/// should follow select ,be used it for ending
		/// </summary>
		/// <returns></returns>
		public static SqlString OrderBy(this SqlString Sql, OrderBy[] Orderbys)
		{


			for (int i = 0; i < Orderbys.Length; i++)
			{
				Sql.OrderBy.Add(Orderbys[i].Getstring());
			}
			for (int i = 0; i < Sql.OrderBy.Count; i++)
			{

				if (i==0)
				{
					Sql.result += " ORDER BY "+ Sql.OrderBy[i];
				}
				else
				{
					Sql.result +=","+Sql.OrderBy[i];
				}

				
			}
			return Sql;
		}

		public static string Getstring(this OrderBy orderby)
		{
			//orderby.order.GetType().GetEnumName(orderby.order);

			return $"{orderby.itemname} {orderby.order.ToString()}";

		}

		//SELECT Company, OrderNumber FROM Orders 

	}




}
