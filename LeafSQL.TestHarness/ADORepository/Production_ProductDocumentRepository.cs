using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using LeafSQL.Library.Client;
using LeafSQL.Library.Payloads.Models;

namespace LeafSQL.TestHarness.ADORepository
{
	public partial class Production_ProductDocumentRepository
	{        
		public void Export_Production_ProductDocument()
		{
            LeafSQLClient client = new LeafSQLClient("http://localhost:6858/", "admin", "admin");

            if(client.Schema.Exists("AdventureWorks2012:Production:ProductDocument"))
			{
				return;
			}

            client.Transaction.Begin();

            client.Schema.Create("AdventureWorks2012:Production:ProductDocument");

			using (SqlConnection connection = new SqlConnection("Server=.;Database=AdventureWorks2012;Trusted_Connection=True;"))
			{
				connection.Open();

				try
				{
					using (SqlCommand command = new SqlCommand("SELECT * FROM Production.ProductDocument", connection))
					{
						command.CommandTimeout = 10000;
						command.CommandType = System.Data.CommandType.Text;

						using (SqlDataReader dataReader = command.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
						{
                            int indexOfId = dataReader.GetOrdinal("Id");
						    int indexOfProductID = dataReader.GetOrdinal("ProductID");
						    int indexOfModifiedDate = dataReader.GetOrdinal("ModifiedDate");
						    int indexOfDocumentId = dataReader.GetOrdinal("DocumentId");
						    
							int rowCount = 0;


							while (dataReader.Read() /*&& rowCount++ < 10000*/)
							{
								if(rowCount > 0 && (rowCount % 100) == 0)
								{
									Console.WriteLine("AdventureWorks2012:Production:ProductDocument: {0}", rowCount);
								}

								if(rowCount > 0 && (rowCount % 1000) == 0)
								{
									Console.WriteLine("Comitting...");
									client.Transaction.Commit();
									client.Transaction.Begin();
								}

								try
								{
									client.Document.Store("AdventureWorks2012:Production:ProductDocument", new Document(new Models.Production_ProductDocument
									{
											Id= dataReader.GetInt32(indexOfId),
											ProductID= dataReader.GetInt32(indexOfProductID),
											ModifiedDate= dataReader.GetDateTime(indexOfModifiedDate),
											DocumentId= dataReader.GetNullableInt32(indexOfDocumentId),
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

