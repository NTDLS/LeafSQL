using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads;

namespace LeafSQL.TestHarness.ADORepository
{
	public partial class Production_ProductModelProductDescriptionCultureRepository
	{        
		public void Export_Production_ProductModelProductDescriptionCulture()
		{
            LeafSQLClient client = new LeafSQLClient("http://localhost:6858/", "admin", "admin");

            if(client.Schema.Exists("AdventureWorks2012:Production:ProductModelProductDescriptionCulture"))
			{
				return;
			}

            client.Transaction.Begin();

            client.Schema.Create("AdventureWorks2012:Production:ProductModelProductDescriptionCulture");

			using (SqlConnection connection = new SqlConnection("Server=.;Database=AdventureWorks2012;Trusted_Connection=True;"))
			{
				connection.Open();

				try
				{
					using (SqlCommand command = new SqlCommand("SELECT * FROM Production.ProductModelProductDescriptionCulture", connection))
					{
						command.CommandTimeout = 10000;
						command.CommandType = System.Data.CommandType.Text;

						using (SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
						{
                            int indexOfProductModelID = dataReader.GetOrdinal("ProductModelID");
						    int indexOfProductDescriptionID = dataReader.GetOrdinal("ProductDescriptionID");
						    int indexOfCultureID = dataReader.GetOrdinal("CultureID");
						    int indexOfModifiedDate = dataReader.GetOrdinal("ModifiedDate");
						    
							int rowCount = 0;


							while (dataReader.Read() /*&& rowCount++ < 10000*/)
							{
								if(rowCount > 0 && (rowCount % 100) == 0)
								{
									Console.WriteLine("AdventureWorks2012:Production:ProductModelProductDescriptionCulture: {0}", rowCount);
								}

								if(rowCount > 0 && (rowCount % 1000) == 0)
								{
									Console.WriteLine("Comitting...");
									client.Transaction.Commit();
									client.Transaction.Begin();
								}

								try
								{
									client.Document.Store("AdventureWorks2012:Production:ProductModelProductDescriptionCulture", new Document(new Models.Production_ProductModelProductDescriptionCulture
									{
											ProductModelID= dataReader.GetInt32(indexOfProductModelID),
											ProductDescriptionID= dataReader.GetInt32(indexOfProductDescriptionID),
											CultureID= dataReader.GetString(indexOfCultureID),
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
