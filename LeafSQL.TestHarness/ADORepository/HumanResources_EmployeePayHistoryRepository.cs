using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.TestHarness.ADORepository
{
	public partial class HumanResources_EmployeePayHistoryRepository
	{        
		public void Export_HumanResources_EmployeePayHistory()
		{
            LeafSQLClient client = new LeafSQLClient("http://localhost:6858/", "admin", "");

            if(client.Schema.Exists("AdventureWorks2012:HumanResources:EmployeePayHistory"))
			{
				return;
			}

            client.Transaction.Begin();

            client.Schema.Create("AdventureWorks2012:HumanResources:EmployeePayHistory");

			using (SqlConnection connection = new SqlConnection("Server=.;Database=AdventureWorks2012;Trusted_Connection=True;"))
			{
				connection.Open();

				try
				{
					using (SqlCommand command = new SqlCommand("SELECT * FROM HumanResources.EmployeePayHistory", connection))
					{
						command.CommandTimeout = 10000;
						command.CommandType = System.Data.CommandType.Text;

						using (SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
						{
                            int indexOfBusinessEntityID = dataReader.GetOrdinal("BusinessEntityID");
						    int indexOfRateChangeDate = dataReader.GetOrdinal("RateChangeDate");
						    int indexOfRate = dataReader.GetOrdinal("Rate");
						    int indexOfPayFrequency = dataReader.GetOrdinal("PayFrequency");
						    int indexOfModifiedDate = dataReader.GetOrdinal("ModifiedDate");
						    
							int rowCount = 0;


							while (dataReader.Read() && rowCount++ < 1000 /*easy replace*/)
							{
								if(rowCount > 0 && (rowCount % 100) == 0)
								{
									Console.WriteLine("AdventureWorks2012:HumanResources:EmployeePayHistory: {0}", rowCount);
								}

								if(rowCount > 0 && (rowCount % 1000) == 0)
								{
									Console.WriteLine("Comitting...");
									client.Transaction.Commit();
									client.Transaction.Begin();
								}

								try
								{
									client.Document.Store("AdventureWorks2012:HumanResources:EmployeePayHistory", new Document(new Models.HumanResources_EmployeePayHistory
									{
											BusinessEntityID= dataReader.GetInt32(indexOfBusinessEntityID),
											RateChangeDate= dataReader.GetDateTime(indexOfRateChangeDate),
											Rate= dataReader.GetDecimal(indexOfRate),
											PayFrequency= dataReader.GetByte(indexOfPayFrequency),
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

