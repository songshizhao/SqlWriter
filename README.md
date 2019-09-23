# SqlWriter

```csharp
//insert itemlist valuelist
var insertSql = new SqlString();
insertSql.Insert("userlist", new string[] { "username", "password","enable","time"}, new object[] { "admin", "a_secret",false,DateTime.Now});
Console.WriteLine(insertSql.result + "\n\r");
		

//delete
var deleteSql = new SqlString();
deleteSql.Delete("userlist");
deleteSql.Where("username", "hacker");
deleteSql.WhereAnd("password", "<script..");
Console.WriteLine(deleteSql.result + "\n\r");
	
//update
var updateSql = new SqlString();
updateSql.Update("userlist", new string[] { "username", "password", "enable", "time" }, new object[] { "admin", "a_secret", false, DateTime.Now });
updateSql.Where("username", "godman");
updateSql.WhereAnd("password", "bequiet0123");
Console.WriteLine(updateSql.result + "\n\r");

//select -> where ->whereand -> order
var selectSql0 = new SqlString();
selectSql0.Select(null, "userlist");
selectSql0.Where("username", "admin");
selectSql0.WhereAnd("password", "sercetX");
selectSql0.WhereAnd("isreal", true);
selectSql0.OrderBy(new OrderBy[] { new OrderBy("SignUpTime", Order.ASC), new OrderBy("followers", Order.DESC) });

Console.WriteLine(selectSql0.result + "\n\r");
	
//select -> where ->whereand -> order
var selectSql = new SqlString();
selectSql.Select(new string[] { "sex", "age" }, "userlist");
selectSql.Where("username", "admin");
selectSql.WhereAnd("password", "sercetX");
selectSql.WhereAnd("isreal", true);
selectSql.OrderBy(new OrderBy[] { new OrderBy("SignUpTime", Order.ASC), new OrderBy("followers", Order.DESC) });

Console.WriteLine(selectSql.result+ "\n\r");
	

//select -> where ->whereand -> order
var selectSql2 = new SqlString();
selectSql2.SelectTop(10,new string[] { "sex", "age" }, "userlist");
selectSql2.Where("username", "admin");
selectSql2.WhereAnd("password", "sercetX");
selectSql2.WhereAnd("isreal", true);
selectSql2.OrderBy(new OrderBy[] { new OrderBy("SignUpTime", Order.ASC), new OrderBy("followers", Order.DESC) });

Console.WriteLine(selectSql2.result + "\n\r");


Console.Read();
```
