using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads;

namespace LeafSQL.TestHarness.ADORepository
{
	public partial class Person_EmailAddressRepository
	{        
		public void Export_Person_EmailAddress()
		{
            LeafSQLClient client = new LeafSQLClient("http://localhost:6858/");

            if(client.Schema.Exists("AdventureWorks2012:Person:EmailAddress"))
			{
				return;
			}

            client.Transaction.Begin();

            client.Schema.Create("AdventureWorks2012:Person:EmailAddress");

			using (SqlConnection connection = new SqlConnection("Server=.;Database=AdventureWorks2012;Trusted_Connection=True;"))
			{
				connection.Open();

				try
				{
					using (SqlCommand command = new SqlCommand("SELECT * FROM Person.EmailAddress", connection))
					{
						command.CommandTimeout = 10000;
						command.CommandType = System.Data.CommandType.Text;

						using (SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
						{
                            int indexOfBusinessEntityID = dataReader.GetOrdinal("BusinessEntityID");
						    int indexOfEmailAddressID = dataReader.GetOrdinal("EmailAddressID");
						    int indexOfEmailAddress = dataReader.GetOrdinal("EmailAddress");
						    int indexOfrowguid = dataReader.GetOrdinal("rowguid");
						    int indexOfModifiedDate = dataReader.GetOrdinal("ModifiedDate");
						    
							int rowCount = 0;


							while (dataReader.Read() /*&& rowCount++ < 10000*/)
							{
								if(rowCount > 0 && (rowCount % 100) == 0)
								{
									Console.WriteLine("AdventureWorks2012:Person:EmailAddress: {0}", rowCount);
								}

								if(rowCount > 0 && (rowCount % 1000) == 0)
								{
									Console.WriteLine("Comitting...");
									client.Transaction.Commit();
									client.Transaction.Begin();
								}

								try
								{
									client.Document.Store("AdventureWorks2012:Person:EmailAddress", new Document(new Models.Person_EmailAddress
									{
											BusinessEntityID= dataReader.GetInt32(indexOfBusinessEntityID),
											EmailAddressID= dataReader.GetInt32(indexOfEmailAddressID),
											EmailAddress= dataReader.GetNullableString(indexOfEmailAddress),
											rowguid= dataReader.GetGuid(indexOfrowguid),
											ModifiedDate= dataReader.GetDateTime(indexOfModifiedDate),
										}));
								}
								catch(Exception ex)
								{
									Console.WriteLine(ex.Message);
								}
								
								rowCount++;
							}
						}
					}
					connection.Close();
				}
				catch
				{
					//TODO: add error handling/logging
					throw;
				}

				client.Transaction.Commit();
            }
		}
	}
}

