using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.TestHarness.ADORepository
{
	public partial class Production_LocationRepository
	{        
		public void Export_Production_Location()
		{
            LeafSQLClient client = new LeafSQLClient("http://localhost:6858/", "admin", "");

            if(client.Schema.Exists("AdventureWorks2012:Production:Location"))
			{
				return;
			}

            client.Transaction.Begin();

            client.Schema.Create("AdventureWorks2012:Production:Location");

			using (SqlConnection connection = new SqlConnection("Server=.;Database=AdventureWorks2012;Trusted_Connection=True;"))
			{
				connection.Open();

				try
				{
					using (SqlCommand command = new SqlCommand("SELECT * FROM Production.Location", connection))
					{
						command.CommandTimeout = 10000;
						command.CommandType = System.Data.CommandType.Text;

						using (SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
						{
                            int indexOfLocationID = dataReader.GetOrdinal("LocationID");
						    int indexOfName = dataReader.GetOrdinal("Name");
						    int indexOfCostRate = dataReader.GetOrdinal("CostRate");
						    int indexOfAvailability = dataReader.GetOrdinal("Availability");
						    int indexOfModifiedDate = dataReader.GetOrdinal("ModifiedDate");
						    
							int rowCount = 0;


							while (dataReader.Read() && rowCount++ < 1000 /*easy replace*/)
							{
								if(rowCount > 0 && (rowCount % 100) == 0)
								{
									Console.WriteLine("AdventureWorks2012:Production:Location: {0}", rowCount);
								}

								if(rowCount > 0 && (rowCount % 1000) == 0)
								{
									Console.WriteLine("Comitting...");
									client.Transaction.Commit();
									client.Transaction.Begin();
								}

								try
								{
									client.Document.Store("AdventureWorks2012:Production:Location", new Document(new Models.Production_Location
									{
											LocationID= dataReader.GetInt16(indexOfLocationID),
											Name= dataReader.GetString(indexOfName),
											CostRate= dataReader.GetDecimal(indexOfCostRate),
											Availability= dataReader.GetDecimal(indexOfAvailability),
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

